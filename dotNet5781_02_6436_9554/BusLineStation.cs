using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
   public class BusLineStation// : BusStation
    {
 
        public TimeSpan TravelTime { get; set; }
        private double distance;
        // BusStation station;
        private BusStation station;

        public BusStation Station
        {
            get { return station; }
            set { station = value; }//***************************************
        }

        public double Distance
        {
            get { return distance; }
            set {
                if (value > 0)
                    distance = value;
                else
                    throw new ArgumentOutOfRangeException(string.Format("distance should be positive."));
            }
        }

        public BusLineStation( int num,string address, TimeSpan t, double d)//:base(num,address)
        {
            TravelTime = t;
            Distance = d;
        }
        public override string ToString()
        {
            string str = station.ToString();
            str += string.Format("Distance from the last station: , {0}, Time from the last station: ,{1}.", Distance, TravelTime);
            str += string.Format("\n");
            return str;
        }


    }
}
