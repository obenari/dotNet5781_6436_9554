using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{

    public interface IBL
    {

        #region Bus
        IEnumerable<BO.Bus> GetAllBusses();
        IEnumerable<BO.Bus> GetAllBussesBy(Predicate<BO.Bus> predicate);
        BO.Bus GetBus(int License);
        void AddBus(BO.Bus bus);
        void UpdateBus(BO.Bus bus);
        void DeleteBus(BO.Bus bus);
        void RefuelBus(BO.Bus bus);
        bool IsReadyToDriving(BO.Bus bus,int km);
        void TreatmentBus(BO.Bus bus);
        #endregion

        #region Line
        IEnumerable<BO.Line> GetAllLines();
        IEnumerable<BO.Line> GetAllLinesByArea(BO.Areas area);
        BO.Line GetLine(int id);
        int AddLine(BO.Line line);
        void UpdateLine(BO.Line line);
      //  void UpdateLine(int id, Action<BO.Line> update); //method that knows to updt specific fields in Line
        void DeleteLine(int id);
        #endregion

        #region LineTrip
        IEnumerable<BO.LineTrip> GetAllLinesTrip();
        IEnumerable<BO.LineTrip> GetAllLinesTripBy(Predicate<BO.LineTrip> predicate);
        BO.LineTrip GetLineTrip(int id);
        void AddLineTrip(BO.LineTrip lineTrip);
        void AddLineTrip(int lineId, TimeSpan start, TimeSpan finish, TimeSpan frequency);
        void DeleteLineTrip(int id);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        IEnumerable<BO.Station> GetAllStationsBy(Predicate<BO.Station> predicate);
        BO.Station GetStation(int code);
        int AddStation(BO.Station station);
      
        void DeleteStation(int code);
        #endregion

        #region LineTiming
        IEnumerable<BO.LineTiming> GetLinesTiming(int code, TimeSpan start);
        #endregion
      

       
    }
}
