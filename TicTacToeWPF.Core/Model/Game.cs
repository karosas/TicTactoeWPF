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
        private ObservableCollection<string> _grid;
        private int _turn;
        private bool _gameEnd;
        private int moveCount;

        public Game()
        {
            initGrid();
            Turn = 1;
            _gameEnd = false;
            moveCount = 0;
        }

        private void initGrid()
        {
            // Fills collection with 9 empty string elements.
            _grid = new ObservableCollection<string>(Enumerable.Repeat("", 9).ToArray());
        }

        public void makeMove(int index)
        {
            if(_grid[index].Equals("") && !_gameEnd)
            {
                moveCount++;
                if (Turn == 1)
                {
                    _grid[index] = "X";
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
                    _grid[index] = "O";
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
                _grid[i] = "";

            Turn = 1;
            _gameEnd = false;
            GameEndMessage = "";
            moveCount = 0;
        }
        
        public ObservableCollection<string> Grid
        {
            get { return _grid; }
            set
            {
                _grid = value;
                OnPropertyChanged("Grid");
            }
        }

        public string GameEndMessage { get; set; }
    }
}
