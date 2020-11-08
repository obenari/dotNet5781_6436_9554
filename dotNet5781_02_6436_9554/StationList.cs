using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    /// <summary>
    /// this class contain all the station in our program
    /// </summary>
     public class StationList 
    {
        List<BusStation> busStop;
        public List<BusStation> BusStop {
            get
            {
                return busStop;
            } 
            set
            {
                busStop = value;
            }
        }
        public StationList()
        {
            busStop = new List<BusStation>();
        }
        /// <summary>
        /// this indexer retirn or set according to index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BusStation this[int index]
        {
            get
            {
                if (index < 0 || index >= BusStop.Count())
                    throw new IndexOutOfRangeException("the index is not valid");
                return BusStop[index];
            }
            set
            {
                BusStop[index] = value;
            }
        }
        
       
        /// <summary>
        /// this func check if the required station is exist
        /// </summary>
        /// <param name="num">the number of station</param>
        /// <returns></returns>
        public bool isExist(int num)
        {
            foreach (BusStation item in busStop)
            {
                if (item.BusStationKey == num)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// this function return the index of the required station, if the station is not exist, return -1
        /// </summary>
        /// <param name="num">number of station</param>
        /// <returns></returns>
        public int index(int num)
        {
            int i = 0;
            foreach (BusStation item in busStop)
            {
                if (item.BusStationKey == num)
                    break;
                i++;
            }
            if (i == busStop.Count)//if num is not exist
                return -1;
            return i;
        }
        /// <summary>
        /// add a new bus station to the list
        /// </summary>
        /// <param name="s">the new station</param>
        public void add(BusStation s)
        {
            busStop.Add(s);
        }
       
    }
}
