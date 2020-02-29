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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            drawPanel.SetCenter();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            drawPanel.SetCenter(e.GetPosition(this).X, e.GetPosition(this).Y);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                drawPanel.SetCenter(e.GetPosition(this).X, e.GetPosition(this).Y);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            drawPanel.SetTextType((sender as CheckBox).IsChecked.GetValueOrDefault());
        }
    }
}
