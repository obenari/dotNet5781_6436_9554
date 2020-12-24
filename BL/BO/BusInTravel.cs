using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A class that defines a  bus in travel
    /// </summary>
    public class BusInTravel
    {


        /// <summary>
        /// Runner number of specific trip
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The license number of the physical bus that in travel
        /// </summary>
        public string LicenseNum { get; set; }
        /// <summary>
        /// tbe line number of the bus
        /// </summary>
        public int Line { get; set; }
        /// <summary>
        /// the planned time to take off 
        /// </summary>
        public TimeSpan PlannedTakeOff { get; set; }
        /// <summary>
        /// the actual time the bus take off 
        /// </summary>
        public TimeSpan ActualTakeOff { get; set; }
        /// <summary>
        /// the prev station that the bus passed by
        /// </summary>
        public int PrevStation { get; set; }
        /// <summary>
        /// the time that passed from the prev station
        /// </summary>
        public TimeSpan PrevStationAt { get; set; }
        /// <summary>
        /// the time until the bus will come to the next station
        /// </summary>
        public TimeSpan NextStationAt { get; set; }



    }
}
