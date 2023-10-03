using AddressBook.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AddressBook.converter
{
    internal class TelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tel = (string)value;
            if (Regex.IsMatch(tel, @"^[0-9]{11}$") == true)
                return string.Format("{0:000-0000-0000}", long.Parse(tel));
            if (Regex.IsMatch(tel, @"^[0-9]{10}$") == true)
            {
                using (var lc = new LearnContext())
                {
                    string[] area_codes = { tel.Substring(1, 1), tel.Substring(1, 2), tel.Substring(1, 3), tel.Substring(1, 4) };
                    var result = lc.MstAreaCodes
                        .GroupBy(x => x.AreaCode)
                        .Select(x => new { AreaCode = x.Key, x.Key.Length })
                        .Where(x => area_codes.Contains(x.AreaCode))
                        .OrderByDescending(x => x.Length);
                    if (result.Any())
                    {
                        var len = result.First().Length;
                        string len3 = string.Format("{0}-{1}-{2}", tel.Substring(0, len + 1), tel.Substring(len + 1, 5 - len), tel.Substring(6, 4));
                        return len3;
                    }
                }
            }
            return tel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tel = (string)value;
            return tel.Replace("-", "");
        }
    }
}
