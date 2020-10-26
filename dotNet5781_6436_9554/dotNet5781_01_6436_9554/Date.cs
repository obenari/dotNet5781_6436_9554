using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6436_9554
{
    class Date
    {
        int day;
        int month;
        int year;

        public Date(int day,int month,int year)
        {
            if(day<=30)
            this.day = day;
            else
                Console.WriteLine("Incorrect data entered");
            if (month <= 30)
                this.month = month;
            else
                Console.WriteLine("Incorrect data entered");
            if (year>=1900)
            this.year = year;
            else
                Console.WriteLine("Incorrect data entered");
            
        }
           

    }
}
