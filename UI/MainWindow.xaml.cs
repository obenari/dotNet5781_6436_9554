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

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            StationWindow stnWindow = new StationWindow(bl);
            stnWindow.ShowDialog();
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            LineWindow lineWindow = new LineWindow(bl);
            lineWindow.ShowDialog();
        }

        private void btnBus_Click(object sender, RoutedEventArgs e)
        {
            BusWindow busWindow = new BusWindow(bl);
            busWindow.ShowDialog();
        }

        private void btnLineTrip_Click(object sender, RoutedEventArgs e)
        {
            LineTripWindow lineTripWindow = new LineTripWindow(bl);
            lineTripWindow.ShowDialog();
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    ManangmentWindow manangmentWindow = new ManangmentWindow(bl);
        //    manangmentWindow.ShowDialog();
        //}
    }
}
