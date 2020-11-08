using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    /// <summary>
    /// this class contain a varible from BusStation type and a data about the last station
    /// </summary>
   public class BusLineStation
    {
 
        public TimeSpan TravelTime { get; set; }
        private double distance;
        private BusStation station;

        public BusStation Station
        {
            get { return station; }
            private  set { station = value; }
        }

        public double Distance
        {
            get { return distance; }
            set {
                if (value >= 0)
                    distance = value;
                else
                    throw new ArgumentOutOfRangeException(string.Format("distance should be positive."));
            }
        }
        /// <summary>
        /// this ctor dont get a BusStation as a varible, it create a new one
        /// </summary>
        /// <param name="num">station number </param>
        /// <param name="address"></param>
        /// <param name="t">The time it takes to travel from the previous station to the current one</param>
        /// <param name="d">Distance from previous station to current</param>
        public BusLineStation( int num, TimeSpan t, double d,double lut,double lon,string address="")
        {
            station = new BusStation(num, lut, lon,address);
            TravelTime = t;
            Distance = d;
        }
        /// <summary>
        /// this ctor get BusStation as a varible 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="address"></param>
        /// <param name="t"></param>
        /// <param name="d"></param>
        public BusLineStation(BusStation s, TimeSpan t, double d)
        {
            Station = s;
            TravelTime = t;
            Distance = d;
        }
        /// <summary>
        /// this ctor, get only a BusStation, and it put random value
        /// </summary>
        /// <param name="s"></param>
        public BusLineStation(BusStation s)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Station = s;
            TravelTime = new TimeSpan(r.Next(6),r.Next(60),r.Next(60));
            Distance = r.NextDouble()+r.Next(1000);
        }
        public override string ToString()
        {
            string str = station.ToString();
            str += string.Format("Distance from the last station:  {0} Time from the last station: {1}", Distance, TravelTime);
            str += string.Format("\n");
            return str;
        }


    }
}
