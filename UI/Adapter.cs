using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// This class gets BO object and return a PO object 
    /// </summary>
    static class Adapter
    {

        /// <summary>
        /// the method get a BO.Station and return a PO.Station and the opposite dirction
        /// </summary>
        /// <param name="boStation"></param>
        /// <returns></returns>
        public static  PO.Station POBOAdapter(BO.Station boStation)
        {
            PO.Station poStation = new PO.Station();
            poStation.Code = boStation.Code;
            poStation.Name = boStation.Name;
            if (boStation.ListLines== null)
                poStation.ListLines = null;
            else
            {
                poStation.ListLines = new ObservableCollection<BO.InformationForStation>(boStation.ListLines.ToList());
            }
            return poStation;
        }
        /// <summary>
        /// the method get a BO.BusLine and return a PO.BusLine
        /// </summary>
        /// <param name="boLine"></param>
        /// <returns></returns>
        public static PO.BusLine POBOAdapter(BO.Line boLine)
        {
            
            PO.BusLine poLine = new PO.BusLine();
            poLine.LineNumber = boLine.LineNumber;
            poLine.Area = boLine.Area;
            poLine.FirstStation = boLine.FirstStationName;
            poLine.LastStation = boLine.LastStationName;
            poLine.Id = boLine.Id;
            poLine.Stations = new ObservableCollection<PO.LineStation>(boLine.Stations.ToList().ConvertAll(lstn => Adapter.POBOAdapter(lstn)));
            return poLine;
        }
        /// <summary>
        /// the method get a BO.LineStation and return a PO.LineStation
        /// </summary>
        /// <param name="boLineStation"></param>
        /// <returns></returns>
        public static PO.LineStation POBOAdapter(BO.LineStation boLineStation)
        {
            PO.LineStation poLineStation = new PO.LineStation();
            poLineStation.StationName = boLineStation.stationName;
            poLineStation.Distance = boLineStation.Distance;
            poLineStation.StationCode = boLineStation.stationCode;
            poLineStation.Time = boLineStation.Time;
            return poLineStation;
        }
        /// <summary>
        /// the method get a BO.Bus and return a PO.Bus
        /// </summary>
        /// <param name="boBus"></param>
        /// <returns></returns>
        public static PO.Bus POBOAdapter(BO.Bus boBus)
        {
            PO.Bus poBus = new PO.Bus();
            poBus.TotalKms = boBus.TotalKms;
            poBus.TotalKmsFromLastTreatment = boBus.TotalKmsFromLastTreatment;
            poBus.Status = boBus.Status;
            poBus.DateOfTreatment = boBus.DateOfTreatment;
            poBus.Fuel = boBus.Fuel;
            poBus.License = boBus.License;
            poBus.StartOfWork = boBus.StartOfWork;

            return poBus;
        }
        /// <summary>
        /// the method get a PO.Bus and return a BO.Bus
        /// </summary>
        /// <param name="poBus"></param>
        /// <returns></returns>
        public static BO.Bus BOPOAdapter(PO.Bus poBus)
        {
            BO.Bus boBus = new BO.Bus();
            boBus.TotalKms = poBus.TotalKms;
            boBus.TotalKmsFromLastTreatment = poBus.TotalKmsFromLastTreatment;
            boBus.Status = poBus.Status;
            boBus.DateOfTreatment = poBus.DateOfTreatment;
            boBus.Fuel = poBus.Fuel;
            //remove "-" from the lincense
            int index = poBus.License.IndexOf("-");
            string str= poBus.License.Remove(index,1);
            index =str.IndexOf("-");
            str = str.Remove(index,1);
            boBus.License = str;
            boBus.StartOfWork = poBus.StartOfWork;

            return boBus;
        }
        /// <summary>
        /// the method get a BO.LineStation and return a PO.LineStation
        /// </summary>
        /// <param name="poLineStation"></param>
        /// <returns></returns>
        public static BO.LineStation BOPOAdapter(PO.LineStation poLineStation)
        {
            BO.LineStation boLineStation = new BO.LineStation();
            boLineStation.stationName = poLineStation.StationName;
            boLineStation.Distance = poLineStation.Distance;
            boLineStation.stationCode = poLineStation.StationCode;
            boLineStation.Time = poLineStation.Time;
            return boLineStation;
        }
        /// <summary>
        /// the method get a BO.Line and return a PO.Line
        /// </summary>
        /// <param name="poLine"></param>
        /// <returns></returns>
        public static BO.Line BOPOAdapter(PO.BusLine poLine)
        {
            BO.Line boLine = new BO.Line();
            boLine.LineNumber = poLine.LineNumber;
            boLine.Area = poLine.Area;
            boLine.FirstStationName = poLine.FirstStation;
            boLine.LastStationName = poLine.LastStation;
            boLine.Id = poLine.Id;
            boLine.Stations = new List<BO.LineStation>(poLine.Stations.ToList().ConvertAll(lstn => Adapter.BOPOAdapter(lstn)));
            return boLine;
        }
    }
}
