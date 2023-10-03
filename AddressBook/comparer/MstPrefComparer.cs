using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.model;

namespace AddressBook.comparer
{
    internal class MstPrefComparer : IEqualityComparer<MstPrefecture>
    {
        public bool Equals(MstPrefecture? x, MstPrefecture? y)
        {
            if (x is not null && y is not null)
                return x.Pref.Equals(y.Pref) && x.PrefKana.Equals(y.PrefKana);
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(MstPrefecture obj)
        {
            return obj?.Pref.GetHashCode() ^ obj?.PrefKana.GetHashCode() ?? 0;
        }
    }
}
