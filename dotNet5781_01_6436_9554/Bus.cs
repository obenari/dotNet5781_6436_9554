﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6436_9554
{
    class Bus
    {
       DateTime currentDate= DateTime.Now;
        private int amountOfFuelLeft =1200;//how much kilometer the bus could drive 
        private int kilometer = 0;
        private DateTime startOfActivity;
        private DateTime dateOftreatment;
        private string licenseNumber;
        private int kilometerFromTheLastTreatment=0;
        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }
        public DateTime DateOftreatment
        {
            get { return dateOftreatment; }
            set { dateOftreatment = value; }
        }
        public DateTime StartOfActivity
        {
            get { return startOfActivity; }
            set { startOfActivity = value; }
        }
        public int Kilometer
        {
            get { return kilometer; }
            set { kilometer += value; }
        }
        public int AmountOfFuelLeft
        {
            get { return amountOfFuelLeft; }
            set { amountOfFuelLeft -= value; }
        }
        public string LicenseNumber
        {
            get { return licenseNumber; }
            set { licenseNumber = value; }
        }
        public int KilometerFromTheLastTreatment
        {
            get { return kilometerFromTheLastTreatment; }
            set { kilometerFromTheLastTreatment += value; }
        }
       public Bus(string license,DateTime start)
        {
            licenseNumber = license;
            startOfActivity = start;
            DateOftreatment= CurrentDate;//***************************
            
                

        }
        //public void busNumber(int license,List<Bus> lst)
        //{
        //   // foreach()
        //}
        public static bool busExist(int license, List<Bus> lst)//this func chech if the bus is exist in the system
        {
            Console.WriteLine("The bus is not exist.");
            return true;
        }
        //public void chooseBus(int license,List<Bus> lst)
        //{
        //    if(busExist(license,lst))
        //    {
        //        Random r = new Random(DateTime.Now.Millisecond);

        //    }

        //}
        public bool needTreatment(/*int kilometerInThisDrive = 0*/)//the func check if the bus is available or need need a treatment
        {

            // TimeSpan t = currentDate - dateOftreatment;
            //if (kilometerFromTheLastTreatment + kilometerNow <= 20000 && currentDate.Year - dateOftreatment.Year < 1)
            //    return true;
            //else
            //{
            //    if(kilometerFromTheLastTreatment + kilometerNow > 20000)
            //       Console.WriteLine("The bus is dangerous.");

            //    return false;
            //}
            //if (kilometerFromTheLastTreatment + kilometerInThisDrive > 20000)
            //{
            //    Console.WriteLine("The bus is dangerous, required treatment.");
            //    return true;
            //}
            currentDate = DateTime.Now;
            if(currentDate.Year - dateOftreatment.Year >1)
            {
                //Console.WriteLine("required treatment.");
                return true;
            }
            return false;
        }
        public bool dangerous(int kilometerInThisDrive=0)
        {
            if (kilometerFromTheLastTreatment + kilometerInThisDrive > 20000)
            {
                //Console.WriteLine("The bus is dangerous, required treatment.");
                return true;
            }
            return false;
        }
       
        public bool enoughFuel(int kilometerInThisDrive)
        {
            if (amountOfFuelLeft - kilometerInThisDrive > 0)
                return true;
            else
            {
               // Console.WriteLine("There is not enough Fuel.");
                return false;
            }
        }
        public void print()
        {
            Console.WriteLine("license number is:{0}",licenseNumber);
            Console.WriteLine("Mileage is:{0}", kilometer);
        }

    }
}
