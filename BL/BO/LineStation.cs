using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    /// <summary>
    /// a class that defines a logical bus station of spacific line
    /// </summary>
    public class LineStation
    {
        /// <summary>
        /// the number of line 
        /// </summary>
        public int LineId { get; set; }
        /// <summary>
        /// the code of the physical station
        /// </summary>
        public int stationCode { get; set; }
        ///// <summary>
        ///// the name of the physical station//************************************8
        ///// </summary>
        ///

        //public string stationName { get; set; }
        /// <summary>
        /// the index of the station in the spacific line
        /// </summary>
        public int LineStationIndex { get; set; }
        /// <summary>
        /// the number of the prev station
        /// if it's the first station, this field will be null 
        /// </summary>
        public int? PrevStation { get; set; }
        /// <summary>
        /// the number of the next station
        /// if it's the last station, this field will be null 
        /// </summary>
        public int? NextStation { get; set; }
        
    }
}
