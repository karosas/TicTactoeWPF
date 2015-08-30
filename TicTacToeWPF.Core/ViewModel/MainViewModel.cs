using System;
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
        private ICommand _menuCommand;
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
        // Grid button commands
        public ICommand GameCommand
        {
            get
            {
                if(_gameCommand == null)
                {
                    _gameCommand = new RelayCommand(
                        param => this.ExecuteGameCommand(param),
                        param => this.canGameCommandExecute()
                        );
                }
                return _gameCommand;
            }
        }
        // Reset game button command
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
        // Game mode button command
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
        // Player vs. AI difficulty button command
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

        public ICommand MenuCommand
        {
            get
            {
                if (_menuCommand == null)
                {
                    _menuCommand = new RelayCommand(
                        param => this.ExecuteMenuCommand(),
                        param => this.CanExecute()
                        );
                }
                return _menuCommand;
            }
        }
        // Check whether it's Player vs AI mode and whether it's AI turn
        private bool canGameCommandExecute()
        {
            if(Game.AiEnabled)
            {
                if(Game.AiTurn == Game.Turn)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        // Other buttons can execute always
        private bool CanExecute()
        {
            return true;
        }

        private void ExecuteGameCommand(object o)
        {
            Game.makeMove(Convert.ToInt32((string)o)-1);

            if (!Game.GameEnd)
            {
                Game.checkForAiTurns();
            }

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
                Game.enableAi("easy");
                _isEasyAi = true;
            } else
            {
                Game.enableAi("hard");
                _isEasyAi = false;
            }
            InMenu = false;
        }

        public void ExecuteMenuCommand()
        {
            ButtonsEnabled = true;
            GameEndOverlayEnabled = false;
            _inMenu = true;
            _isAIMode = false;
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
