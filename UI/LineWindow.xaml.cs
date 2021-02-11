using BLAPI;
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
    /// Interaction logic for LineWindow.xaml
    /// </summary>
    public partial class LineWindow : Window
    {
        IBL bl;
        ObservableCollection<PO.BusLine> LinesCollection;
        ObservableCollection<PO.Station> StationCollection;
        PO.BusLine LineToAdd;
        public LineWindow(IBL MyBL)
        {
            InitializeComponent();
            bl = MyBL;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            areaComboBox.SelectedIndex = (int)BO.Areas.Jerusalem;
            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            cbArea.SelectedIndex = 0;// (int)BO.Areas.Jerusalem;
            LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(BO.Areas.Jerusalem).ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;
            
        }

        private void areaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Areas area = (BO.Areas)areaComboBox.SelectedItem;
            LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(area).
                ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;

        }

        private void lineDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.BusLine busLine = lineDataGrid.SelectedItem as PO.BusLine;
            this.stationGrid.DataContext = busLine;
            lvStation.DataContext = busLine.Stations;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lineDataGrid.SelectedItem == null)
            {
                MessageBox.Show("לא נבחר קו");
                return;
            }
            int lineNum = (lineDataGrid.SelectedItem as BusLine).LineNumber;

            if (MessageBox.Show(string.Format("?קו מספר  " + lineNum + " עומד להימחק אתה בטוח"), "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    PO.BusLine lineToRemove = lineDataGrid.SelectedItem as BusLine;
                    bl.DeleteStation(lineToRemove.Id);
                    LinesCollection.Remove(lineToRemove);
                    if (stationGrid.DataContext == lineToRemove)
                    {
                        if (LinesCollection.Count != 0)
                        {
                            this.stationGrid.DataContext = LinesCollection[0];
                            this.lvStation.DataContext = LinesCollection[0].Stations;
                        }
                        else
                        {
                            this.stationGrid.DataContext = null;
                            this.lvStation.DataContext = null;

                        }
                    }


                }
                catch//in order the program will not fail due to exception that the engeneering not thougt about it
                {
                    MessageBox.Show("משהו השתבש נסה שנית");
                }
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lineDataGrid.SelectedItem == null)
            {
                MessageBox.Show("לא נבחר קו");
                return;
            }
            PO.BusLine lineToUpdate  = lineDataGrid.SelectedItem as BusLine;
            UpdateLineWindow lineWindow = new UpdateLineWindow(bl, lineToUpdate);
            lineWindow.ShowDialog();
            //update the line in the collection
            LinesCollection.Remove(lineToUpdate);
            LinesCollection.Add(Adapter.POBOAdapter(bl.GetLine(lineToUpdate.Id)));
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddLineWindow addLineWindow = new AddLineWindow(bl);
            addLineWindow.ShowDialog();
            BO.Areas area = (BO.Areas)areaComboBox.SelectedItem;
            LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(area).
                ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;

         //   LinesCollection=new ObservableCollection<BusLine>( bl.GetAllLinesByArea(areaComboBox.SelectedItem );

            //showGrid.Visibility = Visibility.Collapsed;
            //addGrid.Visibility = Visibility.Visible;
            //StationCollection = new ObservableCollection<PO.Station>(
            //    bl.GetAllStations().ToList().ConvertAll(s => Adapter.POBOAdapter(s)));
            //stationDataGrid.DataContext = StationCollection;
            //LineToAdd = new PO.BusLine
            //{
            //    Stations = new ObservableCollection<PO.LineStation>()
            //};
            //lvLine.DataContext = LineToAdd.Stations;
            //cbArea.ItemsSource = Enum.GetValues(typeof(BO.Areas));

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

        private void btnTime_Click(object sender, RoutedEventArgs e)
        {
            // get the button of update and meke it collapsed
            ((sender as Button).Parent as Grid).Children[0].Visibility = Visibility.Collapsed;
            ((sender as Button).Parent as Grid).Children[1].Visibility = Visibility.Visible;

        }

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
                bl.AddLine(boLineToAdd);
                this.Close();
            }
            catch (BO.NotEnoughInformationException ex)//if there is no information about distance and time between all the lineSation,
            { //bl will thrown an exception
                string str = ":נא לעדכן את המרחק והזמן בין התחנות";
                str += "\n";
                str += ex.Message;
                MessageBox.Show(str);
            }
            catch
            {
                MessageBox.Show("משהו השתבש נסה שנית");
            }

        }


    }
}
//  Color="#FFCAF7EC"