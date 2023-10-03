using AddressBook.model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AddressBook
{
    internal class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<ViewAddress> _addressList = new ObservableCollection<ViewAddress>();
        public ObservableCollection<ViewAddress> AddressList
        {
            get { return _addressList; }
            set { SetProperty(ref _addressList, value); }
        }

        private int _currentIndex = -1;

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                SetProperty(ref _currentIndex, value);
            }
        }

        private ViewAddress _selectedAddress = new ViewAddress();
        public ViewAddress SelectedAddress
        {
            get
            {
                if (_currentIndex == -1)
                    return null;
                return _addressList[_currentIndex];
            }
            set
            {
                if (_currentIndex == -1)
                    return;
                AddressList[_currentIndex] = value;
            }
        }


        private string _name = string.Empty;
        private string _zipCode = string.Empty;
        private string _address = string.Empty;
        private string _tel = string.Empty;
        private string _mail = string.Empty;
        private string _memo = string.Empty;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string ZipCode
        {
            get
            {
                return _zipCode;
            }
            set
            {
                SetProperty(ref _zipCode, value);
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                SetProperty(ref _address, value);
            }
        }

        public string Tel
        {
            get
            {
                return _tel;
            }
            set
            {
                SetProperty(ref _tel, value);
            }
        }

        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                SetProperty(ref _mail, value);
            }
        }

        public string Memo
        {
            get
            {
                return _memo;
            }
            set
            {
                SetProperty(ref _memo, value);
            }
        }
    }
}