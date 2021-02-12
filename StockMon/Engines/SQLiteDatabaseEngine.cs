using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using StockMon.Helpers;
using StockMon.Models.Application;
using StockMon.Models.SQLite;

namespace StockMon.Engines
{
    public static class SQLiteDatabaseEngine<T>  where T : new()
    {
        #region Saving Data

        public static Task<ResponseClass> SaveRecord(T instance)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
                {
                    conn.CreateTable<T>();
                    int rows = conn.Insert(instance);
                    if (rows > 0)
                    {
                        return Task.FromResult( new ResponseClass("Succesfully Inserted!"));
                    } else {
                        var errorMsg = "Insert Failed";
                        return Task.FromResult(new ResponseClass(true, -1, errorMsg));
                    }
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseClass(true, ex.HResult, ex.Message));
            }
        }

        #endregion

        #region Reading Table Data

        public static TableQuery<T> ReadRecord()
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            {
                conn.CreateTable<T>();
                var bla = conn.Table<T>().ToList();
                return conn.Table<T>();
            }
        }

        public static List<T> ReadRecordToList()
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            {
                conn.CreateTable<T>();
                var bla = conn.Table<T>().ToList();
                return conn.Table<T>().ToList();
            }
        }

        #endregion

        #region Deletion Approach

        public static Task<ResponseClass> DeleteRecord(System.Linq.Expressions.Expression<Func<T, bool>> whereClause)
        {
            try
            {
                int rows;
                using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
                {
                    rows = conn.Table<T>().Where(whereClause).Delete();
                }
                if (rows > 0)
                {
                    return Task.FromResult(new ResponseClass($"Succesfully Deleted - {rows} items"));
                }
                else
                {
                    var errorMsg = "Delete Failed";
                    return Task.FromResult(new ResponseClass(true, -1, errorMsg));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseClass(true, ex.HResult, ex.Message));
            }

        }

        #endregion

        #region OrderingApproach

        public static TableQuery<T> ReadRecord(System.Linq.Expressions.Expression<Func<T, object>> orderBy)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            {
                conn.CreateTable<T>();
                return conn.Table<T>().OrderBy(orderBy);
            }
        }

        public static List<T> ReadRecordToList(System.Linq.Expressions.Expression<Func<T, object>> orderBy)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            {
                conn.CreateTable<T>();
                return conn.Table<T>().OrderBy(orderBy).ToList();
            }
        }

        #endregion

        #region WhereApproach

        public static TableQuery<T> ReadRecord(System.Linq.Expressions.Expression<Func<T, bool>> whereClause)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            {
                conn.CreateTable<T>();
                return conn.Table<T>().Where(whereClause);
            }
        }

        public static List<T> ReadRecordToList(System.Linq.Expressions.Expression<Func<T, bool>> whereClause)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            {
                conn.CreateTable<T>();
                return conn.Table<T>().Where(whereClause).ToList();
            }
        }
        #endregion
    }
}
