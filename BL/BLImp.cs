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

        public void DeleteLine(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            throw new NotImplementedException();
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
    }
}
