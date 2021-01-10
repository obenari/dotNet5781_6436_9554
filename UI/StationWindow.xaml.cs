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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using BLAPI;
using PO;
namespace UI
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBL bl;
        ObservableCollection<PO.Station> StationCollection;
     //   List<BO.Station> item;
        public StationWindow(IBL MyBL)
        {
            InitializeComponent();
            bl = MyBL;
            //convert all the BO stations to PO station, and put in  ObservableCollection
            StationCollection = new ObservableCollection<PO.Station>(bl.GetAllStations().ToList().ConvertAll(stn=> Adapter.POBOAdapter(stn)));
            this.stationDataGrid.DataContext = StationCollection;
            lvLines.DataContext = StationCollection[0].ListLines;
            lineGrid.DataContext = StationCollection[0];
        }

        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double lon = double.Parse(txtLongintude.Text);
                double lat = double.Parse(txtLatitude.Text);
                BO.Station newStation = new BO.Station
                {
                    Name = txtName.Text,
                    Longitude = lon,
                    Latitude = lat
                };
                bl.AddStation(newStation);
                PO.Station station = Adapter.POBOAdapter(newStation);
                StationCollection.Add(station);
            }
            catch(BO.OutOfLatitudeIsraelLimitException ex)
            {
                MessageBox.Show("הקו רוחב חורג מגבולות ישראל");
                txtLatitude.Text = "";
                btnAddStation.IsEnabled = false;

            }
            catch (BO.OutOfLongitudeIsraelLimitException ex)
            {
                MessageBox.Show("הקו אורך חורג מגבולות ישראל");
                txtLongintude.Text = "";
                btnAddStation.IsEnabled = false;

            }
            catch (BO.DuplicateStationException ex)//***************************
            {
                MessageBox.Show("התחנה כבר קיימת במערכת");
                btnAddStation.IsEnabled = false;
                txtLongintude.Text = "";
                txtLatitude.Text = "";
                txtName.Text = "";


            }
            catch(Exception ex)
            {
                MessageBox.Show("הנתונים שהוזנו אינם חוקיים");
                MessageBox.Show(ex.ToString());

            }
        }
        /// <summary>
        /// the button add is enable only when all the details are not empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void key_up_btnIsenabled(object sender, KeyEventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtLongintude.Text)) && !(string.IsNullOrEmpty(txtLatitude.Text))
                && !(string.IsNullOrEmpty(txtName.Text)))
            {
                btnAddStation.IsEnabled = true;
            }
            else
            {
                btnAddStation.IsEnabled = false;

            }
        }
        /// <summary>
        /// make the grid of add station visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            lineGrid.Visibility = Visibility.Collapsed;
            addGrid.Visibility = Visibility.Visible;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            addGrid.Visibility = Visibility.Collapsed;
            lineGrid.Visibility = Visibility.Visible;

        }

        private void stationDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Station station = stationDataGrid.SelectedItem as Station;
            lineGrid.DataContext = station;
            lvLines.DataContext = station.ListLines;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(stationDataGrid.SelectedItem==null)
            {
                MessageBox.Show("לא נבחרה תחנה");
                return;
            }
            string nameStation = (stationDataGrid.SelectedItem as Station).Name;
            //MessageBox.Show(string.Format("?התחנה  " + nameStation + " עומדת להימחק אתה בטוח")," ggg", MessageBoxButtons.YesNo);

            if (MessageBox.Show(string.Format("?התחנה  " + nameStation + " עומדת להימחק אתה בטוח"), "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    PO.Station stationToRemove = stationDataGrid.SelectedItem as Station;
                    bl.DeleteStation(stationToRemove.Code);
                    //after station is deleted, there is information in the ObservableCollection, about lines that passing by stations that is not updeted,
                    //so we update the ObservableCollection
                    StationCollection = new ObservableCollection<PO.Station>(bl.GetAllStations().ToList().ConvertAll(s => Adapter.POBOAdapter(s)));
                    stationDataGrid.DataContext = StationCollection;
                    //StationCollection.Remove(stationToRemove);
                    if (lineGrid.DataContext == stationToRemove)
                    {
                        if (StationCollection == null)
                        {
                            this.lineGrid.DataContext = null;
                            this.lvLines.DataContext = null;

                        }
                        else
                        {
                            lineGrid.DataContext = StationCollection[0];
                            this.lvLines.DataContext = StationCollection[0].ListLines;
                        }
                    }
                }
                catch (Exception ex)//in order the program will not fail due to exception that the engeneering not thougt about it
                {
                    MessageBox.Show("משהו השתבש נסה שנית");
                }
            }
          
        }
    }
}
