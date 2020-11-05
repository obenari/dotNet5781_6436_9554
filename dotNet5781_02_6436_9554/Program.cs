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
            StationList bussesStop = new StationList();
            
            BusCompany egged = new BusCompany();
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i=0;i<50;i++)
            {
                try
                {
                    bussesStop.add(new BusStation(r.Next(1000000)));
                }
                catch(IndexOutOfRangeException ex)
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

            }
            for(int i=0;i<20)
        }
    }
}
