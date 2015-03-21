using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Manager
{
    public class MultiplierConverter : BaseConverter
    {

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return (double)value * (double)parameter;
            }
            catch(InvalidCastException e)
            {
                Console.WriteLine("value is not double");
                return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
