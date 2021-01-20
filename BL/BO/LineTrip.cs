using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// a class that defines bus line in trip
    /// </summary>
    public class LineTrip
    {
        /// <summary>
        ///Runner number of  line in trip
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the number of the line in trip
        /// </summary>
        public int LineId { get; set; }
        /// <summary>
        /// the time of start working
        /// </summary>
        public TimeSpan StartAt { get; set; }
       

    }
}
