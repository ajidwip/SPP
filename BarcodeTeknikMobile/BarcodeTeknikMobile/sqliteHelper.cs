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
using Android.Database.Sqlite;
using System.Runtime.CompilerServices;

namespace BarcodeTeknikMobile
{
    public class sqliteHelper : SQLiteOpenHelper
    {
        private static sqliteHelper dbInstance = null;
        private const string _DatabaseName = "collector.db";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public sqliteHelper(Context context) : base(context, _DatabaseName, null, 1)
        {

        }

        public static sqliteHelper getDbInstance(Context context)
        {

            if (dbInstance == null)

                dbInstance = new sqliteHelper(context.ApplicationContext);
            return dbInstance;
        }
        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(sqliteTable.CREATE_TableWoTransaction);

            db.ExecSQL(sqliteTable.Creat_TableTrxId);
            db.ExecSQL(sqliteTable.Creat_TableSite);
            db.ExecSQL(sqliteTable.create_TableStockOpname);
            db.ExecSQL(sqliteTable.Creat_TableRak); 
        }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {

        }
        public void initDatabase()
        {
            SQLiteDatabase db = this.WritableDatabase;
            if (db.IsOpen)
            {
                db.Close();
            }
        }
    }
}