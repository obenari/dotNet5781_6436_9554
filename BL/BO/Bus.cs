using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A class that defines a physical bus/
    /// </summary>
    public class Bus 
    {

        /// <summary>
        /// The license number of the bus
        /// this field is a string in order to enable save a license nuber with zero prefix
        /// </summary>
        public string License { get; set; }
        /// <summary>
        /// the date  start of working
        /// </summary>
        public DateTime StartOfWork { get; set; }
        /// <summary>
        /// The date of the last treatment
        /// </summary>
        public DateTime DateOfTreatment { get; set; }
        /// <summary>
        /// the mileage
        /// </summary>
        public int TotalKms { get; set; }
        /// <summary>
        /// How many kilometers left the bus could drive before refuel
        /// </summary>
        public int Fuel { get; set; }
        /// <summary>
        /// enum that contain the status of the bus (redy, intreatment etc..)
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// the total kilometers from the last treartment
        /// </summary>
        public int TotalKmsFromLastTreatment { get; set; }

        public override string ToString()
        {
            return
               $"License is: {License}, \n" +
               $"Start of Work: {StartOfWork.ToShortDateString()}, \n" +
               $"Total KM: {TotalKms}, \n" +
               $"Ready to drive: {Status} \n";
        }
        //private void OnPropertyChange(Bus bus, PropertyChangedEventArgs args)
        //{
        //    PropertyChanged?.Invoke(bus, args);
        //}
        //public event PropertyChangedEventHandler PropertyChanged;


    }
}
