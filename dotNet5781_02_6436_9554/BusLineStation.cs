using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
   public class BusLineStation : BusStation
    {
        double distance;
        int travelTime;
        public int TravelTime { get; set; }
        public double Distance { get; set; }
        public BusLineStation(ref string name,string address,int t, double d):base(ref name,address)
        {
            travelTime = t;
            distance = d;
        }



    }
}
