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
        IBL bl = BLFactory.GetBL();
        ObservableCollection<BO.Station> StationCollection;
     //   List<BO.Station> item;
        public StationWindow()
        {
            InitializeComponent();
          
            //item= bl.GetAllStations().ToList();
            //  StationCollection = item;
         //   StationCollection =(ObservableCollection< BO.Station >)bl.GetAllStations();
            StationCollection = new ObservableCollection<BO.Station>(bl.GetAllStations().ToList());
            this.stationDataGrid.DataContext = StationCollection;
        }

      
    }
}
