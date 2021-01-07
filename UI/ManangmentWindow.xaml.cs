using BLAPI;
using System;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for ManangmentWindow.xaml
    /// </summary>
    public partial class ManangmentWindow : Window
    {
        IBL MyBL;
        public ManangmentWindow(IBL bl)
        {
            InitializeComponent();
            MyBL = bl;
        }

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            StationWindow stnWindow = new StationWindow();
            stnWindow.ShowDialog();
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            LineWindow lineWindow = new LineWindow();
            lineWindow.ShowDialog();
        }
    }
}
