using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace dotNet5781_3B_6436_9554_
{
    public class StateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str;
            switch (value)
            {
                case State.isReady: str= "Green";
                    break;
                case State.isDangerous: str= "Red";
                    break;
                case State.inTreatment: str= "Yellow";
                    break;
                case State.duringDriving: str= "Brown";
                    break;
                case State.refueling: str= "Blue";
                    break;
                default: str= "Black";
                    break;
            }
            return str ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            State state;
            switch (value)
            {
                case "Green":
                    state = State.isReady;
                    break;
                case "Red":
                    state = State.isDangerous;
                    break;
                case "Yellow":
                    state = State.inTreatment;
                    break;
                case "Brown":
                    state = State.duringDriving;
                    break;
                case "Blue":
                    state = State.refueling ;
                    break;
                default:
                    state = State.isReady;
                    break;
            }
            return state;
        }
    }
}
