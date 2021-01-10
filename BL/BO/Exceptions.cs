using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    #region bus
    public class BusNotFoundException : Exception
    {
        public int License;
        public BusNotFoundException(int license) : base() => License = license;
        public BusNotFoundException(int license, string message) :
            base(message) => License = license;
        public BusNotFoundException(int license, string message, Exception innerException) :
            base(message, innerException) => License = license;
        public override string ToString() => base.ToString() + $", The bus: {License} is not exist";
    }
    public class DuplicateBusException : Exception
    {
        public int License;
        public DuplicateBusException(int license) : base() => License = license;
        public DuplicateBusException(int license, string message) :
            base(message) => License = license;
        public DuplicateBusException(int license, string message, Exception innerException) :
            base(message, innerException) => License = license;
        public override string ToString() => base.ToString() + $", The bus: {License} is already exist";
    }
    #endregion
    #region line
    public class BusLineNotFoundException : Exception
    {
        public int Line;
        public int Id;
        public BusLineNotFoundException(int line, int id) : base()
        {
            Line = line;
            Id = id;
        }
        public BusLineNotFoundException(int line, int id, string message) : base(message)
        {
            Line = line;
            Id = id;
        }
        public BusLineNotFoundException(int line, int id, Exception innerException,string message="" ) : base(message, innerException)
        {
            Line = line;
            Id = id;
        }
        public override string ToString() => base.ToString() + $", The bus: {Line} is not exist";
    }
    public class DuplicateBusLineException : Exception
    {
        public int Line;
        public Areas Area;
        public DuplicateBusLineException(int line, Areas area) : base()
        {
            Line = line;
            Area = area;
        }
        public DuplicateBusLineException(int line, Areas area, string message) :
            base(message)
        {
            Line = line;
            Area = area;
        }
        public DuplicateBusLineException(int line, Areas area, Exception innerException, string message) :
            base(message, innerException)
        {
            Line = line;
            Area = area;
        }
        public override string ToString() => base.ToString() + $", The bus: {Line} is already exist";
    }
    public class NotEnoughInformationException : Exception
    {
        public NotEnoughInformationException() : base() { }
        
        public NotEnoughInformationException(string message) : base(message) { }
        
        public NotEnoughInformationException( Exception innerException, string message = "") : base(message, innerException) { }
        
       
    }
    #endregion
    #region station

    public class StationNotFoundException : Exception
    {
        public int StationId;
        public StationNotFoundException(int code) : base() => StationId = code;
        public StationNotFoundException(int code, string message) :
            base(message) => StationId = code;
        public StationNotFoundException(int code, string message, Exception innerException) :
            base(message, innerException) => StationId = code;
        public override string ToString() => base.ToString() + $", The station: {StationId} is not exist";
    }
    public class DuplicateStationException : Exception
    {
        public string StationName;
        public DuplicateStationException(string code) : base() => StationName = code;
        public DuplicateStationException(string code, string message) :
            base(message) => StationName = code;
        public DuplicateStationException(string code, string message, Exception innerException) :
            base(message, innerException) => StationName = code;
        public override string ToString() => base.ToString() + $", The station: {StationName} is already exist";
    }
    public class OutOfLongitudeIsraelLimitException : Exception
    {
        double Longitude;
        public OutOfLongitudeIsraelLimitException(double lon) : base() => Longitude = lon;
        public OutOfLongitudeIsraelLimitException(double lon, string message) :
            base(message) => Longitude = lon;
        public OutOfLongitudeIsraelLimitException(double lon, string message, Exception innerException) :
            base(message, innerException) => Longitude = lon;
        public override string ToString() => base.ToString() + $", The longitude: {Longitude} is not in Israel limits";
    }
    public class OutOfLatitudeIsraelLimitException : Exception
    {
        double Latitude;
        public OutOfLatitudeIsraelLimitException(double lat) : base() => Latitude = lat;
        public OutOfLatitudeIsraelLimitException(double lat, string message) :
            base(message) => Latitude = lat;
        public OutOfLatitudeIsraelLimitException(double lat, string message, Exception innerException) :
            base(message, innerException) => Latitude = lat;
        public override string ToString() => base.ToString() + $", The longitude: {Latitude} is not in Israel limits";
    }
    #endregion
    #region LineStation

    public class LineStationNotFoundException : Exception
    {
        public int StationId;
        public int LineId;
        public LineStationNotFoundException(int code, int line) : base()
        {
            StationId = code;
            LineId = line;
        }

        public LineStationNotFoundException(int code, int line, string message) : base(message)
        {
            StationId = code;
            LineId = line;
        }
        public LineStationNotFoundException(int code, int line, string message, Exception innerException) :
            base(message, innerException)
        {
            StationId = code;
            LineId = line;
        }
        public override string ToString() => base.ToString() + $", The station: {StationId} is not exist";
    }
    public class DuplicateLineStationException : Exception
    {
        public int StationId;
        public int LineId;
        public DuplicateLineStationException(int code, int line) : base()
        {
            StationId = code;
            LineId = line;
        }

        public DuplicateLineStationException(int code, int line, string message) : base(message)
        {
            StationId = code;
            LineId = line;
        }
        public DuplicateLineStationException(int code, int line, string message, Exception innerException) :
            base(message, innerException)
        {
            StationId = code;
            LineId = line;
        }
        public override string ToString() => base.ToString() + $", The station: {StationId} is not exist";
    }
    #endregion
    #region AdjacentStations

    public class AdjacentStationsNotFoundException : Exception
    {
        public int StationId1;
        public int StationId2;
        public AdjacentStationsNotFoundException(int code1, int code2) : base()
        {
            StationId1 = code1;
            StationId2 = code2;
        }

        public AdjacentStationsNotFoundException(int code1, int code2, string message) : base(message)
        {
            StationId1 = code1;
            StationId2 = code2;
        }
        public AdjacentStationsNotFoundException(int code1, int code2, string message, Exception innerException) :
            base(message, innerException)
        {
            StationId1 = code1;
            StationId2 = code2;
        }
        public override string ToString() => base.ToString() + $", The information about the two station: {StationId1},{StationId2} is not exist";
    }
    public class DuplicateAdjacentStationsException : Exception
    {

        public int StationId1;
        public int StationId2;
        public DuplicateAdjacentStationsException(int code1, int code2) : base()
        {
            StationId1 = code1;
            StationId2 = code2;
        }

        public DuplicateAdjacentStationsException(int code1, int code2, string message) : base(message)
        {
            StationId1 = code1;
            StationId2 = code2;
        }
        public DuplicateAdjacentStationsException(int code1, int code2, string message, Exception innerException) :
            base(message, innerException)
        {
            StationId1 = code1;
            StationId2 = code2;
        }
        public override string ToString() => base.ToString() + $", The information about the two station: {StationId1},{StationId2} is already exist";
    }
    #endregion


}
