using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a class that defines a trip for spacific user
    /// </summary>
    public class Trip
    {
        /// <summary>
        ///  Runner number of  the specific trip
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the number of the line in the trip
        /// </summary>
        public int LineId { get; set; }
        /// <summary>
        /// the user's first station   
        /// </summary>
        public int Firststation { get; set; }
        /// <summary>
        /// the user's last station   
        /// </summary>
        public int Lasttstation { get; set; }
        /// <summary>
        /// the time the user cmo in
        /// </summary>
        public TimeSpan InAt { get; set; }
        /// <summary>
        /// the time the user go out
        /// </summary>
        public TimeSpan OutAt { get; set; }
        /// <summary>
        /// the name of the user in the trip
        /// </summary>
        public string UserName { get; set; }

    }
}
