using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;


namespace PO
{
 public   class LineStation:DependencyObject
    {
        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(TimeSpan), typeof(LineStation));
        // Using a DependencyProperty as the backing store for stationCode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationCodeProperty =
            DependencyProperty.Register("stationCode", typeof(int), typeof(LineStation));
        // Using a DependencyProperty as the backing store for stationName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationNameProperty =
            DependencyProperty.Register("stationName", typeof(string), typeof(LineStation));

        //// Using a DependencyProperty as the backing store for LineStationIndex.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty LineStationIndexProperty =
        //    DependencyProperty.Register("LineStationIndex", typeof(int), typeof(LineStation));
        // Using a DependencyProperty as the backing store for Distance.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register("Distance", typeof(double), typeof(LineStation));

        /// <summary>
        /// the code of the physical station
        /// </summary> 
        public int StationCode
        {
            get { return (int)GetValue(stationCodeProperty); }
            set { SetValue(stationCodeProperty, value); }
        }
        ///// <summary>
        ///// the name of the physical station//************************************8
        ///// </summary>
        ///
        public string StationName
        {
            get { return (string)GetValue(stationNameProperty); }
            set { SetValue(stationNameProperty, value); }
        }
        ///// <summary>
        ///// the index of the station in the spacific line
        ///// </summary>
        ///// 
        //public int LineStationIndex
        //{
        //    get { return (int)GetValue(LineStationIndexProperty); }
        //    set { SetValue(LineStationIndexProperty, value); }
        //}
        /// <summary>
        /// the distance from  the two last station
        /// </summary>
        public double Distance
        {
            get { return (double)GetValue(DistanceProperty); }
            set { SetValue(DistanceProperty, value); }
        }
        /// <summary>
        /// the average travel time from  the last station
        /// </summary>
        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        

    }
}
