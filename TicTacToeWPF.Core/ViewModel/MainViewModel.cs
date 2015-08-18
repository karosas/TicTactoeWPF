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
        private ICommand _gameModeCommand;
        private ICommand _difficultyCommand;
        private bool _buttonsEnabled;
        private bool _gameEndOverlayEnabled;
        private string _gameEndMessage;
        private bool _inMenu;
        private bool _isAIMode;
        private bool _isEasyAi;

        public MainViewModel()
        {
            _game = new Game();
            ButtonsEnabled = true;
            GameEndOverlayEnabled = false;
            _inMenu = true;
            _isAIMode = false;
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

        public ICommand GameModeCommand
        {
            get
            {
                if(_gameModeCommand == null)
                {
                    _gameModeCommand = new RelayCommand(
                        param => this.ExecuteGameModeCommand(param),
                        param => this.CanExecute()
                        );
                }
                return _gameModeCommand;
            }
        }

        public ICommand DifficultyCommand
        {
            get
            {
                if(_difficultyCommand == null)
                {
                    _difficultyCommand = new RelayCommand(
                        param => this.ExecuteDifficultyCommand(param),
                        param => this.CanExecute()
                        );
                }
                return _difficultyCommand;
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

        public void ExecuteGameModeCommand(object o)
        {
            if (((string)o).Equals("pvp"))
            {
                IsAiMode = false;
                InMenu = false;
            }
            else
            {
                IsAiMode = true;
            }
           
        }

        public void ExecuteDifficultyCommand(object o)
        {
            if(((string)o).Equals("easy")) {
                _isEasyAi = true;
            } else
            {
                _isEasyAi = false;
            }
            InMenu = false;
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

        public bool InMenu
        {
            get { return _inMenu; }
            set
            {
                _inMenu = value;
                OnPropertyChanged("InMenu");
            }
        }

        public bool IsAiMode
        {
            get { return _isAIMode; }
            set
            {
                _isAIMode = value;
                OnPropertyChanged("IsAiMode");
            }
        }
    }
}
