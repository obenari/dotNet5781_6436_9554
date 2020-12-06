﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_3b_6436_9554
{
    class Bus
    {
        private int amountOfFuelLeft = 1200;//how much kilometer the bus could drive 
        private int kilometer ;
        private DateTime startOfActivity;
        private DateTime dateOftreatment;
        private string licenseNumber;
        private int kilometerFromTheLastTreatment;
        
        public DateTime DateOftreatment
        {
            get { return dateOftreatment; }
            set
            {
                if (DateTime.Now < value)
                    throw new ArgumentException("the date is unlegal");

                dateOftreatment = value;
            }
        }
        public DateTime StartOfActivity
        {
            get { return startOfActivity; }
            set
            {
                if (DateTime.Now < value)
                    throw new ArgumentException("the date is unlegal"); 
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
                if (value < kilometer)
                    throw new ArgumentException("it's impossible to reduce the mileage");
                kilometer = value; }
        }
        public int AmountOfFuelLeft
        {
            get { return amountOfFuelLeft; }
            set 
            {
                if (1200 < value)
                    throw new ArgumentException("full tank of fuel is 1200 liters.");
                amountOfFuelLeft = value; }
        }
        public string LicenseNumber
        {
            get { return licenseNumber; }
            private set { licenseNumber = value; }
        }
        /// <summary>
        /// this feild return the license Number in the appropriate Format (xxx-xx-xxx)
        /// </summary>
        public string AppropriateFormatLicenseNumber { get { return appropriateFormatLicenseNumber(); }  }
        public int KilometerFromTheLastTreatment
        {
            get { return kilometerFromTheLastTreatment; }
            set { kilometerFromTheLastTreatment = value; }
        }
        public Bus(string license, DateTime start,DateTime last,int km)
        {
            LicenseNumber = license;
            StartOfActivity = start;
            DateOftreatment = last;
            Kilometer = km;
        }

        /// <summary>
        /// //the func check if passed more than one year from the last treatment
        /// </summary>
        /// <returns></returns>

        public bool isOldBus()
        {
            if (DateTime.Now.Year - dateOftreatment.Year >= 1)
            {
                if(DateTime.Now.Month - dateOftreatment.Month>=0)
                    if(DateTime.Now.Day - dateOftreatment.Day >= 0)
                        return true;
            }
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

    }
}
