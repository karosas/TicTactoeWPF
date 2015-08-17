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
        private int turn;

        public Game()
        {
            initGrid();
            turn = 1;
        }

        private void initGrid()
        {
            // Fills collection with 9 empty string elements.
            _grid = new ObservableCollection<string>(Enumerable.Repeat("", 9).ToArray());
        }

        public void makeMove(int index)
        {
            if(turn == 1)
            {
                _grid[index] = "X";
                turn = 2;
            }
            else
            {
                _grid[index] = "O";
                turn = 1;
            }
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
