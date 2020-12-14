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
        /// <summary>
        /// add bus to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow d = new AddBusWindow(BusCollection);
            d.Show();
        }
        /// <summary>
        /// when the user click double click, a new window will show the details of the choosen bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // var item = sender as FrameworkElement;
            var item = lvBus.SelectedItem;
            Bus bus = item as Bus;
            ShowBusWindow dr = new ShowBusWindow(bus,this);
            dr.ShowDialog();
            if(dr.isClickRefuel==true)
            {
                //var parent = sender 
                // Button btn = parent.Children[1] as Button;
                Button btn = lvBus.SelectedItem as Button;
                btnRefuel_Click(btn, new RoutedEventArgs());
            }
            if (dr.isClickTreatment == true)
            {
                var parent = item as Grid;
                Button btn = parent.Children[2] as Button;
                btnRefuel_Click(btn, new RoutedEventArgs());
            }
        }
      
        //public void updateState()
        //{
        //    foreach (var item in BusCollection)
        //    {
        //        if (item.isOldBus() || item.dangerous())
        //        {
        //            item.MyState = State.isDangerous;
                  
        //        }
        //    }
        //}
        /// <summary>
        /// this func update the status of the bus
        /// </summary>
        /// <param name="bus"></param>
        public void updateStateOfBus(Bus bus)
        {
           
                if (bus.isOldBus() || bus.dangerous()||bus.AmountOfFuelLeft==0)
                {
                    bus.MyState = State.isDangerous;

                }
            
        }
        /// <summary>
        /// this func create a thread and send the bus to driving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            string num="";//the value of km is send from the DrivingWindow
            DrivingWindow dr = new DrivingWindow(/*ref num*/);
            dr.ShowDialog();
            Button btn = sender as Button;
            Bus bus = btn.DataContext as Bus;
            num = dr.tbKm.Text;
            int km = int.Parse(num);
            if (bus.dangerous(km))
            {
                MessageBox.Show("This driving cannot be made due to exceeding \n the mileage from the previous treatment");
                return;
            }
            if (!bus.enoughFuel(km))
            {
                MessageBox.Show("There is not enough fuel for this driving.");
                return;
            }
            if (bus.isOldBus())
            {
                MessageBox.Show("the bus need a treatment");
                return;
            }
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
            TextBlock tb = parent.Children[0] as TextBlock;
            tb.Foreground = Brushes.Brown;
            btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
            BackgroundWorker drivingWorker = new BackgroundWorker();
            drivingWorker.DoWork += driving_DoWork;
            drivingWorker.ProgressChanged += driving_ProgressChanged;
            drivingWorker.RunWorkerCompleted += driving_RunWorkerCompleted;
            drivingWorker.WorkerReportsProgress = true;
            drivingWorker.WorkerSupportsCancellation = true;
            int speed = r.Next(20, 50);
            double drivingTime = (double)km / speed;
            List<object> lst = new List<object> { drivingTime, btn, btn1, btn2, btn3, progressBar, bus, tb };
            drivingWorker.RunWorkerAsync(lst);
        }

        public void driving_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)//****************
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
            progressBar.Visibility = Visibility.Collapsed;
            Bus bus = lst[6] as Bus;
            bus.MyState = State.isReady;
            updateStateOfBus(bus);
            TextBlock tb = lst[7] as TextBlock;
            if (bus.MyState == State.isReady)
                tb.Foreground = Brushes.Green;
            else
                tb.Foreground = Brushes.Red;

        }


        public void driving_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentage = e.ProgressPercentage;
            ProgressBar progressBar = e.UserState as ProgressBar;
            progressBar.Value = percentage;
        }
        public void driving_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> lst = e.Argument as List<object>;
            double timeToSleep = (double)lst[0] /20;//the progressbar will update 20 times
           // int sleep =( int)(timeToSleep * 6000);
            BackgroundWorker b = sender as BackgroundWorker;
            for (int i = 1; i <= 20; i++)
            {
                Thread.Sleep((int)(timeToSleep * 6000));
                b.ReportProgress(i*5,(ProgressBar)lst[5]);

            }
            e.Result = lst;
        }
        /// <summary>
        /// this func create a thread and send the bus to refuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefuel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Bus bus = btn.DataContext as Bus;
            if (bus.AmountOfFuelLeft == 1200)
            {
                MessageBox.Show("The tank is full");
                return;
            }
            BackgroundWorker refuelWorker = new BackgroundWorker();
            bus.MyState = State.refueling;
            btn.IsEnabled = false;
            var parent = btn.Parent as Grid;
            TextBlock tbTimer = parent.Children[5] as TextBlock;
            tbTimer.Visibility = Visibility.Visible;
            //make all the buttons in the row dis enabeled
            Button btn1 = parent.Children[1] as Button;
            Button btn2 = parent.Children[2] as Button;
            Button btn3 = parent.Children[3] as Button;
            TextBlock tbLicense = parent.Children[0] as TextBlock;
            btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
            //changed the color acording status
            tbLicense.Foreground = Brushes.Blue;
            refuelWorker.DoWork += timer_DoWork;
            refuelWorker.ProgressChanged += timer_ProgressChanged;
            refuelWorker.RunWorkerCompleted += timer_RunWorkerCompleted;
            refuelWorker.WorkerReportsProgress = true;
            refuelWorker.WorkerSupportsCancellation = true;
            List<object> lst = new List<object> { btn, btn1, btn2, btn3, bus, tbLicense,tbTimer ,12};//12 seconds is the time that refuel take in simulation watch
            refuelWorker.RunWorkerAsync(lst);
            bus.AmountOfFuelLeft = 1200;//refuel
        }
        private void timer_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> lst = e.Argument as List<object>;
            int time = (int)lst[7];
                                                    
            BackgroundWorker b = sender as BackgroundWorker;
            for (int i = time; i >0; i--)
            {
                Thread.Sleep(1000);
                b.ReportProgress(i, (TextBlock)lst[6]);

            }
            e.Result = lst;
        }
       
        private void timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentage = e.ProgressPercentage;
            TextBlock timer = e.UserState as TextBlock;
            string str;
            if (percentage < 10)
            {
                 str = "0" + percentage.ToString() + ":00";
            }
            else
            { 
                 str = percentage.ToString() + ":00";
            }
            timer.Text =str ;
        }
        private void timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<object> lst = e.Result as List<object>;
            Button b = lst[0] as Button;
            b.IsEnabled = true;
            b = lst[1] as Button;
            b.IsEnabled = true;
            b = lst[2] as Button;
            b.IsEnabled = true;
            b = lst[3] as Button;
            b.IsEnabled = true;
            TextBlock timer = lst[6] as TextBlock;
            timer.Visibility = Visibility.Collapsed;
            Bus bus = lst[4] as Bus;
            //update the status of the bus
            bus.MyState = State.isReady;
            updateStateOfBus(bus);
            TextBlock tbLicense = lst[5] as TextBlock;
            if (bus.MyState == State.isReady)
                tbLicense.Foreground = Brushes.Green;
            else
                tbLicense.Foreground = Brushes.Red;
        }
        /// <summary>
        ///this func create a thread and send the bus to treatment

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTreatment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Bus bus = btn.DataContext as Bus;
            BackgroundWorker treatmentWorker = new BackgroundWorker();
            bus.MyState = State.inTreatment;
            btn.IsEnabled = false;
            var parent = btn.Parent as Grid;
            TextBlock tbTimer = parent.Children[5] as TextBlock;
            tbTimer.Visibility = Visibility.Visible;
            //make all the buttons in the row disenabeled
            Button btn1 = parent.Children[1] as Button;
            Button btn2 = parent.Children[2] as Button;
            Button btn3 = parent.Children[3] as Button;
            btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
            TextBlock tbLicense = parent.Children[0] as TextBlock;
            //changed the text color acording the status
            tbLicense.Foreground = Brushes.Purple;
            treatmentWorker.DoWork += timer_DoWork;
            treatmentWorker.ProgressChanged += timer_ProgressChanged;
            treatmentWorker.RunWorkerCompleted += timer_RunWorkerCompleted;
            treatmentWorker.WorkerReportsProgress = true;
            treatmentWorker.WorkerSupportsCancellation = true;
            List<object> lst = new List<object> { btn, btn1, btn2, btn3, bus, tbLicense, tbTimer, 144 };//144 seconds is the time that treatment take in simulation watch
            treatmentWorker.RunWorkerAsync(lst);
            bus.DateOftreatment = DateTime.Now;
            bus.KilometerFromTheLastTreatment = 0;
            bus.AmountOfFuelLeft = 1200;//acording to the instruction, the bus is also refuel after the treatment
        }
    }
}
