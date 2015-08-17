using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToeWPF.Core.Model;

namespace TicTacToeWPF.Core.ViewModel
{
    public class MainViewModel : Observable
    {

        private Game _game;
        private ICommand _gameCommand;

        public MainViewModel()
        {
            _game = new Game();
            ButtonsEnabled = true;
        }

        public ICommand GameCommand
        {
            get
            {
                if(_gameCommand == null)
                {
                    _gameCommand = new RelayCommand(
                        param => this.ExecuteCommand(param),
                        param => this.CanExecute()
                        );
                }
                return _gameCommand;
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        private void ExecuteCommand(object o)
        {
            Game.makeMove(Convert.ToInt32((string)o)-1);
            if(Game.GameEnd)
            {
                
            }
        }

        public bool ButtonsEnabled { get; set; }

        public Game Game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged("Game");
            }
        }
    }
}
