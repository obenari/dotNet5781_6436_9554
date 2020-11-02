using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    enum Area { General,North,South,Center,Jerusalem,}
    class BusLine
    {
        int busNumber;
        BusLineStation firstStation;
        BusLineStation lastStation;
        Area busArea;
        List<BusLineStation> stations;
        public int BusNumber { get; set; }
        public BusLineStation FirstStation { get; set; }
        public BusLineStation LastStation { get; set; }
        public Area BusArea { get; set; }
        public BusLine(int line,Area a)
        {
            busNumber = line;
            busArea = a;
            stations = new List<BusLineStation>();
        }
        public override string ToString()//*********************************
        {
            Console.WriteLine();
            return "Bus line :" + busNumber + ", area :" + busArea;
        }
        public bool stationExist(string name)
        {
            foreach (BusLineStation item in stations)
            {
                if (item.BusStationKey == name)
                    return true;
            }
            return false;
        }
        public void addStation()//*******************************
        {
            Console.WriteLine("please enter a number of bus station");
            string number = Console.ReadLine();
            if (stationExist(number))
                throw "   ";
            Console.WriteLine("please enter an area");
            bool 
        }
    }
}
