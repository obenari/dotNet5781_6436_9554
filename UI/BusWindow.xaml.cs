﻿using BLAPI;
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
        /// when the user click double click, a new window will show the details of the choosen bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void lvBus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    // var item = sender as FrameworkElement;
        //    var item = lvBus.SelectedItem;
        //    Bus bus = item as Bus;
        //    ShowBusWindow dr = new ShowBusWindow(bus);
        //    dr.ShowDialog();
        //}

        private void btnRefuel_Click(object sender, RoutedEventArgs e)
        {
            PO.Bus bus = (sender as Button).DataContext as PO.Bus;
            bus.Fuel = 1200;
            BO.Bus boBus = Adapter.BOPOAdapter(bus);
            bl.UpdateBus(boBus);
            MessageBox.Show("האוטובוס נשלח לתדלוק");
        }
        private void btnTreatment_Click(object sender, RoutedEventArgs e)
        {
            PO.Bus bus = (sender as Button).DataContext as PO.Bus;
            bus.Fuel = 1200;
            bus.DateOfTreatment = DateTime.Now;
            bus.TotalKmsFromLastTreatment = 0;
            BO.Bus boBus = Adapter.BOPOAdapter(bus);
            bl.UpdateBus(boBus);
            MessageBox.Show("האוטובוס נשלח לטיפול");
        }

        private void lvBus_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var item = lvBus.SelectedItem;
            Bus bus = item as Bus;
            ShowBusWindow dr = new ShowBusWindow(bus);
            dr.ShowDialog();
        }

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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
        ///// <summary>
        ///// this func create a thread and send the bus to driving
        /////we are used in BackgroundWorker because this is the most match to use with wpf
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnDrive_Click(object sender, RoutedEventArgs e)
        //{
        //    string num = "";//the value of km is send from the DrivingWindow
        //    DrivingWindow dr = new DrivingWindow(/*ref num*/);
        //    dr.ShowDialog();
        //    Button btn = sender as Button;
        //    Bus bus = btn.DataContext as Bus;
        //    num = dr.tbKm.Text;
        //    int km = int.Parse(num);
        //    if (bus.dangerous(km))
        //    {
        //        MessageBox.Show("This driving cannot be made due to exceeding \n the mileage from the previous treatment");
        //        return;
        //    }
        //    if (!bus.enoughFuel(km))
        //    {
        //        MessageBox.Show("There is not enough fuel for this driving.");
        //        return;
        //    }
        //    if (bus.isOldBus())
        //    {
        //        MessageBox.Show("the bus need a treatment");
        //        return;
        //    }
        //    bus.Fuel -= km;
        //    bus.TotalKmsFromLastTreatment += km;
        //    bus.TotalKms += km;
        //    bus.Status = BO.Status.Driving;
        //    btn.IsEnabled = false;
        //    var parent = btn.Parent as Grid;
        //    ProgressBar progressBar = parent.Children[5] as ProgressBar;
        //    progressBar.Visibility = Visibility.Visible;
        //    Button btn1 = parent.Children[2] as Button;
        //    Button btn2 = parent.Children[3] as Button;
        //    Button btn3 = parent.Children[4] as Button;
        //    TextBlock tb = parent.Children[1] as TextBlock;
        //    tb.Foreground = Brushes.Brown;
        //    btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
        //    BackgroundWorker drivingWorker = new BackgroundWorker();
        //    drivingWorker.DoWork += driving_DoWork;
        //    drivingWorker.ProgressChanged += driving_ProgressChanged;
        //    drivingWorker.RunWorkerCompleted += driving_RunWorkerCompleted;
        //    drivingWorker.WorkerReportsProgress = true;
        //    drivingWorker.WorkerSupportsCancellation = true;
        //    int speed = r.Next(20, 50);
        //    double drivingTime = (double)km / speed;
        //    List<object> lst = new List<object> { drivingTime, btn, btn1, btn2, btn3, progressBar, bus, tb };
        //    drivingWorker.RunWorkerAsync(lst);
        //}

        //public void driving_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)//****************
        //{
        //    List<object> lst = e.Result as List<object>;
        //    Button b = lst[1] as Button;
        //    b.IsEnabled = true;
        //    b = lst[2] as Button;
        //    b.IsEnabled = true;
        //    b = lst[3] as Button;
        //    b.IsEnabled = true;
        //    b = lst[4] as Button;
        //    b.IsEnabled = true;
        //    ProgressBar progressBar = lst[5] as ProgressBar;
        //    progressBar.Visibility = Visibility.Collapsed;
        //    Bus bus = lst[6] as Bus;
        //    bus.Status = BO.Status.Ready;
        //    updateStateOfBus(bus);
        //    TextBlock tb = lst[7] as TextBlock;
        //    if (bus.Status == BO.Status.Ready)
        //        tb.Foreground = Brushes.Green;
        //    else
        //        tb.Foreground = Brushes.Red;

        //}


        //public void driving_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    int percentage = e.ProgressPercentage;
        //    ProgressBar progressBar = e.UserState as ProgressBar;
        //    progressBar.Value = percentage;
        //}
        //public void driving_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    List<object> lst = e.Argument as List<object>;
        //    double timeToSleep = (double)lst[0] / 20;//the progressbar will update 20 times
        //                                             // int sleep =( int)(timeToSleep * 6000);
        //    BackgroundWorker b = sender as BackgroundWorker;
        //    for (int i = 1; i <= 20; i++)
        //    {
        //        Thread.Sleep((int)(timeToSleep * 6000));
        //        b.ReportProgress(i * 5, (ProgressBar)lst[5]);

        //    }
        //    e.Result = lst;
        //}
        ///// <summary>
        ///// this func create a thread and send the bus to refuel
        ///// ///we are used in BackgroundWorker because this is the most match to use with wpf
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnRefuel_Click(object sender, RoutedEventArgs e)
        //{
        //    Button btn = sender as Button;
        //    Bus bus = btn.DataContext as Bus;
        //    if (bus.Fuel == 1200)
        //    {
        //        MessageBox.Show("The tank is full");
        //        return;
        //    }
        //    BackgroundWorker refuelWorker = new BackgroundWorker();
        //    bus.Status = BO.Status.Refueling;
        //    btn.IsEnabled = false;
        //    var parent = btn.Parent as Grid;
        //    TextBlock tbTimer = parent.Children[6] as TextBlock;
        //    tbTimer.Visibility = Visibility.Visible;
        //    //make all the buttons in the row dis enabeled
        //    Button btn1 = parent.Children[2] as Button;
        //    Button btn2 = parent.Children[3] as Button;
        //    Button btn3 = parent.Children[4] as Button;
        //    TextBlock tbLicense = parent.Children[1] as TextBlock;
        //    btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
        //    //changed the color acording status
        //    tbLicense.Foreground = Brushes.Blue;
        //    refuelWorker.DoWork += timer_DoWork;
        //    refuelWorker.ProgressChanged += timer_ProgressChanged;
        //    refuelWorker.RunWorkerCompleted += timer_RunWorkerCompleted;
        //    refuelWorker.WorkerReportsProgress = true;
        //    refuelWorker.WorkerSupportsCancellation = true;
        //    List<object> lst = new List<object> { btn, btn1, btn2, btn3, bus, tbLicense, tbTimer, 12 };//12 seconds is the time that refuel take in simulation watch
        //    refuelWorker.RunWorkerAsync(lst);
        //    bus.Fuel = 1200;//refuel
        //}
        //private void timer_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    List<object> lst = e.Argument as List<object>;
        //    int time = (int)lst[7];

        //    BackgroundWorker b = sender as BackgroundWorker;
        //    for (int i = time; i > 0; i--)
        //    {
        //        Thread.Sleep(1000);
        //        b.ReportProgress(i, (TextBlock)lst[6]);

        //    }
        //    e.Result = lst;
        //}

        //private void timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    int percentage = e.ProgressPercentage;
        //    TextBlock timer = e.UserState as TextBlock;
        //    string str;
        //    if (percentage < 10)
        //    {
        //        str = "00:" + "0" + percentage.ToString();
        //    }
        //    else
        //    {
        //        str = "00:" + percentage.ToString();
        //    }
        //    timer.Text = str;
        //}
        //private void timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    List<object> lst = e.Result as List<object>;
        //    Button b = lst[0] as Button;
        //    b.IsEnabled = true;
        //    b = lst[1] as Button;
        //    b.IsEnabled = true;
        //    b = lst[2] as Button;
        //    b.IsEnabled = true;
        //    b = lst[3] as Button;
        //    b.IsEnabled = true;
        //    TextBlock timer = lst[6] as TextBlock;
        //    timer.Visibility = Visibility.Collapsed;
        //    Bus bus = lst[4] as Bus;
        //    //update the status of the bus
        //    bus.Status = BO.Status.Ready;
        //    updateStateOfBus(bus);
        //    TextBlock tbLicense = lst[5] as TextBlock;
        //    if (bus.Status == BO.Status.Ready)
        //        tbLicense.Foreground = Brushes.Green;
        //    else
        //        tbLicense.Foreground = Brushes.Red;
        //}
        /// <summary>
        ///this func create a thread and send the bus to treatment
        ///we are used in BackgroundWorker because this is the most match to use with wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnTreatment_Click(object sender, RoutedEventArgs e)
        //{
        //    Button btn = sender as Button;
        //    Bus bus = btn.DataContext as Bus;
        //    BackgroundWorker treatmentWorker = new BackgroundWorker();
        //    bus.Status = BO.Status.Treatment;
        //    btn.IsEnabled = false;
        //    var parent = btn.Parent as Grid;
        //    TextBlock tbTimer = parent.Children[6] as TextBlock;
        //    tbTimer.Visibility = Visibility.Visible;
        //    //make all the buttons in the row disenabeled
        //    Button btn1 = parent.Children[2] as Button;
        //    Button btn2 = parent.Children[3] as Button;
        //    Button btn3 = parent.Children[4] as Button;
        //    btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = false;//when the bus is during a driving, all the bottons is disenabled
        //    TextBlock tbLicense = parent.Children[1] as TextBlock;
        //    //changed the text color acording the status
        //    tbLicense.Foreground = Brushes.Purple;
        //    treatmentWorker.DoWork += timer_DoWork;
        //    treatmentWorker.ProgressChanged += timer_ProgressChanged;
        //    treatmentWorker.RunWorkerCompleted += timer_RunWorkerCompleted;
        //    treatmentWorker.WorkerReportsProgress = true;
        //    treatmentWorker.WorkerSupportsCancellation = true;
        //    List<object> lst = new List<object> { btn, btn1, btn2, btn3, bus, tbLicense, tbTimer, 144 };//144 seconds is the time that treatment take in simulation watch
        //    treatmentWorker.RunWorkerAsync(lst);
        //    bus.DateOfTreatment = DateTime.Now;
        //    bus.TotalKmsFromLastTreatment = 0;
        //    bus.Fuel = 1200;//acording to the instruction, the bus is also refuel after the treatment
        //}

        //private void doubleClick_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //    Button btn = sender as Button;
        //    Bus bus = btn.DataContext as Bus;
        //    ShowBusWindow dr = new ShowBusWindow(bus);
        //    dr.ShowDialog();
        //    if (dr.isClickRefuel == true)
        //    {

        //        var parent = btn.Parent as Grid;
        //        Button btnRefuel = parent.Children[2] as Button;
        //        btnRefuel_Click(btn, new RoutedEventArgs());
        //    }
        //    if (dr.isClickTreatment == true)
        //    {
        //        var parent = btn.Parent as Grid;
        //        Button btnTreatment = parent.Children[3] as Button;
        //        btnTreatment_Click(btn, new RoutedEventArgs());
        //    }
        //  }
    }
}
