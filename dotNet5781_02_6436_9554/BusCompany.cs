using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    class BusCompany : IEnumerable
    {
        private List<BusLine> busses;
        

        public List<BusLine> Busses
        {
            get {
                List<BusLine> temp = new List<BusLine>(busses);
                return temp; }
            set { busses = value; }
        }
        public BusCompany()
        {
            busses= new List<BusLine>();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return busses.GetEnumerator();
        }
        public BusLine this[int index]
        {
            get 
            {
                if (index < 0 || index >= Busses.Count)
                    throw new IndexOutOfRangeException("the index is not valid");
                return busses[index];
            }
            set
            {
                busses[index] = value;
            }
        }
        /// <summary>
        /// this function is check how many times the bus is exist in the system
        /// </summary>
        /// <param name="numBus">the number of the requird bus </param>
        /// <returns></returns>
        public int timeBusExist(int numBus)
        {
            int sum=0;
            foreach (BusLine item in busses)
            {
                if (item.BusNumber == numBus)
                    sum++;
            }
            return sum;
        }
      /// <summary>
      /// this function return the index of the first bus in the list
      /// </summary>
      /// <param name="num"> bus number</param>
      /// <returns></returns>
        public int busIndex(int num)
        {
            int i = 0;
            foreach (BusLine item in busses)
            {
                if (item.BusNumber == num)
                    break;
                i++;
            }
            if (i == busses.Count)//if the bus is not exist
                throw new KeyNotFoundException(string.Format("the bus {0} is not exist", num));
            return i;
        }
        /// <summary>
        /// this function return the index of the bus with the required bus number and first station number
        /// </summary>
        /// <param name="num">bus number</param>
        /// <param name="firstStation"> the first station of the bus</param>
        /// <returns></returns>
        public int busIndex(int num, int firstStation)
        {
            int i = 0;
            foreach (BusLine item in busses)
            {
                if (item.BusNumber == num&&item.FirstStation.Station.BusStationKey==firstStation)
                    break;
                i++;
            }
            if (i == busses.Count)//if the bus is not exist
                throw new KeyNotFoundException(string.Format("the bus {0} with the first station {1} is not exist", num,firstStation));
            return i;
        }
        public void addBus(BusLine bus)
        {
            int temp = timeBusExist(bus.BusNumber);
            if (temp == 2)//if the bus is exist twice
                throw new DuplicateNameException(string.Format("The bus{0} is already exist twice", bus.BusNumber));
            if(temp==1)
            {
                int i = busIndex(bus.BusNumber);
                if (busses[i].FirstStation != bus.LastStation || busses[i].LastStation != bus.FirstStation)
                {
                    string str = "the bus " + bus.BusNumber + " is exist one time, but the last or the first station are not match to reverse route ";
                    throw new ArgumentException(str);
                }
                //if we came here, the first and last stations are match to the reverse route
                busses.Add(bus);
            }
            if (temp == 0)//the bus number is not exist in the system
                busses.Add(bus);
        }
        public void deleteBus(int numFirstStation, int busNumber)
        {
            int i = busIndex(busNumber, numFirstStation);
            busses.Remove(busses[i]);
        }
        /// <summary>
        /// this function return a  sort list according to the total teravel time 
        /// </summary>
        /// <returns></returns>
        public List<BusLine> sortList()
        {
            busses.Sort();
            List<BusLine> lst = new List<BusLine>(busses);//lst is a copy of the sort list in order to protect the bus list
            return lst;
        }
        /// <summary>
        /// this function return a list with all the busses that passing through the required station
        /// </summary>
        public List<int> busesPassing(int stationNumber)
        {
            List<int> lst = new List<int>();
            foreach (BusLine item in busses)
            {
                if (item.stationIsExist(stationNumber))
                    lst.Add(item.BusNumber);
            }
            if (lst.Count == 0)
                throw new KeyNotFoundException(string.Format("there is no busses that passing through the station {0}", stationNumber));
            return lst;
        }
    }
}
