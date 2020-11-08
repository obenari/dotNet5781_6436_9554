using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    
    /// <summary>
    /// This class include data about the addrass and station number of bus station
    /// </summary>
    /// 
    public class BusStation
    {

       static List<int> keys = new List<int>();
        private int busStationKey;
        private double longitude;
        private double latitude;
        public string Address { get; set; }

        public int BusStationKey
        {
            get { return busStationKey; }
            set {
                if(value>0&&value<1000000)
                {
                    busStationKey = value;
                   
                }
                else
                {
                    throw new ArgumentException(string.Format("{0} is not a valid key number", value));
                }
                }
        }


        public double Latitude
        {
            get { return latitude; }
            set {
                if (value <= 90 && value >= -90)
                { latitude = value; }
                else
                {
                    throw new ArgumentOutOfRangeException(string.Format("{0} should be between -90 and 90",value));
                }
            }
        }
        

        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (value <= 180 && value >= -180)
                { longitude = value; }
                else
                {
                    throw new ArgumentOutOfRangeException(string.Format("{0} should be between -180 and 180", value));
                }
            }
        }

     /// <summary>
     /// the ctor get only the number of the station and put a random value in the other field
     /// </summary>
     /// <param name="num"></param>
     /// <param name="address"></param>
        public BusStation(int num, string address="")
        {
            if (keys.Contains(num))
                throw new DuplicateNameException(string.Format("the key {0} is already exist.", num));
       
            this.BusStationKey = num;
            this.Address = address;
            keys.Add(num);
            Random r = new Random(DateTime.Now.Millisecond);
            Latitude = r.NextDouble() * (33.3 - 31) + 31;
            Longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;

        }

        /// <summary>
        /// the ctor get all the data from the user
        /// </summary>
        /// <param name="num"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="address"></param>
        public BusStation(int num,double lat,double lon, string address = "")
        {
            if (keys.Count() > 0 && keys.Contains(num))
                throw new DuplicateNameException(string.Format("the key {0} is already exist.", num));

            this.BusStationKey = num;
            this.Address = address;
            keys.Add(num);
            Latitude = lat;
            Longitude = lon;

        }

        public BusStation() { }
        public override string ToString()
        {
            string result = "busStation Code: " + busStationKey;
            result += string.Format(",{0}°{1} {2}°{3}",
                Math.Abs(Latitude), (Latitude > 0) ? "N" : "s",
                Math.Abs(Longitude), (Longitude > 0) ? "E" : "W");
            result += string.Format("\n");
            return result;
        }

    }
}
