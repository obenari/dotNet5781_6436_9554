using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BLApi
{
    public interface IBL
    {
        #region AdjacentStations
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate);
        AdjacentStations GetAdjacentStations(int code1, int code2);
        void AddAdjacentStations(AdjacentStations station);
        void UpdateAdjacentStations(AdjacentStations station);
        void UpdateAdjacentStations(int code, int code2, Action<AdjacentStations> update); //method that knows to updt specific fields in AdjacentStations
        void DeleteAdjacentStations(int code, int code2);
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

        #region BusInTravel
        //IEnumerable<Bus> GetAllBusses();
        IEnumerable<BusInTravel> GetAllBussesIntravelBy(Predicate<BusInTravel> predicate);
        BusInTravel GetBusInTravel(int id, int licensenum, int line);
        void AddBusIntravel(BusInTravel busInTravel);
        void UpdateBusInTtavel(BusInTravel busInTravel);
        void UpdateBusIntravel(int id, int licensenum, int line, Action<BusInTravel> update); //method that knows to updt specific fields in busInTravel
        void DeleteBusInTravel(int id, int licensenum, int line);
        #endregion

        #region Line
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        Line GetLine(int id);
        void AddLine(Line line);
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
        void DeleteLineStation(int line, int numSation);
        #endregion

        #region LineTrip
        IEnumerable<LineTrip> GetAllLinesTrip();
        IEnumerable<LineTrip> GetAllLinesTripBy(Predicate<LineTrip> predicate);
        LineTrip GetLineTrip(int id, TimeSpan start);
        void AddLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(int id, TimeSpan start, Action<LineTrip> update); //method that knows to updt specific fields in LineTrip
        void DeleteLineTrip(int id, TimeSpan start);
        #endregion

        #region Station
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate);
        Station GetStation(int code);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int code, Action<Station> update); //method that knows to updt specific fields in Station
        void DeleteStation(int code);
        #endregion

        #region Trip
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> predicate);
        Trip GetTrip(int id);
        void AddTrip(Trip trip);
        void UpdateTrip(Trip trip);
        void UpdateTrip(int id, Action<Trip> update); //method that knows to updt specific fields in Trip
        void DeleteTrip(int id);
        #endregion

        #region User
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersBy(Predicate<User> predicate);
        User GetUser(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void UpdateUser(int id, Action<User> update); //method that knows to updt specific fields in User
        void DeleteUser(int id);
        #endregion
    } 
}
