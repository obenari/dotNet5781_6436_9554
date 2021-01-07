using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO//maby be deleted/***********************************
{
    public class InformationForStation: DependencyObject
    {
        // Using a DependencyProperty as the backing store for LineNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineNumberProperty =
            DependencyProperty.Register("LineNumber", typeof(int), typeof(BusLine)/*, new PropertyMetadata(0)*/);
        // Using a DependencyProperty as the backing store for LastStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastStationProperty =
            DependencyProperty.Register("LastStation", typeof(string), typeof(BusLine)/*, new PropertyMetadata(0)*/);
        // Using a DependencyProperty as the backing store for FirstSattion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstStationProperty =
            DependencyProperty.Register("FirstStation", typeof(string), typeof(BusLine)/*, new PropertyMetadata(0)*/);
        // Using a DependencyProperty as the backing store for NextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextStationProperty =
            DependencyProperty.Register("NextStation", typeof(string), typeof(InformationForStation));


        public int LineNumber
        {
            get { return (int)GetValue(LineNumberProperty); }
            set { SetValue(LineNumberProperty, value); }
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


        public string NextStation
        {
            get { return (string)GetValue(NextStationProperty); }
            set { SetValue(NextStationProperty, value); }
        }

    
    }
}
