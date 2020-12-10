using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace dotNet5781_3B_6436_9554_
{
   public class Bus
    {
        private int amountOfFuelLeft = 1200;//how much kilometer the bus could drive 
        private int kilometer;
        private DateTime startOfActivity;
        private DateTime dateOftreatment;
        private string licenseNumber;
        private int kilometerFromTheLastTreatment=0;
        private State myState;

        public State MyState
        {
            get { return myState; }
            set { myState = value; }
        }

        /// <summary>
        /// to make sure that the bus is not exist already
        /// </summary>
        public static List<string> keys = new List<string>();
        public DateTime DateOftreatment
        {
            get { return dateOftreatment; }
            set
            {
                if (DateTime.Now < value)
                    throw new ArgumentException("the date of treatment wasn't yet");
                if (value < startOfActivity)
                    throw new ArgumentException("the date of treatment can't be before start of activity");
                dateOftreatment = value;
            }
        }
        public DateTime StartOfActivity
        {
            get { return startOfActivity; }
            set
            {
                if (DateTime.Now < value)
                    throw new ArgumentException("the date of start wasn't yet");
                startOfActivity = value;
            }
        }
        public int Kilometer
        {
            get
            {
                return kilometer;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("the mileage must be positive");
                if (value < kilometer)
                    throw new ArgumentException("it's impossible to reduce the mileage");
                kilometer = value;
            }
        }
        public int AmountOfFuelLeft
        {
            get { return amountOfFuelLeft; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("the amount of fuel must be positive");
                if (1200 < value)
                    throw new ArgumentException("full tank of fuel is 1200 liters.");
                amountOfFuelLeft = value;
            }
        }
        public string LicenseNumber
        {
            get { return licenseNumber; }
            private set
            {
                if (!(int.TryParse(value,out int n)))
                    throw new ArgumentException("the license number must have only digits");
                if (keys.Contains(value))
                    throw new ArgumentException("the bus is alredy exist");
                if (value.Length < 7 || value.Length > 8)
                    throw new ArgumentException("license number should be 7 or 8 digits.");
                if (startOfActivity.Year > 2017 && value.Length == 7 || startOfActivity.Year <= 2017 && value.Length == 8)//if there is no match with the license number and date
                    throw new ArgumentException("The number of digits of the license number does not match\n the start year of the activity");
                
                licenseNumber = value;
            }
        }
        /// <summary>
        /// this feild return the license Number in the appropriate Format (xxx-xx-xxx)
        /// </summary>
        public string AppropriateFormatLicenseNumber { get { return appropriateFormatLicenseNumber(); } }
        public int KilometerFromTheLastTreatment
        {
            get { return kilometerFromTheLastTreatment; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("the kilometers must be positive");
                if (value > 20000)
                    throw new ArgumentException("the bus is need a treatment");
                kilometerFromTheLastTreatment = value;
            }
        }
        /// <summary>
        /// this ctor doesnt get the fuel patrameter
        /// </summary>
        /// <param name="license"></param>
        /// <param name="start"></param>
        /// <param name="last"></param>
        /// <param name="km"></param>
        public Bus(string license, DateTime start, DateTime last, int km)
        {
            StartOfActivity = start;
            DateOftreatment = last;
            LicenseNumber = license;
            Kilometer = km;
            keys.Add(license);
            if(this.isOldBus()||this.dangerous())
            {
                MyState = State.isDangerous;
            }
            else
            {
                MyState = State.isReady;
            }
        }
        /// <summary>
        /// /// this ctor  get the fuel patrameter
        /// </summary>
        /// <param name="license"></param>
        /// <param name="start"></param>
        /// <param name="last"></param>
        /// <param name="km"></param>
        public Bus(string license, DateTime start, DateTime last, int km, int fuel)
        {
            StartOfActivity = start;
            DateOftreatment = last;
            LicenseNumber = license;
            Kilometer = km;
            AmountOfFuelLeft = fuel;
            keys.Add(license);
            if (this.isOldBus() || this.dangerous())
            {
                MyState = State.isDangerous;
            }
            else
            {
                MyState = State.isReady;
            }
        }
        /// <summary>
        /// /// this ctor  get all the parameters
        /// </summary>
        /// <param name="license"></param>
        /// <param name="start"></param>
        /// <param name="last"></param>
        /// <param name="km"></param>
        public Bus(string license, DateTime start, DateTime last, int km, int fuel, int kmFromLastTreatment)
        {
            StartOfActivity = start;
            DateOftreatment = last;
            LicenseNumber = license;
            Kilometer = km;
            AmountOfFuelLeft = fuel;
            keys.Add(license);
            kilometerFromTheLastTreatment = kmFromLastTreatment;
            if (this.isOldBus() || this.dangerous()||kilometerFromTheLastTreatment>=20000)
            {
                MyState = State.isDangerous;
            }
            else
            {
                MyState = State.isReady;
            }
        }
        /// <summary>
        /// //the func check if passed more than one year from the last treatment
        /// </summary>
        /// <returns></returns>

        public bool isOldBus()
        {
            //if (DateTime.Now.Year - dateOftreatment.Year >= 1)
            //{
            //    if (DateTime.Now.Month - dateOftreatment.Month >= 0)
            //        if (DateTime.Now.Day - dateOftreatment.Day >= 0)
            //            return true;
            //}
            DateTime yearAgo = DateTime.Today.AddYears(-1);
            if (dateOftreatment < yearAgo)
                return true;
            return false;
        }
        /// <summary>
        /// //the func check if after the next drive,the bus will drive more than
        /// 20000 km from the last treatment
        /// </summary>
        /// <param name="kilometerInThisDrive">the km that the next drive will take</param>
        /// <returns></returns>
        public bool dangerous(int kilometerInThisDrive = 0)
        {
            if (kilometerFromTheLastTreatment + kilometerInThisDrive > 20000)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// //the func check if there is enough fuel for the next drive
        /// </summary>
        /// <param name="kilometerInThisDrive">the km that the next drive will take</param>
        /// <returns></returns>
        public bool enoughFuel(int kilometerInThisDrive)
        {
            if (amountOfFuelLeft - kilometerInThisDrive > 0)
                return true;
            else
            {
                return false;
            }
        }
        /// <summary>
        /// the func return the licenseNumber in the format xxx-xx-xxx
        /// </summary>
        /// <returns></returns>
        public string appropriateFormatLicenseNumber()
        {
            string help = licenseNumber;
            if (help.Length == 7)//in order to print in this format: xx-xxx-xx
            {
                help = help.Insert(2, "-");
                help = help.Insert(6, "-");
            }
            else//in order to print in this format: xxx-xx-xxx
            {
                help = help.Insert(3, "-");
                help = help.Insert(6, "-");
            }
            return help;
        }
        public void refueling()
        {
         AmountOfFuelLeft = 1200;
        }

    }
}
