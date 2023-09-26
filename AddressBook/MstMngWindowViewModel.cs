using AddressBook.model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AddressBook
{
    internal class MstMngWindowViewModel : BindableBase
    {
        private string _zipFileName = string.Empty;
        private string _telFileName = string.Empty;

        public string ZipFileName
        {
            get
            {
                return _zipFileName;
            }
            set
            {
                SetProperty(ref _zipFileName, value);
            }
        }

        public string TelFileName
        {
            get
            {
                return _telFileName;
            }
            set
            {
                SetProperty(ref _telFileName, value);
            }
        }
    }
}