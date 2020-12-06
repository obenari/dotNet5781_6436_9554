using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace dotNet5781_3B_6436_9554_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random r = new Random(DateTime.Now.Millisecond);
        private ObservableCollection<Bus> BusCollection = new ObservableCollection<Bus>();

        public MainWindow()
        {
            InitializeComponent();
            initBusses();
        }
        /// <summary>
        /// this func create 10 busses
        /// </summary>
        private void initBusses()
        {
            string license;
            DateTime start;
            DateTime last;
            int km;
            for (int i = 0; i < 7; i++)
            {
                try
                {
                    license = r.Next(1000000, 10000000).ToString();
                    start = new DateTime(r.Next(2018), r.Next(13), r.Next(29));
                    last = new DateTime(r.Next(2018, 2020), r.Next(13), r.Next(29));
                    km = r.Next();
                    BusCollection.Add(new Bus(license, start, last, km));
                }
                catch (Exception e)
                {
                    i--;//if there is an exception, the bus is not created
                }
            }
            //create a bus that need a treatment
            BusCollection.Add(new Bus("12345678", new DateTime(2018, 4, 1), new DateTime(2019, 1, 1), 15000));
            //create a bus that need a refueling
            BusCollection.Add(new Bus("11111111", new DateTime(2018, 5, 1), new DateTime(2019, 1, 1), 400000, 5));
            //create a bus with high mileage from the last treatment
            BusCollection.Add(new Bus("22222222", new DateTime(2019, 4, 1), new DateTime(2019, 7, 1), 15000, 1200, 19000));
        }
    }
}
