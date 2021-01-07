using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    static class Adapter
    {
     //   IBL bl = BLFactory.GetBL();

        public static  PO.Station POBOAdapter(BO.Station boStation)
        {
            PO.Station poStation = new PO.Station();
            poStation.Code = boStation.Code;
            poStation.Name = boStation.Name;
           // List<BO.InformationForStation> info = boStation.ListLines.ToList();
            if (boStation.ListLines== null)
                poStation.ListLines = null;
            else
            {
                poStation.ListLines = new ObservableCollection<BO.InformationForStation>(boStation.ListLines.ToList());
            }
            return poStation;
        }
        public static PO.BusLine POBOAdapter(BO.Line boLine)//************************
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
        public static PO.LineStation POBOAdapter(BO.LineStation boLineStation)//************************
        {
            PO.LineStation poLineStation = new PO.LineStation();
            poLineStation.stationName = boLineStation.stationName;
            poLineStation.LineStationIndex = boLineStation.LineStationIndex;
            poLineStation.Distance = boLineStation.Distance;
            poLineStation.stationCode = boLineStation.stationCode;
            poLineStation.Time = boLineStation.Time;
            return poLineStation;
        }
    }
}
