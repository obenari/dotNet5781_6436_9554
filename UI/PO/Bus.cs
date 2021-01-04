using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
namespace PO
{
   public class Bus:DependencyObject
    {
        // Using a DependencyProperty as the backing store for TotalKmsFromLastTreatment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalKmsFromLastTreatmentProperty =
            DependencyProperty.Register("TotalKmsFromLastTreatment", typeof(int), typeof(Bus), new PropertyMetadata(0));
        // Using a DependencyProperty as the backing store for License.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LicenseProperty =
            DependencyProperty.Register("License", typeof(string), typeof(Bus), new PropertyMetadata(BO.Status.Ready));
        // Using a DependencyProperty as the backing store for StartOfWork.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartOfWorkProperty =
            DependencyProperty.Register("StartOfWork", typeof(DateTime), typeof(Bus)/*, new PropertyMetadata(new )*/);
        // Using a DependencyProperty as the backing store for DateOfTreatment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateOfTreatmentProperty =
            DependencyProperty.Register("DateOfTreatment", typeof(DateTime), typeof(Bus)/*, new PropertyMetadata(0)*/);
        // Using a DependencyProperty as the backing store for TotalKms.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalKmsProperty =
            DependencyProperty.Register("TotalKms", typeof(int), typeof(Bus), new PropertyMetadata(0));

        /// <summary>
        /// The license number of the bus
        /// this field is a string in order to enable save a license nuber with zero prefix
        /// </summary>
        public string License
        {
            get { return (string)GetValue(LicenseProperty); }
            set { SetValue(LicenseProperty, value); }
        }
        /// <summary>
        /// the date  start of working
        /// </summary>
        public DateTime StartOfWork
        {
            get { return (DateTime)GetValue(StartOfWorkProperty); }
            set { SetValue(StartOfWorkProperty, value); }
        }
        /// <summary>
        /// The date of the last treatment
        /// </summary>
        public DateTime DateOfTreatment
        {
            get { return (DateTime)GetValue(DateOfTreatmentProperty); }
            set { SetValue(DateOfTreatmentProperty, value); }
        }
        /// <summary>
        /// the mileage
        /// </summary>
        public int TotalKms
        {
            get { return (int)GetValue(TotalKmsProperty); }
            set { SetValue(TotalKmsProperty, value); }
        }
        /// <summary>
        /// How many kilometers left the bus could drive before refuel
        /// </summary>
        public int Fuel
        {
            get { return (int)GetValue(FuelProperty); }
            set { SetValue(FuelProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Fuel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FuelProperty =
            DependencyProperty.Register("Fuel", typeof(int), typeof(Bus), new PropertyMetadata(0));
        /// <summary>
        /// enum that contain the status of the bus (redy, intreatment etc..)
        /// </summary>
        public BO.Status Status
        {
            get { return (BO.Status)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(BO.Status), typeof(Bus), new PropertyMetadata(0));
        /// <summary>
        /// the total kilometers from the last treartment
        /// </summary>

        public int TotalKmsFromLastTreatment
        {
            get { return (int)GetValue(TotalKmsFromLastTreatmentProperty); }
            set { SetValue(TotalKmsFromLastTreatmentProperty, value); }
        }
        public override string ToString()
        {
            return
               $"License is: {License}, \n" +
               $"Start of Work: {StartOfWork.ToShortDateString()}, \n" +
               $"Total KM: {TotalKms}, \n" +
               $"Ready to drive: {Status} \n";
        }
    }
}
