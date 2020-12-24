using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    /// <summary>
    /// A class that defines two stations one by one on a specific line
    /// </summary>
    public class AdjacentStations
    {
        /// <summary>
        /// The ID of the first station
        /// </summary>
        public int Station1 { get; set; }
        /// <summary>
        /// The ID of the second station
        /// </summary>
        public int Station2 { get; set; }
        /// <summary>
        /// the distance between the two stations
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// the average travel time between the two stations
        /// </summary>
        public TimeSpan Time { get; set; }


    }
}
