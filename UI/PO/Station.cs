using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;


namespace PO
{
  public  class Station:DependencyObject
    {


        
        // Using a DependencyProperty as the backing store for Code.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.Register("Code", typeof(int), typeof(Station), new PropertyMetadata(0));
        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Station), new PropertyMetadata(""));



        /// <summary>
        /// the station number
        /// </summary>
        public int Code
        {
            get { return (int)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        /// <summary>
        /// the name of the station
        /// </summary>


        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

       

        /// <summary>
        /// list of the lines that passing by the specific station
        /// </summary>
        public ObservableCollection<BusLine> ListLines { get; set; }
    }
}
