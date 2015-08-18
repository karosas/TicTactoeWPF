using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToeWPF.Core.ViewModel;

namespace TicTactoeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int turn;
        private int i, j;
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            turn = 1;
            viewModel = new MainViewModel();
            LayoutRoot.DataContext = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            turn = 1;
        }

        private void win(string btnContent)
        {
            if ((Btn1.Content == btnContent & Btn2.Content == btnContent &
                 Btn3.Content == btnContent)
               | (Btn1.Content == btnContent & Btn4.Content == btnContent &
                 Btn7.Content == btnContent)
               | (Btn1.Content == btnContent & Btn5.Content == btnContent &
                 Btn9.Content == btnContent)
               | (Btn2.Content == btnContent & Btn5.Content == btnContent &
                 Btn8.Content == btnContent)
               | (Btn3.Content == btnContent & Btn6.Content == btnContent &
                 Btn9.Content == btnContent)
               | (Btn4.Content == btnContent & Btn5.Content == btnContent &
                 Btn6.Content == btnContent)
               | (Btn7.Content == btnContent & Btn8.Content == btnContent &
                 Btn9.Content == btnContent)
               | (Btn3.Content == btnContent & Btn5.Content == btnContent &
                 Btn7.Content == btnContent))
            {
                if (btnContent == "O")
                {

                    MessageBox.Show("PLAYER O WINS");
                    //Label1.Content = ++i;
                }
                else if (btnContent == "X")
                {
                    MessageBox.Show("PLAYER X WINS");
                    //Label2.Content = ++j;
                }
                disableButtons();
            }
            else
            {
                foreach (Button btn in BtnPanel.Children)
                {
                    if (btn.IsEnabled == true)
                        return;
                }
                MessageBox.Show("GAME OVER NO ONE WINS");
            }
        }

        private void disableButtons()
        {
            foreach(Button btn in BtnPanel.Children)
            {
                btn.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if( turn == 1)
            {
                btn.Content = "O";
                PlayerTurn.Text = "X";
            }
            else
            {
                btn.Content = "X";
                PlayerTurn.Text = "O";
            }
            btn.IsEnabled = false;
            win(btn.Content.ToString());
            turn++;
            if (turn > 2)
                turn = 1;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OverlayBorder.Visibility = Visibility.Collapsed;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach(Button btn in BtnPanel.Children)
            {
                btn.Content = "";
                btn.IsEnabled = true;
            }
        } 
    
    }
}
