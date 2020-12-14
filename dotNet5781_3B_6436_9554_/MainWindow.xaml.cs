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
using System.ComponentModel;
using System.Threading;

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
            DataContext = BusCollection;
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
                    start = new DateTime(r.Next(1950, 2018), r.Next(13), r.Next(29));
                    last = new DateTime(r.Next(2019, 2020), 12, r.Next(29));//we put 12 in the month,
                    //So it doesn't take a year from the date of treatment of all the buses
                    //(because the current month is 12)
                    km = r.Next(100000);
                    BusCollection.Add(new Bus(license, start, last, km));
                }
                catch
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

        private void btnAddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow d = new AddBusWindow(BusCollection);
            d.Show();
        }
        private void lvBus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // var item = sender as FrameworkElement;
            var item = lvBus.SelectedItem;
            Bus bus = item as Bus;
            ShowBusWindow dr = new ShowBusWindow(bus);
            dr.Show();
        }
        public void updateState()
        {
            foreach (var item in BusCollection)
            {
                if (item.isOldBus() || item.dangerous())
                {
                    item.MyState = State.isDangerous;
                  
                }
            }
        }
        public void updateStateOfBus(Bus bus)
        {
           
                if (bus.isOldBus() || bus.dangerous()||bus.AmountOfFuelLeft==0)
                {
                    bus.MyState = State.isDangerous;

                }
            
        }

        private void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            int km = 100;//the value of km is send from the DrivingWindow
            DrivingWindow dr = new DrivingWindow(ref km);
            dr.ShowDialog();
            //var item = sender as FrameworkElement;
            Button btn = sender as Button;
            Bus bus = btn.DataContext as Bus;

            if (bus.dangerous(km))
            {
                MessageBox.Show("This driving cannot be made due to exceeding \n the mileage from the previous treatment");
            }
            else
            {
                if (!bus.enoughFuel(km))
                {
                    MessageBox.Show("There is not enough fuel for this driving.");
                }
                else
                {
                    bus.AmountOfFuelLeft -= km;
                    bus.KilometerFromTheLastTreatment += km;
                    bus.Kilometer += km;
                    bus.MyState = State.duringDriving;
                    btn.IsEnabled = false;
                    var parent = btn.Parent as Grid;
                    ProgressBar progressBar = parent.Children[4] as ProgressBar;
                    progressBar.Visibility = Visibility.Visible;
                    Button btn1 = parent.Children[1] as Button;
                    Button btn2 = parent.Children[2] as Button;
                    Button btn3 = parent.Children[3] as Button;
                    btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
                    BackgroundWorker drivingWorker = new BackgroundWorker();
                    drivingWorker.DoWork += driving_DoWork;
                    drivingWorker.ProgressChanged += driving_ProgressChanged;
                    drivingWorker.RunWorkerCompleted += driving_RunWorkerCompleted;
                    drivingWorker.WorkerReportsProgress = true;
                    drivingWorker.WorkerSupportsCancellation = true;
                    int speed = 10;// r.Next(20, 50);
                    double drivingTime = (double)km / speed;
                    List<object> lst = new List<object> { drivingTime, btn, btn1, btn2, btn3, progressBar, bus };
                    drivingWorker.RunWorkerAsync(lst);
                }
            }
        }

        private void driving_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)//****************
        {
            List<object> lst = e.Result as List<object>;
            Button b = lst[1] as Button;
            b.IsEnabled = true;
            b = lst[2] as Button;
            b.IsEnabled = true;
            b = lst[3] as Button;
            b.IsEnabled = true;
            b = lst[4] as Button;
            b.IsEnabled = true;
            ProgressBar progressBar = lst[5] as ProgressBar;
            progressBar.Visibility = Visibility.Hidden;
            Bus bus = lst[6] as Bus;
            bus.MyState = State.isReady;
            updateStateOfBus(bus);
        }


        private void driving_ProgressChanged(object sender, ProgressChangedEventArgs e)//*********************
        {
            int percentage = e.ProgressPercentage;
            ProgressBar progressBar = e.UserState as ProgressBar;
            progressBar.Value = percentage;
        }
        private void driving_DoWork(object sender, DoWorkEventArgs e)//*********************
        {
            List<object> lst = e.Argument as List<object>;
            double timeToSleep = (double)lst[0] /20;//the progressbar will update 20 times
            BackgroundWorker b = sender as BackgroundWorker;
            for (int i = 1; i <= 20; i++)
            {
                Thread.Sleep((int)timeToSleep * 6000);
                b.ReportProgress(i*5,(ProgressBar)lst[5]);

            }
            e.Result = lst;
        }
    }
}
