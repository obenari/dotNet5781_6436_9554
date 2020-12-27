using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// A class that defines a physical bus/
    /// </summary>
    public class Bus
    {

        /// <summary>
        /// The license number of the bus
        /// </summary>
        public int License { get; set; }
        /// <summary>
        /// the date  start of working
        /// </summary>
        public DateTime StartOfWork { get; set; }
        /// <summary>
        /// The date of the last treatment
        /// </summary>
        public DateTime DateOfTreatment { get; set; }
        /// <summary>
        /// the mileage
        /// </summary>
        public int TotalKms { get; set; }
        /// <summary>
        /// How many kilometers left the bus could drive before refuel
        /// </summary>
        public int Fuel { get; set; }
        /// <summary>
        /// enum that contain the status of the bus (redy, intreatment etc..)
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// the total kilometers from the last treartment
        /// </summary>
        public int TotalKmsFromLastTreatment { get; set; }
        
        public override string ToString()
        {
            return this.ToString();
        }
        /// <summary>
        /// this field return false when the argument is not deleted
        /// </summary>
        public bool IsDeleted { get; set; }


    }
}
