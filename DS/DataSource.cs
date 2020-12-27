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
        public static List<Bus> Busses;
        public static List<Station> Stations;
        public static List<Line> Lines;
        public static List<LineStation> LineStations;
        public static List<AdjacentStations> TwoAdjacentStations;





        static DataSource()
        {
            InitAllList();
        }
        static void InitAllList()
        {
            Stations = new List<Station>
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
    Name = "שדרות גולדה מאיר/משעול קיפניס ",
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
            List<LineStation> LineStations = new List<LineStation>
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
                LineId = 1,
                stationCode = 10,
                PrevStation = null,
                NextStation = 9,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 9,
                PrevStation = 10,
                NextStation = 8,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 8,
                PrevStation = 9,
                NextStation = 7,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 7,
                PrevStation = 8,
                NextStation = 6,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 6,
                PrevStation = 7,
                NextStation = 5,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 5,
                PrevStation = 6,
                NextStation = 4,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 4,
                PrevStation = 5,
                NextStation = 3,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 3,
                PrevStation = 4,
                NextStation = 2,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 1,
                stationCode = 2,
                PrevStation = 3,
                NextStation = 1,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 1,
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
                LineId = 2,
                stationCode = 11,
                PrevStation = null,
                NextStation = 12,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 12,
                PrevStation = 11,
                NextStation = 13,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 13,
                PrevStation = 12,
                NextStation = 14,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 14,
                PrevStation = 13,
                NextStation = 15,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 15,
                PrevStation = 14,
                NextStation = 16,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 16,
                PrevStation = 15,
                NextStation = 17,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 17,
                PrevStation = 16,
                NextStation = 18,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 18,
                PrevStation = 17,
                NextStation = 19,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 19,
                PrevStation = 18,
                NextStation = 20,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 2,
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
                LineId = 2,
                stationCode = 20,
                PrevStation = 19,
                NextStation = null,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 19,
                PrevStation = null,
                NextStation = 20,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 18,
                PrevStation = 17,
                NextStation = 19,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 17,
                PrevStation = 16,
                NextStation = 18,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 16,
                PrevStation = 15,
                NextStation = 17,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 15,
                PrevStation = 14,
                NextStation = 16,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 14,
                PrevStation = 13,
                NextStation = 15,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 13,
                PrevStation = 12,
                NextStation = 14,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 2,
                stationCode = 12,
                PrevStation = 11,
                NextStation = 13,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 2,
                stationCode = 11,
                PrevStation = null,
                NextStation = 12,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                #region line3
            new LineStation
            {
                LineId = 3,
                stationCode = 21,
                PrevStation = null,
                NextStation = 22,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 22,
                PrevStation = 21,
                NextStation = 23,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 23,
                PrevStation = 22,
                NextStation = 24,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 24,
                PrevStation = 23,
                NextStation = 25,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 25,
                PrevStation = 24,
                NextStation = 26,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 26,
                PrevStation = 25,
                NextStation = 27,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 27,
                PrevStation = 26,
                NextStation = 25,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 28,
                PrevStation = 27,
                NextStation = 29,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 29,
                PrevStation = 28,
                NextStation = 30,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 3,
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
                LineId = 3,
                stationCode = 30,
                PrevStation = null,
                NextStation = 29,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 29,
                PrevStation = 30,
                NextStation = 28,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 28,
                PrevStation = 7,
                NextStation = 9,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 27,
                PrevStation = 6,
                NextStation = 8,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 26,
                PrevStation = 5,
                NextStation = 7,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 25,
                PrevStation = 4,
                NextStation = 6,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 24,
                PrevStation = 3,
                NextStation = 5,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 23,
                PrevStation = 2,
                NextStation = 4,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 3,
                stationCode = 22,
                PrevStation = 1,
                NextStation = 3,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 3,
                stationCode = 21,
                PrevStation = null,
                NextStation = 2,
                LineStationIndex = 9,
                IsDeleted = false
            },
#endregion
                #region line4
            new LineStation
            {
                LineId = 4,
                stationCode = 31,
                PrevStation = null,
                NextStation = 32,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 32,
                PrevStation = 31,
                NextStation = 33,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 33,
                PrevStation = 32,
                NextStation = 34,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 34,
                PrevStation = 33,
                NextStation = 35,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 35,
                PrevStation = 34,
                NextStation = 36,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 36,
                PrevStation = 35,
                NextStation = 37,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 37,
                PrevStation = 36,
                NextStation = 35,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 38,
                PrevStation = 37,
                NextStation = 39,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 39,
                PrevStation = 38,
                NextStation = 40,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 4,
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
                LineId = 4,
                stationCode = 40,
                PrevStation = 39,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 39,
                PrevStation = 38,
                NextStation = 40,
                LineStationIndex = 8,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 38,
                PrevStation = 37,
                NextStation = 39,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 37,
                PrevStation = 36,
                NextStation = 38,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 36,
                PrevStation = 35,
                NextStation = 37,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 35,
                PrevStation = 34,
                NextStation = 36,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 34,
                PrevStation = 33,
                NextStation = 35,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 33,
                PrevStation = 32,
                NextStation = 34,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 4,
                stationCode = 32,
                PrevStation = 31,
                NextStation = 33,
                LineStationIndex = 1,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 4,
                stationCode = 31,
                PrevStation = null,
                NextStation = 32,
                LineStationIndex = 0,
                IsDeleted = false
            },
#endregion
                #region line5
            new LineStation
            {
                LineId = 5,
                stationCode = 41,
                PrevStation = null,
                NextStation = 42,
                LineStationIndex = 0,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 42,
                PrevStation = 41,
                NextStation = 43,
                LineStationIndex = 1,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 43,
                PrevStation = 42,
                NextStation = 44,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 44,
                PrevStation = 43,
                NextStation = 45,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 45,
                PrevStation = 44,
                NextStation = 46,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 46,
                PrevStation = 45,
                NextStation = 47,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 47,
                PrevStation = 46,
                NextStation = 45,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 48,
                PrevStation = 47,
                NextStation = 49,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 49,
                PrevStation = 48,
                NextStation = 50,
                LineStationIndex = 8,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 5,
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
                LineId = 5,
                stationCode = 50,
                PrevStation = 49,
                NextStation = null,
                LineStationIndex = 9,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 49,
                PrevStation = 48,
                NextStation = 50,
                LineStationIndex = 8,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 48,
                PrevStation = 47,
                NextStation = 49,
                LineStationIndex = 7,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 47,
                PrevStation = 46,
                NextStation = 48,
                LineStationIndex = 6,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 46,
                PrevStation = 45,
                NextStation = 47,
                LineStationIndex = 5,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 45,
                PrevStation = 44,
                NextStation = 46,
                LineStationIndex = 4,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 44,
                PrevStation = 43,
                NextStation = 45,
                LineStationIndex = 3,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 43,
                PrevStation = 42,
                NextStation = 44,
                LineStationIndex = 2,
                IsDeleted = false
            },
            new LineStation
            {
                LineId = 5,
                stationCode = 42,
                PrevStation = 41,
                NextStation = 43,
                LineStationIndex = 1,
                IsDeleted = false

            },
            new LineStation
            {
                LineId = 5,
                stationCode = 41,
                PrevStation = null,
                NextStation = 42,
                LineStationIndex = 0,
                IsDeleted = false
            },
#endregion
                        };

            Busses = new List<Bus>
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
            Lines = new List<Line>
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
                     LastStation =500,
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
            List<AdjacentStations> TwoAdjacentStations = new List<AdjacentStations> { };

        }
    }

};
