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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Net;
using System.Data;
using System.Threading.Tasks;

namespace BarcodeTeknikMobile
{
    [Application]
    public class CoreApplication : Application
    {
        sqliteHelper dbInstance = null;
  
        public static CoreApplication app;
        public static DataTable dt;
       
        public CoreApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {

        }
        public CoreApplication()
        {
           
        
        }

        public override void OnCreate()
        {
            base.OnCreate();
            dbInstance = sqliteHelper.getDbInstance(this.ApplicationContext);
            dbInstance.initDatabase();
            app = this;
        }
       
        public static CoreApplication getInstance()
        {
            return app;
        }
        public sqliteHelper getDatabase()
        {
            return dbInstance;
        }

    }
}