using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    class Program
    {
        static void Main(string[] args)
        {
            BusCompany f = new BusCompany();
            foreach (BusLine item in f)
            {
                Console.WriteLine(item);
            }
        }
    }
}
