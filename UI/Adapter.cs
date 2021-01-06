using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    static class Adapter
    {
      public static  PO.Station POBOAdapter(BO.Station boStation)
        {
            PO.Station poStation = new PO.Station();
            poStation.Code = boStation.Code;
            poStation.Name = boStation.Name;

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



            return poLine;
        }

    }
}
