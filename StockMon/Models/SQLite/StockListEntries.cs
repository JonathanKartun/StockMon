using System;
using SQLite;

namespace StockMon.Models.SQLite
{
    public class StockListEntries
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string StockLongName { get; set; }
        [MaxLength(15)]
        public string StockCode { get; set; }

        public bool MonitorStockActivated { get; set; }

        public double MonitorLevelReached { get; set; }
        public DateTime DateAdded { get; set; }

        public double LastRecordedRate { get; set; }

        public byte CategoryIndex { get; set; } //For When I introduce Categories
        public byte PriorityIndex { get; set; } //Maybe a marker Icon for Priority
        public int DisplayIndex { get; set; }  //Index to Display on the List ?
        public string DisplayHexColor { get; set; } //Change Backcolor
    }
}
