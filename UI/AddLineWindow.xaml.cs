﻿using BLAPI;
using BO;
using PO;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        PO.BusLine LineToAdd = new BusLine { Stations = new ObservableCollection<PO.LineStation>()};
        ObservableCollection<PO.Station> StationCollection;

        IBL bl;
        public AddLineWindow(IBL myBl)
        {
            InitializeComponent();
            bl = myBl;
            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            cbArea.SelectedIndex = (int)BO.Areas.Jerusalem;
         
            StationCollection = new ObservableCollection<PO.Station>(
                bl.GetAllStations().ToList().ConvertAll(s => Adapter.POBOAdapter(s)));
            stationDataGrid.DataContext = StationCollection;
            lvLine.DataContext = LineToAdd.Stations;

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
        /// this method add the selected station to the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {
            if (stationDataGrid.SelectedItem == null)
            {
                MessageBox.Show("לא נבחרה תחנה");
                return;
            }
            if (int.Parse(tbIndex.Text) - 1 > LineToAdd.Stations.Count())
            {
                MessageBox.Show("לא ניתן לבחור אינדקס החורג ממספר התחנות בקו");
                return;
            }
            else
            {
                PO.Station stationToAdd = stationDataGrid.SelectedItem as PO.Station;
                PO.LineStation newLineStation = new PO.LineStation
                {
                    StationCode = stationToAdd.Code,
                    StationName = stationToAdd.Name,
                };
                LineToAdd.Stations.Insert(int.Parse(tbIndex.Text) - 1, newLineStation);//the user start to insert index from 1, 
                //therefor we insert the new lineStation in the index minus 1
            }
        }
        /// <summary>
        /// this method remove the selected lineStation from the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveStation_Click(object sender, RoutedEventArgs e)
        {
            PO.LineStation lineStationToRemove = (sender as Button).DataContext as PO.LineStation;
            LineToAdd.Stations.Remove(lineStationToRemove);
        }
        private void btnUpdateTime_Click(object sender, RoutedEventArgs e)
        {

            TimeSpan newTime;
            string help = (((sender as Button).Parent as Grid).Children[0] as TextBox).Text;
            bool succes = TimeSpan.TryParse(help, out newTime);
            if (succes == false)
            {
                MessageBox.Show("הפורמט שהוכנס אינו תקין");
                return;
            }
            PO.LineStation lineStation = (sender as Button).DataContext as PO.LineStation;
            lineStation.Time = newTime;
            ((sender as Button).Parent as Grid).Visibility = Visibility.Collapsed;//get the grid of update and meke it collapsed
            //get the button of update and meke it visible
            (((sender as Button).Parent as Grid).Parent as Grid).Children[0].Visibility = Visibility.Visible;

        }
        /// <summary>
        /// this method make the update time grid (in the listView) to be visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTime_Click(object sender, RoutedEventArgs e)
        {
            // get the button of update and meke it collapsed
            ((sender as Button).Parent as Grid).Children[0].Visibility = Visibility.Collapsed;
            ((sender as Button).Parent as Grid).Children[1].Visibility = Visibility.Visible;

        }
        /// <summary>
        /// this method put the distance in the lineStation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateDistance_Click(object sender, RoutedEventArgs e)
        {

            double newDistance;
            string help = (((sender as Button).Parent as Grid).Children[0] as TextBox).Text;
            bool succes = double.TryParse(help, out newDistance);
            if (succes == false)
            {
                MessageBox.Show("הפורמט שהוכנס אינו תקין");
                return;
            }
            PO.LineStation lineStation = (sender as Button).DataContext as PO.LineStation;
            lineStation.Distance = newDistance;
            ((sender as Button).Parent as Grid).Visibility = Visibility.Collapsed;//get the grid of update and meke it collapsed
            //get the button of update and meke it visible
            (((sender as Button).Parent as Grid).Parent as Grid).Children[0].Visibility = Visibility.Visible;

        }
        /// <summary>
        /// this method add the new line to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLineToBl_Click(object sender, RoutedEventArgs e)
        {
            if (LineToAdd.Stations.Count < 2)
            {
                MessageBox.Show("קו אוטובוס צריך להכיל לפחות שתי תחנות");
                return;
            }
            LineToAdd.Area = (BO.Areas)(cbArea.SelectedItem);
            if (tbLineNumber.Text == "")
            {
                MessageBox.Show("נא להכניס מספר קו");
                return;
            }

            LineToAdd.LineNumber = int.Parse(tbLineNumber.Text);
            LineToAdd.LastStation = LineToAdd.Stations[LineToAdd.Stations.Count() - 1].StationName;
            LineToAdd.FirstStation = LineToAdd.Stations[0].StationName;
            BO.Line boLineToAdd = Adapter.BOPOAdapter(LineToAdd);
            try
            {
                LineToAdd.Id = bl.AddLine(boLineToAdd);///////////////////
                this.Close();
            }
            catch (BO.NotEnoughInformationException ex)//if there is no information about distance and time between all the lineSation,
            { //bl will thrown an exception
                string str = ":נא לעדכן את המרחק והזמן בין התחנות";
                str += "\n";
                str += ex.Message;
                MessageBox.Show(str);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("משהו השתבש נסה שנית");
            }

        }
        /// <summary>
        /// this method closed this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
