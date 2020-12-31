using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// a class that defines a physical station
    /// </summary>
    public class Station
    {
        /// <summary>
        /// the station number
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// the longitude of the station
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// the latitude of the station
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// the name of the station
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// list of the lines that passing by the specific station
        /// </summary>
        public IEnumerable<Line> ListLines { get; set; }


    }
}
