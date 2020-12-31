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
            lineBO.LineNumber =lineDO.LineNumber ;
            lineBO.Id = lineDO.Id;
            lineBO.Area = (BO.Areas)(int)lineDO.Area;
            //get the first and the last stations of lineDO in order to get their name' to put in lineBo
            DO.Station station1 = dl.GetStation(lineDO.FirstStation);
            DO.Station station2 = dl.GetStation(lineDO.LastStation);
            lineBO.FirstStationName =station1.Name;
            lineBO.LastStationName = station2.Name;

            //collect all the lineStation that belong to lineBo
            lineBO.Stations = from stn in dl.GetAllLineStationsBy(item => item.LineId == lineDO.Id)
                              orderby stn.LineStationIndex
                              select LineStationDoBoAdapter(stn);//convert from DO.lineStation to BO.lineStation

            //collect all the adjacentStations that belong to lineBo
            lineBO.adjacentStations = from stn in dl.GetAllAdjacentStations()
                                      let stations= lineBO.Stations.ToList()//save all the LineStations that belong to LineBo in list
                                      //Now check if LineStations list contain the two stations in the adjacentStations
                                      where stations.Any(x => x.stationCode == stn.Station1)
                                             && stations.Any(x => x.stationCode == stn.Station2)
                                      select adjacentStationsDoBoAdapter(stn);
            return lineBO;
        }

       
        public void AddLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void DeleteLine(int id,int line)
        {
            try
            {
                dl.DeleteLine(id);
            }
            catch (DO.BusLineNotFoundException ex)
            {

                throw new BO.BusLineNotFoundException(line, id);
            }

        }

        public IEnumerable<Line> GetAllLines()
        {
            return from line in dl.GetAllLines()
                   select LineDoBoAdapter(line);
        }

        public IEnumerable<Line> GetAllLinesBy(Predicate<BO.Line> predicate)
        {
            return from line in dl.GetAllLines()
                   where predicate(LineDoBoAdapter(line))
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
        private BO.LineStation LineStationDoBoAdapter(DO.LineStation lineStn)
        {
            BO.LineStation lineStation = new LineStation();
            lineStation.LineId = lineStn.LineId ;
            lineStation.stationCode = lineStn.stationCode ;
            lineStation.PrevStation = lineStn.PrevStation ;
            lineStation.NextStation = lineStn.NextStation ;
            lineStation.LineStationIndex = lineStn.LineStationIndex ;
            return lineStation;


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
            BO.Bus  boBus= new Bus();
            string licenseStr = doBus.License.ToString();
            if (licenseStr.Length <= 7)
            {
                string help="";
                if (doBus.StartOfWork.Year >= 2018)//the license number need 8 digit
                {
                    for (int i = 0; i < 8- licenseStr.Length; i++)
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
               licenseStr= licenseStr.Insert(0, help);

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
                   let b=BusDoBoAdapter(bus)
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

        #region Station
       public IEnumerable<BO.Station> GetAllStations()
        {
            return from station in dl.GetAllLineStations()
                   select StationDoBoAdapter(station);
        }

        private BO.Station StationDoBoAdapter(DO.LineStation station)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Station> GetAllStationsBy(Predicate<Station> predicate)
        {

        }
        public BO.Station GetStation(int code)
        {
            DO.Station station = dl.GetStation(code);
            return StationDoBoAdapter(station);
        }
        public void AddStation(BO.Station station)
        {

        }
        public  void UpdateStation(Station station)
        {

        }
        public void UpdateStation(int code, Action<BO.Station> update)
        {

        }
        public void DeleteStation(int code)
        {
            try
            {
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
