using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace dotNet5781_3B_6436_9554_
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        private ObservableCollection<Bus> collection;

        public AddBusWindow(ObservableCollection<Bus> c)
        {
            InitializeComponent();
            collection = c;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string license = tbLicense.Text;
                int km = int.Parse(tbKm.Text);
                int kmLast= int.Parse(tbKmLast.Text);
                DateTime start = dpStart.SelectedDate.Value;
                DateTime last = dpLast.SelectedDate.Value;
                Bus bus = new Bus(license, start, last, km, 1200, kmLast);
                collection.Add(bus);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void textOnlyNumber(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;


        }
        /// <summary>
        /// the button add is enable only when all the details are not empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void  key_up_btnIsenabled(object sender, KeyEventArgs e)
        {
            if (!(string.IsNullOrEmpty(tbKm.Text)) && !(string.IsNullOrEmpty(tbKmLast.Text))
                && !(string.IsNullOrEmpty(tbLicense.Text)) && !(string.IsNullOrEmpty(dpLast.Text)) && !(string.IsNullOrEmpty(dpStart.Text)))
            {
                btnAdd.IsEnabled = true;
            }
            else
            {
                btnAdd.IsEnabled = false;

            }
        }
    }
}
