using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    public enum Area { General, North, South, Center, Jerusalem, }
    public class BusLine: IComparable
    {
        
        private BusLineStation firstStation;
        private BusLineStation lastStation;
        private Area busArea=new Area();
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

        
        public BusLineStation FirstStation { get; private set; }
        public BusLineStation LastStation { get; private set; }
        // public Area BusArea { get; set; }
        //public BusLine(int line, Area a)
        //{
        //    BusNumber = line;
        //    BusArea = a;
        //}
        

        public Area BusArea
        {
            get { return busArea; }
            set { busArea = value; }
        }

        public BusLine(int line, Area a,BusLineStation first,BusLineStation last)
        {
            BusNumber = line;
            BusArea = a;
            FirstStation = first;
            LastStation = last;
            stations.Add(first);
            stations.Add(last);
        }

        public override string ToString()//*********************************
        {
              
            string result= "Bus line :" + busNumber + ", area :" + busArea;
            foreach (BusLineStation item in stations)
            {
                result += string.Format(item.ToString());
            }
            return result;
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
       
            Console.WriteLine("enter a new distance from the next station");
            int n;
            bool succes = int.TryParse(Console.ReadLine(), out n);
            if (!succes||n<=0)
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
            //now insert to first place
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
            if (index == stations.Count())
                addToEnd(station);
            else
            {
                if (index == 0)
                    addfirst(station);
                else
                {
                    Console.WriteLine("enter a new distance from the next station");
                    int n;
                    bool succes = int.TryParse(Console.ReadLine(), out n);
                    if (!succes)
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
                    stations.Insert(index, station);
                    //now update the  station that will be after the new station
                    stations[index+1].Distance = n;
                    stations[index+1].TravelTime = t;
                    //stations.Insert(index, station);
                    //stations.Add(station);
                    //lastStation = stations[stations.Count - 1];
                }
            }
            //if (index < stations.Count())//if station is not the last station, we need to update the next station field
            //{
            //    Console.WriteLine("enter a new distance from the next station");
            //    int n;
            //    bool succes = int.TryParse(Console.ReadLine(), out n);
            //    if(!succes)
            //    {
            //        throw new FormatException("the distance is not legal");
            //    }
            //    Console.WriteLine("enter a new time to the next station");
            //    TimeSpan t;
            //    succes = TimeSpan.TryParse(Console.ReadLine(), out t);
            //    if (!succes)
            //    {
            //        throw new FormatException("the time is not legal");
            //    }
            //    //now update the next station
            //    stations[index + 1].Distance = n;
            //    stations[index + 1].TravelTime = t;
            //}
            //stations.Add(station);
            //lastStation = stations[stations.Count - 1];
          
        }
        public void deleteStation(int stationNumber)
        {
            
            
            int i = StationIndex(stationNumber);//if the required station is not exist, the function stationIndex throw an exception
            if (stations.Count == 2)
                throw new CannotDeletedException("It's impossible to delete a station if there are only two stations");
            if (i == 0)
            {
                FirstStation = stations[1];
            }
            if(i==stations.Count-1)
            {
                LastStation = stations[i - 1];
            }
            stations.Remove(stations[i]);//if the required station is found,remove her from the stations list
            

        }
        public bool stationIsExist(int stationNumber)
        {
           
            foreach (BusLineStation item in stations)
            {
                if (item.Station.BusStationKey == stationNumber)
                    return true;
            }
            //if we came here, the station is not exist
            return false;
        }
        public double distanceBetweenTheStations(int num1,int num2)
        {
            if(!stationIsExist(num1)||!stationIsExist(num2))//if the stations are not found
                throw new KeyNotFoundException("The station is not found.");
            //if we came here, both stations are found
            int i = StationIndex(num1);
            int j = StationIndex(num2);
            double distance=0;
            if(i<j)
            {
                i++;//we start to total the distance from the next station
                while(i!=j+1)
                {
                    distance += stations[i].Distance;
                }
                return distance;
            }
            //if we came here, it means that j<i

            j++;//we start to total the distance from the next station
            while (j != i + 1)
            {
                distance += stations[j].Distance;
            }
            return distance;

        }
        public TimeSpan travelTimeBetweenTheStations(int num1, int num2)
        {
            if (!stationIsExist(num1) || !stationIsExist(num2))//if the stations are not found
                throw new KeyNotFoundException("The station is not found.");
            //if we came here, both stations are found
            int i = StationIndex(num1);
            int j = StationIndex(num2);
            TimeSpan time = new TimeSpan(0,0,0);
            if (i < j)
            {
                i++;//we start to total the time from the next station
                while (i != j + 1)
                {
                    time += stations[i].TravelTime;
                }
                return time;
            }
            //if we came here, it means that j<i

            j++;//we start to total the time from the next station
            while (j != i + 1)
            {
                time += stations[j].TravelTime;
            }
            return time;

        }

        public int StationIndex(int stationNumber)
        {
            int i = 0;

           
            foreach (BusLineStation item in stations)
            {
                if (item.Station.BusStationKey == stationNumber)
                    break;
                i++;
            }
            if (i == stations.Count())//it means that the required station is not found
                throw new KeyNotFoundException(string.Format("The {0} station is not found.", stationNumber));
            return i;
        }
        /// <summary>
        /// this function return a sub path between the two stations
        /// </summary>
        /// <param name="num1">the number of the first station  </param>
        /// <param name="num">the number of the second station </param>
        /// <returns></returns>
        public BusLine subPathe(int num1, int num2)
        {
            if (num1 == num2)
                throw new ArgumentException("It's impossible  return the path of the same station");
            if (!stationIsExist(num1) || !stationIsExist(num2))//if the stations are not found
            {
                throw new KeyNotFoundException("The station is not found.");
            }
            //if we came here, both stations are found
            int i = StationIndex(num1);
            int j = StationIndex(num2);
            BusLine bus;
            if (i < j)
            {
               bus = new BusLine(BusNumber, BusArea, stations[i], stations[i+1]);
                i += 2;
                while (i <= j)
                {
                    bus.addToEnd(stations[i]);
                    i++;
                }
                return bus;
            }
            //if we came here, it means that i>j
            bus = new BusLine(BusNumber, BusArea, stations[j], stations[j + 1]);
            j += 2;
            while (j <= i)
            {
                bus.addToEnd(stations[j]);
                j++;
            }
            return bus;
        }
        public TimeSpan totalTime()
        {
            TimeSpan t = new TimeSpan(0, 0, 0);
            for(int i=1;i<stations.Count();i++)// the first station is not included in the total time
            {
                t += stations[i].TravelTime;
            }
            return t;
            //foreach (BusLineStation item in stations)
            //{
            //    t+=
            //}
        }
        public int CompareTo(object item)
        {
            BusLine bus = (BusLine)item;
            if (totalTime() < bus.totalTime())
                return -1;
            if (totalTime() > bus.totalTime())
                return 1;
            //if we here they are equal to each other
            return 0;
        }
    }
}
