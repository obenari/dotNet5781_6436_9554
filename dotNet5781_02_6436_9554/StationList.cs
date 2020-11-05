using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
     public class StationList
    {
        List<BusStation> busStop = new List<BusStation>();
        public List<BusStation> BusStop { get; private set; }
        public BusStation this[int index]
        {
            get
            {
                if (index < 0 || index >= BusStop.Count)
                    throw new IndexOutOfRangeException("the index is not valid");
                return BusStop[index];
            }
            set
            {
                BusStop[index] = value;
            }
        }
        public bool isExist(int num)
        {
            foreach (BusStation item in busStop)
            {
                if (item.BusStationKey == num)
                    return true;
            }
            return false;
        }
        public void add(BusStation s)
        {
            busStop.Add(s);
        }
    }
}
