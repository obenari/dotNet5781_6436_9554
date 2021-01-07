﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// This class  shows only the importent information about single Busline that pasing by the specific station
    /// </summary>
  public  class InformationForStation
    {
        public int LineNumber { get; set; }
        public string FirstStation { get; set; }
        public string LastStation { get; set; }
        /// <summary>
        /// the name of the next station of the  specific station, in the current line
        /// </summary>
        public string NextStation { get; set; }

    }
}
