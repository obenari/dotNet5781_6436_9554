using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    // public class Point
    //{
    //    float x;
    //    float y;
    //    public float X { get=>x; set=>x=value; }
    //    public float Y { get => y; set => y = value; }
    //     public Point(float x, float y)
    //    {
    //        this.x = x;
    //        this.y = y;
    //    }

    //}
    /// <summary>
    /// This class include data about the addrass and station number of bus station
    /// </summary>
    public class BusStation
    {
       // string address;


        public string BusStationKey { get; }//there is no set feild because it imposibble to update this feild
        public double Latitude { get; }//there is no set feild because it imposibble to update this feild
        public double Longitude { get; }
        public string Address { get; set; }
        public BusStation( ref string name, string address="")
        {
            this.BusStationKey = name;
            this.Address = address;
            Random r = new Random(DateTime.Now.Millisecond);
            // float x = (float)r.NextDouble(31,34);
            //   float y= (float)r.NextDouble(31, 34);
            // Latitude = new Point(x, y);
            Latitude = r.NextDouble() + r.Next(31, 34);
            Longitude = r.NextDouble()+r.Next(34,36);

        }
        public override string ToString()
        {
            return "Bus Station Code :" + BusStationKey + ", " + Longitude + "N" + ", " + Latitude + "E";
        }

    }
}
