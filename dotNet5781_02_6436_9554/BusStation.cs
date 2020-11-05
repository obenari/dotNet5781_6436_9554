﻿using System;
using System.Collections.Generic;
using System.Data;
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
    /// 
    public class BusStation
    {
        // string address;

       static List<int> keys = new List<int>();
        private int busStationKey;

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

        private double latitude;

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
        private double longitude;

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

     
        public string Address { get; set; }
        public BusStation(int num, string address="")
        {
            if (keys.Contains(num))
                // throw new ArgumentOutOfRangeException(string.Format("the key {0} is already exist.", num));
                throw new DuplicateNameException(string.Format("the key {0} is already exist.", num));
       
            this.BusStationKey = num;
            this.Address = address;
            keys.Add(num);
            Random r = new Random(DateTime.Now.Millisecond);
            Latitude = r.NextDouble() + r.Next(31, 34);
            Longitude = r.NextDouble()+r.Next(34,36);

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