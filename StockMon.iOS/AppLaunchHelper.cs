using System;
using System.IO;

namespace StockMon.iOS
{
    internal class AppLaunchHelper
    {
        public static string getSQLiteDBPath()
        {
            string dbFileName = "stockmon.db.sqlite";
            string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "..", "Library"); //.. for going back 1 dir.
            return Path.Combine(folderPath, dbFileName);
        }
    }
}
