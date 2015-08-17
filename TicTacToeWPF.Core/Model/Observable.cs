using System.ComponentModel;

namespace TicTacToeWPF.Core.Model
{
    public abstract class Observable : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
