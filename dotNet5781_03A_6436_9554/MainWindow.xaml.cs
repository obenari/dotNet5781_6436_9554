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
using dotNet5781_02_6436_9554;
using System.Data;


namespace dotNet5781_03A_6436_9554
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        private StationList bussesStop = new StationList();
        private BusCompany egged = new BusCompany();
        public MainWindow()
        {
            InitializeComponent();

            initBusseLines();
            cbBusLines.ItemsSource = egged;
            cbBusLines.DisplayMemberPath = "BusNumber";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine(egged.Busses[0].BusNumber);


        }

        private void initBusseLines()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 40; i++)//create 40 stations
            {
                try
                {
                    bussesStop.add(new BusStation(r.Next(1000000)));
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station is not created
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station is not created
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station is not created
                }

            }
            while (egged.Busses.Count < 10)//create 10 busses
            {
                try
                {
                    egged.addBus(new BusLine(r.Next(1000), (Area)r.Next(5),//this row is too long, so we drop row
                    new BusLineStation(bussesStop[r.Next(40)]), new BusLineStation(bussesStop[r.Next(40)])));
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DuplicateNameException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch
                {
                    Console.WriteLine("ERROR");
                }
            }
            for (int i = 0; i < 40; i++)//to make sure that all stations pass at least one bus line
            {
                try
                {
                    if (egged.totalBusses(bussesStop[i].BusStationKey) == 0)//if no bus passing through the the station number i
                        egged.Busses[r.Next(10)].addToEnd(new BusLineStation(bussesStop[i]));

                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (DuplicateNameException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch
                {
                    Console.WriteLine("ERROR");
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
            }
            for (int i = 0; i < 10; i++)//To make sure at least 10 stations pass more than one bus line
            {
                try
                {
                    if (egged.totalBusses(bussesStop[i].BusStationKey) < 2)//Check if less than two buses pass through the station
                    {
                        egged.Busses[r.Next(10)].addToEnd(new BusLineStation(bussesStop[i]));
                    }
                }

                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (DuplicateNameException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch
                {
                    Console.WriteLine("ERROR");
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }

            }
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusNumber);
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = egged[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
    }
}
