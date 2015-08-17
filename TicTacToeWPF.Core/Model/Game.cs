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

        public Game()
        {
            initGrid();
            _turn = 1;
            _gameEnd = false;
        }

        private void initGrid()
        {
            // Fills collection with 9 empty string elements.
            _grid = new ObservableCollection<string>(Enumerable.Repeat("", 9).ToArray());
        }

        public void makeMove(int index)
        {
            if(_grid[index].Equals(""))
            {
                if (_turn == 1)
                {
                    _grid[index] = "X";
                    if (checkForEnd(index))
                    {
                        _gameEnd = true;
                        return;
                    }
                        
                    _turn = 2;
                }
                else
                {
                    _grid[index] = "O";
                    if (checkForEnd(index))
                    {
                        _gameEnd = true;
                        return;
                    }
                    _turn = 1;
                }
            }
        }

        private bool checkForEnd(int index)
        {
            int column = index / 3;
            int row = index % 3; 

            // Check Row
            if(_grid[row*3].Equals(_grid[row * 3 + 1]) 
                && _grid[row * 3 + 1].Equals(_grid[row * 3 + 2]))
            {
                // Win
                return true;
            }
            // Check Column
            if(_grid[column*3].Equals(_grid[column*3+1])
                && _grid[column * 3 + 1].Equals(_grid[column * 3 + 2])) {
                // WIN
                return true;
            }
            // Check if diagonal is possible
            if((index+1)%2 != 0)
            {
                // Diagonal
                if(_grid[0].Equals(_grid[4]) &&
                    _grid[4].Equals(_grid[8]))
                {
                    // Win
                    return true;
                }
                // Anti Diagonal
                else if(_grid[2].Equals(_grid[4]) &&
                    _grid[4].Equals(_grid[6]))
                {
                    // Win
                    return true;
                }
            }
            return false;
        }

        public int Turn
        {
            get { return _turn; }
            set { _turn = value; }
        }

        public bool GameEnd
        {
            get { return _gameEnd; }
            set { _gameEnd = value; }
        }


        public void reset()
        {
            initGrid();
            _turn = 1;
            _gameEnd = false;
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
    }
}
