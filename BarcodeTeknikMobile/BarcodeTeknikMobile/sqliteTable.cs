using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BarcodeTeknikMobile
{
    class sqliteTable
    {
        public static string TableWoTransaction = "Trx_WO";
        public static string TableTrx = "Trx_Id";
        public static string TableSite = "T_Site";
        public static string TableRak= "T_Rak";
        public static string TableStockOpname = "T_StockOpname";

        public static string Wo_Mo = "WO_NO";
        public static string PartNo = "Part_No";
        public static string qty = "QTY";
        public static string Contract = "Contract";
        public static string StatusKirim = "StatusKirim";
        public static string flag = "flag";

        public static string Site = "Site";
        public static string opnamedate = "opnamedate";
        public static string Rakno = "Rakno";
        public static string TransactionID = "TransactionId";

        public static string uom = "uom";

        public static string CREATE_TableWoTransaction = "CREATE TABLE " + TableWoTransaction + "(Id INTEGER PRIMARY KEY AUTOINCREMENT" +
          "," + Wo_Mo + " TEXT," + PartNo + " TEXT,"+qty+" TEXT,"+flag+" TEXT,"+ Contract + " TEXT," + TransactionID + " TEXT,"+ StatusKirim + " TEXT,"+Rakno+" TEXT);";

        public static string Creat_TableTrxId = "CREATE TABLE " + TableTrx + "(Id INTEGER PRIMARY KEY AUTOINCREMENT" +
         "," + TransactionID + " TEXT);";

        public static string Creat_TableSite = "CREATE TABLE " + TableSite + "(Id INTEGER PRIMARY KEY AUTOINCREMENT" +
        "," + Site + " TEXT);";

        public static string Creat_TableRak = "CREATE TABLE " + TableRak + "(Id INTEGER PRIMARY KEY AUTOINCREMENT" +
      "," + Rakno + " TEXT);";

        public static string create_TableStockOpname = "CREATE TABLE " + TableStockOpname + "(Id INTEGER PRIMARY KEY AUTOINCREMENT" +
       "," + PartNo + " TEXT," + qty + " TEXT,"+ opnamedate + " TEXT,"+Contract+" TEXT," + StatusKirim + " TEXT," + Rakno + " TEXT,"+uom+" TEXT);";
    }
}