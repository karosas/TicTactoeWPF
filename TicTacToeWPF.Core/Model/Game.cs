using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWPF.Core.Model
{
    public class Game : Observable
    {
        private List<string> _grid;

        public Game()
        {
            initGrid();
        }

        private void initGrid()
        {
            _grid = new List<string>();
            _grid = Enumerable.Repeat("1", 9).ToList();
        }

        public List<string> Grid
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
