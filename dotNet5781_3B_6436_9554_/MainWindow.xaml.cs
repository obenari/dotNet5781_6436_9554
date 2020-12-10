﻿using System;
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

        private void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            int km = 0;
            DrivingWindow dr = new DrivingWindow(ref km);
            dr.Show();
        }
    }
}
