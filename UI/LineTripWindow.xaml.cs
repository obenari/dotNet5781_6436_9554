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
using System.Windows.Shapes;
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for LineTripWindow.xaml
    /// </summary>
    public partial class LineTripWindow : Window
    {
        IBL bl;
        ObservableCollection<PO.BusLine> LinesCollection;
        ObservableCollection<BO.LineTrip> LineTripCollection;
        PO.BusLine CurrentBusLine;
        public LineTripWindow(IBL myBl)
        {
            InitializeComponent();
            bl = myBl;
            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(BO.Areas.Jerusalem).ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            LineTripCollection = new ObservableCollection<BO.LineTrip>(bl.GetAllLinesTripBy(l => l.LineId == LinesCollection[0].Id));
            cbLine.ItemsSource = LinesCollection;
            CurrentBusLine = LinesCollection[0];
            //cbLine.SelectedIndex = 0;
         //   cbLine.SelectedItem= LinesCollection[0];
            cbArea.SelectedIndex = 0;
            lvLineTrip.DataContext = LineTripCollection;
            this.DataContext = CurrentBusLine;
        }

        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Areas area = (BO.Areas)(this.cbArea.SelectedItem);
            LinesCollection= new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(area).ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            cbLine.ItemsSource = LinesCollection;
            LineTripCollection = new ObservableCollection<BO.LineTrip>(bl.GetAllLinesTripBy(l => l.LineId == LinesCollection[0].Id));
            cbLine.SelectedIndex = 0;
            lvLineTrip.DataContext = LineTripCollection;
            this.DataContext = LinesCollection[0];
        }

        private void cbLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentBusLine = cbLine.SelectedItem as PO.BusLine;
            if (CurrentBusLine != null)
            {
                this.DataContext = CurrentBusLine;
                LineTripCollection =new ObservableCollection<BO.LineTrip>( bl.GetAllLinesTripBy(x => x.LineId == CurrentBusLine.Id));
                lvLineTrip.ItemsSource = LineTripCollection;
            }
            }

        private void btnAddSingle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan timeSpan = new TimeSpan(int.Parse(tbHour.Text), int.Parse(tbMinute.Text), int.Parse(tbSecond.Text));
                BO.LineTrip lineTrip = new BO.LineTrip { LineId = CurrentBusLine.Id, StartAt = timeSpan };
                bl.AddLineTrip(lineTrip);
                LineTripCollection.Add(lineTrip);
            }
            catch(BO.DuplicateLineTripException ex)
            {
                MessageBox.Show("יציאת הקו כבר קיימת במערכת");
            }
            catch
            {
                MessageBox.Show("הנתונים שהוזנו אינם חוקיים");
            }
        }

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

        private void btnAddMulti_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan timeStart = new TimeSpan(int.Parse(tbHourStart.Text), int.Parse(tbMinuteStart.Text), int.Parse(tbSecondStart.Text));
                TimeSpan timeFinish = new TimeSpan(int.Parse(tbHourFinish.Text), int.Parse(tbMinuteFinish.Text), int.Parse(tbSecondFinish.Text));
                TimeSpan timeFrequency = new TimeSpan(int.Parse(tbHourFrequency.Text), int.Parse(tbMinuteFrequency.Text), int.Parse(tbSecondFrequency.Text));
                bl.AddLineTrip(CurrentBusLine.Id ,timeStart,timeFinish,timeFrequency);
                LineTripCollection = new ObservableCollection<BO.LineTrip>(bl.GetAllLinesTripBy(l => l.LineId == CurrentBusLine.Id));
                lvLineTrip.ItemsSource = LineTripCollection;
            }
            catch (BO.DuplicateLineTripException ex)
            {string str="ישנן יציאות קו כפולות\n ";
            
            MessageBox.Show(str+"    אנא מחק יציאות או שנה את נתוני הכנסה");
            }
            catch
            {
                MessageBox.Show("הנתונים שהוזנו אינם חוקיים");
            }
        }

        private void btnMulti_Click(object sender, RoutedEventArgs e)
        {
            btnMulti.IsEnabled = false;
            btnSingle.IsEnabled = true;
            addSingleGrid.Visibility = Visibility.Collapsed;
            addMultiGrid.Visibility = Visibility.Visible;
        }

        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            btnSingle.IsEnabled = false;
            btnMulti.IsEnabled = true;
            addMultiGrid.Visibility = Visibility.Collapsed;
            addSingleGrid.Visibility = Visibility.Visible;

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvLineTrip.SelectedItem == null)
            {
                MessageBox.Show("לא נבחרה יציאת קו");
                return;
            }
         
            if (MessageBox.Show(string.Format("?יציאת הקו עומדת להימחק אתה בטוח"), "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    BO.LineTrip lineTripToRemove = lvLineTrip.SelectedItem as BO.LineTrip;
                    bl.DeleteLineTrip(lineTripToRemove.Id);
                    LineTripCollection.Remove(lineTripToRemove);
                }
                catch (Exception ex)//in order the program will not fail due to exception that the engeneering not thougt about it
                {
                    MessageBox.Show("משהו השתבש נסה שנית");
                }
            }

        }
    }
}