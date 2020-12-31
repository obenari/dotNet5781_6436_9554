using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DLAPI;
using DO;
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
                   where Bus.IsDeleted == false
                   select Bus.Clone();
        }

        public IEnumerable<DO.Bus> GetAllBussesBy(Predicate<DO.Bus> predicate)
        {

            return from Bus in DataSource.ListBusses
                   where predicate(Bus) && Bus.IsDeleted == false
                   select Bus.Clone();
        }

        public Bus GetBus(int License)
        {
            DO.Bus bus = DataSource.ListBusses.Find(item => item.License == License && item.IsDeleted == false);
            if (bus == null)
                throw new BusNotFoundException(License);
            return bus.Clone();
        }

        public void AddBus(Bus bus)//*******************************
        {
            DO.Bus oldBus = DataSource.ListBusses.Find(item => item.License == bus.License);
            if (oldBus != null)//check if the oldBus alredy exist or deleted
            {
                if (oldBus.IsDeleted == true)//if the bus is deleted, update the bus
                    oldBus.IsDeleted = false;
                else//if the bus is exist and not deleted,will thrown an Exception
                    throw new BusNotFoundException(bus.License);

            }
            else
                DataSource.ListBusses.Add(bus.Clone());
        }

        public void UpdateBus(Bus bus)
        {
            DO.Bus oldBus = DataSource.ListBusses.Find(b => b.License == bus.License && b.IsDeleted == false);

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
            DO.Bus oldBus = DataSource.ListBusses.Find(b => b.License == license && b.IsDeleted == false);

            if (oldBus != null)//if the bus is exist
            {
                update(oldBus);
            }
            else
                throw new BusNotFoundException(license);
        }

        public void DeleteBus(int license)
        {
            DO.Bus oldBus = DataSource.ListBusses.Find(b => b.License == license && b.IsDeleted == false);

            if (oldBus != null)//if the bus is exist
            {
                oldBus.IsDeleted = true;
                //  DataSource.ListBusses.Remove(oldBus);
            }
            else
                throw new BusNotFoundException(license);
        }
        #endregion
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   where line.IsDeleted == false
                   select line.Clone();

        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            return from line in DataSource.ListLines
                   where predicate(line) && line.IsDeleted == false
                   select line.Clone();
        }
        public Line GetLine(int id)
        {
            DO.Line line = DataSource.ListLines.Find(item => item.Id == id && item.IsDeleted == false);
            if (line == null)
                throw new BusLineNotFoundException(id);
            return line.Clone();
        }
        public void AddLine(DO.Line line)//******************
        {
            DO.Line oldLine = DataSource.ListLines.Find(item => item.Id == line.Id);
            if (oldLine != null)//check if the oldLine alredy exist or deleted
            {
                if (oldLine.IsDeleted == true)//if the line is deleted, update the line
                    oldLine.IsDeleted = false;
                else//if the line is exist and not deleted,will thrown an Exception
                    throw new BusLineNotFoundException(line.Id);

            }
            else
                DataSource.ListLines.Add(line.Clone());
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line oldLine = DataSource.ListLines.Find(l => l.Id == line.Id && l.IsDeleted == false);

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
            DO.Line oldLine = DataSource.ListLines.Find(l => l.Id == id && l.IsDeleted == false);

            if (oldLine != null)//if the bus is exist
            {
                update(oldLine);
            }
            else
                throw new BusLineNotFoundException(id);
        }
        public void DeleteLine(int id)
        {
            DO.Line oldLine = DataSource.ListLines.Find(l => l.Id == id && l.IsDeleted == false);

            if (oldLine != null)//if the bus is exist
            {
                oldLine.IsDeleted = true;
                // DataSource.ListLines.Remove(oldLine);
            }
            else
                throw new BusLineNotFoundException(id);
        }
        #endregion
        #region LineStation
        public IEnumerable<LineStation> GetAllLineStations()
        {
            return from lineStation in DataSource.ListLineStations
                   where lineStation.IsDeleted == false
                   select lineStation.Clone();
        }
        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> predicate)
        {

            return from lineStation in DataSource.ListLineStations
                   where predicate(lineStation) && lineStation.IsDeleted == false
                   select lineStation.Clone();
        }
        public LineStation GetLineStation(int line, int numSation)
        {
            DO.LineStation lineStation = DataSource.ListLineStations
                .Find(item => item.LineId == line && item.stationCode == numSation && item.IsDeleted == false);
            if (lineStation == null)
                throw new LineStationNotFoundException(numSation, line);
            return lineStation.Clone();
        }
        public void AddLineStation(LineStation lineStation)
        {
            DO.LineStation oldLineStation = DataSource.ListLineStations.
                Find(item => item.stationCode == lineStation.stationCode && item.LineId == lineStation.LineId);
            if (oldLineStation != null)//check if the oldLineStation alredy exist or deleted
            {
                if (oldLineStation.IsDeleted == true)//if the lineStation is deleted, update the line
                    oldLineStation.IsDeleted = false;
                else//if the lineStation is exist and not deleted,will thrown an Exception
                    throw new LineStationNotFoundException(lineStation.stationCode, lineStation.LineId);

            }
            else
                DataSource.ListLineStations.Add(lineStation.Clone());


        }
        public void UpdateLineStation(LineStation lineStation)
        {
            DO.LineStation oldLineStation = DataSource.ListLineStations
                .Find(item => item.stationCode == lineStation.stationCode
                && item.LineId == lineStation.LineId
                && item.IsDeleted == false);

            if (oldLineStation != null)//if the lineStation is exist
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
                .Find(item => item.stationCode == numSation && item.LineId == line
                        && item.IsDeleted == false);

            if (oldLineStation != null)//if the lineSatation is exist
            {
                update(oldLineStation);//update the lineStation acording to the Action
            }
            else
                throw new LineStationNotFoundException(numSation, line);
        }
        public void DeleteLineStation(int line, int numSation)
        {

            DO.LineStation oldLineStation = DataSource.ListLineStations
                .Find(item => item.stationCode == numSation && item.LineId == line
                         && item.IsDeleted == false);

            if (oldLineStation != null)//if the lineStation is exist
            {
                oldLineStation.IsDeleted = true;
                // DataSource.ListLineStations.Remove(oldLineStation);
            }
            else
                throw new LineStationNotFoundException(numSation, line);
        }
        #endregion
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                   where station.IsDeleted == false
                   select station.Clone();

        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station) && station.IsDeleted == false
                   select station.Clone();
        }
        public Station GetStation(int id)
        {
            DO.Station station = DataSource.ListStations.Find(item => item.Code == id && item.IsDeleted == false);
            if (station == null)
                throw new StationNotFoundException(id);
            return station.Clone();
        }
        public void AddStation(DO.Station station)
        {
            DO.Station oldLine = DataSource.ListStations.Find(item => item.Code == station.Code);
            if (oldLine != null)//check if the old station alredy exist or deleted
            {
                if (oldLine.IsDeleted == true)//if the old station is deleted, update the line
                    oldLine.IsDeleted = false;
                else//if the old station is exist and not deleted,will thrown an Exception
                    throw new StationNotFoundException(station.Code);

            }
            else
                DataSource.ListStations.Add(station.Clone());


        }
        public void UpdateStation(DO.Station station)
        {
            DO.Station oldStation = DataSource.ListStations.Find(s => s.Code == station.Code && s.IsDeleted == false);

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
            DO.Station oldStation = DataSource.ListStations.Find(s => s.Code == code && s.IsDeleted == false);

            if (oldStation != null)//if the station is exist
            {
                update(oldStation);
            }
            else
                throw new StationNotFoundException(code);
        }
        public void DeleteStation(int code)
        {
            DO.Station Station = DataSource.ListStations.Find(s => s.Code == code && s.IsDeleted == false);

            if (Station != null)//if the station is exist
            {
                Station.IsDeleted = true;
                //  DataSource.ListStations.Remove(Station);
            }
            else
                throw new StationNotFoundException(code);
        }
        #endregion
        #region AdjacentStations
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from AdjacentStations in DataSource.ListTwoAdjacentStations
                   where AdjacentStations.IsDeleted == false
                   select AdjacentStations.Clone();
            ////
            
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            return from AdjacentStations in DataSource.ListTwoAdjacentStations
                   where predicate(AdjacentStations) && AdjacentStations.IsDeleted == false
                   select AdjacentStations.Clone();
        }
        public AdjacentStations GetAdjacentStations(int code1, int code2)
        {
            DO.AdjacentStations adjacentStations = DataSource.ListTwoAdjacentStations
                 .Find(item => item.Station1 == code1 && item.Station2 == code2
                 && item.IsDeleted == false);
            if (adjacentStations == null)
                throw new AdjacentStationsNotFoundException(code1, code2);
            return adjacentStations.Clone();

        }
        public void AddAdjacentStations(AdjacentStations addAdjacentStation)
        {
            DO.AdjacentStations oldAdjacent = DataSource.ListTwoAdjacentStations.
                Find(item => item.Station1 == addAdjacentStation.Station1
                && item.Station2 == addAdjacentStation.Station2);
            if (oldAdjacent != null)//check if the old station alredy exist or deleted
            {
                if (oldAdjacent.IsDeleted == true)//if the old oldAddAdjacentStations is deleted, update the line
                    oldAdjacent.IsDeleted = false;
                else//if the old station is exist and not deleted,will thrown an Exception
                    throw new AdjacentStationsNotFoundException(addAdjacentStation.Station1, addAdjacentStation.Station2);

            }
            else
                DataSource.ListTwoAdjacentStations.Add(addAdjacentStation.Clone());

        }


        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            DO.AdjacentStations oldAdjacentStations = DataSource.ListTwoAdjacentStations.Find(item => item.Station1 == adjacentStations.Station1
            && item.Station2 == adjacentStations.Station2
            && item.IsDeleted == false);
            if (oldAdjacentStations != null)//if the oldAdjacentStations is exist
            {
                DataSource.ListTwoAdjacentStations.Remove(oldAdjacentStations);
                DataSource.ListTwoAdjacentStations.Add(oldAdjacentStations.Clone());
            }
            else
                throw new AdjacentStationsNotFoundException(adjacentStations.Station1, adjacentStations.Station2);
        }
        public void UpdateAdjacentStations(int code, int code2, Action<AdjacentStations> update)
        {
            DO.AdjacentStations oldAdjacentStations = DataSource.ListTwoAdjacentStations.
                Find(item => item.Station1 == code && item.Station2 == code2
                && item.IsDeleted == false);

            if (oldAdjacentStations != null)//if the oldAdjacentStations is exist
            {
                update(oldAdjacentStations);
            }
            else
                throw new AdjacentStationsNotFoundException(code, code2);
        }
        public void DeleteAdjacentStations(int code, int code2)
        {
            DO.AdjacentStations AdjacentStations = DataSource.ListTwoAdjacentStations.Find(item => item.Station1 == code && item.Station2 == code2
            && item.IsDeleted == false);
            if (AdjacentStations != null)//if the AdjacentStations is exist
            {
                AdjacentStations.IsDeleted = true;
                //  DataSource.ListStations.Remove(Station);
            }
            else
                throw new AdjacentStationsNotFoundException(code, code2);

        }
        #endregion
    }
}
