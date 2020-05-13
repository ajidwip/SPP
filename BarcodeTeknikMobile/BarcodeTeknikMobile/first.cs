using Android;
using Android.App;
using Android.Content.PM;
using Android.Database;
using Android.Database.Sqlite;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Widget;
using Com.Bumptech.Glide;
using Felipecsl.GifImageViewLibrary;
using System;
using System.Data;
using System.Net.Http;

namespace BarcodeTeknikMobile
{
    [Activity(Label = "M-SPP", MainLauncher = true,Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class first : AppCompatActivity
    {
        GifImageView gifImageView, gifImageView2;
      
        sqliteHelper dbinstance;
        SQLiteDatabase catalogdb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.first);

            dbinstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbinstance.WritableDatabase;
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted && ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Permission.Granted && ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.Camera, Manifest.Permission.ReadExternalStorage }, 0);
            }
            try
            {
               
                ICursor cursor = catalogdb.RawQuery("select * from " + sqliteTable.TableSite + "", null);

                if (cursor.Count == 0)
                {
                    localhost1.Service1 service = new localhost1.Service1();
                    localhost1.Site emp = new localhost1.Site();
                    DataTable dt = new DataTable();
                    emp = service.GetSite();
                    dt = emp.SiteTable;


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string a = dt.Rows[i][0].ToString();

                        catalogdb.ExecSQL("Insert into " + sqliteTable.TableSite + "(" + sqliteTable.Site + ") select '" + a + "'");

                    }
                }

                ICursor cursor2 = catalogdb.RawQuery("select * from " + sqliteTable.TableRak + "", null);

                if (cursor2.Count == 0)
                {
                    localhost1.Service1 service = new localhost1.Service1();
                    localhost1.Rak emp2 = new localhost1.Rak();
                    DataTable dt2 = new DataTable();
                    emp2 = service.GetRak();
                    dt2 = emp2.Raktable;


                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        string b = dt2.Rows[i][0].ToString();

                        catalogdb.ExecSQL("Insert into " + sqliteTable.TableRak + "(" + sqliteTable.Rakno + ") select '" + b + "'");

                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Network Error,Please restart app to download data", ToastLength.Short).Show();
            }
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageView);
                gifImageView2 = FindViewById<GifImageView>(Resource.Id.gifImageView2);
                gifimage();

                Handler h = new Handler();
                Action myAction = () =>
                {


                    StartActivity(typeof(login));
                    Finish();


                };

                h.PostDelayed(myAction, 8000);
          
        }
      
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            //if (requestCode == 1)
            //{
            //    if (grantResults.Length == 1 && grantResults[0] != Permission.Granted)
            //    {
            //        Snackbar.Make(rootLayout, Resource.String.permission_not_granted_termininating_app, Snackbar.LengthIndefinite)
            //                .SetAction(Resource.String.ok, delegate { FinishAndRemoveTask(); })
            //                .Show();
            //        return;
            //    }
            //}

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public  void gifimage()
        {
            Glide.With(ApplicationContext)
            .Load(Resource.Drawable.gears)
            .Into(gifImageView);

            Glide.With(ApplicationContext)
          .Load(Resource.Drawable.loading)
          .Into(gifImageView2);
           
        }
       
    }
  
}