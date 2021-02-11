using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO; 
namespace DLAPI
{
    /// <summary>
    /// CRUD Logic:
    /// Create - add new instance
    /// Request - ask for an instance or for a collection
    /// Update - update properties of an instance
    /// Delete - delete an instance
    /// </summary>
    public interface IDL
    {

        #region AdjacentStations
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate);
        AdjacentStations GetAdjacentStations(int code1, int code2);
        void AddAdjacentStations(AdjacentStations station);
        void UpdateAdjacentStations(AdjacentStations station);
        void UpdateAdjacentStations(int code, int code2, Action<AdjacentStations> update); //method that knows to updt specific fields in AdjacentStations
       void DeleteAdjacentStations(int code, int code2);
        bool AdjacentStationsIsExist(int code, int code2);//method that checks if an  AdjacentStations is exist
        #endregion


        #region Bus
        IEnumerable<Bus> GetAllBusses();
        IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate);
        Bus GetBus(int License);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void UpdateBus(int license, Action<Bus> update); //method that knows to updt specific fields in bus
        void DeleteBus(int license);
        #endregion


        #region Line
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        Line GetLine(int id);
        int AddLine(Line line);
        void UpdateLine(Line line);
        void UpdateLine(int id, Action<Line> update); //method that knows to updt specific fields in Line
        void DeleteLine(int id);
        #endregion

        #region LineStation
        IEnumerable<LineStation> GetAllLineStations();
        IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> predicate);
        LineStation GetLineStation(int line, int numSation);
        void AddLineStation(LineStation lineStation);
        void UpdateLineStation(LineStation lineStation);
        void UpdateLineStation(int line, int numSation, Action<LineStation> update); //method that knows to updt specific fields in LineStation
        void DeleteLineStation(int lineId, int numSation);
        LineStation GetLineStationsBy(Predicate<LineStation> predicate);
        #endregion

        #region LineTrip
        IEnumerable<LineTrip> GetAllLinesTrip();
        IEnumerable<LineTrip> GetAllLinesTripBy(Predicate<LineTrip> predicate);
        LineTrip GetLineTrip(int id);
        int AddLineTrip(LineTrip lineTrip);
        void DeleteLineTrip(int id);
        bool IsExistLinetrip(DO.LineTrip lineTrip);//method that checks if a lineTrip is exist
        #endregion

        #region Station
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate);
        Station GetStation(int code);
        int AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int code, Action<Station> update); //method that knows to updt specific fields in Station
        void DeleteStation(int code);
        #endregion

       
    }
}
