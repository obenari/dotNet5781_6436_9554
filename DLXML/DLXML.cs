using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using System.Xml.Linq;
namespace DL
{
    sealed class DLXML : IDL
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
        string bussesPath = @"BussesXml.xml";//XMLSerializer
        string linesPath = @"LinesXml.xml";//XMLSerializer
        string lineStationsPath = @"LineStationsXml.xml";//XMLSerializer
        string lineTripPath = @"LineTripXml.xml";//XElement
        string stationsPath = @"StationsXml.xml";//XElement
        string adjacentStationsPath = @"AdjacentStationsXml.xml";//XElement
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
        /// <summary>
        /// add new bus to the system
        /// </summary>
        /// <param name="bus"></param>
        public void AddBus(DO.Bus bus)
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
        /// <summary>
        /// the method is get update bus, and replace it with the old bus.
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBus(DO.Bus bus)
        {
            List<DO.Bus> listBusses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(bussesPath);
            DO.Bus oldBus = listBusses.Find(b => b.License == bus.License && b.IsDeleted == false);

            if (oldBus != null)//if the bus is exist
            {
                //oldBus.Fuel = bus.Fuel;
                //oldBus.DateOfTreatment = bus.DateOfTreatment;
                //oldBus.TotalKmsFromLastTreatment = bus.TotalKmsFromLastTreatment;
                //oldBus.TotalKms = bus.TotalKms;


                listBusses.Remove(oldBus);
                listBusses.Add(bus);
                XMLTools.SaveListToXMLSerializer(listBusses, bussesPath);
            }
            else
                throw new DO.BusNotFoundException(bus.License);
        }
        /// <summary>
        ///  the method is get predicate,and  license, and update  the old bus.
        /// </summary>
        /// <param name="license"></param>
        /// <param name="update"></param>
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
        /// <summary>
        /// the method get license number, and marks the bus as deleted
        /// </summary>
        /// <param name="license"></param>
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
        /// <summary>
        ///  add new line to the system
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int AddLine(DO.Line line)
        {
            List<DO.Line> listLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(linesPath);
            //get the runner number from the config file, and bring it back plus 1
            XElement runnerNumber = XMLTools.LoadListFromXMLElement("ConfigRunnerNumbers.xml");
            int id = int.Parse(runnerNumber.Element("lineID").Value);
            runnerNumber.Element("lineID").SetValue(++id);
            XMLTools.SaveListToXMLElement(runnerNumber, "ConfigRunnerNumbers.xml");

            line.Id = id;
            listLines.Add(line);
            XMLTools.SaveListToXMLSerializer(listLines, linesPath);
            return line.Id;

        }
        /// <summary>
        /// the method is get update line, and replace it with the old line.
        /// </summary>
        /// <param name="line"></param>
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
        /// <summary>
        ///  the method is get predicate,and  line id, and update  the old line.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
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
        /// <summary>
        /// the method get id number, and marks the line as deleted
        /// </summary>
        /// <param name="id"></param>
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
        /// <summary>
        ///  add new lineStation to the system
        /// </summary>
        /// <param name="lineStation"></param>
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
        /// <summary>
        /// the method is get update lineStation, and replace it with the old lineStation.
        /// </summary>
        /// <param name="lineStation"></param>
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
        /// <summary>
        /// the method is get predicate,and  line id and numStation, and update  the old lineStation.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="numSation"></param>
        /// <param name="update"></param>
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
        /// <summary>
        /// the method get id number, and marks the line as deleted
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="numSation"></param>
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
        /// <summary>
        ///   add new Station to the system
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public int AddStation(DO.Station station)
        {

            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement oldStation = (from stn in stationsRootElem.Elements()
                                   where stn.Element("Name").Value == station.Name
                                         && double.Parse(stn.Element("Longitude").Value) == station.Longitude
                                         && double.Parse(stn.Element("Latitude").Value) == station.Latitude
                                   select stn
                                    ).FirstOrDefault();
            if (oldStation != null && bool.Parse(oldStation.Element("IsDeleted").Value) == false)
                throw new DO.DuplicateStationException(station.Name);
            if (oldStation != null && bool.Parse(oldStation.Element("IsDeleted").Value) == true)
            {//if the station is exist but deleted,"we'll bring it back to life"
                XElement newStation = new XElement("Station",
                                     new XElement("Code", oldStation.Element("Code").Value),
                                     new XElement("Name", oldStation.Element("Name").Value),
                                     new XElement("Longitude", oldStation.Element("Longitude").Value),
                                     new XElement("Latitude", oldStation.Element("Latitude").Value),
                                     new XElement("IsDeleted", oldStation.Element("IsDeleted").Value));
                oldStation.Remove();
                stationsRootElem.Add(newStation);
                // stationsRootElem.Save(stationsPath);
                XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);

                return int.Parse(newStation.Element("Code").Value);
            }
            //if we came here, the station is not exist, and now add it

            //get the runner number from the config file, and bring it back plus 1
            XElement runnerNumber = XMLTools.LoadListFromXMLElement("ConfigRunnerNumbers.xml");
            int id = int.Parse(runnerNumber.Element("stationCode").Value);
            runnerNumber.Element("stationCode").SetValue(++id);
            XMLTools.SaveListToXMLElement(runnerNumber, "ConfigRunnerNumbers.xml");

            XElement newStn = new XElement("Station",
                                 new XElement("Code", id),
                                 new XElement("Name", station.Name),
                                 new XElement("Longitude", station.Longitude),
                                 new XElement("Latitude", station.Latitude),
                                 new XElement("IsDeleted", false));
            stationsRootElem.Add(newStn);
            //  stationsRootElem.Save(stationsPath);
            XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);
            return id;
        }
        /// <summary>
        /// the method is get update Station, and replace it with the old Station.
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(DO.Station station)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement oldStation = (from stn in stationsRootElem.Elements()
                                   where int.Parse(stn.Element("Code").Value) == station.Code
                                            && bool.Parse(stn.Element("IsDeleted").Value) == false
                                   select stn).FirstOrDefault();
            if (oldStation == null)//if the station is exist
                throw new DO.StationNotFoundException(station.Code);
            XElement newStation = new XElement("Station",
                                  new XElement("Code", station.Code),
                                  new XElement("Name", station.Name),
                                  new XElement("Longitude", station.Longitude),
                                  new XElement("Latitude", station.Latitude),
                                  new XElement("IsDeleted", false));
            oldStation.Remove();
            stationsRootElem.Add(newStation);
            //  stationsRootElem.Save(stationsPath);
            XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);

        }
        /// <summary>
        /// the method is get predicate,and  station id and numStation, and update  the old Station.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="update"></param>
        public void UpdateStation(int code, Action<DO.Station> update)
        {

            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement oldStation = (from stn in stationsRootElem.Elements()
                                   where int.Parse(stn.Element("Code").Value) == code
                                            && bool.Parse(stn.Element("IsDeleted").Value) == false
                                   select stn).FirstOrDefault();
            if (oldStation == null)//if the station is exist
                throw new DO.StationNotFoundException(code);
            DO.Station newStn = new DO.Station()
            {
                Code = code,
                Name = oldStation.Element("Name").Value,
                Longitude = double.Parse(oldStation.Element("Longitude").Value),
                Latitude = double.Parse(oldStation.Element("Latitude").Value),
                IsDeleted = bool.Parse(oldStation.Element("IsDeleted").Value),
            };
            update(newStn);
            XElement newStationXml = new XElement("Station",
                                  new XElement("Code", code),
                                  new XElement("Name", newStn.Name),
                                  new XElement("Longitude", newStn.Longitude),
                                  new XElement("Latitude", newStn.Latitude),
                                  new XElement("IsDeleted", false));
            oldStation.Remove();
            stationsRootElem.Add(newStationXml);
            //   stationsRootElem.Save(stationsPath);
            XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);
        }
        /// <summary>
        /// the method get id number, and marks the station as deleted
        /// </summary>
        /// <param name="code"></param>
        public void DeleteStation(int code)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement oldStation = (from stn in stationsRootElem.Elements()
                                   where int.Parse(stn.Element("Code").Value) == code
                                            && bool.Parse(stn.Element("IsDeleted").Value) == false
                                   select stn).FirstOrDefault();
            if (oldStation == null)//if the station is not exist
                throw new DO.StationNotFoundException(code);
            oldStation.Element("IsDeleted").SetValue(true);
            //   stationsRootElem.Save(stationsPath);
            XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);

        }
        #endregion
        #region AdjacentStations
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {

            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            return from stn in adjacentStationsRootElem.Elements()
                   where bool.Parse(stn.Element("IsDeleted").Value) == false
                   select new DO.AdjacentStations()
                   {
                       Station1 = int.Parse(stn.Element("Station1").Value),
                       Station2 = int.Parse(stn.Element("Station2").Value),
                       Distance = double.Parse(stn.Element("Distance").Value),
                       Time = new TimeSpan(int.Parse(stn.Element("Time").Element("Hours").Value),
                                           int.Parse(stn.Element("Time").Element("Minutes").Value),
                                           int.Parse(stn.Element("Time").Element("Seconds").Value)),
                       IsDeleted = bool.Parse(stn.Element("IsDeleted").Value),
                   };

        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            return from stn in adjacentStationsRootElem.Elements()
                   let ads = new DO.AdjacentStations
                   {
                       Station1 = int.Parse(stn.Element("Station1").Value),
                       Station2 = int.Parse(stn.Element("Station2").Value),
                       Distance = double.Parse(stn.Element("Distance").Value),
                       Time = new TimeSpan(int.Parse(stn.Element("Time").Element("Hours").Value),
                                           int.Parse(stn.Element("Time").Element("Minutes").Value),
                                           int.Parse(stn.Element("Time").Element("Seconds").Value)),
                       IsDeleted = bool.Parse(stn.Element("IsDeleted").Value),
                   }
                   where ads.IsDeleted == false && predicate(ads)
                   select ads;
        }
        public DO.AdjacentStations GetAdjacentStations(int code1, int code2)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            DO.AdjacentStations ads = (from stn in adjacentStationsRootElem.Elements()
                                       where bool.Parse(stn.Element("IsDeleted").Value) == false
                                       && int.Parse(stn.Element("Station1").Value) == code1
                                       && int.Parse(stn.Element("Station2").Value) == code2
                                       || bool.Parse(stn.Element("IsDeleted").Value) == false
                                       && int.Parse(stn.Element("Station1").Value) == code2
                                       && int.Parse(stn.Element("Station2").Value) == code1
                                       select new DO.AdjacentStations
                                       {
                                           Station1 = int.Parse(stn.Element("Station1").Value),
                                           Station2 = int.Parse(stn.Element("Station2").Value),
                                           Distance = double.Parse(stn.Element("Distance").Value),
                                           Time = new TimeSpan(int.Parse(stn.Element("Time").Element("Hours").Value),
                                                               int.Parse(stn.Element("Time").Element("Minutes").Value),
                                                               int.Parse(stn.Element("Time").Element("Seconds").Value)),
                                           IsDeleted = bool.Parse(stn.Element("IsDeleted").Value),
                                       }).FirstOrDefault();
            if (ads == null)
            {
                throw new DO.AdjacentStationsNotFoundException(code1, code2);
            }
            return ads;
        }
        /// <summary>
        ///  add new Adjacent Station to the system
        /// </summary>
        /// <param name="addAdjacentStation"></param>
        public void AddAdjacentStations(DO.AdjacentStations addAdjacentStation)//////////////////
        {
            if (AdjacentStationsIsExist(addAdjacentStation.Station1, addAdjacentStation.Station2))//check if the addAdjacentStation is already exist
                throw new DO.DuplicateAdjacentStationsException(addAdjacentStation.Station1, addAdjacentStation.Station2);

            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);
            TimeSpan tm = addAdjacentStation.Time;
            XElement adsXml = new XElement("AdjacentStations",
                new XElement("Station1", addAdjacentStation.Station1),
                new XElement("Station2", addAdjacentStation.Station2),
                new XElement("Distance", addAdjacentStation.Distance),
                new XElement("IsDeleted", addAdjacentStation.IsDeleted),
                new XElement("Time", new XElement("Hours", tm.Hours), new XElement("Minutes", tm.Minutes), new XElement("Seconds", tm.Seconds))
                );
            adjacentStationsRootElem.Add(adsXml);
            //  adjacentStationsRootElem.Save(adjacentStationsPath);
            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationsPath);

        }


        /// <summary>
        /// the method is get update adjacen Station, and replace it with the old Station.
        /// </summary>
        /// <param name="adjacentStations"></param>
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement oldAds = (from stn in adjacentStationsRootElem.Elements()
                               where bool.Parse(stn.Element("IsDeleted").Value) == false
                               && int.Parse(stn.Element("Station1").Value) == adjacentStations.Station1
                               && int.Parse(stn.Element("Station2").Value) == adjacentStations.Station2
                               || bool.Parse(stn.Element("IsDeleted").Value) == false
                               && int.Parse(stn.Element("Station1").Value) == adjacentStations.Station2
                               && int.Parse(stn.Element("Station2").Value) == adjacentStations.Station1
                               select stn).FirstOrDefault();
            if (oldAds == null)
                throw new DO.AdjacentStationsNotFoundException(adjacentStations.Station1, adjacentStations.Station2);
            TimeSpan tm = adjacentStations.Time;
            XElement newAdsXml = new XElement("AdjacentStations",
                 new XElement("Station1", adjacentStations.Station1),
                 new XElement("Station2", adjacentStations.Station2),
                 new XElement("Distance", adjacentStations.Distance),
                 new XElement("IsDeleted", adjacentStations.IsDeleted),
                 new XElement("Time", new XElement("Hours", tm.Hours), new XElement("Minutes", tm.Minutes), new XElement("Seconds", tm.Seconds))
                 );
            oldAds.Remove();
            adjacentStationsRootElem.Add(newAdsXml);
            //  adjacentStationsRootElem.Save(adjacentStationsPath);
            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationsPath);

        }
        /// <summary>
        ///  the method is get predicate,and Adjacent station id and numStation, and update  the old Station.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code2"></param>
        /// <param name="update"></param>
        public void UpdateAdjacentStations(int code1, int code2, Action<DO.AdjacentStations> update)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement oldAds = (from stn in adjacentStationsRootElem.Elements()
                               where bool.Parse(stn.Element("IsDeleted").Value) == false
                               && int.Parse(stn.Element("Station1").Value) == code1
                               && int.Parse(stn.Element("Station2").Value) == code2
                               || bool.Parse(stn.Element("IsDeleted").Value) == false
                               && int.Parse(stn.Element("Station1").Value) == code2
                               && int.Parse(stn.Element("Station2").Value) == code1
                               select stn).FirstOrDefault();
            if (oldAds == null)
                throw new DO.AdjacentStationsNotFoundException(code1, code2);
            DO.AdjacentStations adsToUpdate = new DO.AdjacentStations
            {
                Station1 = int.Parse(oldAds.Element("Station1").Value),
                Station2 = int.Parse(oldAds.Element("Station2").Value),
                Distance = double.Parse(oldAds.Element("Distance").Value),
                Time = new TimeSpan(int.Parse(oldAds.Element("Time").Element("Hours").Value),
                                    int.Parse(oldAds.Element("Time").Element("Minutes").Value),
                                    int.Parse(oldAds.Element("Time").Element("Seconds").Value)),
                IsDeleted = bool.Parse(oldAds.Element("IsDeleted").Value),
            };
            update(adsToUpdate);
            TimeSpan tm = adsToUpdate.Time;
            XElement newAdsXml = new XElement("AdjacentStations",
                 new XElement("Station1", adsToUpdate.Station1),
                 new XElement("Station2", adsToUpdate.Station2),
                 new XElement("Distance", adsToUpdate.Distance),
                 new XElement("IsDeleted", adsToUpdate.IsDeleted),
                 new XElement("Time", new XElement("Hours", tm.Hours), new XElement("Minutes", tm.Minutes), new XElement("Seconds", tm.Seconds))
                 );
            oldAds.Remove();
            adjacentStationsRootElem.Add(newAdsXml);
            //  adjacentStationsRootElem.Save(adjacentStationsPath);
            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationsPath);

        }
        /// <summary>
        ///  the method get two id numbers, and marks the Adjacent station as deleted
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code2"></param>
        public void DeleteAdjacentStations(int code1, int code2)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);
            XElement ads = (from stn in adjacentStationsRootElem.Elements()
                            where bool.Parse(stn.Element("IsDeleted").Value) == false
                                       && int.Parse(stn.Element("Station1").Value) == code1
                                        && int.Parse(stn.Element("Station2").Value) == code2
                                       || bool.Parse(stn.Element("IsDeleted").Value) == false
                                        && int.Parse(stn.Element("Station1").Value) == code2
                                       && int.Parse(stn.Element("Station2").Value) == code1
                            select stn).FirstOrDefault();
            if (ads == null)
                throw new DO.AdjacentStationsNotFoundException(code1, code2);
            ads.Element("IsDeleted").SetValue(true);
            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationsPath);

        }
        /// <summary>
        /// the method check if the  AdjacentStations Is Exist
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code2"></param>
        /// <returns></returns>
        public bool AdjacentStationsIsExist(int code1, int code2)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);
            return adjacentStationsRootElem.Elements().Any(stn =>
                                      bool.Parse(stn.Element("IsDeleted").Value) == false
                                      && int.Parse(stn.Element("Station1").Value) == code1
                                    && int.Parse(stn.Element("Station2").Value) == code2
                                      || bool.Parse(stn.Element("IsDeleted").Value) == false
                                       && int.Parse(stn.Element("Station1").Value) == code2
                                      && int.Parse(stn.Element("Station2").Value) == code1);
        }

        #endregion
        #region LineTrip
        public IEnumerable<DO.LineTrip> GetAllLinesTrip()
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            return from lineTrip in lineTripRootElem.Elements()
                   where bool.Parse(lineTrip.Element("IsDeleted").Value) == false
                   select new DO.LineTrip()
                   {
                       Id = int.Parse(lineTrip.Element("Id").Value),
                       LineId = int.Parse(lineTrip.Element("LineId").Value),
                       IsDeleted = bool.Parse(lineTrip.Element("IsDeleted").Value),
                       StartAt = new TimeSpan(int.Parse(lineTrip.Element("StartAt").Element("Hours").Value),
                                              int.Parse(lineTrip.Element("StartAt").Element("Minutes").Value),
                                              int.Parse(lineTrip.Element("StartAt").Element("Seconds").Value)),
                   };
        }
        public IEnumerable<DO.LineTrip> GetAllLinesTripBy(Predicate<DO.LineTrip> predicate)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            return from lineTrip in lineTripRootElem.Elements()
                   let lt = new DO.LineTrip
                   {
                       Id = int.Parse(lineTrip.Element("Id").Value),
                       LineId = int.Parse(lineTrip.Element("LineId").Value),
                       IsDeleted = bool.Parse(lineTrip.Element("IsDeleted").Value),
                       StartAt = new TimeSpan(int.Parse(lineTrip.Element("StartAt").Element("Hours").Value),
                                              int.Parse(lineTrip.Element("StartAt").Element("Minutes").Value),
                                              int.Parse(lineTrip.Element("StartAt").Element("Seconds").Value)),
                   }
                   where lt.IsDeleted == false && predicate(lt)
                   select lt;
        }
        public DO.LineTrip GetLineTrip(int id)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            DO.LineTrip lt = (from lineTrip in lineTripRootElem.Elements()
                              where bool.Parse(lineTrip.Element("IsDeleted").Value) == false
                                    && int.Parse(lineTrip.Element("Id").Value) == id
                              select new DO.LineTrip()
                              {
                                  Id = int.Parse(lineTrip.Element("Id").Value),
                                  LineId = int.Parse(lineTrip.Element("LineId").Value),
                                  IsDeleted = bool.Parse(lineTrip.Element("IsDeleted").Value),
                                  StartAt = new TimeSpan(int.Parse(lineTrip.Element("StartAt").Element("Hours").Value),
                                               int.Parse(lineTrip.Element("StartAt").Element("Minutes").Value),
                                               int.Parse(lineTrip.Element("StartAt").Element("Seconds").Value)),
                              }).FirstOrDefault();
            if (lt == null)
                throw new DO.LineTripNotFoundException(id);
            return lt;
        }
        /// <summary>
        /// add new Adjacent Station to the system
        /// </summary>
        /// <param name="lineTrip"></param>
        /// <returns></returns>
        public int AddLineTrip(DO.LineTrip lineTrip)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);
            TimeSpan tm = lineTrip.StartAt;
            XElement oldLineTrip = (from lt in lineTripRootElem.Elements()
                                    where int.Parse(lt.Element("StartAt").Element("Hours").Value) == tm.Hours
                                         && int.Parse(lt.Element("StartAt").Element("Minutes").Value) == tm.Minutes
                                         && int.Parse(lt.Element("StartAt").Element("Seconds").Value) == tm.Seconds
                                         && int.Parse(lt.Element("LineId").Value) == lineTrip.LineId
                                    select lt).FirstOrDefault();
            if (oldLineTrip != null && bool.Parse(oldLineTrip.Element("IsDeleted").Value) == false)
                throw new DO.DuplicateLineTripException(lineTrip.Id);
            //get the runner number from the config file, and bring it back plus 1
            XElement runnerNumber = XMLTools.LoadListFromXMLElement("ConfigRunnerNumbers.xml");
            int id = int.Parse(runnerNumber.Element("lineTripId").Value);
            runnerNumber.Element("lineTripId").SetValue(++id);
            XMLTools.SaveListToXMLElement(runnerNumber, "ConfigRunnerNumbers.xml");

            XElement newLineTripXml = new XElement("LineTrip",
                    new XElement("Id", id),
                    new XElement("LineId", lineTrip.LineId),
                    new XElement("IsDeleted", false),
                    new XElement("StartAt", new XElement("Hours", tm.Hours), new XElement("Minutes", tm.Minutes), new XElement("Seconds", tm.Seconds))
                    );
            lineTripRootElem.Add(newLineTripXml);
            //    lineTripRootElem.Save(lineTripPath);
            XMLTools.SaveListToXMLElement(lineTripRootElem, lineTripPath);
            return id;
        }
        /// <summary>
        ///  the method get two id numbers, and marks the Adjacent station as deleted
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLineTrip(int id)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement lt = (from lineTrip in lineTripRootElem.Elements()
                           where int.Parse(lineTrip.Element("Id").Value) == id
                           select lineTrip).FirstOrDefault();
            if (lt == null || bool.Parse(lt.Element("IsDeleted").Value) == false)
                throw new DO.LineTripNotFoundException(id);
            lt.Element("IsDeleted").SetValue(true);
            //  lineTripRootElem.Save(lineTripPath);
            XMLTools.SaveListToXMLElement(lineTripRootElem, lineTripPath);
        }
        /// <summary>
        /// the method check if the  lineTrip Is Exist
        /// </summary>
        /// <param name="lineTrip"></param>
        /// <returns></returns>
        public bool IsExistLinetrip(DO.LineTrip lineTrip)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            return lineTripRootElem.Elements().Any(lt =>
                 bool.Parse(lt.Element("IsDeleted").Value) == false
                 && int.Parse(lt.Element("LineId").Value) == lineTrip.LineId
                 && int.Parse(lt.Element("StartAt").Element("Hours").Value) == lineTrip.StartAt.Hours
                 && int.Parse(lt.Element("StartAt").Element("Minutes").Value) == lineTrip.StartAt.Minutes
                 && int.Parse(lt.Element("StartAt").Element("Seconds").Value) == lineTrip.StartAt.Seconds);
        }

        #endregion
    }
}
// לסדר את המספר רץ עם SetValue 