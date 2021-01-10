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
using System.Windows.Shapes;

namespace AAiLProject.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.FirstPlotStackPanel.Visibility = Visibility.Collapsed;
            this.LastPlotStackPanel.Visibility = Visibility.Collapsed;
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Minimalize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void tbCellNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbCellNumber.Text == "0")
            {
                tbCellNumber.Text = null;
            }
        }

        private void tbFlowSpeed_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFlowSpeed.Text == "0")
            {
                tbFlowSpeed.Text = null;
            }
        }

        private void tbFlushTime_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFlushTime.Text == "0")
            {
                tbFlushTime.Text = null;
            }
        }

        private void tbDinamicViscosity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbDinamicViscosity.Text == "0")
            {
                tbDinamicViscosity.Text = null;
            }
        }

        private void tbDistance_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbDistance.Text == "0")
            {
                tbDistance.Text = null;
            }
        }

        private void GetMeanAndStressFunction_Click(object sender, RoutedEventArgs e)
        {
            this.LastPlotStackPanel.Visibility = Visibility.Visible;
            this.FirstPlotStackPanel.Visibility = Visibility.Collapsed;
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            this.FirstPlotStackPanel.Visibility = Visibility.Visible;
            this.LastPlotStackPanel.Visibility = Visibility.Collapsed;
        }
    }
}
