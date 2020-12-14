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

namespace dotNet5781_3B_6436_9554_
{
    /// <summary>
    /// Interaction logic for ShowBusWindow.xaml
    /// </summary>
    public partial class ShowBusWindow : Window
    {
        Bus myBus;
        MainWindow mainWindow;
        public bool isClickRefuel = false;
        public bool isClickTreatment = false;
        public ShowBusWindow(Bus bus, MainWindow m)
        {
            InitializeComponent();
            DataContext = bus;
            myBus = bus;
            mainWindow = m;
        }

        private void Button_Click_Refuel(object sender, RoutedEventArgs e)
        {
            isClickRefuel = true;
            this.Close();
        }

        private void Button_Click_Tretment(object sender, RoutedEventArgs e)
        {
            isClickTreatment = true;
            this.Close();

        }
    }
}
