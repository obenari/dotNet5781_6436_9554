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
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        IBL bl = BLFactory.GetBL();
        public MainWindow()
        {
            InitializeComponent();
         
        }
/// <summary>
/// this method open the station window
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            StationWindow stnWindow = new StationWindow(bl);
            stnWindow.ShowDialog();
        }
        /// <summary>
        /// this method open the lines window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            LineWindow lineWindow = new LineWindow(bl);
            lineWindow.ShowDialog();
        }
        /// <summary>
        /// this method open the busses window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBus_Click(object sender, RoutedEventArgs e)
        {
            BusWindow busWindow = new BusWindow(bl);
            busWindow.ShowDialog();
        }
        /// <summary>
        /// this method open the linesTrip window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLineTrip_Click(object sender, RoutedEventArgs e)
        {
            LineTripWindow lineTripWindow = new LineTripWindow(bl);
            lineTripWindow.ShowDialog();
        }
      
    }
}
