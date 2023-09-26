using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AddressBook
{
    internal class ZipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string zip = (string)value;
            if (Regex.IsMatch(zip, @"^[0-9]{7}$") == true)
                return string.Format("{0:000-0000}", int.Parse(zip));
            return zip;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string zip = (string)value;
            if (Regex.IsMatch(zip, @"^[0-9]{3}-[0-9]{4}$") == true)
                return zip.Replace("-", "");
            return zip;
        }
    }
}
