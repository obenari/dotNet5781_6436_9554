using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    /// <summary>
    /// the explanation about this class is in BO
    /// </summary>
  public  class BusLine : DependencyObject
    {
        // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaProperty =
            DependencyProperty.Register("Area", typeof(BO.Areas), typeof(BusLine), new PropertyMetadata(0));
        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(BusLine), new PropertyMetadata(0));
        // Using a DependencyProperty as the backing store for LineNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineNumberProperty =
            DependencyProperty.Register("LineNumber", typeof(int), typeof(BusLine), new PropertyMetadata(0));
        // Using a DependencyProperty as the backing store for LastStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastStationProperty =
            DependencyProperty.Register("LastStation", typeof(string), typeof(BusLine), new PropertyMetadata(0));
        // Using a DependencyProperty as the backing store for FirstSattion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstStationProperty =
            DependencyProperty.Register("FirstStation", typeof(string), typeof(BusLine), new PropertyMetadata(0));
        // Using a DependencyProperty as the backing store for Stations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsProperty =
            DependencyProperty.Register("Stations", typeof(ObservableCollection<int>), typeof(BusLine), new PropertyMetadata(0));

        public int LineNumber
        {
            get { return (int)GetValue(LineNumberProperty); }
            set { SetValue(LineNumberProperty, value); }
        }

        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public BO.Areas Area
        {
            get { return (BO.Areas)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }


        public ObservableCollection<int> Stations
        {
            get { return (ObservableCollection<int>)GetValue(StationsProperty); }
            set { SetValue(StationsProperty, value); }
        }


        public string FirstStation
        {
            get { return (string)GetValue(FirstStationProperty); }
            set { SetValue(FirstStationProperty, value); }
        }


        public string LastStation
        {
            get { return (string)GetValue(LastStationProperty); }
            set { SetValue(LastStationProperty, value); }
        }
        public override string ToString()
        {
            return string.Format($"Line: {LineNumber}, from {FirstStation} to {LastStation} in {Area}");
        }






    }
}
