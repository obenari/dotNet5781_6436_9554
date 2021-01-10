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
    internal class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();
        #region Line
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
            //lineBO.Stations.ElementAt(0).Distance = 0;
            //lineBO.Stations.ElementAt(0).Time = new TimeSpan(0);

            //lineBO.Stations = lineBO.Stations.ToList();

            //foreach (var item in lineBO.Stations)
            //{

            //}

            ////collect all the adjacentStations that belong to lineBo
            //lineBO.adjacentStations = from stn in dl.GetAllAdjacentStations()
            //                          let stations= lineBO.Stations.ToList()//save all the LineStations that belong to LineBo in list
            //                          //Now check if LineStations list contain the two stations in the adjacentStations
            //                          where stations.Any(x => x.stationCode == stn.Station1)
            //                                 && stations.Any(x => x.stationCode == stn.Station2)
            //                          select adjacentStationsDoBoAdapter(stn);
            return lineBO;
        }


        public int AddLine(Line line)
        {
            //check if there is information about distance and time between all the stations
            string stationWithoutInformation = "";
            List<BO.LineStation> lineStations = line.Stations.ToList();
            for (int i = 0; i < line.Stations.Count()-1; i++)
            {
                try
                {
                    dl.GetAdjacentStations(lineStations[i].stationCode, lineStations[i + 1].stationCode);
                }
                catch(DO.AdjacentStationsNotFoundException ex)
                {
                    stationWithoutInformation += lineStations[i].stationName + " " + lineStations[i + 1].stationName + "\n";
                }
            }
            if(stationWithoutInformation=="")
            {
                throw new NotEnoughInformationException(stationWithoutInformation);
            }
            //now create DO.lineStation and add to dl
            for (int i=0;i< lineStations.Count();i++)
            {
                DO.LineStation newLineStation=new DO.LineStation//******************************************************************************
               // {
                   // PrevStation=
               // }
            }
            return 1;
        }

        public void DeleteLine(int id, int line)
        {
            try
            {
                dl.DeleteLine(id);
            }
            catch (DO.BusLineNotFoundException ex)
            {

                throw new BO.BusLineNotFoundException(line, id, ex);
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

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region AdjacentStations
        private BO.AdjacentStations adjacentStationsDoBoAdapter(DO.AdjacentStations stnA)
        {
            BO.AdjacentStations adjacentStations = new AdjacentStations();
            adjacentStations.Distance = stnA.Distance;
            adjacentStations.Time = stnA.Time;
            adjacentStations.Station1 = stnA.Station1;
            adjacentStations.Station2 = stnA.Station2;
            return adjacentStations;
        }
        #endregion
        #region LineStation
        private BO.LineStation LineStationDoBoAdapter(DO.LineStation doLineStation)
        {
            BO.LineStation boLineStation = new LineStation();
            boLineStation.stationCode = doLineStation.stationCode;
            //lineStation.PrevStation = lineStn.PrevStation;
            //lineStation.NextStation = lineStn.NextStation;
            DO.Station temp = dl.GetStation(doLineStation.stationCode);
            boLineStation.stationName = temp.Name;
            boLineStation.LineStationIndex = doLineStation.LineStationIndex;
            //if (lineStation.PrevStation == null)//it is the first station
            //{
            //    lineStation.Time = new TimeSpan(0);
            //    lineStation.Distance = 0;
            //}
            //else {
            //    DO.AdjacentStations ads = dl.GetAdjacentStations(lineStn.stationCode, (int)(lineStn.PrevStation));
            //    lineStation.Distance = ads.Distance;
            //    lineStation.Time = ads.Time;
            //}
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
            boBus.Status = (BO.Status)(int)doBus.Status;
            boBus.Fuel = doBus.Fuel;
            boBus.TotalKms = doBus.TotalKms;
            boBus.TotalKmsFromLastTreatment = doBus.TotalKmsFromLastTreatment;
            return boBus;

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
        public void AddBus(BO.Bus bus)
        {

        }
        public void UpdateBus(BO.Bus bus)
        {

        }
        public void UpdateBus(int license, Action<BO.Bus> update)
        {

        }
        public void DeleteBus(int license)
        {
            try
            {
                dl.DeleteBus(license);
            }
            catch (DO.BusLineNotFoundException ex)
            {

                throw new BO.BusNotFoundException(license);
            }
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
                    DO.LineStation nextStation = dl.GetLineStationsBy(s => s.LineStationIndex == doLineStation.LineStationIndex + 1&&s.LineId==lineId);
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
                throw new BO.StationNotFoundException(ex.StationId,doLineStation.stationCode.ToString()+lineId.ToString());
            }
        }
        #endregion 
        #region Station
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from station in dl.GetAllStations()
                   select StationDoBoAdapter(station);
        }

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

            //boStation.ListLines = (from line in dl.GetAllLines()
            //                       from lineStation in dl.GetAllLineStations()
            //                       where lineStation.LineId == line.Id && lineStation.stationCode == boStation.Code
            //                       select makeInformationForStation(line,doStation));
            return boStation;
        }
        private DO.Station StationBoDoAdapter(BO.Station boStation)
        {
            DO.Station doStation = new DO.Station();
            doStation.Code = boStation.Code;
            doStation.Latitude = boStation.Latitude;
            doStation.Longitude = boStation.Longitude;
            doStation.Name = boStation.Name;
            return doStation;
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
                throw new DuplicateStationException(ex.StationName, "", ex);
            }
        }
        public void UpdateStation(Station station)
        {

        }
        public void UpdateStation(int code, Action<BO.Station> update)
        {

        }
        public void DeleteStation(int code)
        {
            try
            {

                //update the lines that their first station is deleted
                var linesWhereFirstStationDeleted = dl.GetAllLinesBy(l => l.FirstStation == code).ToList();
                for (int i=0; i< linesWhereFirstStationDeleted.Count(); i++)
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
                    DO.LineStation lineStationToRemove= dl.GetLineStation(linesWhereLastStationDeleted[i].Id, code);
                    DO.LineStation oneBeforeTheLast = dl.GetLineStationsBy(
                        s => s.LineStationIndex ==lineStationToRemove.LineStationIndex-1 
                        && s.LineId == linesWhereLastStationDeleted[i].Id);
                    linesWhereLastStationDeleted[i].LastStation = oneBeforeTheLast.stationCode;
                    dl.UpdateLine(linesWhereLastStationDeleted[i]);
                }

                //update the indexes of the rest lineStation in the line (item)
                //group all the lineStation by lineId, and than check if each group contatin the 
                //station to remove, and than update the index
                var lineGroup = from lineStation in dl.GetAllLineStations()
                                orderby lineStation.LineStationIndex
                                group lineStation by lineStation.LineId ;
                                
                                
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
                //delete all linesation than belong to the station to remove
                IEnumerable<DO.LineStation> stationsToDelete = dl.GetAllLineStationsBy(s => s.stationCode == code);
                foreach (var item in stationsToDelete)
                {
                    dl.DeleteLineStation(item.LineId, item.stationCode);
                }
                //update the new distance and timebetween the next and prev station
                foreach (var item in stationsToDelete)
                {
                    if (item.NextStation != null && item.PrevStation != null)
                    {
                        DO.AdjacentStations prev = dl.GetAdjacentStations(item.stationCode, (int)item.PrevStation);
                        DO.AdjacentStations next = dl.GetAdjacentStations(item.stationCode, (int)item.NextStation);
                        DO.AdjacentStations newAdjacentStations = new DO.AdjacentStations
                        {
                            Distance = prev.Distance + next.Distance,
                            Time = prev.Time + next.Time
                        };
                        dl.AddAdjacentStations(newAdjacentStations);
                    }
                    if (item.PrevStation != null)
                    {
                        DO.LineStation prevLineStation = dl.GetLineStation(item.LineId, (int)item.PrevStation);
                        prevLineStation.NextStation = item.NextStation;
                        dl.UpdateLineStation(prevLineStation);
                    }
                    if (item.NextStation != null)
                    {
                        DO.LineStation nextLineStation = dl.GetLineStation(item.LineId, (int)item.NextStation);
                        nextLineStation.PrevStation = item.PrevStation;
                        dl.UpdateLineStation(nextLineStation);
                    }
                }
                //remove all AdjacentStations that contain the station to remove
                IEnumerable<DO.AdjacentStations> aStationToDelete = dl.GetAllAdjacentStationsBy(s => s.Station1 == code || s.Station2 == code);
                foreach (var item in aStationToDelete)
                {
                    dl.DeleteAdjacentStations(item.Station1,item.Station2);         
                }
                //delete the request station from dl
                dl.DeleteStation(code);

            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BO.StationNotFoundException(code);
            }
        }
        #endregion
    }
}
