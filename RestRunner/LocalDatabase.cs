using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestRunner
{
    class LocalDatabase
    {
        public static void CreateDatabase()
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "History.sqlite");
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<History>();

                conn.Insert(new History() { Id = Guid.NewGuid(), Title = "history entry", Url = "http://bla", Payload = "q=x", Method = HttpMethod.Post.Method });
            }
        }
    }


    public class History
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Url { get; set; }
        public String Payload { get; set; }
        public String Method { get; set; }
    }
}
