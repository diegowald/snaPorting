using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void CloseButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();            
        }

        private void MaximizeButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void ChangeViewButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Normal;           
        }

        private void MinimizeButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;            
        }

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();         
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.ChangedButton == MouseButton.Left)
            {
                //this.DragMove();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}