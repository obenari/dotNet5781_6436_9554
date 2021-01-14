﻿using System;
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
           //     FirstStation = lineStations[0].stationCode,
              //  LastStation = lineStations[lineStations.Count() - 1].stationCode,
                IsDeleted = false
            };
            line.Id = dl.AddLine(lineToadd);
            //now create DO.lineStation and add to dl
            DO.LineStation firstLineStation = new DO.LineStation
            {//the first station
                IsDeleted = false,
              //  PrevStation = null,
               // NextStation = lineStations[1].stationCode,
                stationCode = lineStations[0].stationCode,
                LineId = line.Id,
                LineStationIndex = 0
            };
            dl.AddLineStation(firstLineStation);
            //the last station
            DO.LineStation lastLineStation = new DO.LineStation
            {//the first station
                IsDeleted = false,
              //  PrevStation = lineStations[lineStations.Count() - 2].stationCode,
              //  NextStation = null,
                stationCode = lineStations[lineStations.Count() - 1].stationCode,
                LineId = line.Id,
                LineStationIndex = lineStations.Count() - 1
            };
            dl.AddLineStation(lastLineStation);
            for (int i = 1; i < lineStations.Count() - 1; i++)
            {
                DO.LineStation newLineStation = new DO.LineStation
                {
                //    PrevStation = lineStations[i - 1].stationCode,
                  //  NextStation = lineStations[i + 1].stationCode,
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
          
        

        public void DeleteLine(int id, int line)//*************לא צריך את מספר הקו
        {
            try
            {
                dl.DeleteLine(id);
                var lineStations = dl.GetAllLineStationsBy(s => s.LineId == id).ToList();///delete all the line stations
                for (int i = 0; i < lineStations.Count(); i++)
                {
                    dl.DeleteLineStation(id, lineStations[i].stationCode);
                }
            }
            catch (DO.BusLineNotFoundException ex)
            {

                throw new BO.BusLineNotFoundException(line, id, ex);
            }
            catch (DO.LineStationNotFoundException ex)
            {

                throw new BO.LineStationNotFoundException(line, id,"",ex);
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
                if (ls!=null)//if the line station is exist,only update the index
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
                        IsDeleted=false
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

        //public void UpdateLine(int id, Action<Line> update)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
        #region AdjacentStations///אולי למחוק
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
        private DO.Bus BusBoDoAdapter(BO.Bus boBus)
        {
            DO.Bus doBus = new DO.Bus();
            while (boBus.License[0] == '0')
                boBus.License = boBus.License.Remove(0, 1);
            doBus.License = int.Parse(boBus.License);
            doBus.DateOfTreatment = boBus.DateOfTreatment;
            doBus.StartOfWork = boBus.StartOfWork;
            doBus.Status = (DO.Status)(int)boBus.Status;
            doBus.Fuel = boBus.Fuel;
            doBus.TotalKms = boBus.TotalKms;
            doBus.TotalKmsFromLastTreatment = boBus.TotalKmsFromLastTreatment;
            return doBus;

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
        public void AddBus(BO.Bus boBus)
        {
            try
            {
             
                if (boBus.License.Length < 7 )
                    throw new LicenseFormatException(int.Parse(boBus.License),"");
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
        public void UpdateBus(BO.Bus bus)
        {
            try
            {
                dl.UpdateBus(BusBoDoAdapter(bus));
            }
            catch(DO.BusNotFoundException ex)
            {
                throw new BusNotFoundException(ex.License.ToString(), "", ex);
            }
        }
        public void UpdateBus(int license, Action<BO.Bus> update)////אולי למחוק
        {

        }
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
                throw new BO.BusNotFoundException(boBus.License,"",ex);
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
                var lineStationsToDelete = dl.GetAllLineStationsBy(s => s.stationCode == code).ToList();
               
                //update the new distance and time between the next and prev station
                for (int i=0;i< lineStationsToDelete.Count();i++)
                {
                    var item = lineStationsToDelete[i];//in order to shrink the code
                    // in order to know if it is the last station,we need to get all the
                    // linestations that belong to it
                    var allLinestations = dl.GetAllLineStationsBy(s => s.LineId == item.LineId).OrderBy(s => s.LineStationIndex).ToList();
                    if (item.LineStationIndex!=allLinestations.Count()-1&&item.LineStationIndex!=0  )
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
                        catch(DuplicateAdjacentStationsException ex) { }
                    }
                    //if (item.PrevStation != null)
                    //{
                    //    DO.LineStation prevLineStation = dl.GetLineStation(item.LineId, (int)item.PrevStation);
                    //    prevLineStation.NextStation = item.NextStation;
                    //    dl.UpdateLineStation(prevLineStation);
                    //}
                    //if (item.NextStation != null)
                    //{
                    //    DO.LineStation nextLineStation = dl.GetLineStation(item.LineId, (int)item.NextStation);
                    //    nextLineStation.PrevStation = item.PrevStation;
                    //    dl.UpdateLineStation(nextLineStation);
                    //}
                }
               
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
               
                //remove all AdjacentStations that contain the station to remove
                IEnumerable<DO.AdjacentStations> aStationToDelete = dl.GetAllAdjacentStationsBy(s => s.Station1 == code || s.Station2 == code);
                foreach (var item in aStationToDelete)
                {
                    dl.DeleteAdjacentStations(item.Station1,item.Station2);         
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
        #endregion
    }
}
