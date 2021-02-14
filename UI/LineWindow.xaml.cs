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
        /// <summary>
        /// 
        /// </summary>
        IBL bl;
        ObservableCollection<PO.BusLine> LinesCollection;
        ObservableCollection<PO.Station> StationCollection;
        public LineWindow(IBL MyBL)
        {
            InitializeComponent();
            bl = MyBL;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            areaComboBox.SelectedIndex = (int)BO.Areas.Jerusalem;
                       LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(BO.Areas.Jerusalem).ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;
            
        }
        /// <summary>
        /// when the user select a new area, this func show the lines of the request area 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void areaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Areas area = (BO.Areas)areaComboBox.SelectedItem;
            LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(area).
                ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;
            if(LinesCollection!=null||LinesCollection.Count>0)
               lvStation.ItemsSource = LinesCollection[0].Stations;

        }
        /// <summary>
        /// this func shows the data about the line that the user DoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lineDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.BusLine busLine = lineDataGrid.SelectedItem as PO.BusLine;
            this.stationGrid.DataContext = busLine;
            lvStation.ItemsSource = busLine.Stations;
        }
        /// <summary>
        /// this func delete the request line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    bl.DeleteLine(lineToRemove.Id);
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
        /// <summary>
        /// this func open UpdateLineWindow, to update the request line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            BO.Areas area = (BO.Areas)areaComboBox.SelectedItem;
            LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(area).
                ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;
        }
        /// <summary>
        /// this func open an addLineWindow to add a new line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddLineWindow addLineWindow = new AddLineWindow(bl);
            addLineWindow.ShowDialog();
            //update  this window, to show the rellevant data
            BO.Areas area = (BO.Areas)areaComboBox.SelectedItem;
            LinesCollection = new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(area).
                ToList().ConvertAll(line => Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;
        }
        /// <summary>
        ///  The method allows only numbers to be entered
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
