using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
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
        public int LineId;
        public BusLineNotFoundException(int line) : base() => LineId = line;
        public BusLineNotFoundException(int line, string message) :
            base(message) => LineId = line;
        public BusLineNotFoundException(int line, string message, Exception innerException) :
            base(message, innerException) => LineId = line;
        public override string ToString() => base.ToString() + $", The bus: {LineId} is not exist";
    }
    public class DuplicateBusLineException : Exception
    {
        public int LineId;
        public DuplicateBusLineException(int line) : base() => LineId = line;
        public DuplicateBusLineException(int line, string message) :
            base(message) => LineId = line;
        public DuplicateBusLineException(int line, string message, Exception innerException) :
            base(message, innerException) => LineId = line;
        public override string ToString() => base.ToString() + $", The bus: {LineId} is already exist";
    }
    #endregion
    #region station

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

        public LineStationNotFoundException(int code, int line,string message) : base(message)
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
    #region xml
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
    #endregion
    #region lineTrip
    public class LineTripNotFoundException : Exception
    {
        public int Id;
        public LineTripNotFoundException(int id) : base() => Id = id;
        public LineTripNotFoundException(int id, string message) :
            base(message) => Id = id;
        public LineTripNotFoundException(int id, string message, Exception innerException) :
            base(message, innerException) => Id = id;
    }
    public class DuplicateLineTripException : Exception
    {
        public int Id;
        public DuplicateLineTripException(int id) : base() => Id = id;
        public DuplicateLineTripException(int id, string message) :
            base(message) => Id = id;
        public DuplicateLineTripException(int id, string message, Exception innerException) :
            base(message, innerException) => Id = id;
    }
    #endregion
}
