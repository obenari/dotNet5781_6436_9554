using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DS;

namespace DL
{
    internal class DLObject : IDL
    {
        static readonly DLObject instance = new DLObject();
        // The public Instance property to use 
        public static DLObject Instance { get { return instance; } }

        // Explicit static constructor to ensure instance initialization
        // is done just before first usage
        static DLObject() { }

        DLObject() { } // default => private


    }
}
