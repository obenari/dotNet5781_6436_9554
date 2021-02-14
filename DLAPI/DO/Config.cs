using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
  public static   class Config
    {
        //for running numbers
         static int lineID = 10;
        public static int LineID => ++lineID;
        static int stationCode = 50;
        public static int StationCode => ++stationCode;
        
        //for consts
        public const int MAX_LICENSE_NUM = 8;
        /// <summary>
        /// the limit of Israel country
        /// </summary>
        public  const double MIN_LAT = 31;
        public const double MAX_LAT = 33.3;
        public const double MIN_LON = 34.3;
        public const double MAX_LON = 35.5;
        /// <summary>
        /// the size of full tank
        /// </summary>
        public const int FullTank = 1200;
    }
}
