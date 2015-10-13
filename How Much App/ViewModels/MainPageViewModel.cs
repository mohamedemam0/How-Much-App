using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace How_Much_App.ViewModels
{
    class MainPageViewModel
    {


        public async void InsertData(string Name, string Price)
        {
            if (Name != "" && Price != "")
            {
                var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "expenses.sqlite");
                using (var db = new SQLite.SQLiteConnection(dbPath))
                {
                    db.CreateTable<Data>();
                    db.Insert(new Data()
                    {
                        DataName = Name,
                        DataPrice = Price + " EGP",
                        DateTime = DateTime.Now
                    });
                }
                Name = "";
                Price = "";
            }
            else
            {
                await new MessageDialog("Please fill the Name and Price fields!", "Error").ShowAsync();
            }
        }

        public void LoadData(ListView listView)
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "expenses.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                var res = db.Table<Data>().ToList();
                listView.ItemsSource = res;
            }
        }

        public void ClearData()
        {
          
                var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "expenses.sqlite");
                using (var db = new SQLite.SQLiteConnection(dbPath))
                {
                    db.DeleteAll<Data>();
                }
           
        }

        public async void Calculate()
        {
            try
            {
                var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "expenses.sqlite");
                using (var db = new SQLite.SQLiteConnection(dbPath))
                {
                    SQLiteCommand comm = new SQLiteCommand(db);
                    comm.CommandText = "SELECT SUM(DataPrice) FROM Data";
                    var sum = comm.ExecuteScalar<int>();
                    await new MessageDialog("Money Spent: " + sum.ToString() + " EGP", "Sum").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog("There's no data!","Error").ShowAsync();
            }

           
        }
    }
}
