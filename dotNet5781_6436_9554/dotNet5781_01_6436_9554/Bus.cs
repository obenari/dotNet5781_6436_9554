using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6436_9554
{
    class Bus
    {
        string licenseNumber;//מחרוזת של ספרות של מס' הרישוי
                             

        DateTime startDate;

        int Odometer;//מד המרחק
        int lastTreatOdometer;//הקלימוטראז של הטיפול האחרון 
        int fuelGauge;//מד דלק

        public  string LicenseNumber// בשביל לגשת לשדות של מספר הרישוי
        {
         get {
                int temp;
            
                    if (startDate.Year <= 2017)//  האדום בגלל שעדיין לא חיברנו מחלקות.. אז הוא לא מכיר את השדה שנה של המשתנה- StartDate
                        temp = 7;
                    else
                        temp = 8;
                for (int i = 0; i < temp + 2; i++)
                {
                    this.licenseNumber += licenseNumber;//הכנסת הפרמטרים ללחוית הרישוי 
                }
                return licenseNumber; }
            set { licenseNumber = value; }
        }
        public int FuelGauge // בשביל לגשת לשדות של מד הדלק
        {
            get { return fuelGauge; }
            set { fuelGauge = value; }
        }
        public DateTime StartDate// בשביל לגשת לשדות של תאריך הטיפול
        {
            get { return startDate; }
             private set { startDate = value; }
        }
        
        


        //    if (Odometer + this.Odometer >= 20000)//treatment
        //        Console.WriteLine("Dangerous!!");
        //    else
        //    {
        //        this.Odometer += Odometer;
        //    }ה
        //}
        public Bus()
        {

        }






    }
}
