﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        IBL bl = BLFactory.GetBL();
        public MainWindow()
        {
            InitializeComponent();
            // MessageBox.Show(bl.GetLine(2).FirstStationName);
           // string hi = "הי";
            //string name = "שרה";
          //  MessageBox.Show(string.Format("{0} לך,{1}", hi,name));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManangmentWindow manangmentWindow = new ManangmentWindow(bl);
            manangmentWindow.ShowDialog();
        }
    }
}
