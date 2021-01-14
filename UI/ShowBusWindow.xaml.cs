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
    /// Interaction logic for ShowBusWindow.xaml
    /// </summary>
    public partial class ShowBusWindow : Window
    {
        PO.Bus myBus;
      
        public ShowBusWindow(PO.Bus bus)
        {
            InitializeComponent();
            DataContext = bus;
            myBus = bus;
        }
    }
}
