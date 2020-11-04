using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    enum Area { General, North, South, Center, Jerusalem, }
    class BusLine
    {
        //int busNumber;
        BusLineStation firstStation;
        BusLineStation lastStation;
        Area busArea;
        List<BusLineStation> stations = new List<BusLineStation>();
        private int busNumber;

        public int BusNumber
        {
            get { return busNumber; }
            set
            {
                if (value > 0)
                    busNumber = value;
                else
                    throw new ArgumentOutOfRangeException(string.Format("bus number should be positive."));
            }
        }

        //public int BusNumber { get; set
        //    {

        //    } }
        public BusLineStation FirstStation { get; private set; }
        public BusLineStation LastStation { get; private set; }
        public Area BusArea { get; set; }
        public BusLine(int line, Area a)
        {
            BusNumber = line;
            BusArea = a;
        }
        public override IEnumerator<string> ToString()//*********************************
        {
            Console.WriteLine();
            yield return "Bus line :" + busNumber + ", area :" + busArea;
        }
        //public bool stationExist(string name)
        //{
        //    foreach (BusLineStation item in stations)
        //    {
        //        if (item.BusStationKey == name)
        //            return true;
        //    }
        //    return false;
        //}
        public void addfirst(BusLineStation station)//*******************************
        {

            stations.Insert(0, station);
            firstStation = stations[0];
            
        }
        public void addToEnd(BusLineStation station)//*******************************
        {
            stations.Add(station);
            lastStation = stations[stations.Count-1];
        }
        public void add(int index, BusLineStation station)//*******************************
        {
            if(index<0||index>stations.Count())
            {
                throw new IndexOutOfRangeException("The index is not valid.");
            }
            if (index < stations.Count())//if station is not the last station, we need to update the next station field
            {
                Console.WriteLine("enter a new distance from the next station");
                int n;
                bool succes = int.TryParse(Console.ReadLine(), out n);
                if(!succes)
                {
                    throw new FormatException("the distance is not legal");
                }
                Console.WriteLine("enter a new time to the next station");
                TimeSpan t;
                succes = TimeSpan.TryParse(Console.ReadLine(), out t);
                if (!succes)
                {
                    throw new FormatException("the time is not legal");
                }
                //now update the next station
                stations[index + 1].Distance = n;
                stations[index + 1].TravelTime = t;
            }
            stations.Add(station);
            lastStation = stations[stations.Count - 1];
          
        }

    }
}
