using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6436_9554
{
    enum BusActivity { Add = 1, ChooseBus, Refuel, Treatment, PrintBuses, Exit };

    class Program
    {
        static void Main(string[] args)
        {
            BusList buses=new BusList();
            int n=0;
            Console.WriteLine(@"Choose one of the following:
1: Insert a new bus to the system.
2: Select a bus to drive.
3: Refuel
4: Send to treatment.
5: Print all the buses mileage since the last treatment.
6: Exit.");
            while (n != 6)
            {
                while (!(int.TryParse(Console.ReadLine(), out n)))
                    Console.WriteLine("Wrong input, enter a number again.");

                BusActivity choice = (BusActivity)n;
                switch (choice)
                {
                    case BusActivity.Add:
                        buses.addBus();
                        break;
                    case BusActivity.ChooseBus:
                        buses.chooseBus();
                        break;
                    case BusActivity.Refuel:
                        buses.refueling();
                        break;
                    case BusActivity.Treatment:
                        buses.treatment();
                        break;
                    case BusActivity.PrintBuses:
                        buses.print();
                        break;
                    case BusActivity.Exit:
                        Console.WriteLine("Bye, have a nice day.");
                        break;
                    default:
                        break;
                }

            }
        }

    }
}
/*
 Choose one of the following:
1: Insert a new bus to the system.
2: Select a bus to drive.
3: Refuel
4: Send to treatment.
5: Print all the buses mileage since the last treatment.
6: Exit.
1
Enter a license number
12345678
please enter a date of start
12/12/2018
1
Enter a license number
0000000
please enter a date of start
10/12/2010
2
please enter a license number
0000000
2
please enter a license number
0000000
5
license number is:123-45-678
Mileage is:0
license number is:00-000-00
Mileage is:797
2
please enter a license number
0000000
5
license number is:123-45-678
Mileage is:0
license number is:00-000-00
Mileage is:1035
2
please enter a license number
0000000
There is not enough Fuel.
5
license number is:123-45-678
Mileage is:0
license number is:00-000-00
Mileage is:1035
3
please enter a license number
0000000
2
please enter a license number
0000000
5
license number is:123-45-678
Mileage is:0
license number is:00-000-00
Mileage is:1512
4
please enter a license number
0000000
5
license number is:123-45-678
Mileage is:0
license number is:00-000-00
Mileage is:0
6
Bye, have a nice day.
Press any key to continue . . .
*/
