using AddressBook.comparer;
using AddressBook.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
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
    /// MstMngWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MstMngWindow : Window
    {
        private MstMngWindowViewModel VM { get; set; }
        public MstMngWindow()
        {
            InitializeComponent();
            VM = new MstMngWindowViewModel();
            DataContext = VM;
        }

        private void buttonBrowseZip_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "CSVファイル (*.csv)|*.csv|全てのファイル (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                VM.ZipFileName = dialog.FileName;
            }
        }

        private void buttonBrowseTel_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "CSVファイル (*.csv)|*.csv|全てのファイル (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                VM.TelFileName = dialog.FileName;
            }
        }

        private void buttonUpdateZip_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;

            using (StreamReader stream = new StreamReader(VM.ZipFileName))
            {
                List<MstAddress> addresses = new List<MstAddress>();
                List<MstZip> zips = new List<MstZip>();
                List<MstCity> cities = new List<MstCity>();
                List<MstPrefecture> prefectures = new List<MstPrefecture>();
                string? line = string.Empty;
                while ((line = stream.ReadLine()) != null)
                {
                    line = line.Replace("\"", "");
                    string[] strings = line.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
                    
                    // 住所マスタ
                    MstAddress mstAddress = new MstAddress()
                    {
                        GovCode = strings[0],
                        ZipCodeOld = strings[1],
                        ZipCode = strings[2],
                        PrefKana = strings[3],
                        CityCana = strings[4],
                        TownKana = strings[5],
                        Pref = strings[6],
                        City = strings[7],
                        Town = strings[8],
                        Code1 = int.Parse(strings[9]),
                        Code2 = int.Parse(strings[10]),
                        Code3 = int.Parse(strings[11]),
                        Code4 = int.Parse(strings[12]),
                        Code5 = int.Parse(strings[13]),
                        Code6 = int.Parse(strings[14])
                    };
                    addresses.Add(mstAddress);
                    // 都道府県マスタ
                    MstPrefecture pref = new MstPrefecture()
                    {
                        Pref = mstAddress.Pref,
                        PrefKana = mstAddress.PrefKana
                    };
                    prefectures.Add(pref);

                    // 市区町村マスタ
                    MstCity city = new MstCity()
                    {
                        GovCode = mstAddress.GovCode,
                        Pref = mstAddress.Pref,
                        City = mstAddress.City
                    };
                    cities.Add(city);

                    // 郵便番号マスタ
                    MstZip zip = new MstZip()
                    {
                        GovCode = mstAddress.GovCode,
                        ZipCode = mstAddress.ZipCode,
                        Town = mstAddress.Town
                    };

                    List<string> listTowns = new List<string>();

                    // 数値は半角に変換する。
                    string town = Regex.Replace(zip.Town, "[０-９]", p => ((char)(p.Value[0] - '０' + '0')).ToString());

                    // 正規表現【（.*?）】にマッチする文字列を削除する。ただし、（ｘｘ階）は残す。【（[0-9]*階）】
                    Regex regex = new Regex(@"（.*?）");
                    if (regex.IsMatch(town) == true)
                    {
                        // （ｘｘ階）が含まれていない場合、除去する
                        if (Regex.IsMatch(town, @"（[0-9]*階）") == false)
                        {
                            town = regex.Replace(town, "");
                        }
                    }

                    // 【、】を含む文字列は分割し、2レコードにする。
                    if (town.IndexOf("、") != -1)
                    {
                        string[] towns = town.Split("、");
                        if (towns.Length == 2)
                        {
                            listTowns.AddRange(towns);
                        }
                    }

                    // 【～】を含む文字列は数値を検出し分割する。ただし、99を超える場合は削除する。
                    else if (town.IndexOf("〜") != -1)
                    {
                        string[] towns = town.Split("〜");
                        if (towns.Length == 2)
                        {
                            int num1, num2;
                            regex = new Regex(@"[0-9].");
                            if (int.TryParse(regex.Match(towns[0]).Value, out num1) && int.TryParse(regex.Match(towns[1]).Value, out num2))
                            {
                                if (num2 < 100)
                                {
                                    string s = regex.Replace(towns[0], "{0}");
                                    for (int i = num1; i <= num2; i++)
                                    {
                                        listTowns.Add(string.Format(s, i));
                                    }
                                }
                            }
                        }
                    }

                    // 未処理データ
                    else
                    {
                        listTowns.Add(town);
                    }

                    // データ追加
                    foreach (string s in listTowns)
                    {
                        zips.Add(new MstZip
                        {
                            ZipCode = zip.ZipCode,
                            GovCode = zip.GovCode,
                            Town = Regex.Replace(s, "[0-9]", p => ((char)(p.Value[0] - '0' + '０')).ToString())
                        });
                    }
                }

                using (var lc = new LearnContext())
                {
                    // 都道府県マスタ
                    lc.Database.ExecuteSqlRaw(@"delete from mst_prefecture");
                    lc.MstPrefectures.AddRange(prefectures.Distinct(new MstPrefComparer()));
                    lc.Database.ExecuteSqlRaw(@"DBCC CHECKIDENT ('mst_prefecture', RESEED, 0)");

                    // 市区町村マスタ
                    lc.Database.ExecuteSqlRaw(@"delete from mst_city");
                    lc.MstCities.AddRange(cities.Distinct(new MstCityComparer()));

                    // 郵便番号マスタ
                    lc.Database.ExecuteSqlRaw(@"delete from mst_zip");
                    lc.MstZips.AddRange(zips.Distinct(new MstZipComparer()));
                    lc.Database.ExecuteSqlRaw(@"DBCC CHECKIDENT ('mst_zip', RESEED, 0)");
                    
                    lc.SaveChanges();
                }
            }
            Cursor = Cursors.Arrow;
            MessageBox.Show("登録終了", this.Title);
        }

        private void buttonUpdateTel_Click(object sender, RoutedEventArgs e)
        {
            //Cursor = Cursors.Wait;
            List<MstAreaCode> areas = new List<MstAreaCode>();
            using (StreamReader stream = new StreamReader(VM.TelFileName))
            {
                string? line = string.Empty;
                while ((line = stream.ReadLine()) != null)
                {
                    line = line.Replace("\"", "");
                    string[] strings = line.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);

                    MstAreaCode area = new MstAreaCode()
                    {
                        SectionCode = strings[0],
                        Section = strings[1],
                        AreaCode = strings[2],
                        CityCode = strings[3]
                    };
                    areas.Add(area);
                }

            }
            using (var lc = new LearnContext())
            {
                // 郵便番号マスタ全レコード削除
                lc.Database.ExecuteSqlRaw(@"delete from mst_area_code");
                lc.MstAreaCodes.AddRange(areas);
                lc.Database.ExecuteSqlRaw(@"DBCC CHECKIDENT ('mst_area_code', RESEED, 0)");
                lc.SaveChanges();
            }
            //Cursor = Cursors.Arrow;
            MessageBox.Show("登録終了", this.Title);
        }
    }
}
