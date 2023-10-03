using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.model;

namespace AddressBook.comparer
{
    internal class MstCityComparer : IEqualityComparer<MstCity>
    {
        public bool Equals(MstCity? x, MstCity? y)
        {
            if (x is not null && y is not null)
                return x.GovCode.Equals(y.GovCode) && x.Pref.Equals(y.Pref) && x.City.Equals(y.City);
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(MstCity obj)
        {
            return obj?.GovCode.GetHashCode() ^ obj?.Pref.GetHashCode() ^ obj?.City.GetHashCode() ?? 0;
        }
    }
}
