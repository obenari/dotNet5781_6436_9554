using System;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace UI
{
    /// <summary>
    /// convert from bus Status to color
    /// </summary>
    public class StatusConverter : IValueConverter
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
        /// <summary>
        /// this convert side is needless beacause the color dont infuence the status of the bus
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();//we dont use this convert back
        }
    }
}


