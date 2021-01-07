using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> ListBusses;
        public static List<Station> ListStations;
        public static List<Line> ListLines;
        public static List<LineStation> ListLineStations;
        public static List<AdjacentStations> ListTwoAdjacentStations;





        static DataSource()
        {
            InitAllList();
        }
        static void InitAllList()
        {
            ListStations = new List<Station>
            {
#region jerusalem
                
                new Station
                {
                    Code =1,
                    Latitude = 31.793096  ,
                    Longitude = 35.207412 ,
                    Name ="תכלת מרדכי/ארגמן",
                    IsDeleted = false
                },
               new Station
               {
                   Code =2,
                   Latitude = 31.771179  ,
                   Longitude =35.181133  ,
                   Name ="עין כרם/שדרות הרצל",
                   IsDeleted = false
               },
               new Station
               {
                   Code =3,
                   Latitude = 31.759032  ,
                   Longitude =35.208696  ,
                   Name ="בן זכאי/דוסתאי",
                   IsDeleted = false
               },
               new Station
               {
                   Code =4,
                    Latitude =31.828986   ,
                   Longitude = 35.238355 ,
                   Name ="חיל האויר",
                   IsDeleted = false
               },
               new Station
               {
                   Code =5,
                   Latitude = 31.785134  ,
                   Longitude = 35.20423 ,
                   Name ="בנייני האומה/הנשיא השישי",
                   IsDeleted = false
               },
               new Station
               {
                   Code =6,
                   Latitude = 31.826025  ,
                   Longitude =  35.240145,
                   Name ="סיירת דוכיפת",
                   IsDeleted = false
               },
new Station
{
    Code =7,
                    Latitude =  31.823713 ,
    Longitude = 35.237567 ,
    Name ="פסגת זאב מרכז",
    IsDeleted = false
},
new Station
{
    Code =8,
                    Latitude =31.779457   ,
    Longitude =35.224207  ,
    Name ="העירייה",
    IsDeleted = false
},
new Station
{
    Code =9,
                    Latitude = 31.793248  ,
    Longitude = 35.226314 ,
    Name ="שמעון הצדיק",
    IsDeleted = false
},

new Station
{
    Code =10,
                    Latitude = 31.718125  ,
    Longitude = 35.230657 ,
    Name ="עמנואל זיסמן",
    IsDeleted = false
},
                #endregion         
#region Beer Sheva
                new Station
                          {
                              Code =11,
                               Latitude =31.255103   ,
                            Longitude = 34.757316 ,
                         Name ="כפר האירוסים/דרך אילן רמון",
                            IsDeleted = false
                },
new Station
{
    Code =12,
                    Latitude = 31.26895  ,
    Longitude =34.787595  ,
    Name ="סנהדרין/בעלי התוספות",
    IsDeleted = false
},
new Station
{
    Code =13,
                    Latitude = 31.242886  ,
    Longitude = 34.798546 ,
    Name ="באר שבע מרכז",
    IsDeleted = false
},
new Station
{
    Code =14,
                    Latitude =31.264302   ,
    Longitude = 34.811764 ,
    Name ="פארק הייטק/התבונה",
    IsDeleted = false
},
new Station
{
    Code =15,
                    Latitude =31.249575   ,
    Longitude = 34.769026 ,
    Name ="שדרות יגאל ידין/עין גדי",
    IsDeleted = false
},
new Station
{
    Code =16,
                    Latitude =31.257409   ,
    Longitude =34.814271  ,
    Name ="יהושע הצורף",
    IsDeleted = false
},
new Station
{
    Code =17,
                    Latitude =  31.22371 ,
    Longitude =  34.799634,
    Name ="ישפרו פלאנט/יצחק סלמה",
    IsDeleted = false
},
new Station
{
    Code =18,
                    Latitude =31.221687  ,
    Longitude = 34.801528 ,
    Name ="אליעזר וצילה מרגולין/גוזלן",
    IsDeleted = false
},
new Station
{
    Code =19,
                    Latitude =31.215779  ,
    Longitude =34.809186  ,
    Name ="הברזל/הזורע",
    IsDeleted = false
},
new Station
{
    Code =20,
                    Latitude = 31.78745  ,
    Longitude = 35.226909 ,
    Name ="שבטי ישראל",
    IsDeleted = false
},
                #endregion
#region Tel Aviv

                new Station
{
    Code =21,
                    Latitude = 32.049799  ,
    Longitude = 34.76056 ,
    Name ="דרך בן צבי/יהודה הימית",
    IsDeleted = false
},
new Station
{
    Code =22,
                    Latitude = 32.123097  ,
    Longitude = 34.808383 ,
    Name ="בית הלוחם/שמואל ברקאי",
    IsDeleted = false
},
new Station
{
    Code =23,
                    Latitude =32.049804   ,
    Longitude = 34.755758 ,
    Name ="יהודה הימית/אברהם לוינסון",
    IsDeleted = false
},
new Station
{
    Code =24,
                    Latitude =32.126058   ,
    Longitude =34.835016  ,
    Name ="סשה ארגוב/מקס זליגמן",
    IsDeleted = false
},
new Station
{
    Code =25,
                    Latitude = 32.123449  ,
    Longitude =34.815203  ,
    Name ="אהרון בקר/מרדכי מאיר",
    IsDeleted = false
},

new Station
{
    Code = 26,
    Latitude = 34.799862,
    Longitude = 32.052456,
    Name = "דרך משה דיין/ההגנה ",
    IsDeleted = false
},
new Station
{
    Code = 27,
    Latitude = 34.797919,
    Longitude = 32.112327,
    Name = "קניון רמת אביב/ברודצקי  ",
    IsDeleted = false
},
new Station
{
    Code = 28,
    Latitude = 34.788342,
    Longitude = 32.078598,
    Name = "בית המשפט/ויצמן ",
    IsDeleted = false
},
new Station
{
    Code = 29,
    Latitude = 34.795294,
    Longitude = 32.059086,
    Name = "לה גוורדיה/לוחמי גליפולי ",
    IsDeleted = false
},
new Station
            {
                Code = 30,
                Latitude = 34.79339,
                Longitude = 32.072666,
                Name = " ת. רכבת השלום",
                IsDeleted = false
            },
                #endregion
#region   Haifa
                new Station
{
    Code = 31,
    Latitude = 35.006342,
    Longitude = 32.767129,
    Name = "שבדיה ג  " ,
    IsDeleted = false
},
new Station
{
    Code = 32,
    Latitude = 35.074972,
    Longitude = 32.815284,
    Name = "אורט ביאליק/שדרות ההסתדרות ",
    IsDeleted = false
},
new Station
{
    Code = 33,
    Latitude = 35.08835,
    Longitude = 32.842017,
    Name = "קריון דרום/ד.עכו חיפה ",
    IsDeleted = false
},
new Station
{
    Code = 34,
    Latitude = 35.075485,
    Longitude = 32.803195,
    Name = "דרך חיפה/זבולון ",
    IsDeleted = false
},
new Station
{
    Code = 35,
    Latitude = 35.027195,
    Longitude = 32.778556,
    Name = " טכניון/הנדסה כימית",
    IsDeleted = false
},
new Station
{
    Code = 36,
    Latitude = 35.027276,
    Longitude = 32.776017,
    Name = "טכניון/בית ספר להנדסאים ",
    IsDeleted = false
},
new Station
{
    Code =37,
                    Latitude = 35.014026,
    Longitude = 32.779589,
    Name = "מלל/יערי ",
    IsDeleted = false
},
new Station
{
    Code = 38,
    Latitude = 35.011449,
    Longitude = 32.77423,
    Name = "דרך בירם/יגאל אלון ",
    IsDeleted = false
},
new Station
{
    Code = 39,
    Latitude = 35.06881,
    Longitude = 32.810104,
    Name = " הסתדרות/צומת קרית אתא",
    IsDeleted = false
},
new Station
{
    Code = 40,
    Latitude = 35.021788,
    Longitude = 32.795426,
    Name = "בר יהודה/גשר גדוד 21 ",
    IsDeleted = false
},
                #endregion
#region  Natania
                new Station
{
    Code = 41,
    Latitude = 32.798692,
    Longitude = 35.017746,
    Name = "בר יהודה/גשר פז ",
    IsDeleted = false
},
new Station
{
    Code = 42,
    Latitude = 34.838116,
    Longitude = 32.261417,
    Name = "מכון וינגייט ",
    IsDeleted = false
},
new Station
{
    Code = 43,
    Latitude = 34.84564,
    Longitude = 32.271389,
    Name = "מנחם סבידור/אנצו סירני ",
    IsDeleted = false
},
new Station
{
    Code = 44,
    Latitude = 34.878765,
    Longitude = 32.299994,
    Name = "דרך הפארק/הרב נריה ",
    IsDeleted = false
},
new Station
{
    Code = 45,
    Latitude = 34.837598,
    Longitude = 32.270586,
    Name = "החוף הירוק ",
    IsDeleted = false
},
new Station
{
    Code = 46,
    Latitude = 34.838726,
    Longitude = 32.272632,
    Name = "רמת פולג ",
    IsDeleted = false
},
new Station
{
    Code = 47,
    Latitude = 34.8413,
    Longitude = 32.274543,
    Name = "אהוד מנור/מנחם בגין ",
    IsDeleted = false
},
new Station
{
    Code = 48,
    Latitude = 34.845201,
    Longitude = 32.27572,
    Name = "שזר/אמנון ותמר ",
    IsDeleted = false
},
new Station
{
    Code = 49,
    Latitude = 34.85165,
    Longitude = 32.27659,
    Name = " גולדה מאיר/משעול קיפניס ",
    IsDeleted = false
},
new Station
{
    Code = 50,
    Latitude = 34.856933,
    Longitude = 32.272698,
    Name = "צומת אביר/כביש 553 ",
    IsDeleted = false
}
#endregion
            };
            ListLineStations = new List<LineStation>
            {
                #region line1
                new LineStation
                {
                    LineId=1,
                    stationCode=1,
                    PrevStation=null,
                    NextStation=2,
                    LineStationIndex=0,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=2,
                    PrevStation=1,
                    NextStation=3,
                    LineStationIndex=1,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=3,
                    PrevStation=2,
                    NextStation=4,
                    LineStationIndex=2,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=4,
                    PrevStation=3,
                    NextStation=5,
                    LineStationIndex=3,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=5,
                    PrevStation=4,
                    NextStation=6,
                    LineStationIndex=4,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=6,
                    PrevStation=5,
                    NextStation=7,
                    LineStationIndex=5,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=7,
                    PrevStation=6,
                    NextStation=8,
                    LineStationIndex=6,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=8,
                    PrevStation=7,
                    NextStation=9,
                    LineStationIndex=7,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=9,
                    PrevStation=8,
                    NextStation=10,
                    LineStationIndex=8,
                    IsDeleted=false
                },
                new LineStation
                {
                    LineId=1,
                    stationCode=10,
                    PrevStation=9,
                    NextStation=null,
                    LineStationIndex=9,
                    IsDeleted=false
                },
                #endregion
                #region line1 reverse
            new LineStation
            {
                LineId = 2,
                stationCode = 10,
                PrevStation = null,
                NextStation = 9,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 9,
                PrevStation = 10,
                NextStation = 8,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 8,
                PrevStation = 9,
                NextStation = 7,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 7,
                PrevStation = 8,
                NextStation = 6,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 6,
                PrevStation = 7,
                NextStation = 5,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 5,
                PrevStation = 6,
                NextStation = 4,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 4,
                PrevStation = 5,
                NextStation = 3,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 3,
                PrevStation = 4,
                NextStation = 2,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 2,
                PrevStation = 3,
                NextStation = 1,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 2,
                stationCode = 1,
                PrevStation = 2,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
                #endregion
                #region line2
            new LineStation
            {
                LineId = 3,
                stationCode = 11,
                PrevStation = null,
                NextStation = 12,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 12,
                PrevStation = 11,
                NextStation = 13,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 13,
                PrevStation = 12,
                NextStation = 14,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 14,
                PrevStation = 13,
                NextStation = 15,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 15,
                PrevStation = 14,
                NextStation = 16,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 16,
                PrevStation = 15,
                NextStation = 17,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 17,
                PrevStation = 16,
                NextStation = 18,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 18,
                PrevStation = 17,
                NextStation = 19,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 19,
                PrevStation = 18,
                NextStation = 20,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 3,
                stationCode = 20,
                PrevStation = 19,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
                #endregion
                #region line2 reverse
                new LineStation
                {
                    LineId = 4,
                    stationCode = 20,
                    PrevStation = null,
                    NextStation = 19,
                    LineStationIndex = 0,
                    IsDeleted = false
                },
            new LineStation
            {
                LineId = 4,
                stationCode = 19,
                PrevStation = 20,
                NextStation = 18,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 18,
                PrevStation = 19,
                NextStation = 17,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 17,
                PrevStation = 18,
                NextStation = 16,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 16,
                PrevStation = 17,
                NextStation = 15,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 15,
                PrevStation = 16,
                NextStation = 14,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 14,
                PrevStation = 15,
                NextStation = 13,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 13,
                PrevStation = 14,
                NextStation = 12,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 12,
                PrevStation = 13,
                NextStation = 11,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 4,
                stationCode = 11,
                PrevStation = 12,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                #region line3
            new LineStation
            {
                LineId = 5,
                stationCode = 21,
                PrevStation = null,
                NextStation = 22,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 22,
                PrevStation = 21,
                NextStation = 23,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 23,
                PrevStation = 22,
                NextStation = 24,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 24,
                PrevStation = 23,
                NextStation = 25,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 25,
                PrevStation = 24,
                NextStation = 26,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 26,
                PrevStation = 25,
                NextStation = 27,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 27,
                PrevStation = 26,
                NextStation = 25,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 28,
                PrevStation = 27,
                NextStation = 29,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 29,
                PrevStation = 28,
                NextStation = 30,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 5,
                stationCode = 30,
                PrevStation = 29,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },

#endregion
                #region line3 reverse
             new LineStation
            {
                LineId = 6,
                stationCode = 30,
                PrevStation = null,
                NextStation = 29,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 6,
                stationCode = 29,
                PrevStation = 30,
                NextStation = 28,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 6,
                stationCode = 28,
                PrevStation = 29,
                NextStation = 27,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 6,
                stationCode = 27,
                PrevStation = 28,
                NextStation = 26,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 6,
                stationCode = 26,
                PrevStation = 27,
                NextStation = 25,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId =6,
                stationCode = 25,
                PrevStation = 26,
                NextStation = 24,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 6,
                stationCode = 24,
                PrevStation = 25,
                NextStation = 23,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 6,
                stationCode = 23,
                PrevStation = 24,
                NextStation = 22,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 6,
                stationCode = 22,
                PrevStation = 23,
                NextStation = 21,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 6,
                stationCode = 21,
                PrevStation = 22,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                #region line4
            new LineStation
            {
                LineId =7,
                stationCode = 31,
                PrevStation = null,
                NextStation = 32,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 7,
                stationCode = 32,
                PrevStation = 31,
                NextStation = 33,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 7,
                stationCode = 33,
                PrevStation = 32,
                NextStation = 34,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 7,
                stationCode = 34,
                PrevStation = 33,
                NextStation = 35,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 7,
                stationCode = 35,
                PrevStation = 34,
                NextStation = 36,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 7,
                stationCode = 36,
                PrevStation = 35,
                NextStation = 37,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 7,
                stationCode = 37,
                PrevStation = 36,
                NextStation = 35,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 7,
                stationCode = 38,
                PrevStation = 37,
                NextStation = 39,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId =7,
                stationCode = 39,
                PrevStation = 38,
                NextStation = 40,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId =7,
                stationCode = 40,
                PrevStation = 39,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                #region line4 reverse
            new LineStation
            {
                LineId = 8,
                stationCode =40,
                PrevStation = null,
                NextStation = 39,
                LineStationIndex = 0,
                IsDeleted = false
            },
             new LineStation
            {
                LineId = 8,
                stationCode =39,
                PrevStation = 40,
                NextStation = 38,
                LineStationIndex = 1,
                IsDeleted = false

            },
             new LineStation
            {
                LineId = 8,
                stationCode = 38,
                PrevStation = 39,
                NextStation = 37,
                LineStationIndex = 2,
                IsDeleted = false
            },
             new LineStation
            {
                LineId = 8,
                stationCode = 37,
                PrevStation = 38,
                NextStation = 36,
                LineStationIndex = 3,
                IsDeleted = false
            },
              new LineStation
            {
                LineId = 8,
                stationCode = 36,
                PrevStation = 37,
                NextStation = 35,
                LineStationIndex = 4,
                IsDeleted = false
            },
             new LineStation
            {
                LineId = 8,
                stationCode = 35,
                PrevStation = 36,
                NextStation = 34,
                LineStationIndex = 5,
                IsDeleted = false
            },
             new LineStation
            {
                LineId = 8,
                stationCode = 34,
                PrevStation = 35,
                NextStation = 33,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId =8,
                stationCode = 33,
                PrevStation =34,
                NextStation = 32,
                LineStationIndex = 7,
                IsDeleted = false
            },
             new LineStation
            {
                LineId = 8,
                stationCode = 32,
                PrevStation = 33,
                NextStation = 31,
                LineStationIndex = 8,
                IsDeleted = false
            },
             new LineStation
            {
                LineId = 8,
                stationCode = 31,
                PrevStation = 32,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                #region line5
            new LineStation
            {
                LineId = 9,
                stationCode = 41,
                PrevStation = null,
                NextStation = 42,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 42,
                PrevStation = 41,
                NextStation = 43,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 43,
                PrevStation = 42,
                NextStation = 44,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 44,
                PrevStation = 43,
                NextStation = 45,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 45,
                PrevStation = 44,
                NextStation = 46,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 46,
                PrevStation = 45,
                NextStation = 47,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 47,
                PrevStation = 46,
                NextStation = 45,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 48,
                PrevStation = 47,
                NextStation = 49,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 9,
                stationCode = 49,
                PrevStation = 48,
                NextStation = 50,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId =9,
                stationCode = 50,
                PrevStation = 49,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                #region line5 reverse
            new LineStation
            {
                LineId = 10,
                stationCode = 50,
                PrevStation = null,
                NextStation = 49,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 49,
                PrevStation = 50,
                NextStation = 48,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 48,
                PrevStation = 49,
                NextStation = 47,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 47,
                PrevStation = 48,
                NextStation = 46,
                LineStationIndex =3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 46,
                PrevStation = 47,
                NextStation = 45,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 45,
                PrevStation = 46,
                NextStation = 44,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 44,
                PrevStation = 45,
                NextStation = 43,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 43,
                PrevStation = 44,
                NextStation = 42,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 10,
                stationCode = 42,
                PrevStation = 43,
                NextStation = 41,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 10,
                stationCode = 41,
                PrevStation = 42,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                        };

            ListBusses = new List<Bus>
            {
                      new Bus
       {
         License = 1,
         StartOfWork = new DateTime(1983,5,6),
         DateOfTreatment =new DateTime(1990,1,18),
         TotalKms =  16976 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 2,
         StartOfWork = new DateTime(1995,4,14),
         DateOfTreatment =new DateTime(2003,3,2),
         TotalKms =  5122 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 3,
         StartOfWork = new DateTime(2006,1,16),
         DateOfTreatment =new DateTime(2012,9,11),
         TotalKms =  9922 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 4,
         StartOfWork = new DateTime(2015,2,6),
         DateOfTreatment =new DateTime(1991,5,9),
         TotalKms =  8105 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 5,
         StartOfWork = new DateTime(2005,4,15),
         DateOfTreatment =new DateTime(1988,4,15),
         TotalKms =  15566 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 6,
         StartOfWork = new DateTime(1982,3,7),
         DateOfTreatment =new DateTime(1981,2,24),
         TotalKms =  4559 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 7,
         StartOfWork = new DateTime(1997,10,14),
         DateOfTreatment =new DateTime(2005,2,15),
         TotalKms =  12897 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 8,
         StartOfWork = new DateTime(2008,9,10),
         DateOfTreatment =new DateTime(1992,8,13),
         TotalKms =  3966 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 9,
         StartOfWork = new DateTime(1991,12,27),
         DateOfTreatment =new DateTime(2005,4,12),
         TotalKms =  14867 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 10,
         StartOfWork = new DateTime(2019,12,11),
         DateOfTreatment =new DateTime(2019,7,13),
         TotalKms =  12012 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 11,
         StartOfWork = new DateTime(2012,4,21),
         DateOfTreatment =new DateTime(2003,7,8),
         TotalKms =  8633 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 12,
         StartOfWork = new DateTime(1991,9,18),
         DateOfTreatment =new DateTime(1990,10,21),
         TotalKms =  18417 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 13,
         StartOfWork = new DateTime(2001,3,25),
         DateOfTreatment =new DateTime(2003,1,16),
         TotalKms =  15536 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 14,
         StartOfWork = new DateTime(1989,4,11),
         DateOfTreatment =new DateTime(2013,6,14),
         TotalKms =  9232 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 15,
         StartOfWork = new DateTime(2013,2,18),
         DateOfTreatment =new DateTime(2004,1,2),
         TotalKms =  18335 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 16,
         StartOfWork = new DateTime(1996,7,20),
         DateOfTreatment =new DateTime(1986,3,21),
         TotalKms =  88 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 17,
         StartOfWork = new DateTime(1991,5,11),
         DateOfTreatment =new DateTime(2001,9,23),
         TotalKms =  8811 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 18,
         StartOfWork = new DateTime(2011,6,11),
         DateOfTreatment =new DateTime(1997,11,23),
         TotalKms =  11263 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 19,
         StartOfWork = new DateTime(1982,5,21),
         DateOfTreatment =new DateTime(1982,10,27),
         TotalKms =  9178 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },
      new Bus
       {
         License = 20,
         StartOfWork = new DateTime(2002,12,18),
         DateOfTreatment =new DateTime(2008,9,1),
         TotalKms =  2761 ,
          Fuel = 1200,
          Status =Status.Ready ,
          TotalKmsFromLastTreatment =0,
           IsDeleted = false
        },


          };
            ListLines = new List<Line>
            {
                new Line
            {
                  Id = 1,
                  LineNumber = 1,
                  Area = Areas.Jerusalem,
                  FirstStation =1,
                  LastStation =10,
                  IsDeleted =false
              },
            new Line
               {
                 Id = 2,
                 LineNumber = 1,
                 Area = Areas.Jerusalem,
                 FirstStation =10,
                 LastStation =1,
                 IsDeleted = false
               },

                new Line
                {
                     Id=3,
                     LineNumber=2,
                     Area=Areas.BeerSheva,
                     FirstStation=11,
                     LastStation =20,
                     IsDeleted=false
                },
                new Line
                {
                     Id=4,
                     LineNumber=2,
                     Area=Areas.BeerSheva,
                     FirstStation=20,
                     LastStation =11,
                     IsDeleted=false
                },
                new Line
                {
                     Id=5,
                     LineNumber=3,
                     Area=Areas.TelAviv,
                     FirstStation=21,
                     LastStation =30,
                     IsDeleted=false
                },
                new Line
                {
                     Id=6,
                     LineNumber=3,
                     Area=Areas.TelAviv,
                     FirstStation=30,
                     LastStation =21,
                     IsDeleted=false
                },
                 new Line
                {
                     Id=7,
                     LineNumber=4,
                     Area=Areas.Haifa,
                     FirstStation=31,
                     LastStation =40,
                     IsDeleted=false
                },
                new Line
                {
                     Id=8,
                     LineNumber=4,
                     Area=Areas.Haifa,
                     FirstStation=40,
                     LastStation =31,
                     IsDeleted=false
                },
                new Line
                {
                     Id=9,
                     LineNumber=5,
                     Area=Areas.Natania,
                     FirstStation=41,
                     LastStation =50,
                     IsDeleted=false
                },
                new Line
                {
                     Id=10,
                     LineNumber=5,
                     Area=Areas.Natania,
                     FirstStation=50,
                     LastStation =41,
                     IsDeleted=false
                },
            };
            ListTwoAdjacentStations = new List<AdjacentStations>
            {
    new AdjacentStations
       {
          Station1 = 1,
         Station2 =  2 ,
         Distance = 5224.25523033912  ,
           Time =new TimeSpan(0,6,13),
            IsDeleted = false
        },
      new AdjacentStations
       {
          Station1 = 2,
          Station2 =  3 ,
         Distance = 4406.36878055019  ,
          Time =new TimeSpan(0,5,14),
          IsDeleted = false
        },
      new AdjacentStations
       {
          Station1 = 3,
         Station2 =  4 ,
         Distance = 12412.96929238  ,
           Time =new TimeSpan(0,14,46),
            IsDeleted = false
        },
      new AdjacentStations
       {
          Station1 = 4,
         Station2 =  5 ,
         Distance = 8776.50356464839  ,
           Time =new TimeSpan(0,10,26),
            IsDeleted = false
        },
      new AdjacentStations
       {
          Station1 = 5,
         Station2 =  6 ,
         Distance = 8518.12323208466  ,
           Time =new TimeSpan(0,10,8),
            IsDeleted = false
        },
      new AdjacentStations
       {
          Station1 = 6,
         Station2 =  7 ,
         Distance = 531.66925309886  ,
           Time =new TimeSpan(0,0,37),
            IsDeleted = false
        },
      new AdjacentStations
       {
          Station1 = 7,
         Station2 =  8 ,
         Distance = 7627.21207061098  ,
           Time =new TimeSpan(0,9,4),
            IsDeleted = false
        },

      new AdjacentStations
      {
          Station1 = 8,
          Station2 = 9,
          Distance = 2321.55224801146,
          Time = new TimeSpan(0, 2, 45),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 9,
          Station2 = 10,
          Distance = 12555.9045738122,
          Time = new TimeSpan(0, 14, 56),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 10,
          Station2 = 11,
          Distance = 102542.754328282,
          Time = new TimeSpan(2, 2, 4),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 11,
          Station2 = 12,
          Distance = 4900.22196456259,
          Time = new TimeSpan(0, 5, 50),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 12,
          Station2 = 13,
          Distance = 4623.17567745198,
          Time = new TimeSpan(0, 5, 30),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 13,
          Station2 = 14,
          Distance = 4042.24100806827,
          Time = new TimeSpan(0, 4, 48),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 14,
          Station2 = 15,
          Distance = 6575.8038686599,
          Time = new TimeSpan(0, 7, 49),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 15,
          Station2 = 16,
          Distance = 6588.04852609923,
          Time = new TimeSpan(0, 7, 50),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 16,
          Station2 = 17,
          Distance = 6000.98021584888,
          Time = new TimeSpan(0, 7, 8),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 17,
          Station2 = 18,
          Distance = 432.61534386625,
          Time = new TimeSpan(0, 0, 30),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 18,
          Station2 = 19,
          Distance = 1472.40290998135,
          Time = new TimeSpan(0, 1, 45),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 19,
          Station2 = 20,
          Distance = 112438.335355433,
          Time = new TimeSpan(2, 13, 51),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 20,
          Station2 = 21,
          Distance = 79275.1093643125,
          Time = new TimeSpan(1, 34, 22),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 21,
          Station2 = 22,
          Distance = 13981.1567418722,
          Time = new TimeSpan(0, 16, 38),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 22,
          Station2 = 23,
          Distance = 14321.3615400786,
          Time = new TimeSpan(0, 17, 2),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 23,
          Station2 = 24,
          Distance = 16961.7559649122,
          Time = new TimeSpan(0, 20, 11),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 24,
          Station2 = 25,
          Distance = 2834.7631144041,
          Time = new TimeSpan(0, 3, 22),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 25,
          Station2 = 26,
          Distance = 589569.291808265,
          Time = new TimeSpan(11, 41, 52),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 26,
          Station2 = 27,
          Distance = 8213.61459036976,
          Time = new TimeSpan(0, 9, 46),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 27,
          Station2 = 28,
          Distance = 4892.53243184386,
          Time = new TimeSpan(0, 5, 49),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 28,
          Station2 = 29,
          Distance = 2915.86862522787,
          Time = new TimeSpan(0, 3, 28),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 29,
          Station2 = 30,
          Distance = 1888.60819327896,
          Time = new TimeSpan(0, 2, 14),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 30,
          Station2 = 31,
          Distance = 101509.376217118,
          Time = new TimeSpan(2, 0, 50),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 31,
          Station2 = 32,
          Distance = 13212.8195760903,
          Time = new TimeSpan(0, 15, 43),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 32,
          Station2 = 33,
          Distance = 4280.71492018868,
          Time = new TimeSpan(0, 5, 5),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 33,
          Station2 = 34,
          Distance = 5721.78809530072,
          Time = new TimeSpan(0, 6, 48),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 34,
          Station2 = 35,
          Distance = 8736.32433582928,
          Time = new TimeSpan(0, 10, 24),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 35,
          Station2 = 36,
          Distance = 347.346490134101,
          Time = new TimeSpan(0, 0, 24),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 36,
          Station2 = 37,
          Distance = 2265.17177885899,
          Time = new TimeSpan(0, 2, 41),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 37,
          Station2 = 38,
          Distance = 849.664968569505,
          Time = new TimeSpan(0, 1, 0),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 38,
          Station2 = 39,
          Distance = 10757.9963416712,
          Time = new TimeSpan(0, 12, 48),
           IsDeleted = false

      },
      new AdjacentStations
      {
          Station1 = 39,
          Station2 = 40,
          Distance = 8101.95968592306,
          Time = new TimeSpan(0, 9, 38),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 40,
          Station2 = 41,
          Distance = 482170.984197982,
          Time = new TimeSpan(9, 34, 0),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 41,
          Station2 = 42,
          Distance = 511861.306542711,
          Time = new TimeSpan(10, 9, 21),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 42,
          Station2 = 43,
          Distance = 1855.87884819223,
          Time = new TimeSpan(0, 2, 12),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 43,
          Station2 = 44,
          Distance = 6777.21508104246,
          Time = new TimeSpan(0, 8, 4),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 44,
          Station2 = 45,
          Distance = 7965.92957099255,
          Time = new TimeSpan(0, 9, 28),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 45,
          Station2 = 46,
          Distance = 337.707367940885,
          Time = new TimeSpan(0, 0, 24),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 46,
          Station2 = 47,
          Distance = 503.183031328715,
          Time = new TimeSpan(0, 0, 35),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 47,
          Station2 = 48,
          Distance = 670.887559572589,
          Time = new TimeSpan(0, 0, 47),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 48,
          Station2 = 49,
          Distance = 1083.1504396778,
          Time = new TimeSpan(0, 1, 17),
           IsDeleted = false
      },
      new AdjacentStations
      {
          Station1 = 49,
          Station2 = 50,
          Distance = 1030.55993018019,
          Time = new TimeSpan(0, 1, 13),
           IsDeleted = false
      },

            };
        }
    }

};
