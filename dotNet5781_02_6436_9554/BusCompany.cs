﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
   public class BusCompany : IEnumerable
    {
        private List<BusLine> busses;
        

        public List<BusLine> Busses
        {
            get {
               // List<BusLine> temp = new List<BusLine>(busses);
                return busses; }
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
        //public BusLine this[int index]
        //{
        //    get 
        //    {
        //        if (index < 0 || index >= Busses.Count)
        //            throw new IndexOutOfRangeException("the index is not valid");
        //        return busses[index];
        //    }
        //    set
        //    {
        //        busses[index] = value;
        //    }
        //}
        public BusLine this[int num]
        {
            get
            {
                int i = 0;
                foreach (var item in busses)
                {
                    if (item.BusNumber == num)
                        break;
                    i++;
                }
                if (i == busses.Count)
                    throw new KeyNotFoundException(string.Format("the bus number {0} is not exist", num));
                return busses[i];
            }
            set
            {
                int i = 0;
                foreach (var item in busses)
                {
                    if (item.BusNumber == num)
                        break;
                    i++;
                }
                if (i == busses.Count)
                    throw new KeyNotFoundException(string.Format("the bus number {0} is not exist", num));
                busses[i] = value;
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
        public void deleteBus(int numFirstStation, int busNumber)//*************************8
        {
            int i = busIndex(busNumber, numFirstStation);//if the bus is not exist an exception will throwen
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
        /// <summary>
        /// this function return the the sum of the busses that passing through the required station
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        public int totalBusses(int stationNum)
        {
            int sum = 0;
            foreach (BusLine item in busses)
            {
                if (item.stationIsExist(stationNum))
                    sum++;
            }
            return sum;
        }
        public void printAllPath(int num1, int num2)
        {
            List<BusLine> lst = new List<BusLine>();//this list contain all the sub bus that passing through the two stations
            foreach (BusLine item in busses)
            {
                if (item.stationIsExist(num1) && item.stationIsExist(num2))
                    lst.Add(item.subPathe(num1, num2));
            }
            lst.Sort();
            if (lst.Count == 0)
                throw new KeyNotFoundException("There is no pathes between the stations");
            foreach (BusLine item in lst)
            {
                Console.WriteLine(item);
            }
        }
        public void deleteStation(int line,int first,int sDelete)
        {
            int i = busIndex(line, first);//if the bus is not exist, an exception will thrown
            busses[i].deleteStation(sDelete);//this row call to the func deleteStation in BusLine class
        }
    }
}
