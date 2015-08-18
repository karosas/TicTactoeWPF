﻿using System;
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
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            LayoutRoot.DataContext = viewModel;
            MenuGrid.DataContext = viewModel; 
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OverlayBorder.Visibility = Visibility.Collapsed;
        }

    
    }
}
