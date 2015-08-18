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
        private ICommand _resetCommand;
        private bool _buttonsEnabled;
        private bool _gameEndOverlayEnabled;
        private string _gameEndMessage;

        public MainViewModel()
        {
            _game = new Game();
            ButtonsEnabled = true;
            GameEndOverlayEnabled = false;
        }

        public ICommand GameCommand
        {
            get
            {
                if(_gameCommand == null)
                {
                    _gameCommand = new RelayCommand(
                        param => this.ExecuteGameCommand(param),
                        param => this.CanExecute()
                        );
                }
                return _gameCommand;
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(
                        param => this.ExecuteResetCommand(),
                        param => this.CanExecute()
                        );
                }
                return _resetCommand;
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        private void ExecuteGameCommand(object o)
        {
            Game.makeMove(Convert.ToInt32((string)o)-1);
            if(Game.GameEnd)
            {
                GameEndOverlayEnabled = true;
                GameEndMessage = Game.GameEndMessage;
            }
        }

        private void ExecuteResetCommand()
        {
            Game.reset();
        }

        public bool ButtonsEnabled {
            get { return _buttonsEnabled; }
            set
            {
                _buttonsEnabled = value;
                OnPropertyChanged("ButtonsEnabled");
            }
        }

        public bool GameEndOverlayEnabled {
            get { return _gameEndOverlayEnabled; }
            set
            {
                _gameEndOverlayEnabled = value;
                OnPropertyChanged("GameEndOverlayEnabled");
            }
        }

        public string GameEndMessage
        {
            get { return _gameEndMessage; }
            set
            {
                _gameEndMessage = value;
                OnPropertyChanged("GameEndMessage");
            }
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
    }
}
