using AddressBook.model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace AddressBook
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel VM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            VM = new MainWindowViewModel();
            DataContext = VM;
        }
        private void UpdateAddressDataGrid()
        {
            using (var lc = new LearnContext())
            {
                var result = lc.TrnAddresses.Select(x => new ViewAddress
                {
                    Id = x.Id,
                    Name = x.Name,
                    ZipCode = x.ZipCode,
                    Address = x.Pref + x.City + x.Town + x.Block + x.Apart,
                    Tel = x.Tel,
                    Mail = x.Mail,
                    Memo = x.Memo
                });
                if (VM.Name != string.Empty)
                {
                    result = result.Where(x => x.Name.Contains(VM.Name));
                }
                if (VM.ZipCode != string.Empty)
                {
                    result = result.Where(x => x.ZipCode.Contains(VM.ZipCode));
                }
                if (VM.Address != string.Empty)
                {
                    result = result.Where(x => x.Address.Contains(VM.Address));
                }
                if (VM.Tel != string.Empty)
                {
                    result = result.Where(x => x.Tel != null && x.Tel.Contains(VM.Tel));
                }
                if (VM.Mail != string.Empty)
                {
                    result = result.Where(x => x.Mail != null && x.Mail.Contains(VM.Mail));
                }
                if (VM.Memo != string.Empty)
                {
                    result = result.Where(x => x.Memo != null && x.Memo.Contains(VM.Memo));
                }

                VM.AddressList.Clear();
                foreach (var item in result)
                {
                    VM.AddressList.Add(item);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAddressDataGrid();
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = string.Empty;
            VM.ZipCode = string.Empty;
            VM.Address = string.Empty;
            VM.Tel = string.Empty;
            VM.Mail = string.Empty;
            VM.Memo = string.Empty;
        }

        private void buttonFind_Click(object sender, RoutedEventArgs e)
        {
            UpdateAddressDataGrid();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            EditWindow window = new EditWindow();
            window.Mode = EditWindow.EditMode.Create;
            window.ShowDialog();
            UpdateAddressDataGrid();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            EditWindow window = new EditWindow();
            window.Mode = EditWindow.EditMode.Update;
            window.Id = VM.SelectedAddress.Id;
            window.ShowDialog();
            UpdateAddressDataGrid();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var lc = new LearnContext())
            {
                var board = lc.TrnAddresses.Find(VM.SelectedAddress.Id);
                if (board != null)
                {
                    if (MessageBox.Show("削除しますか", this.Title, MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        lc.TrnAddresses.Remove(board);
                        lc.SaveChanges();
                        MessageBox.Show("削除しました", this.Title);
                        UpdateAddressDataGrid();
                    }
                }
            }
        }

        private void buttonMstAddressMng_Click(object sender, RoutedEventArgs e)
        {
            MstMngWindow window = new MstMngWindow();
            window.ShowDialog();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
