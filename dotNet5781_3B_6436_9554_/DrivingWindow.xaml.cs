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

namespace dotNet5781_3B_6436_9554_
{
    /// <summary>
    /// Interaction logic for DrivingWindow.xaml
    /// </summary>
    public partial class DrivingWindow : Window
    {
        int  kilometer;
        /// <summary>
        /// the ctor get parameter by ref in order
        /// to update the main window within the distance of the inserted value
        /// </summary>
        /// <param name="km"></param>
        public DrivingWindow(ref int  km)
        {
            InitializeComponent();
            kilometer =  km;
        }
        /// <summary>
        /// in order to enable insert only a numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// only when the text box is not empty, the label "press enter to continue" is visible
        /// when the user insert enter, the window will be closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbKm.Text))
               labelContinue.Visibility = Visibility.Visible;
            else
                labelContinue.Visibility = Visibility.Hidden;
            if(e.Key==Key.Enter)
            {
                try
                {
                    kilometer = int.Parse(tbKm.Text);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("error");
                }
            }

        }
    }
}
