using BLAPI;
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
using System.Windows.Shapes;
using PO;
using System.ComponentModel;
using System.Threading;

namespace UI
{
    /// <summary>
    /// Interaction logic for BusWindow.xaml
    /// </summary>
    public partial class BusWindow : Window
    {
        IBL bl;
        private static Random r = new Random(DateTime.Now.Millisecond);
        private ObservableCollection<PO.Bus> BusCollection = new ObservableCollection<PO.Bus>();
        public BusWindow(IBL myBl)
        {
            InitializeComponent();
            bl = myBl;
            BusCollection = new ObservableCollection<Bus>(bl.GetAllBusses().ToList().ConvertAll(b => Adapter.POBOAdapter(b)));
            DataContext = BusCollection;
        }
       
        /// <summary>
        /// add bus to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow d = new AddBusWindow(BusCollection,bl);
            d.Show();
        }
       

    
    /// <summary>
    /// when the user click double click,this method will show the details of the choosen bus
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void lvBus_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var item = lvBus.SelectedItem;
            Bus bus = item as Bus;
            busDetailsGrid.DataContext = bus;
        }
        /// <summary>
        /// this method delete the request bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvBus.SelectedItem == null)
            {
                MessageBox.Show("לא נבחר אוטובוס");
                return;
            }
            PO.Bus bus = lvBus.SelectedItem as PO.Bus;

            if (MessageBox.Show(string.Format("?האוטובוס  " + bus.License + " עומד להימחק אתה בטוח"), "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    bl.DeleteBus(Adapter.BOPOAdapter(bus));
                    BusCollection.Remove(bus);
                }
                catch (Exception ex)//in order the program will not fail due to exception that the engeneering not thougt about it
                {
                    MessageBox.Show("משהו השתבש נסה שנית");
                }
            }

        }


        /// <summary>
        /// this func create a thread and send the bus to driving
        ///we are used in BackgroundWorker because this is the most match to use with wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string num = "0";//the value of km is send from the DrivingWindow
                DrivingWindow dr = new DrivingWindow(/*ref num*/);
                dr.ShowDialog();
                Button btn = sender as Button;
                Bus bus = btn.DataContext as Bus;
                num = dr.tbKm.Text;
                int km = int.Parse(num);
                BO.Bus boBus = Adapter.BOPOAdapter(bus);
                if (bl.IsReadyToDriving(boBus, km) == false)//if the bus could do the driving, the values of boBus is exchanged in the func in bl
                {
                    MessageBox.Show("אין אפשרות לצאת לנסיעה");
                    bus.Status = boBus.Status;
                    return;
                }
                //if the bus  could do the driving, update its values
                bus.TotalKms = boBus.TotalKms;
                bus.TotalKmsFromLastTreatment = boBus.TotalKmsFromLastTreatment;
                bus.Fuel = boBus.Fuel;
                bus.Status = BO.Status.Driving;
                //now update the buttons in the bus row to be disenable
                var parent = btn.Parent as Grid;
                ProgressBar progressBar = parent.Children[4] as ProgressBar;
                progressBar.Visibility = Visibility.Visible;
                Button btn1 = parent.Children[1] as Button;
                Button btn2 = parent.Children[2] as Button;
                Button btn3 = parent.Children[3] as Button;
                TextBlock tb = parent.Children[0] as TextBlock;
                btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
                BackgroundWorker drivingWorker = new BackgroundWorker();
                drivingWorker.DoWork += driving_DoWork;
                drivingWorker.ProgressChanged += driving_ProgressChanged;
                drivingWorker.RunWorkerCompleted += driving_RunWorkerCompleted;
                drivingWorker.WorkerReportsProgress = true;
                drivingWorker.WorkerSupportsCancellation = true;
                int speed = r.Next(20, 50);
                double drivingTime = (double)km / speed;
                List<object> lst = new List<object> { drivingTime, btn, btn1, btn2, btn3, progressBar, bus };
                drivingWorker.RunWorkerAsync(lst);
            }
            catch
            {
                MessageBox.Show("משהו השתבש נסה שנית");
            }
        }
        /// <summary>
        /// this method make the row of the bus in driving to be regular
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void driving_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
            bus.Status = BO.Status.Ready;

        }

        /// <summary>
        /// this metthod update the progressbar of the bus in driving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void driving_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentage = e.ProgressPercentage;
            ProgressBar progressBar = e.UserState as ProgressBar;
            progressBar.Value = percentage;
        }
        /// <summary>
        /// this method sleep and call to progressChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void driving_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> lst = e.Argument as List<object>;
            double timeToSleep = (double)lst[0] / 20;//the progressbar will update 20 times
                                                     // int sleep =( int)(timeToSleep * 6000);
            BackgroundWorker b = sender as BackgroundWorker;
            for (int i = 1; i <= 20; i++)
            {
                b.ReportProgress(i * 5, (ProgressBar)lst[5]);
                Thread.Sleep((int)(timeToSleep * 6000));

            }
            e.Result = lst;
        }
        /// <summary>
        /// this func create a thread and send the bus to refuel
        /// ///we are used in BackgroundWorker because this is the most match to use with wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefuel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                Bus bus = btn.DataContext as Bus;
                BackgroundWorker refuelWorker = new BackgroundWorker();
                bus.Status = BO.Status.Refueling;
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
                refuelWorker.DoWork += timer_DoWork;
                refuelWorker.ProgressChanged += timer_ProgressChanged;
                refuelWorker.RunWorkerCompleted += timer_RunWorkerCompleted;
                refuelWorker.WorkerReportsProgress = true;
                refuelWorker.WorkerSupportsCancellation = true;
                List<object> lst = new List<object> { btn, btn1, btn2, btn3, bus, tbLicense, tbTimer, 12 };//12 seconds is the time that refuel take in simulation watch
                refuelWorker.RunWorkerAsync(lst);
                BO.Bus boBus = Adapter.BOPOAdapter(bus);
                bl.RefuelBus(boBus);
                bus.Fuel = boBus.Fuel;
            }
            catch (Exception ex)//in order the program will not fail due to exception that the engeneering not thougt about it
            {
                MessageBox.Show("משהו השתבש נסה שנית");
            }
        }
        private void timer_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> lst = e.Argument as List<object>;
            int time = (int)lst[7];

            BackgroundWorker b = sender as BackgroundWorker;
            for (int i = time; i > 0; i--)
            {
                Thread.Sleep(1000);
                b.ReportProgress(i, (TextBlock)lst[6]);

            }
            e.Result = lst;
        }
        /// <summary>
        /// this method update the time left until the bus will comeback from treatment or refuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentage = e.ProgressPercentage;
            TextBlock timer = e.UserState as TextBlock;
            string str;
            if (percentage%60 < 10||percentage<10)
            {
                 str = string.Format("0" + percentage / 60 + ":0" + percentage % 60);
            }
            else
            {
                str = string.Format("0" + percentage / 60 + ":" + percentage % 60);
            }
            timer.Text = str;
        }
        /// <summary>
        /// this method make the row of the bus to be regular
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            bus.Status = BO.Status.Ready;
          
        }
        /// <summary>
        ///this func create a thread and send the bus to treatment
        ///we are used in BackgroundWorker because this is the most match to use with wpf
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
        private void btnTreatment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Bus bus = btn.DataContext as Bus;
            BackgroundWorker treatmentWorker = new BackgroundWorker();
            bus.Status = BO.Status.Treatment;
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
            treatmentWorker.DoWork += timer_DoWork;
            treatmentWorker.ProgressChanged += timer_ProgressChanged;
            treatmentWorker.RunWorkerCompleted += timer_RunWorkerCompleted;
            treatmentWorker.WorkerReportsProgress = true;
            treatmentWorker.WorkerSupportsCancellation = true;
            List<object> lst = new List<object> { btn, btn1, btn2, btn3, bus, tbLicense, tbTimer, 144 };//144 seconds is the time that treatment take in simulation watch
            treatmentWorker.RunWorkerAsync(lst);
            BO.Bus boBus = Adapter.BOPOAdapter(bus);
            bl.TreatmentBus(boBus);
            //now update the poBus acording boBus
            bus.Fuel = boBus.Fuel;
            bus.DateOfTreatment = boBus.DateOfTreatment;
            bus.TotalKmsFromLastTreatment = boBus.TotalKmsFromLastTreatment;

        }
        /// <summary>
        /// this method alow to type only digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textOnlyNumber(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
        /// <summary>
        /// this method close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
