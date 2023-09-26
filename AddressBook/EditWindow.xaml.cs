using AddressBook.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AddressBook
{
    /// <summary>
    /// EditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class EditWindow : Window
    {
        public enum EditMode
        {
            Create,
            Update
        }

        public EditMode Mode { get; set; } = EditMode.Create;

        public int Id { get; set; } = 0;

        private EditWindowViewModel VM { get; set; }

        public EditWindow()
        {
            InitializeComponent();
            VM = new EditWindowViewModel();
            DataContext = VM;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WaitWindow_Loaded));
        }

        private void WaitWindow_Loaded(object? state)
        {
            Thread.Sleep(10);
            this.Dispatcher.Invoke((Action)(() =>
            {
                using (var lc = new LearnContext())
                {
                    if (Mode == EditMode.Update)
                    {
                        {
                            int id = Id;
                            var item = lc.TrnAddresses.Where(x => x.Id == id).First();
                            VM.Name = item.Name;
                            VM.ZipCode = item.ZipCode;
                            VM.Pref = item.Pref;
                            VM.City = item.City;
                            VM.Town = item.Town;
                            VM.Block = item.Block;
                            VM.Apart = item.Apart;
                            VM.Tel = item.Tel;
                            VM.Mail = item.Mail;
                            VM.Memo = item.Memo;
                        }
                        this.Title = "修正";
                        buttonCreate.Visibility = Visibility.Collapsed;
                        buttonUpdate.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.Title = "登録";
                        buttonCreate.Visibility = Visibility.Visible;
                        buttonUpdate.Visibility = Visibility.Collapsed;
                    }
                    //
                    var result = lc.MstPrefectures.OrderBy(id => id.Id);
                    foreach (var item in result)
                    {
                        VM.ComboPrefItems.Add(item);
                    }
                }
            }));
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = string.Empty;
            VM.ZipCode = string.Empty;
            VM.Pref = string.Empty;
            VM.City = string.Empty;
            VM.Town = string.Empty;
            VM.Block = string.Empty;
            VM.Apart = string.Empty;
            VM.Tel = string.Empty;
            VM.Mail = string.Empty;
            VM.Memo = string.Empty;
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            using (var lc = new LearnContext())
            {
                lc.TrnAddresses.Add(new TrnAddress
                {
                    Name = VM.Name,
                    ZipCode = VM.ZipCode,
                    Pref = VM.Pref,
                    City = VM.City,
                    Town = VM.Town,
                    Block = VM.Block,
                    Apart = VM.Apart,
                    Tel = VM.Tel,
                    Mail = VM.Mail,
                    Memo = VM.Memo
                });
                lc.SaveChanges();
            }
            MessageBox.Show("登録完了", this.Title);
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var lc = new LearnContext())
            {
                var item = lc.TrnAddresses.Where(x => x.Id == Id).First();
                item.Name = VM.Name;
                item.ZipCode = VM.ZipCode;
                item.Pref = VM.Pref;
                item.City = VM.City;
                item.Town = VM.Town;
                item.Block = VM.Block;
                item.Apart = VM.Apart;
                item.Tel = VM.Tel;
                item.Mail = VM.Mail;
                item.Memo = VM.Memo;
                lc.SaveChanges();
            }
            MessageBox.Show("修正完了", this.Title);
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonZip_Click(object sender, RoutedEventArgs e)
        {
            // 郵便番号検索
            using (var lc = new LearnContext())
            {
                var result = lc.MstZips.Join(
                    lc.MstCities,
                    p => p.GovCode,
                    c => c.GovCode,
                    (p, c) => new
                    {
                        p.ZipCode,
                        p.GovCode,
                        c.Pref,
                        c.City,
                        p.Town
                    })
                    .Where(x => x.ZipCode == VM.ZipCode)
                    .ToList();

                // ドロップダウンリスト更新
                VM.ComboTownItems.Clear();
                foreach (var item in result)
                {
                    VM.ComboTownItems.Add(item);
                }

                // フィールドをクリア
                VM.Pref = string.Empty;
                VM.City = string.Empty;
                VM.Town = string.Empty;

                // 検索結果が0の場合終了
                if (result.Count == 0)
                {
                    return;
                }

                // フィールドを設定
                VM.Pref = result[0].Pref;
                VM.City = result[0].City;
                if (result.Count() == 1)
                {
                    VM.Town = result[0].Town;
                }
            }
        }
    }
}
