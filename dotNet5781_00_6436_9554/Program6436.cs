using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_00_6436_9554
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6436();
            Welcome9554();
            Console.ReadKey();
            //
            //************

        }
        static partial void Welcome9554();
        private static void Welcome6436()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
/*Enter your name:
dani
dani, welcome to my first console application
*/