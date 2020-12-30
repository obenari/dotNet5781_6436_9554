﻿using System;
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
        public int StationId;
        public DuplicateStationException(int code) : base() => StationId = code;
        public DuplicateStationException(int code, string message) :
            base(message) => StationId = code;
        public DuplicateStationException(int code, string message, Exception innerException) :
            base(message, innerException) => StationId = code;
        public override string ToString() => base.ToString() + $", The station: {StationId} is already exist";
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

}