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
        public LineWindow(IBL MyBL)
        {
            InitializeComponent();
            bl=MyBL;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));

            LinesCollection =new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(BO.Areas.Jerusalem).ToList().ConvertAll(line=> Adapter.POBOAdapter(line)));
            lineDataGrid.DataContext = LinesCollection;

        }

        private void areaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           BO.Areas area = (BO.Areas)areaComboBox.SelectedItem;
            LinesCollection= new ObservableCollection<PO.BusLine>(bl.GetAllLinesByArea(area).
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
                    PO.BusLine lineToRemove= lineDataGrid.SelectedItem as BusLine;
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
                catch
                {

                }
            }
        }
    }
}
//  Color="#FFCAF7EC"