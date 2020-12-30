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
        #region bus
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
                throw new BusNotFoundException(License);
            return bus.Clone();
        }

        public void AddBus(Bus bus)
        {

            if (DataSource.ListBusses.Find(item => item.License == bus.License) != null)//check if the new bus is already exist
                throw new BusNotFoundException(bus.License);
            DataSource.ListBusses.Add(bus.Clone());
        }

        public void UpdateBus(Bus bus)
        {
            DO.Bus oldBus = DataSource.ListBusses.Find(b => b.License == bus.License);

            if (oldBus != null)//if the bus is exist
            {
                DataSource.ListBusses.Remove(oldBus);
                DataSource.ListBusses.Add(bus.Clone());
            }
            else
                throw new BusNotFoundException(bus.License);
        }

        public void UpdateBus(int license, Action<Bus> update)
        {
            DO.Bus oldBus = DataSource.ListBusses.Find(b => b.License == license);

            if (oldBus != null)//if the bus is exist
            {
                update(oldBus);
            }
            else
                throw new BusNotFoundException(license);
        }

        public void DeleteBus(int license)
        {
            DO.Bus oldBus = DataSource.ListBusses.Find(b => b.License == license);

            if (oldBus != null)//if the bus is exist
            {
                DataSource.ListBusses.Remove(oldBus);
            }
            else
                throw new BusNotFoundException(license);
        }
        #endregion
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   select line.Clone();

        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            return from line in DataSource.ListLines
                   where predicate(line)
                   select line.Clone();
        }
        public Line GetLine(int id)
        {
            DO.Line line = DataSource.ListLines.Find(item => item.Id == id);
            if (line == null)
                throw new BusLineNotFoundException(id);
            return line.Clone();
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.Find(item => item.Id == line.Id) != null)//check if the new bus is already exist
                throw new BusLineNotFoundException(line.Id);
            DataSource.ListLines.Add(line.Clone());
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line oldLine = DataSource.ListLines.Find(l => l.Id== line.Id);

            if (oldLine != null)//if the bus is exist
            {
                DataSource.ListLines.Remove(oldLine);
                DataSource.ListLines.Add(line.Clone());
            }
            else
                throw new BusLineNotFoundException(line.Id);
        }
        public void UpdateLine(int id, Action<DO.Line> update)
        {
            DO.Line oldLine = DataSource.ListLines.Find(l => l.Id == id);

            if (oldLine != null)//if the bus is exist
            {
                update(oldLine);
            }
            else
                throw new BusLineNotFoundException(id);
        }
        public void DeleteLine(int id)
        {
            DO.Line oldLine = DataSource.ListLines.Find(l => l.Id == id);

            if (oldLine != null)//if the bus is exist
            {
                DataSource.ListLines.Remove(oldLine);
            }
            else
                throw new BusLineNotFoundException(id);
        }
        #endregion
        #region LineStation
       public IEnumerable<LineStation> GetAllLineStations()
        {
            return from lineStation in DataSource.ListLineStations
                   select lineStation.Clone();
        }
        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> predicate)
        {

            return from lineStation in DataSource.ListLineStations
                   where predicate(lineStation)
                   select lineStation.Clone();
        }
        public LineStation GetLineStation(int line, int numSation)
        {
            DO.LineStation lineStation = DataSource.ListLineStations.Find(item => item.LineId == line&&item.stationCode==numSation);
            if (lineStation == null)
                throw new LineStationNotFoundException(numSation,line);
            return lineStation.Clone();
        }
        public void AddLineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations
                .Find(item => item.stationCode == lineStation.stationCode&&item.LineId== lineStation.LineId) != null)//check if the new bus is already exist
                throw new LineStationNotFoundException(lineStation.stationCode,lineStation.LineId);
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            DO.LineStation oldLineStation = DataSource.ListLineStations
                .Find(item => item.stationCode == lineStation.stationCode && item.LineId == lineStation.LineId);

            if (oldLineStation != null)//if the bus is exist
            {
                DataSource.ListLineStations.Remove(oldLineStation);
                DataSource.ListLineStations.Add(lineStation.Clone());
            }
            else
                throw new LineStationNotFoundException(lineStation.stationCode, lineStation.LineId);
        }
        public void UpdateLineStation(int line, int numSation, Action<LineStation> update)
        {

            DO.LineStation oldLineStation = DataSource.ListLineStations
                .Find(item => item.stationCode == numSation && item.LineId == line);

            if (oldLineStation != null)//if the bus is exist
            {
                update(oldLineStation);
            }
            else
                throw new LineStationNotFoundException(numSation, line);
        }
        public void DeleteLineStation(int line, int numSation)
        {

            DO.LineStation oldLineStation = DataSource.ListLineStations
                .Find(item => item.stationCode == numSation && item.LineId == line);

            if (oldLineStation != null)//if the bus is exist
            {
                DataSource.ListLineStations.Remove(oldLineStation);
            }
            else
                throw new LineStationNotFoundException(numSation, line);
        }
        #endregion
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                   select station.Clone();

        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
        }
        public Station GetStation(int id)
        {
            DO.Station station = DataSource.ListStations.Find(item => item.Code == id);
            if (station == null)
                throw new StationNotFoundException(id);
            return station.Clone();
        }
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.Find(item => item.Code == station.Code) != null)//check if the new station is already exist
                throw new StationNotFoundException(station.Code);
            DataSource.ListStations.Add(station.Clone());
        }
        public void UpdateStation(DO.Station station)
        {
            DO.Station oldStation = DataSource.ListStations.Find(s => s.Code == station.Code);

            if (oldStation != null)//if the station is exist
            {
                DataSource.ListStations.Remove(oldStation);
                DataSource.ListStations.Add(station.Clone());
            }
            else
                throw new StationNotFoundException(station.Code);
        }
        public void UpdateStation(int code, Action<DO.Station> update)
        {
            DO.Station oldStation = DataSource.ListStations.Find(s => s.Code == code);

            if (oldStation != null)//if the station is exist
            {
                update(oldStation);
            }
            else
                throw new StationNotFoundException(code);
        }
        public void DeleteStation(int code)
        {
            DO.Station Station = DataSource.ListStations.Find(s => s.Code == code);

            if (Station != null)//if the station is exist
            {
                DataSource.ListStations.Remove(Station);
            }
            else
                throw new StationNotFoundException(code);
        }
        #endregion
    }




}

