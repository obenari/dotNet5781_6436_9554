using System;
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
            set { kilometer = value; }
        }
        public int AmountOfFuelLeft
        {
            get { return amountOfFuelLeft; }
            set { amountOfFuelLeft = value; }
        }
        public string LicenseNumber
        {
            get { return licenseNumber; }
            set { licenseNumber = value; }
        }
        public int KilometerFromTheLastTreatment
        {
            get { return kilometerFromTheLastTreatment; }
            set { kilometerFromTheLastTreatment = value; }
        }
       public Bus(string license,DateTime start)
        {
            licenseNumber = license;
            startOfActivity = start;
            DateOftreatment= DateTime.Now;//***************************
            
                

        }
        
        
       
        public bool needTreatment()//the func check if the bus is available or need need a treatment
        {
            currentDate = DateTime.Now;
            if(currentDate.Year - dateOftreatment.Year >=1)
            {
                return true;
            }
            return false;
        }
        
        public bool dangerous(int kilometerInThisDrive=0)//the func check if after the next drive,the bus will drive more than 20000 km from the last treatment
        {
            if (kilometerFromTheLastTreatment + kilometerInThisDrive > 20000)
            {
                return true;
            }
            return false;
        }
       
        public bool enoughFuel(int kilometerInThisDrive)//the func check if there is enough fuel for the next drive
        {
            if (amountOfFuelLeft - kilometerInThisDrive > 0)
                return true;
            else
            {
                return false;
            }
        }
        public void print()//the func printlicense number, and the mileage from the last treatment 
        {
            string help = licenseNumber;
            if(help.Length==7)//in order to print in this format: xx-xxx-xx
            {
               help= help.Insert(2, "-");
                help = help.Insert(6, "-");
            }
            else//in order to print in this format: xxx-xx-xxx
            {
                help = help.Insert(3, "-");
                help = help.Insert(6, "-");
            }
            Console.WriteLine("license number is:{0}",help);
            Console.WriteLine("Mileage is:{0}", kilometerFromTheLastTreatment);
        }

    }
}
