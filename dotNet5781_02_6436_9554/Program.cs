using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    class Program
    {
       public enum Menu { Exit,AddBus,AddExistingStation,AddNewStation,
            DeleteBus, DeleteStation, BusLinesPassingThrough,
            PrintAllPath,PrintBusses,PrintBussesAndStation };
        static void Main(string[] args)
        {
            StationList bussesStop = new StationList();
            
            BusCompany egged = new BusCompany();
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i=0;i<40;i++)//create 40 stations
            {
                try
                {
                    bussesStop.add(new BusStation(r.Next(1000000)));
                }
                catch(IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station is not created
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station is not created
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station is not created
                }

            }
      
            while(egged.Busses.Count<10)//create 10 busses
            {
                try
                {
                    egged.addBus(new BusLine(r.Next(1000), (Area)r.Next(5),//this row is too long, so we drop row
                    new BusLineStation(bussesStop[r.Next(40)]), new BusLineStation(bussesStop[r.Next(40)])));
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DuplicateNameException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch
                {
                    Console.WriteLine("ERROR");
                }
            }

            for (int i=0;i<40;i++)//to make sure that all stations pass at least one bus line
            {
                try
                {
                    if (egged.totalBusses(bussesStop[i].BusStationKey) == 0)//if no bus passing through the the station number i
                        egged.Busses[r.Next(10)].addToEnd(new BusLineStation(bussesStop[i]));

                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch (DuplicateNameException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
                catch
                {
                    Console.WriteLine("ERROR");
                    i--;//If an exception is thrown, no buses pass through the station yet.
                }
            }
            for(int i=0;i<10;i++)//To make sure at least 10 stations pass more than one bus line
            {
                try
                {
                    if (egged.totalBusses(bussesStop[i].BusStationKey) < 2)//Check if less than two buses pass through the station
                    {
                        egged.Busses[r.Next(10)].addToEnd(new BusLineStation(bussesStop[i]));
                    }
                }

                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch (DuplicateNameException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                catch
                {
                    Console.WriteLine("ERROR");
                    i--;//If there's exception, that means the station still doesn't have at least two busses.
                }
                
            }
            int n;
            Menu choice ;
            do
            {
                Console.WriteLine(@"Choose one of the following:
0: To exit
1: To add a new bus to the system.
2: To add an existing station to the bus.
3: To add a new station to the bus.
4: To delete a bus.
5: To delete a station from a bus.
6: To print all bus numbers passing through the station.
7: To Print all paths between two stations.
8: To print all bus numbers.
9: To print all the data about the busses.");

                while (!(int.TryParse(Console.ReadLine(), out n)))
                    Console.WriteLine("Wrong input, enter a number again.");
                choice = (Menu)n;
                try {
                    switch (choice)
                    {
                        case Menu.Exit:
                            Console.WriteLine("Bye, have a nice day.");
                            break;
                        case Menu.AddBus:
                            {
                                Console.WriteLine("please enter a bus number");
                                int num;
                                bool succes = int.TryParse(Console.ReadLine(), out num);
                                if (!succes)
                                    throw new ArgumentException("the number is not legal");
                                Console.WriteLine("enter an area");
                                Area a = (Area)int.Parse(Console.ReadLine());
                                Console.WriteLine("if you want to add a new stations enter 1,else enter 2");

                                int j = int.Parse(Console.ReadLine());
                                if (j == 1)
                                {
                                    Console.WriteLine("enter a number of first station");
                                    int num1 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter a latitude");
                                    double lat = double.Parse(Console.ReadLine());
                                    Console.WriteLine("enter a longitude");
                                    double lon = double.Parse(Console.ReadLine());
                                    BusStation first = new BusStation(num1, lat, lon);
                                    bussesStop.add(first);//add the new station the the list with all the stations
                                    Console.WriteLine("enter a number of the last station");
                                    int num2 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter a latitude");
                                    lat = double.Parse(Console.ReadLine());
                                    Console.WriteLine("enter a longitude");
                                    lon = double.Parse(Console.ReadLine());
                                    BusStation last = new BusStation(num1, lat, lon);
                                    bussesStop.add(last);
                                    Console.WriteLine("enter the travel time from the last station");
                                    TimeSpan t = TimeSpan.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the distance  from the last station");
                                    double d = double.Parse(Console.ReadLine());
                                    TimeSpan t2 = new TimeSpan(0, 0, 0);
                                    BusLine bus = new BusLine(num, a, new BusLineStation(first, t2, 0), new BusLineStation(last, t, d));
                                    egged.addBus(bus);
                                }
                                else//to add an existing station
                                {
                                    Console.WriteLine("enter a number of first station");
                                    int num1 = int.Parse(Console.ReadLine());
                                    BusStation first = bussesStop[bussesStop.index(num1)];
                                    Console.WriteLine("enter a number of the last station");
                                    int num2 = int.Parse(Console.ReadLine());
                                    BusStation last = bussesStop[bussesStop.index(num1)];
                                    Console.WriteLine("enter the travel time from the last station");
                                    TimeSpan t = TimeSpan.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the distance  from the last station");
                                    double d = double.Parse(Console.ReadLine());
                                    TimeSpan t2 = new TimeSpan(0, 0, 0);
                                    BusLine bus = new BusLine(num, a, new BusLineStation(first, t2, 0), new BusLineStation(last, t, d));
                                    egged.addBus(bus);
                                }
                                break;
                            }
                        case Menu.AddExistingStation:
                            {
                                Console.WriteLine("enter a number of bus to add");
                                int line = int.Parse(Console.ReadLine());
                                BusLine bus = egged[line];//the indexer return a busLine according to the bus number, and throw an exception if necessary
                                Console.WriteLine("enter a number of the station");
                                int stn = int.Parse(Console.ReadLine());
                                BusStation s = bussesStop[bussesStop.index(stn)];//the indexer will throw an exception if necessary
                                Console.WriteLine("enter the travel time from the last station");
                                TimeSpan t = TimeSpan.Parse(Console.ReadLine());
                                Console.WriteLine("enter the distance  from the last station");
                                double d = double.Parse(Console.ReadLine());
                                Console.WriteLine("please enter an index to insert the station");
                                int i = int.Parse(Console.ReadLine());
                                bus.add(i, new BusLineStation(s, t, d));
                                break;
                            }
                        case Menu.AddNewStation:
                            {

                                Console.WriteLine("enter a number of bus to add");
                                int line = int.Parse(Console.ReadLine());
                                BusLine bus = egged[line];//the indexer return a busLine according to the bus number, and throw an exception if necessary
                                Console.WriteLine("enter a number of the station");
                                int stn = int.Parse(Console.ReadLine());
                                Console.WriteLine("enter a latitude");
                                double lat = double.Parse(Console.ReadLine());
                                Console.WriteLine("enter a longitude");
                                double lon = double.Parse(Console.ReadLine());
                                BusStation s = new BusStation(stn, lat, lon);
                                Console.WriteLine("enter the travel time from the last station");
                                TimeSpan t = TimeSpan.Parse(Console.ReadLine());
                                Console.WriteLine("enter the distance  from the last station");
                                double d = double.Parse(Console.ReadLine());
                                Console.WriteLine("please enter an index to insert the station");
                                int i = int.Parse(Console.ReadLine());
                                bus.add(i, new BusLineStation(s, t, d));
                                bussesStop.add(s);//insert the station s to the stations list only if exception is not throwed
                                break;
                            }
                        case Menu.DeleteBus:
                            {
                                Console.WriteLine("Please enter a number of bus to delete");
                                int line = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter the number of the first stop of the bus");
                                int stn = int.Parse(Console.ReadLine());
                                egged.deleteBus(stn, line);//if the bus is not exist, an exception wil thrown
                                break;
                            }
                        case Menu.DeleteStation:
                            {
                                Console.WriteLine("Please enter a number of the bus ");
                                int line = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter the number of the first stop of the bus");
                                int first = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter the number of the stop to delete");
                                int stn = int.Parse(Console.ReadLine());
                                egged.deleteStation(line, first, stn);//if the bus is not exist, an exception wil thrown
                                break;

                            }
                        case Menu.BusLinesPassingThrough:
                            {
                                Console.WriteLine("enter a number of station, to print all the busses that passing through");
                                int stn = int.Parse(Console.ReadLine());
                                List<int> lst = egged.busesPassing(stn);
                                foreach (var item in lst)
                                {
                                    Console.WriteLine(item);
                                }
                                break; 
                            }
                    case Menu.PrintAllPath:
                            {
                                Console.WriteLine("enter a number of first station, to print all pathes");
                                int stn1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("enter a number of second station, to print all pathes");
                                int stn2 = int.Parse(Console.ReadLine());
                                egged.printAllPath(stn1, stn2);//if there is no busses, an exception wil thrown
                                break;
                            }
                    case Menu.PrintBusses:
                            {
                                foreach (BusLine item in egged)
                                {
                                    Console.WriteLine(item.BusNumber);
                                }
                                break;
                            }
                    case Menu.PrintBussesAndStation:
                            {
                                foreach (var item in egged)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            }
                    default:
                            Console.WriteLine("ERROR");
                        break;
                }
            }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DuplicateNameException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch
                {
                    Console.WriteLine("ERROR");
                }
               
            } while (choice!=0);
        }
    }
}
