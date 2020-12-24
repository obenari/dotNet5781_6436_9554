using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a class that defines a user in the system
    /// </summary>
    public class User
    {
        /// <summary>
        /// the user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// the user pasword
        /// </summary>
        public string Pasword { get; set; }
        /// <summary>
        /// a boolean field that check if the user is a worker in the comapny or a passenger 
        /// </summary>
        public bool Admin { get; set; }


    }
}
