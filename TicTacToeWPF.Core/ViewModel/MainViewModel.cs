using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeWPF.Core.Model;

namespace TicTacToeWPF.Core.ViewModel
{
    public class MainViewModel : Observable
    {

        private Game _game;
        private List<string> _grid;

        public MainViewModel()
        {
            _game = new Game();
            
        }

        
        public Game Game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged("Game");
            }
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
