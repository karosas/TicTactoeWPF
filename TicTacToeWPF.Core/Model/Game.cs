using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWPF.Core.Model
{
    public class Game : Observable
    {
        private ObservableCollection<int> _grid;
        private int _turn;
        private bool _gameEnd;
        private int moveCount;
        private int _lastMove;
        private int _aiTurn;
        private bool _aiEnabled;
        private string _aiDifficulty;

        public Game()
        {
            initGrid();
            Turn = 1;
            _gameEnd = false;
            moveCount = 0;
            _aiEnabled = false;
        }

        private void initGrid()
        {
            // Fills collection with 9 empty string elements.
            _grid = new ObservableCollection<int>(Enumerable.Repeat(0, 9).ToArray());
        }

        public void enableAi(string difficulty)
        {
            _aiEnabled = true;
            Random r = new Random();
            _aiTurn = r.Next(1, 2);
            _aiDifficulty = difficulty;
            if(_aiTurn == Turn)
            {
                if(_aiDifficulty.Equals("easy"))
                {
                    easyComputerMove();
                } else
                {
                    hardComputerMove(_grid.ToList());
                }
            }
        }

        public void makeMove(int index)
        {
            if(_grid[index] == 0 && !_gameEnd)
            {
                moveCount++;
                _lastMove = index;
                if (Turn == 1)
                {
                    _grid[index] = 1;
                    Turn = 2;
                    if (checkForEnd(index))
                    {
                        _gameEnd = true;
                        GameEndMessage = "Player X Won!";
                        return;
                    }
                        
                    
                }
                else
                {
                    _grid[index] = 2;
                   Turn = 1;
                    if (checkForEnd(index))
                    {
                        _gameEnd = true;
                        GameEndMessage = "Player O Won!";
                        return;
                    }
                    
                }
            }
            if(moveCount >= 9)
            {
                _gameEnd = true;
                GameEndMessage = "Tie!";
                return;
            }
        }

        private bool checkForEnd(int index)
        {
            int column = index % 3;
            int row = index / 3;

            // It's impossible to win in less than 5 moves
            if (moveCount < 5)
                return false;

            // Check Row
            if (checkRow(row))
                return true;
            // Check Column
            if (checkColumn(column))
                return true;
            // Check if diagonal is possible
            if((index+1)%2 != 0)
            {
                if (checkDiagonals())
                    return true;
            }
            return false;
        }

        private bool checkColumn(int column)
        {
            if (_grid[column].Equals(_grid[column + 3])
                && _grid[column + 3].Equals(_grid[column + 6])
                && !_grid[column * 3].Equals(""))
            {
                // WIN
                return true;
            }
            return false;
        }

        private bool checkRow(int row)
        {
            if (_grid[row * 3].Equals(_grid[row * 3 + 1])
                && _grid[row * 3 + 1].Equals(_grid[row * 3 + 2])
                && !_grid[row * 3].Equals(""))
            {
                // Win
                return true;
            }
            return false;
        }

        private bool checkDiagonals()
        {
            // Diagonal
            if (_grid[0].Equals(_grid[4]) &&
                _grid[4].Equals(_grid[8])
                && !_grid[0].Equals(""))
            {
                // Win
                return true;
            }
            // Anti Diagonal
            else if (_grid[2].Equals(_grid[4]) &&
                _grid[4].Equals(_grid[6])
                 && !_grid[2].Equals(""))
            {
                // Win
                return true;
            }
            return false;
        }

        public void checkForAiTurns()
        {
            if(_aiEnabled && _aiTurn==_turn)
            {
                if(AiDifficulty == "easy")
                {
                    easyComputerMove();
                }
                else
                {
                    hardComputerMove(_grid.ToList());
                }
            }
        }
        // determines if player won, returns 0 otherwise
        private int win(List<int> dummyGrid)
        {
            int[,] wins = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for(int i = 0; i < 8; ++i)
            {
                if (dummyGrid[wins[i, 0]] != 0 &&
                    dummyGrid[wins[i, 0]] == dummyGrid[wins[i, 1]] &&
                    dummyGrid[wins[i, 0]] == dummyGrid[wins[i, 2]])
                    return dummyGrid[wins[i, 2]];
            }
            return 0;
        }

        private int minimax(List<int> dummyGrid, int player)
        {
            int winner = win(dummyGrid);
            if (winner != 0) return winner * player;
           
            int move = -1;
            int score = -2;

            for(int i = 0; i < 9; ++i)
            {
                if(dummyGrid[i] == 0)
                {
                    dummyGrid[i] = player;
                    int thisScore = -minimax(dummyGrid, player*-1);
                    if(thisScore > score)
                    {
                        score = thisScore;
                        move = i;
                    }
                    dummyGrid[i] = 0;
                }
            }
            if (move == -1)
                return 0;
            return score;
        }

        public void hardComputerMove(List<int> dummyGrid)
        {
            int move = -1;
            int score = -2;
            for(int i = 0; i < 9; ++i)
            {
                if(dummyGrid[i] == 0)
                {
                    dummyGrid[i] = 1;
                    int tempScore = -minimax(dummyGrid, -1);
                    dummyGrid[i] = 0;
                    if(tempScore > score)
                    {
                        score = tempScore;
                        move = i;
                    }
                }
            }
            dummyGrid[move] = 1;
            makeMove(move);
        }

        public void easyComputerMove()
        {
            List<int> emptySpaceIndexes = new List<int>();
            for (int i = 0; i < _grid.Count; i++)
            {
                if (_grid[i] == 0)
                    emptySpaceIndexes.Add(i);
            }
            Random r = new Random();
            makeMove(emptySpaceIndexes.ElementAt(r.Next(0, emptySpaceIndexes.Count())));
        }

        private string getPlayerFromTurn(int turn)
        {
            if (turn == 1)
                return "X";
            else
                return "O";
            
        }

        private string getAnotherPlayer(string player)
        {
            if (player.Equals("X"))
                return "O";
            else
                return "X";
        }

        public int Turn
        {
            get { return _turn; }
            set {
                _turn = value;
                OnPropertyChanged("Turn");
            }
        }

        public bool GameEnd
        {
            get { return _gameEnd; }
            set { _gameEnd = value; }
        }


        public void reset()
        {
            for (int i = 0; i < 9; i++)
                _grid[i] = 0;

            Turn = 1;
            _gameEnd = false;
            GameEndMessage = "";
            moveCount = 0;
            enableAi(_aiDifficulty);
        }
        
        public ObservableCollection<int> Grid
        {
            get { return _grid; }
            set
            {
                _grid = value;
                OnPropertyChanged("Grid");
            }
        }

        public string GameEndMessage { get; set; }

        public bool AiEnabled
        {
            get { return _aiEnabled; }
            set { _aiEnabled = value; }
        }

        public int AiTurn
        {
            get { return _aiTurn; }
            set { _aiTurn = value; }
        }

        public string AiDifficulty
        {
            get { return _aiDifficulty; }
            set { _aiDifficulty = value; }
        }
    }
}
