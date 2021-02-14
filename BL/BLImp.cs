using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using BO;
using DLAPI;
namespace BL
{
    internal sealed class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

        #region singelton
        static readonly BLImp instance = new BLImp();
        // The public Instance property to use 
        public static BLImp Instance { get { return instance; } }
        // Explicit static constructor to ensure instance initialization
        // is done just before first usage
        static BLImp() { }
        BLImp() { } // default => private
        #endregion
        #region Line
        /// <summary>
        ///  add new line to the system
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int AddLine(Line line)
        {
            //check if there is information about distance and time between all the stations
            string stationWithoutInformation = "";
            List<BO.LineStation> lineStations = line.Stations.ToList();
            TimeSpan zeroTime = new TimeSpan(0, 0, 0);
            for (int i = 1; i < line.Stations.Count(); i++)
            {
                try
                {
                    if (lineStations[i].Time == zeroTime && lineStations[i].Distance == 0)//if the user dont give information about the distance and time
                        //check if there is information about it ,in the system
                        dl.GetAdjacentStations(lineStations[i].stationCode, lineStations[i - 1].stationCode);
                }
                catch (DO.AdjacentStationsNotFoundException ex)
                {
                    stationWithoutInformation += lineStations[i].stationName + ", " + lineStations[i - 1].stationName + "\n";
                }
            }
            if (stationWithoutInformation != "")//it means that we  dont have information about all the station
            {
                throw new NotEnoughInformationException(stationWithoutInformation);
            }
            //now add the line to dl
            DO.Line lineToadd = new DO.Line
            {
                Area = (DO.Areas)(line.Area),
                LineNumber = line.LineNumber,
                FirstStation = lineStations[0].stationCode,
                LastStation = lineStations[lineStations.Count() - 1].stationCode,
                IsDeleted = false
            };
            line.Id = dl.AddLine(lineToadd);
            //now create DO.lineStation and add to dl
            DO.LineStation firstLineStation = new DO.LineStation
            {//the first station
                IsDeleted = false,
                stationCode = lineStations[0].stationCode,
                LineId = line.Id,
                LineStationIndex = 0
            };
            dl.AddLineStation(firstLineStation);
            //the last station
            DO.LineStation lastLineStation = new DO.LineStation
            {//the first station
                IsDeleted = false,
                stationCode = lineStations[lineStations.Count() - 1].stationCode,
                LineId = line.Id,
                LineStationIndex = lineStations.Count() - 1
            };
            dl.AddLineStation(lastLineStation);
            for (int i = 1; i < lineStations.Count() - 1; i++)
            {
                DO.LineStation newLineStation = new DO.LineStation
                {
                    stationCode = lineStations[i].stationCode,
                    LineId = line.Id,
                    LineStationIndex = i,
                    IsDeleted = false,
                };
                dl.AddLineStation(newLineStation);
            }
            // now create or update the djacentStations
            for (int i = 1; i < lineStations.Count(); i++)
            {
                if (lineStations[i].Time != zeroTime && lineStations[i].Distance != 0)//if the user add or update the distance
                {
                    DO.AdjacentStations ads = new DO.AdjacentStations
                    {
                        Distance = lineStations[i].Distance,
                        Time = lineStations[i].Time,
                        Station1 = lineStations[i].stationCode,
                        Station2 = lineStations[i - 1].stationCode,
                        IsDeleted = false
                    };
                    if (dl.AdjacentStationsIsExist(lineStations[i].stationCode, lineStations[i - 1].stationCode))
                        dl.UpdateAdjacentStations(ads);
                    else
                        dl.AddAdjacentStations(ads);
                }
            }
            return line.Id;
        }
        /// <summary>
        ///the method get line id and delete the line
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLine(int id)
        {
            try
            {
                dl.DeleteLine(id);
                var lineStations = dl.GetAllLineStationsBy(s => s.LineId == id).ToList();///delete all the line stations
                for (int i = 0; i < lineStations.Count(); i++)
                {
                    dl.DeleteLineStation(id, lineStations[i].stationCode);
                }
                var linesTrip = dl.GetAllLinesTripBy(x => x.LineId == id);
                for (int i = 0; i < linesTrip.Count(); i++)
                {
                    dl.DeleteLineTrip(id);
                }
            }
            catch (DO.BusLineNotFoundException ex)
            {

                throw new BO.BusLineNotFoundException( id, ex);
            }
            catch (DO.LineStationNotFoundException ex)
            {

                throw new BO.LineStationNotFoundException( ex.LineId, id);
            }
            catch(DO.LineTripNotFoundException ex)
            {
                throw new BO.LineTripNotFoundException(ex.Id,"",ex);
            }

        }

        public IEnumerable<Line> GetAllLines()
        {
            return from line in dl.GetAllLines()
                   select LineDoBoAdapter(line);
        }

        public IEnumerable<Line> GetAllLinesByArea(BO.Areas area)
        {
            return from line in dl.GetAllLines()
                   where (int)line.Area == (int)area
                   select LineDoBoAdapter(line);
        }

        public BO.Line GetLine(int id)
        {
            DO.Line line = dl.GetLine(id);
            return LineDoBoAdapter(line);
        }
        /// <summary>
        /// the method is get update line, and replace it with the old line.
        /// </summary>
        /// <param name="lineToUpdate"></param>
        public void UpdateLine(Line lineToUpdate)//it is disable to update the line id, line number and area
        {
            //check if there is information about distance and time between all the stations
            string stationWithoutInformation = "";
            List<BO.LineStation> lineStations = lineToUpdate.Stations.ToList();
            TimeSpan zeroTime = new TimeSpan(0, 0, 0);
            for (int i = 1; i < lineToUpdate.Stations.Count(); i++)
            {
                try
                {
                    if (lineStations[i].Time == zeroTime && lineStations[i].Distance == 0)//if the user dont give information about the distance and time
                        //check if there is information about it ,in the system
                        dl.GetAdjacentStations(lineStations[i].stationCode, lineStations[i - 1].stationCode);
                }
                catch (DO.AdjacentStationsNotFoundException ex)
                {
                    stationWithoutInformation += lineStations[i].stationName + ", " + lineStations[i - 1].stationName + "\n";
                }
            }
            if (stationWithoutInformation != "")//it means that we  dont have information about all the station
            {
                throw new NotEnoughInformationException(stationWithoutInformation);
            }
            //the last or first station maybe changed, so update the line in DO
            dl.UpdateLine(lineToUpdate.Id, lineU =>
            {
                lineU.LastStation = lineStations[lineStations.Count() - 1].stationCode;
                lineU.FirstStation = lineStations[0].stationCode;
            });
            var allOldLineStations = dl.GetAllLineStationsBy(s => s.LineId == lineToUpdate.Id).OrderBy(s => s.LineStationIndex).ToList();
            var allNewLineStations = lineToUpdate.Stations.ToList();
            for (int i = 0; i < allNewLineStations.Count(); i++)
            {
                DO.LineStation ls = allOldLineStations.Find(s => s.stationCode == allNewLineStations[i].stationCode);
                if (ls != null)//if the line station is exist,only update the index
                {
                    dl.UpdateLineStation(lineToUpdate.Id, allNewLineStations[i].stationCode, s => s.LineStationIndex = i);
                    allOldLineStations.Remove(ls);
                }
                else//create new line station
                {
                    DO.LineStation newLS = new DO.LineStation
                    {
                        LineId = lineToUpdate.Id,
                        LineStationIndex = i,
                        stationCode = allNewLineStations[i].stationCode,
                        IsDeleted = false
                    };
                    dl.AddLineStation(newLS);
                }
            }
            //if left lineStations in  allOldLineStations, it means we need to delete them
            for (int i = 0; i < allOldLineStations.Count(); i++)
            {
                dl.DeleteLineStation(allOldLineStations[i].LineId, allOldLineStations[i].stationCode);
            }
            // now create or update the djacentStations
            for (int i = 1; i < lineStations.Count(); i++)
            {
                if (lineStations[i].Time != zeroTime && lineStations[i].Distance != 0)//if the user add or update the distance
                {
                    DO.AdjacentStations ads = new DO.AdjacentStations
                    {
                        Distance = lineStations[i].Distance,
                        Time = lineStations[i].Time,
                        Station1 = lineStations[i].stationCode,
                        Station2 = lineStations[i - 1].stationCode,
                        IsDeleted = false
                    };
                    if (dl.AdjacentStationsIsExist(lineStations[i].stationCode, lineStations[i - 1].stationCode))
                        dl.UpdateAdjacentStations(ads);
                    else
                        dl.AddAdjacentStations(ads);
                }
            }
        }
        /// <summary>
        ///  the metod get a DO.line and return a BO.line and the opposite dirction
        /// </summary>
        /// <param name="lineDO"></param>
        /// <returns></returns>
        BO.Line LineDoBoAdapter(DO.Line lineDO)
        {
            BO.Line lineBO = new BO.Line();
            lineBO.LineNumber = lineDO.LineNumber;
            lineBO.Id = lineDO.Id;
            lineBO.Area = (BO.Areas)(int)lineDO.Area;
            //get the first and the last stations of lineDO in order to get their name' to put in lineBo
            DO.Station station1 = dl.GetStation(lineDO.FirstStation);
            DO.Station station2 = dl.GetStation(lineDO.LastStation);
            lineBO.FirstStationName = station1.Name;
            lineBO.LastStationName = station2.Name;

            //collect all the lineStation that belong to lineBo
            lineBO.Stations = from stn in dl.GetAllLineStationsBy(item => item.LineId == lineDO.Id)
                              orderby stn.LineStationIndex
                              select LineStationDoBoAdapter(stn);//convert from DO.lineStation to BO.lineStation
            return lineBO;
        }

       
        #endregion

        #region LineStation
        //lineStation dont need the field getAll, because layer UI get the linestation as obeject in busLine

        /// <summary>
        /// this method get DO.lineStation and return a BO.LineStation
        /// </summary>
        /// <param name="doLineStation"></param>
        /// <returns></returns>
        private BO.LineStation LineStationDoBoAdapter(DO.LineStation doLineStation)
        {
            BO.LineStation boLineStation = new LineStation();
            boLineStation.stationCode = doLineStation.stationCode;
            DO.Station temp = dl.GetStation(doLineStation.stationCode);
            boLineStation.stationName = temp.Name;
            boLineStation.LineStationIndex = doLineStation.LineStationIndex;
            if (boLineStation.LineStationIndex == 0)//it is the first station
            {
                boLineStation.Time = new TimeSpan(0);
                boLineStation.Distance = 0;
            }
            else
            {
                DO.LineStation prevStation = dl.GetLineStationsBy(s => s.LineStationIndex == doLineStation.LineStationIndex - 1
                                                                   && s.LineId == doLineStation.LineId);
                DO.AdjacentStations ads = dl.GetAdjacentStations(doLineStation.stationCode, prevStation.stationCode);
                boLineStation.Distance = ads.Distance;
                boLineStation.Time = ads.Time;

            }
            return boLineStation;
        }
        #endregion
        #region Bus
        public IEnumerable<BO.Bus> GetAllBusses()
        {
            return from bus in dl.GetAllBusses()
                   select BusDoBoAdapter(bus);
        }

        public IEnumerable<BO.Bus> GetAllBussesBy(Predicate<BO.Bus> predicate)
        {

            return from bus in dl.GetAllBusses()
                   let b = BusDoBoAdapter(bus)
                   where predicate(b)
                   select b;
        }
        public BO.Bus GetBus(int License)
        {

            DO.Bus bus = dl.GetBus(License);
            return BusDoBoAdapter(bus);
        }
        /// <summary>
        ///the method get a new bus, and and check if the data is okey, and add it  to dl
        /// </summary>
        /// <param name="boBus"></param>
        public void AddBus(BO.Bus boBus)
        {
            try
            {

                if (boBus.License.Length < 7)
                    throw new LicenseFormatException(int.Parse(boBus.License), "");
                if (boBus.StartOfWork.Year > 2017 && boBus.License.Length == 7 || boBus.StartOfWork.Year <= 2017 && boBus.License.Length == 8)//if there is no match with the license number and date
                    throw new SumOfDigitException(int.Parse(boBus.License), "");
                if (boBus.StartOfWork > boBus.DateOfTreatment)
                    throw new TimeException("the date of treatment canot be less than start of work");
                dl.AddBus(BusBoDoAdapter(boBus));
            }
            catch (DO.DuplicateBusException ex)
            {
                throw new DuplicateBusException(ex.License, "", ex);
            }

        }
        /// <summary>
        ///  the method is get update bus, and update it in dl
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBus(BO.Bus bus)
        {
            try
            {
                dl.UpdateBus(BusBoDoAdapter(bus));
            }
            catch (DO.BusNotFoundException ex)
            {
                throw new BusNotFoundException(ex.License.ToString(), "", ex);
            }
        }
       /// <summary>
       /// the method get bus, and delete it from dl
       /// </summary>
       /// <param name="boBus"></param>
        public void DeleteBus(BO.Bus boBus)
        {
            try
            {
                while (boBus.License[0] == '0')
                    boBus.License = boBus.License.Remove(0, 1);
                dl.DeleteBus(int.Parse(boBus.License));
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BO.BusNotFoundException(boBus.License, "", ex);
            }
        }
        /// <summary>
        /// this method update that tank's bus is full
        /// </summary>
        /// <param name="bus"></param>
        public void RefuelBus(BO.Bus bus)
        {
            bus.Fuel = 1200;
            UpdateBus(bus);
        }
        /// <summary>
        /// this method update the bus data (date of treatment...)
        /// </summary>
        /// <param name="bus"></param>
        public void TreatmentBus(BO.Bus bus)
        {
            bus.Fuel = DO.Config.FullTank;
            bus.DateOfTreatment = DateTime.Now;
            bus.TotalKmsFromLastTreatment = 0;
            UpdateBus(bus);
        }
        /// <summary>
        /// this method get a bus and kilometers, and check if the bus could drive this driving
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="km"></param>
        /// <returns></returns>
      public  bool IsReadyToDriving(BO.Bus bus, int km)
        {
            if (bus.TotalKmsFromLastTreatment + km > 20000 || bus.Fuel - km < 0)
                return false;
            DateTime yearAgo = DateTime.Today.AddYears(-1);
            if (bus.DateOfTreatment < yearAgo|| bus.TotalKmsFromLastTreatment==20000)
            {
                bus.Status = BO.Status.Dangerous;
                return false;
            }
            bus.TotalKmsFromLastTreatment = bus.TotalKmsFromLastTreatment + km;
            bus.Fuel = bus.Fuel - km;
            UpdateBus(bus);
            return true;
        }
        /// <summary>
        /// this method get BO.Bus  and return a DO.Bus
        /// </summary>
        /// <param name="boBus"></param>
        /// <returns></returns>
        private DO.Bus BusBoDoAdapter(BO.Bus boBus)
        {
            DO.Bus doBus = new DO.Bus();
            //while (boBus.License[0] == '0')
            //    boBus.License = boBus.License.Remove(0, 1);
            doBus.License = int.Parse(boBus.License);
            doBus.DateOfTreatment = boBus.DateOfTreatment;
            doBus.StartOfWork = boBus.StartOfWork;
            doBus.Fuel = boBus.Fuel;
            doBus.TotalKms = boBus.TotalKms;
            doBus.TotalKmsFromLastTreatment = boBus.TotalKmsFromLastTreatment;
            return doBus;

        }
        /// <summary>
        /// this method get DO.Bus  and return a BO.Bus
        /// </summary>
        /// <param name="doBus"></param>
        /// <returns></returns>
        private BO.Bus BusDoBoAdapter(DO.Bus doBus)
        {
            BO.Bus boBus = new Bus();
            string licenseStr = doBus.License.ToString();
            if (licenseStr.Length <= 7)
            {
                string help = "";
                if (doBus.StartOfWork.Year >= 2018)//the license number need 8 digit
                {
                    for (int i = 0; i < 8 - licenseStr.Length; i++)
                    {
                        help += "0";
                    }
                }
                else//the license number need 7 digit
                {
                    for (int i = 0; i < 7 - licenseStr.Length; i++)
                    {
                        help += "0";
                    }
                }
                licenseStr = licenseStr.Insert(0, help);

            }
            boBus.License = licenseStr;
            boBus.DateOfTreatment = doBus.DateOfTreatment;
            boBus.StartOfWork = doBus.StartOfWork;
            if (doBus.Fuel == 0 || DateTime.Now.AddYears(-1) > doBus.DateOfTreatment||doBus.TotalKmsFromLastTreatment>=20000)
                boBus.Status = BO.Status.Dangerous;
            else
                boBus.Status = BO.Status.Ready;
            boBus.Fuel = doBus.Fuel;
            boBus.TotalKms = doBus.TotalKms;
            boBus.TotalKmsFromLastTreatment = doBus.TotalKmsFromLastTreatment;
            return boBus;

        }
        #endregion
        #region InformationForStation
        /// <summary>
        /// this function get the station  and the line that passing the station, in order to make an InformationForStation
        /// </summary>
        /// <param name="line">the line that passing the station</param>
        /// <param name="doStation">the station that should the infomation is belong's it</param>
        /// <returns></returns>
        InformationForStation makeInformationForStation(int lineId, DO.LineStation doLineStation)
        {
            InformationForStation information = new InformationForStation();
            DO.Line line = dl.GetLine(lineId);
            information.LineNumber = line.LineNumber;
            //get the first and last station of the line to put their name in the property field
            try
            {
                DO.Station first = dl.GetStation(line.FirstStation);

                DO.Station last = dl.GetStation(line.LastStation);
                information.FirstStation = first.Name;
                information.LastStation = last.Name;
                // get the next Linestation to doStation, in the specific line
                if (line.LastStation == doLineStation.stationCode)//check if it is the last station
                    information.NextStation = "זוהי תחנה אחרונה";
                else
                {
                    DO.LineStation nextStation = dl.GetLineStationsBy(s => s.LineStationIndex == doLineStation.LineStationIndex + 1 && s.LineId == lineId);
                    //get the physical station to get the name 
                    DO.Station next = dl.GetStation(nextStation.stationCode);
                    if (next == null)
                        throw new StationNotFoundException(nextStation.stationCode);
                    information.NextStation = next.Name;
                }
                return information;
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new BO.StationNotFoundException(ex.StationId, doLineStation.stationCode.ToString() + lineId.ToString());
            }
        }
        #endregion 
        #region Station
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from station in dl.GetAllStations()
                   select StationDoBoAdapter(station);
        }

        public IEnumerable<BO.Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            return from station in dl.GetAllStations()
                   let b = StationDoBoAdapter(station)
                   where predicate(b)
                   select b;
        }
        public BO.Station GetStation(int code)
        {
            DO.Station station = dl.GetStation(code);
            return StationDoBoAdapter(station);
        }
        /// <summary>
        /// this method get a new station, check the data and add it to dl
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public int AddStation(BO.Station station)
        {
            if (station.Longitude < DO.Config.MIN_LON || station.Longitude > DO.Config.MAX_LON)
                throw new OutOfLongitudeIsraelLimitException(station.Latitude);
            if (station.Latitude < DO.Config.MIN_LAT || station.Latitude > DO.Config.MAX_LAT)
                throw new OutOfLatitudeIsraelLimitException(station.Latitude);

            try
            {
                //to get a running number of the station;
                station.Code = dl.AddStation(StationBoDoAdapter(station));
                return station.Code;
            }
            catch (DO.DuplicateStationException ex)
            {
                throw new BO.DuplicateStationException(ex.StationName, "", ex);
            }
        }
       /// <summary>
       /// the method get the station code, and delete the station from dl
       /// </summary>
       /// <param name="code"></param>
        public void DeleteStation(int code)
        {
            try
            {
                var lineStationsToDelete = dl.GetAllLineStationsBy(s => s.stationCode == code).ToList();

                //update the new distance and time between the next and prev station
                for (int i = 0; i < lineStationsToDelete.Count(); i++)
                {
                    var item = lineStationsToDelete[i];//in order to shrink the code
                    // in order to know if it is the last station,we need to get all the
                    // linestations that belong to it
                    var allLinestations = dl.GetAllLineStationsBy(s => s.LineId == item.LineId).OrderBy(s => s.LineStationIndex).ToList();
                    if (item.LineStationIndex != allLinestations.Count() - 1 && item.LineStationIndex != 0)
                    {
                        try
                        {
                            DO.AdjacentStations prev = dl.GetAdjacentStations(item.stationCode, allLinestations[item.LineStationIndex - 1].stationCode);
                            DO.AdjacentStations next = dl.GetAdjacentStations(item.stationCode, allLinestations[item.LineStationIndex + 1].stationCode);
                            DO.AdjacentStations newAdjacentStations = new DO.AdjacentStations
                            {
                                Station1 = allLinestations[item.LineStationIndex - 1].stationCode,
                                Station2 = allLinestations[item.LineStationIndex + 1].stationCode,
                                Distance = prev.Distance + next.Distance,
                                Time = prev.Time + next.Time,
                                IsDeleted = false
                            };
                            dl.AddAdjacentStations(newAdjacentStations);
                        }
                        catch (DuplicateAdjacentStationsException ex) { }
                    }

                }//end to update the distance and time between the next and prev station

                //update the lines that their first station is deleted
                var linesWhereFirstStationDeleted = dl.GetAllLinesBy(l => l.FirstStation == code).ToList();
                for (int i = 0; i < linesWhereFirstStationDeleted.Count(); i++)
                {
                    DO.LineStation second = dl.
                        GetLineStationsBy(s => s.LineStationIndex == 1 && s.LineId == linesWhereFirstStationDeleted[i].Id);
                    linesWhereFirstStationDeleted[i].FirstStation = second.stationCode;
                    dl.UpdateLine(linesWhereFirstStationDeleted[i]);

                }
                //update the lines that their last station is deleted
                var linesWhereLastStationDeleted = dl.GetAllLinesBy(l => l.LastStation == code).ToList();
                for (int i = 0; i < linesWhereLastStationDeleted.Count(); i++)
                {
                    DO.LineStation lineStationToRemove = dl.GetLineStation(linesWhereLastStationDeleted[i].Id, code);
                    DO.LineStation oneBeforeTheLast = dl.GetLineStationsBy(
                        s => s.LineStationIndex == lineStationToRemove.LineStationIndex - 1
                        && s.LineId == linesWhereLastStationDeleted[i].Id);
                    linesWhereLastStationDeleted[i].LastStation = oneBeforeTheLast.stationCode;
                    dl.UpdateLine(linesWhereLastStationDeleted[i]);
                }

                //update the indexes of the rest lineStation in the line (item)
                //group all the lineStation by lineId, and than check if each group contatin the 
                //station to remove, and than update the index
                var lineGroup = from lineStation in dl.GetAllLineStations()
                                orderby lineStation.LineStationIndex
                                group lineStation by lineStation.LineId;
                foreach (var item in lineGroup)
                {
                    //get the lineStation to remove from the group( if it is exist)
                    DO.LineStation lineStation = item.FirstOrDefault(s => s.stationCode == code);//
                    if (lineStation != null)//if it's not null, the group contain the line station to remove
                    {
                        List<DO.LineStation> temp = item.ToList();//update the indexes of the rest lineStation in the line (item)
                        for (int i = lineStation.LineStationIndex + 1; i < item.Count(); i++)
                        {
                            temp[i].LineStationIndex--;
                            dl.UpdateLineStation(temp[i]);
                        }
                    }
                }

                //remove all AdjacentStations that contain the station to remove
                IEnumerable<DO.AdjacentStations> aStationToDelete = dl.GetAllAdjacentStationsBy(s => s.Station1 == code || s.Station2 == code);
                foreach (var item in aStationToDelete)
                {
                    dl.DeleteAdjacentStations(item.Station1, item.Station2);
                }
                //delete the request station from dl
                dl.DeleteStation(code);
                //delete all linesation than belong to the station to remove
                foreach (var item in lineStationsToDelete)
                {
                    dl.DeleteLineStation(item.LineId, item.stationCode);
                }
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BO.StationNotFoundException(code);
            }
        }
        /// <summary>
        ///  the metod get a DO.Station and return a BO.Station and the opposite dirction
        /// </summary>
        /// <param name="doStation"></param>
        /// <returns></returns>
        private BO.Station StationDoBoAdapter(DO.Station doStation)
        {
            BO.Station boStation = new Station();
            boStation.Code = doStation.Code;
            boStation.Latitude = doStation.Latitude;
            boStation.Longitude = doStation.Longitude;
            boStation.Name = doStation.Name;
            //sorted all lineSattion by lineId
            var lineGroup = from lineStation in dl.GetAllLineStations()
                            group lineStation by lineStation.LineId;
            //now check all  group (line), if doStation is belong to the group (line), call to makeInformationForStation 
            boStation.ListLines = from line in lineGroup
                                  let lineStation = line.FirstOrDefault(s => s.stationCode == doStation.Code)
                                  where lineStation != null
                                  select makeInformationForStation(line.Key, lineStation);

           
            return boStation;
        }
        /// <summary>
        ///  the metod get a BO.Station and return a DO.Station and the opposite dirction
        /// </summary>
        /// <param name="boStation"></param>
        /// <returns></returns>
        private DO.Station StationBoDoAdapter(BO.Station boStation)
        {
            DO.Station doStation = new DO.Station();
            doStation.Code = boStation.Code;
            doStation.Latitude = boStation.Latitude;
            doStation.Longitude = boStation.Longitude;
            doStation.Name = boStation.Name;
            return doStation;
        }

        #endregion
        #region LineTrip
        public IEnumerable<BO.LineTrip> GetAllLinesTrip()
        {
            return from lineTrip in dl.GetAllLinesTrip()
                   select LineTripDoBoAdapter(lineTrip);
        }

        public IEnumerable<BO.LineTrip> GetAllLinesTripBy(Predicate<BO.LineTrip> predicate)
        {
            return from lineTrip in dl.GetAllLinesTrip()
                   let lt = LineTripDoBoAdapter(lineTrip)
                   where predicate(lt)
                   select lt;

        }
        public BO.LineTrip GetLineTrip(int id)
        {
            BO.LineTrip boLineTrip = (from lineTrip in dl.GetAllLinesTrip()
                                     where lineTrip.Id == id
                   select LineTripDoBoAdapter(lineTrip)).FirstOrDefault();
            if (boLineTrip == null)
                throw new BO.LineTripNotFoundException(id);
            return boLineTrip;
        }
        /// <summary>
        /// the method get a single linetrip, and add it to dl
        /// </summary>
        /// <param name="boLineTrip"></param>
        public void AddLineTrip(BO.LineTrip boLineTrip)
        {
            DO.LineTrip lineTripToAdd = LineTripBoDoAdapter(boLineTrip);
            try
            {
                boLineTrip.Id = dl.AddLineTrip(lineTripToAdd);
            }
            catch(DO.DuplicateLineTripException ex)//if the lineTrip is already exist
            {
                throw new BO.DuplicateLineTripException(ex.Id);
            }
        }
        /// <summary>
        /// The method receives a range of time and frequency and produces all the lineTrip in the range
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="frequency"></param>
        public void AddLineTrip(int lineId, TimeSpan start, TimeSpan finish,TimeSpan frequency)
        {
            List<DO.LineTrip> linesTrip = new List<DO.LineTrip>();
            while(start<=finish)
            {
                DO.LineTrip lineTrip = new DO.LineTrip
                {
                    LineId = lineId,
                    StartAt = start
                };
                if(dl.IsExistLinetrip(lineTrip))
                    throw new BO.DuplicateLineTripException(lineId);
                linesTrip.Add(lineTrip);
             
                start += frequency;
            }
            for (int i = 0; i < linesTrip.Count; i++)
            {
                dl.AddLineTrip(linesTrip[i]);
            }
        }
        /// <summary>
        /// this method delete the request lineTrip
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLineTrip(int id)
        {
            try
            {
                dl.DeleteLineTrip(id);
            }
            catch( DO.LineTripNotFoundException ex)
            {
                throw new BO.LineTripNotFoundException(ex.Id);
            }
        }
        /// <summary>
        /// this method get DO.linetrop and return  BO.lineTrip
        /// </summary>
        /// <param name="doLineTrip"></param>
        /// <returns></returns>
        private BO.LineTrip LineTripDoBoAdapter(DO.LineTrip doLineTrip)
        { 
            return new BO.LineTrip
            {
                Id = doLineTrip.Id,
                LineId = doLineTrip.LineId,
                StartAt = doLineTrip.StartAt
            };
        }
        /// <summary>
        /// this method get BO.linetrop and return  DO.lineTrip
        /// </summary>
        /// <param name="boLineTrip"></param>
        /// <returns></returns>
        private DO.LineTrip LineTripBoDoAdapter(BO.LineTrip boLineTrip)
        {
            return new DO.LineTrip
            {
                Id = boLineTrip.Id,
                LineId = boLineTrip.LineId,
                StartAt = boLineTrip.StartAt
            };
        }
        #endregion
        #region LineTiming
        /// <summary>
        /// this func return IEnumerable of all lineTimings off the required station
        /// </summary>
        /// <param name="code"></param>
        /// <param name="start">this time</param>
        /// <returns></returns>
        public IEnumerable<BO.LineTiming> GetLinesTiming(int code, TimeSpan start)
        {
            
            //group all lineSation by their lineId
            var lineStationsGrouping = from lineStation in dl.GetAllLineStations()
                                       orderby lineStation.LineStationIndex
                                       group lineStation by lineStation.LineId;
           
            return from lsGroup in lineStationsGrouping
                    where lsGroup.Any(x => x.stationCode == code)//check if the request station is in the group
                    let line = GetLine(lsGroup.Key)    
                    let time = calculateTravelTime(line, code)//calculate the travel time from first station to the request station
                    from trip in dl.GetAllLinesTripBy(x => x.LineId == line.Id && x.StartAt <= start).OrderBy(x => x.StartAt)//get all the lineTrip of the specific line that already start their driving
                    let timeNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)//in order to get the accurate time as possible
                    where trip.StartAt + time > timeNow//in order to select only lineTrip who have not yet arrived
                    select new LineTiming
                    {
                        LineCode = line.LineNumber,
                        LastStation = line.LastStationName,
                        TripStart = trip.StartAt,
                        ExpectedTimeTillArrive = trip.StartAt + time - timeNow
                    };


        }
        /// <summary>
        /// calculate the travel time from first station to the request station
        /// </summary>
        /// <param name="line"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private TimeSpan calculateTravelTime(BO.Line line,int code)
        {
            TimeSpan time = new TimeSpan();
            foreach (var item in line.Stations)
            {
                time += item.Time;
                if (item.stationCode == code)
                    break;
            }
            return time;
        }
        
        #endregion
    }
}
