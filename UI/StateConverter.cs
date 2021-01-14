using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI
{
    public class StateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str;
            switch (value)
            {
                case BO.Status.Ready:
                    str = "Green";
                    break;
                case BO.Status.Dangerous:
                    str = "Red";
                    break;
                case BO.Status.Treatment:
                    str = "Purple";
                    break;
                case BO.Status.Driving:
                    str = "Brown";
                    break;
                case BO.Status.Refueling:
                    str = "Blue";
                    break;
                default:
                    str = "Black";
                    break;
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();//we dont use this convert back
        }
    }
}
