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
        private int amountOfFuelLeft =0;//how much kilometer the bus could drive 
        private int kilometer = 0;
        private DateTime startOfActivity;
        private DateTime dateOftreatment;
        private int licenseNumber;
        private int kilometerFromTheLastTreatment;
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
        public int LicenseNumber
        {
            get { return licenseNumber; }
            set { licenseNumber = value; }
        }
        public int KilometerFromTheLastTreatment
        {
            get { return kilometerFromTheLastTreatment; }
            set { kilometerFromTheLastTreatment += value; }
        }
        Bus(int license,DateTime start)
        {
     
            while (license>9999999 && start.Year >= 2018)
            // if (license.Length == 7)
            //if (start.Year > 2018)
            {
                //int n;
                do
                {
                    Console.WriteLine("Enter 8 digits of license number");
                    //license = Console.ReadLine();
                } while (!int.TryParse(Console.ReadLine(), out licenseNumber));
               // LicenseNumber = (string)n;
            }
            //while(license.Length == 8 && start.Year < 2018)
            //{
            //    Console.WriteLine("Enter 7 digits of license number");
            //    license = Console.ReadLine();
            //}
            licenseNumber = license;
            startOfActivity = start;
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
        public void chooseBus(int license,List<Bus> lst)
        {
            if(busExist(license,lst))
            {
                Random r = new Random(DateTime.Now.Millisecond);

            }

        }
        public bool needTreatment(int kilometerInThisDrive = 0)//the func check if the bus is available or need need a treatment
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
            if (kilometerFromTheLastTreatment + kilometerInThisDrive > 20000)
            {
                Console.WriteLine("The bus is dangerous, required treatment.");
                return true;
            }
            if(currentDate.Year - dateOftreatment.Year >1)
            {
                Console.WriteLine("required treatment.");
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
                Console.WriteLine("There is not enough Fuel.");
                return false;
            }
        }

    }
}
