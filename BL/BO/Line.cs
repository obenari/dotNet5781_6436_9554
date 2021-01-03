using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    /// <summary>
    /// a class that defines a logical bus line
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Runner number of specific line (it may be a few same bus line together, so it
        /// have to be a uniqe number of each one)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the line number of the bus
        /// </summary>
        public int LineNumber { get; set; }
        /// <summary>
        /// enum that contain the area of the bus
        /// </summary>
        public Areas Area { get; set; }
        /// <summary>
        /// the name of the first station
        /// </summary>
        public string FirstStationName { get; set; }
        /// <summary>
        /// the name of the last station
        /// </summary>
        public string LastStationName { get; set; }
        public override string ToString()
        {
            return string.Format($"Line: {LineNumber}, from {FirstStationName} to {LastStationName} in {Area}");
        }
        /// <summary>
        /// all the line stations in the line
        /// </summary>
        public IEnumerable<LineStation> Stations { get; set; }
        ///// <summary>
        ///// list of the Adjacent Stations in the specific line
        ///// </summary>
        //public IEnumerable<AdjacentStations> adjacentStations { get; set; }

    }
}
