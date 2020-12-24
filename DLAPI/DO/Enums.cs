using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    //this file contain all the enums for DO

    /// <summary>
    /// all the status of the bus
    /// </summary>
    public enum Status
    {
        Driving, Ready, Refueling, Treatment,Dangerous
    }
    /// <summary>
    /// all the areas that bus could passed by
    /// </summary>
    public enum Areas
    {
        General, North, South, Center, Jerusalem
    }
}
