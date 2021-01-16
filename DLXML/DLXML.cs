using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using System.Xml.Linq;
namespace DL
{
   sealed class DLXML: IDL
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        // The public Instance property to use 
        public static DLXML Instance { get { return instance; } }
        // Explicit static constructor to ensure instance initialization
        // is done just before first usage
        static DLXML() { }
        DLXML() { } // default => private
        #endregion
        #region  XML Files Name

        string bussesPath = @"BussesXml.xml"; 
        string stationsPath = @"StationsXml.xml";
        string adjacentStationsPath = @"AdjacentStationsXml.xml";
        string linesPath = @"LinesXml.xml"; 
        string lineStationsPath = @"LineStationsXml.xml";
        string usersPath = @"UsersXml.xml";
        string lineTripPath = @"LineTripXml.xml";


        #endregion
        #region bus
        public IEnumerable<DO.Bus> GetAllBusses()
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            return from Bus in listBusses
                   where Bus.IsDeleted == false
                   select Bus;
        }

        public IEnumerable<DO.Bus> GetAllBussesBy(Predicate<DO.Bus> predicate)
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            return from Bus in listBusses
                   where predicate(Bus) && Bus.IsDeleted == false
                   select Bus;
        }

        public DO.Bus GetBus(int License)
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            DO.Bus bus = listBusses.Find(item => item.License == License && item.IsDeleted == false);
            if (bus == null)
                throw new DO.BusNotFoundException(License);
            return bus;
        }

        public void AddBus(DO.Bus bus)//*******************************
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            DO.Bus oldBus = listBusses.Find(item => item.License == bus.License);
            if (oldBus != null)//check if the oldBus alredy exist or deleted
            {
                if (oldBus.IsDeleted == true)//if the bus is deleted, update the bus
                {
                    listBusses.Remove(oldBus);
                    listBusses.Add(bus);
                    XMLTools.SaveListToXMLSerializer(listBusses, bussesPath);
                }
                else//if the bus is exist and not deleted,will thrown an Exception
                    throw new DO.BusNotFoundException(bus.License);

            }
            else
            {
                listBusses.Add(bus);
                XMLTools.SaveListToXMLSerializer(listBusses, bussesPath);
            }
            }

            public void UpdateBus(DO.Bus bus)
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            DO.Bus oldBus = listBusses.Find(b => b.License == bus.License && b.IsDeleted == false);

            if (oldBus != null)//if the bus is exist
            {
                listBusses.Remove(oldBus);
                listBusses.Add(bus);
                XMLTools.SaveListToXMLSerializer(listBusses, bussesPath);
            }
            else
                throw new DO.BusNotFoundException(bus.License);
        }

        public void UpdateBus(int license, Action<DO.Bus> update)
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            DO.Bus oldBus = listBusses.Find(b => b.License == license && b.IsDeleted == false);

            if (oldBus != null)//if the bus is exist
            {
                update(oldBus);
                XMLTools.SaveListToXMLSerializer(listBusses, bussesPath);

            }
            else
                throw new DO.BusNotFoundException(license);
        }

        public void DeleteBus(int license)
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            DO.Bus oldBus = listBusses.Find(b => b.License == license && b.IsDeleted == false);

            if (oldBus != null)//if the bus is exist
            {
                oldBus.IsDeleted = true;
                XMLTools.SaveListToXMLSerializer(listBusses, bussesPath);
            }
            else
                throw new DO.BusNotFoundException(license);
        }
        #endregion
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            return from line in listLines
                   where line.IsDeleted == false
                   select line;

        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            return from line in listLines
                   where predicate(line) && line.IsDeleted == false
                   select line;
        }
        public DO.Line GetLine(int id)
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            DO.Line line = listLines.Find(item => item.Id == id && item.IsDeleted == false);
            if (line == null)
                throw new DO.BusLineNotFoundException(id);
            return line;
        }
        public int AddLine(DO.Line line)//******************לטפל במספר רץ
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            line.Id = DO.Config.LineID;
            listLines.Add(line);
            XMLTools.SaveListToXMLSerializer(listLines, linesPath);
            return line.Id;

        }
        public void UpdateLine(DO.Line line)
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            DO.Line oldLine = listLines.Find(l => l.Id == line.Id && l.IsDeleted == false);

            if (oldLine != null)//if the bus is exist
            {
                listLines.Remove(oldLine);
                listLines.Add(line);
                XMLTools.SaveListToXMLSerializer(listLines, linesPath);
            }
            else
                throw new DO.BusLineNotFoundException(line.Id);
        }
        public void UpdateLine(int id, Action<DO.Line> update)
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            DO.Line oldLine = listLines.Find(l => l.Id == id && l.IsDeleted == false);

            if (oldLine != null)//if the bus is exist
            {
                update(oldLine);
                XMLTools.SaveListToXMLSerializer(listLines, linesPath);
            }
            else
                throw new DO.BusLineNotFoundException(id);
        }
        public void DeleteLine(int id)
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            DO.Line oldLine = listLines.Find(l => l.Id == id && l.IsDeleted == false);

            if (oldLine != null)//if the bus is exist
            {
                oldLine.IsDeleted = true;
                XMLTools.SaveListToXMLSerializer(listLines, linesPath);
            }
            else
                throw new DO.BusLineNotFoundException(id);
        }
        #endregion
        #region LineStation
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from lineStation in listLineStations
                   where lineStation.IsDeleted == false
                   select lineStation;
        }
        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from lineStation in listLineStations
                   where predicate(lineStation) && lineStation.IsDeleted == false
                   select lineStation;
        }
        public DO.LineStation GetLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            var l = from lineStation in listLineStations
                    where predicate(lineStation) && lineStation.IsDeleted == false
                    select lineStation;
            return l.FirstOrDefault();
        }
        public DO.LineStation GetLineStation(int line, int numSation)
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation lineStation = listLineStations
                .Find(item => item.LineId == line && item.stationCode == numSation && item.IsDeleted == false);
            if (lineStation == null)
                throw new DO.LineStationNotFoundException(numSation, line);
            return lineStation;
        }
        public void AddLineStation(DO.LineStation lineStation)
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation oldLineStation = listLineStations.
                Find(item => item.stationCode == lineStation.stationCode && item.LineId == lineStation.LineId
                && item.LineStationIndex == lineStation.LineStationIndex);
            if (oldLineStation != null)//check if the oldLineStation alredy exist or deleted
            {
                if (oldLineStation.IsDeleted == true)//if the lineStation is deleted, update the line
                    oldLineStation.IsDeleted = false;
                else//if the lineStation is exist and not deleted,will thrown an Exception
                    throw new DO.DuplicateLineStationException(lineStation.stationCode, lineStation.LineId);
            }
            else
            {
                listLineStations.Add(lineStation);
                XMLTools.SaveListToXMLSerializer(listLineStations, lineStationsPath);
            }

            }
            public void UpdateLineStation(DO.LineStation lineStation)
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation oldLineStation = listLineStations
                .Find(item => item.stationCode == lineStation.stationCode
                && item.LineId == lineStation.LineId
                && item.IsDeleted == false);

            if (oldLineStation != null)//if the lineStation is exist
            {
                listLineStations.Remove(oldLineStation);
                listLineStations.Add(lineStation);
                XMLTools.SaveListToXMLSerializer(listLineStations, lineStationsPath);
            }
            else
                throw new DO.LineStationNotFoundException(lineStation.stationCode, lineStation.LineId);
        }
        public void UpdateLineStation(int line, int numSation, Action<DO.LineStation> update)
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation oldLineStation = listLineStations
                .Find(item => item.stationCode == numSation && item.LineId == line
                        && item.IsDeleted == false);

            if (oldLineStation != null)//if the lineSatation is exist
            {
                update(oldLineStation);//update the lineStation acording to the Action
                XMLTools.SaveListToXMLSerializer(listLineStations, lineStationsPath);
            }
            else
                throw new DO.LineStationNotFoundException(numSation, line);
        }
        public void DeleteLineStation(int lineId, int numSation)
        {
            List<DO.LineStation> listLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation oldLineStation = listLineStations
                .Find(item => item.stationCode == numSation && item.LineId == lineId
                         && item.IsDeleted == false);

            if (oldLineStation != null)//if the lineStation is exist
            {
                oldLineStation.IsDeleted = true;
                XMLTools.SaveListToXMLSerializer(listLineStations, lineStationsPath);
            }
            else
                throw new DO.LineStationNotFoundException(numSation, lineId);
        }
        #endregion
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {

            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            return from stn in stationsRootElem.Elements()
                   where bool.Parse(stn.Element("IsDeleted").Value) == false
                   select new DO.Station()
                   {
                       Code = int.Parse(stn.Element("Code").Value),
                       Name = stn.Element("Name").Value,
                       Longitude = double.Parse(stn.Element("Longitude").Value),
                       Latitude = double.Parse(stn.Element("Latitude").Value),
                       IsDeleted = bool.Parse(stn.Element("IsDeleted").Value),
                   };

        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            return from stn in stationsRootElem.Elements()
                   let station = new DO.Station()
                   {
                       Code = int.Parse(stn.Element("Code").Value),
                       Name = stn.Element("Name").Value,
                       Longitude = double.Parse(stn.Element("Longitude").Value),
                       Latitude = double.Parse(stn.Element("Latitude").Value),
                       IsDeleted = bool.Parse(stn.Element("IsDeleted").Value),
                   }
                   where predicate(station) && station.IsDeleted == false
                   select station;
        }
        public DO.Station GetStation(int code)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            DO.Station station = (from stn in stationsRootElem.Elements()
                                  where int.Parse(stn.Element("Code").Value) == code
                                        && bool.Parse(stn.Element("IsDeleted").Value) == false
                                  select new DO.Station()
                                  {
                                      Code = code,
                                      Name = stn.Element("Name").Value,
                                      Longitude = double.Parse(stn.Element("Longitude").Value),
                                      Latitude = double.Parse(stn.Element("Latitude").Value),
                                      IsDeleted = bool.Parse(stn.Element("IsDeleted").Value),
                                  }
                                  ).FirstOrDefault();

            if (station == null)
                throw new DO.StationNotFoundException(code);
            return station;
        }
        public int AddStation(DO.Station station)//***************לטפל במספר רץ
        {
            
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement oldStation = (from stn in stationsRootElem.Elements()
                                   where stn.Element("Name").Value == station.Name
                                         && double.Parse(stn.Element("Longitude").Value) == station.Longitude
                                         && double.Parse(stn.Element("Latitude").Value) == station.Latitude
                                   select stn
                                    ).FirstOrDefault();
            if(oldStation!=null&&bool.Parse(oldStation.Element("IsDeleted").Value)==false)
                throw new DO.DuplicateStationException(station.Name);
            if(oldStation != null&& bool.Parse(oldStation.Element("IsDeleted").Value) == true)
            {//if the station is exist but deleted,"we'll bring it back to life"
                XElement newStation =new XElement("station",
                                     new XElement("Code", oldStation.Element("Code").Value),
                                     new XElement("Name", oldStation.Element("Name").Value),
                                     new XElement("Longitude", oldStation.Element("Longitude").Value),
                                     new XElement("Latitude", oldStation.Element("Latitude").Value),
                                     new XElement("IsDeleted", oldStation.Element("IsDeleted").Value));
                 oldStation.Remove();
                stationsRootElem.Add(newStation);
                stationsRootElem.Save(stationsPath);
                return int.Parse(newStation.Element("Code").Value);
            }
            //if we came here, the station is not exist, and now add it
            XElement newStn = new XElement("station",
                                 new XElement("Code", 1),
                                 new XElement("Name", station.Name),
                                 new XElement("Longitude", station.Longitude),
                                 new XElement("Latitude", station.Latitude),
                                 new XElement("IsDeleted", false));
            stationsRootElem.Add(newStn);
            stationsRootElem.Save(stationsPath);
            return int.Parse(newStn.Element("Code").Value);
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
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            return from AdjacentStations in listTwoAdjacentStations
                   where AdjacentStations.IsDeleted == false
                   select AdjacentStations;
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            return from AdjacentStations in listTwoAdjacentStations
                   where predicate(AdjacentStations) && AdjacentStations.IsDeleted == false
                   select AdjacentStations;
        }
        public DO.AdjacentStations GetAdjacentStations(int code1, int code2)
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            DO.AdjacentStations adjacentStations = listTwoAdjacentStations
                 .Find(item => item.Station1 == code1 && item.Station2 == code2 ||
                                      item.Station2 == code1 && item.Station1 == code2);
            if (adjacentStations == null || adjacentStations.IsDeleted == true)
            {
                throw new DO.AdjacentStationsNotFoundException(code1, code2);
            }
            return adjacentStations;
        }
        public void AddAdjacentStations(DO.AdjacentStations addAdjacentStation)
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            DO.AdjacentStations oldAdjacent = listTwoAdjacentStations.
                Find(item => item.Station1 == addAdjacentStation.Station1
                && item.Station2 == addAdjacentStation.Station2);
            if (oldAdjacent != null)//check if the old station alredy exist or deleted
            {
                if (oldAdjacent.IsDeleted == true)//if the old oldAddAdjacentStations is deleted, update the line
                    oldAdjacent.IsDeleted = false;
                else//if the old station is exist and not deleted,will thrown an Exception
                    throw new DO.AdjacentStationsNotFoundException(addAdjacentStation.Station1, addAdjacentStation.Station2);

            }
            else
            {
                listTwoAdjacentStations.Add(addAdjacentStation);
                XMLTools.SaveListToXMLSerializer(listTwoAdjacentStations, adjacentStationsPath);
            }
            }


            public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            DO.AdjacentStations oldAdjacentStations = listTwoAdjacentStations.Find(item => item.Station1 == adjacentStations.Station1
            && item.Station2 == adjacentStations.Station2 || item.Station2 == adjacentStations.Station1
            && item.Station1 == adjacentStations.Station2);

            if (oldAdjacentStations != null && oldAdjacentStations.IsDeleted == false)//if the oldAdjacentStations is exist
            {
                listTwoAdjacentStations.Remove(oldAdjacentStations);
                listTwoAdjacentStations.Add(oldAdjacentStations);
                XMLTools.SaveListToXMLSerializer(listTwoAdjacentStations, adjacentStationsPath);
            }
            else
                throw new DO.AdjacentStationsNotFoundException(adjacentStations.Station1, adjacentStations.Station2);
        }
        public void UpdateAdjacentStations(int code, int code2, Action<DO.AdjacentStations> update)
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            DO.AdjacentStations oldAdjacentStations = listTwoAdjacentStations.
                Find(item => item.Station1 == code && item.Station2 == code2
                || item.Station2 == code && item.Station1 == code2);

            if (oldAdjacentStations != null && oldAdjacentStations.IsDeleted == false)//if the oldAdjacentStations is exist
            {
                update(oldAdjacentStations);
                XMLTools.SaveListToXMLSerializer(listTwoAdjacentStations, adjacentStationsPath);
            }
            else
                throw new DO.AdjacentStationsNotFoundException(code, code2);
        }
        public void DeleteAdjacentStations(int code, int code2)
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            DO.AdjacentStations AdjacentStations = listTwoAdjacentStations.Find(item => item.Station1 == code && item.Station2 == code2
            && item.IsDeleted == false);
            if (AdjacentStations != null)//if the AdjacentStations is exist
            {
                AdjacentStations.IsDeleted = true;
                XMLTools.SaveListToXMLSerializer(listTwoAdjacentStations, adjacentStationsPath);
            }
            else
                throw new DO.AdjacentStationsNotFoundException(code, code2);

        }
        public bool AdjacentStationsIsExist(int code, int code2)
        {
            List<DO.AdjacentStations> listTwoAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(adjacentStationsPath);
            DO.AdjacentStations ads = listTwoAdjacentStations.Find(ad => ad.Station1 == code && ad.Station2 == code2 || ad.Station2 == code && ad.Station1 == code2);
            if (ads == null || ads.IsDeleted == true)
                return false;
            return true;
        }

        #endregion
    }
}
