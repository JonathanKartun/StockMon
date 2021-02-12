using System;
using System.IO;

namespace StockMon.Droid
{
    internal class AppLaunchHelper
    {
        public static string getSQLiteDBPath()
        {
            string dbFileName = "stockmon.db.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, dbFileName);
        }
    }
}
