using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6436_9554
{
   public class BusList
    {
        List<Bus> lst;
        public BusList() { lst = new List<Bus>(); }
        public bool busExist(string license)//this func chech if the bus is exist in the system
        {
           // bool flag = false;
            for(int i=0;i<lst.Count;i++)
            {
                if (lst[i].LicenseNumber == license)
                    return true;
            }
            return false;
           // Console.WriteLine("The bus is not exist.");
        }
        public void addBus()
        {
            bool properInput = false;//until the user dont insert proper input he the loop continue to run
            string str;
            Console.WriteLine("Enter a license number");
             while (!properInput)
              {
                int n;
                str = Console.ReadLine();
                  while(!(int.TryParse(str,out n))||str.Length<7||str.Length>8)
                {
                    Console.WriteLine("wrong input, please try again.");
                    str = Console.ReadLine();
                }
                if (busExist(str))
                {
                    Console.WriteLine("The bus is already exist");
                }
                else
                {
                    //if(str.Length<7)
                    //{
                    //    Console.WriteLine();
                    //}
                   // ..int year, month, day;
                  //  year = int.Parse(Console.ReadLine());
                    DateTime t;
                    Console.WriteLine("please enter a date of start");
                    bool check = DateTime.TryParse(Console.ReadLine(), out t);
                    while(!check)
                    {
                        Console.WriteLine("Error date, please enter again.");
                        check = DateTime.TryParse(Console.ReadLine(), out t);
                    }
                    if(t.Year>2017&&str.Length==7||t.Year<=2017&&str.Length==8)
                    {
                        Console.WriteLine("The data does not match, please enter licenses number and date again");
                    }
                    else
                    {
                        Bus b = new Bus(str, t);
                        lst.Add(b);
                        properInput = true;
                    }
                }
            }

        }
        public void chooseBus()
        {
            Console.WriteLine("please enter a license number");
            string license = Console.ReadLine();
            while (!busExist(license))
            {
                Console.WriteLine("the bus is not exist, please try again");
                license = Console.ReadLine();
            }
            Random r = new Random(DateTime.Now.Millisecond);
            int i = r.Next(1000);
            int index = findBusIndex(license);
            bool flag = true;//check if the bus is suitable to use
            if(lst[index].needTreatment())
            {
                Console.WriteLine("required treatment.");
                flag = false;
            }
            if(lst[index].dangerous(i))
            {
                flag = false;
                Console.WriteLine("The bus is dangerous, required treatment.");
            }
            if(!(lst[index].enoughFuel(i)))
            {
                flag = false;
                Console.WriteLine("There is not enough Fuel.");
            }
            if(flag)//if the bus is  suitable to use
            {
                lst[index].Kilometer = lst[index].Kilometer + i;
                lst[index].AmountOfFuelLeft = lst[index].AmountOfFuelLeft - i;
                lst[index].KilometerFromTheLastTreatment = lst[index].KilometerFromTheLastTreatment + i;
            }

        }
        public int findBusIndex(string str)
        {
            for(int i=0;i<lst.Count;i++)
            {
                if (lst[i].LicenseNumber == str)
                    return i;
            }
            return -1;//in case the bus is not exist
        }
        public void refueling()
        {
            Console.WriteLine("please enter a license number");
            string license = Console.ReadLine();
            while (!busExist(license))
            {
                Console.WriteLine("the bus is not exist, please try again");
                license = Console.ReadLine();
            }
            int index = findBusIndex(license);
            lst[index].AmountOfFuelLeft = 1200;
        }
        public void treatment()
        {
            Console.WriteLine("please enter a license number");
            string license = Console.ReadLine();
            while (!busExist(license))
            {
                Console.WriteLine("the bus is not exist, please try again");
                license = Console.ReadLine();
            }
            int index = findBusIndex(license);
            lst[index].DateOftreatment = DateTime.Now;
            lst[index].KilometerFromTheLastTreatment = 0;
        }
        public void print()
        {
            for (int i = 0; i < lst.Count; i++)
                lst[i].print();
        }
    }
}
