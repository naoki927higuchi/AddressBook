using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.model;

namespace AddressBook.comparer
{
    internal class MstZipComparer : IEqualityComparer<MstZip>
    {
        public bool Equals(MstZip? x, MstZip? y)
        {
            if (x is not null && y is not null)
                return x.ZipCode.Equals(y.ZipCode) && x.GovCode.Equals(y.GovCode) && x.Town.Equals(y.Town);
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(MstZip obj)
        {
            return obj?.ZipCode.GetHashCode() ^ obj?.GovCode.GetHashCode() ^ obj?.Town.GetHashCode() ?? 0;
        }
    }
}
