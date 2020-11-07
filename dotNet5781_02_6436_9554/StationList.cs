using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
     public class StationList //: IEnumerable
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
        //public BusStation this[int stationNum]
        //{
        //    get
        //    {
        //        foreach (BusStation item in busStop)
        //        {
        //            if (item.BusStationKey == stationNum)
        //                return item;
        //        }
        //        //if we came here, the bus station is not found
        //        throw new KeyNotFoundException(string.Format("The station {0} , is not found", stationNum));
              
        //    }
        //    set
        //    {
        //        int i = 0;
        //        for(;i<busStop.Count;i++)
        //        {
        //            if (busStop[i].BusStationKey == stationNum)
        //                break;
        //        }
        //        if(i==busStop.Count)
        //            throw new KeyNotFoundException(string.Format("The station {0} , is not found", stationNum));
        //        busStop[i] = value;
        //    }
        //}

        public bool isExist(int num)
        {
            foreach (BusStation item in busStop)
            {
                if (item.BusStationKey == num)
                    return true;
            }
            return false;
        }
        public int index(int num)
        {
            int i = 0;
            foreach (BusStation item in busStop)
            {
                if (item.BusStationKey == num)
                    break;
                i++;
            }
            if (i == busStop.Count)
                return -1;
            return i;
        }
        public void add(BusStation s)
        {
            busStop.Add(s);
        }
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return busStop.GetEnumerator();
        //}
    }
}
