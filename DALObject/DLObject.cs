using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;
using System.Reflection;

namespace DL
{
    internal class DLObject : IDL
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        // The public Instance property to use 
        public static DLObject Instance { get { return instance; } }
        // Explicit static constructor to ensure instance initialization
        // is done just before first usage
        static DLObject() { }
        DLObject() { } // default => private
        #endregion

        public IEnumerable<DO.Bus> GetAllBusses()
        {
            return from Bus in DataSource.ListBusses
                   select Bus.Clone();
        }

        public IEnumerable<DO.Bus> GetAllBussesBy(Predicate<DO.Bus> predicate)
        {

            return from Bus in DataSource.ListBusses
                   where predicate(Bus)
                   select Bus.Clone();
        }

        public Bus GetBus(int License)
        {
            DO.Bus bus = DataSource.ListBusses.Find(item => item.License == License);
            if (bus == null)
                throw new KeyNotFoundException("");
            return bus;
        }

        public void AddBus(Bus bus)
        {
         
            if (DataSource.ListBusses.Find(item => item.License == bus.License) != null)
                throw new Exception();
            DataSource.ListBusses.Add(bus);
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int license, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int license)
        {
            throw new NotImplementedException();
        }


    }
}
