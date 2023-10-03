using AddressBook.model;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBook
{
    internal class EditWindowViewModel : BindableBase
    {
        private ObservableCollection<MstPrefecture> _comboPrefItems = new ObservableCollection<MstPrefecture>();
        public ObservableCollection<MstPrefecture> ComboPrefItems
        {
            get
            {
                return _comboPrefItems;
            }
            set
            {
                SetProperty(ref _comboPrefItems, value);
            }
        }
        private ObservableCollection<object> _comboTownItems = new ObservableCollection<object>();
        public ObservableCollection<object> ComboTownItems
        {
            get
            {
                return _comboTownItems;
            }
            set
            {
                SetProperty(ref _comboTownItems, value);
            }
        }

        private string _name = string.Empty;
        private string _zipCode = string.Empty;
        private string _pref = string.Empty;
        private string _city = string.Empty;
        private string _town = string.Empty;
        private string _block = string.Empty;
        private string _apart = string.Empty;
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
        public string Pref
        {
            get
            {
                return _pref;
            }
            set
            {
                SetProperty(ref _pref, value);
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                SetProperty(ref _city, value);
            }
        }
        public string Town
        {
            get
            {
                return _town;
            }
            set
            {
                SetProperty(ref _town, value);
            }
        }
        public string Block
        {
            get
            {
                return _block;
            }
            set
            {
                SetProperty(ref _block, value);
            }
        }
        public string Apart
        {
            get
            {
                return _apart;
            }
            set
            {
                SetProperty(ref _apart, value);
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

        public EditWindowViewModel()
        {
        }
    }
}
