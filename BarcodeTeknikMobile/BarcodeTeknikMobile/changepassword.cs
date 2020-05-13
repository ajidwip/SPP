using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BarcodeTeknikMobile;

namespace BarcodeTeknikMobile
{
    [Activity(Label = "changepassword", Theme = "@style/MyTheme")]
    public class changepassword : AppCompatActivity
    {
        EditText txtold, txtNew,txtUser;
        ImageButton btnchange;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.changepassword);

            txtUser = FindViewById<EditText>(Resource.Id.txtUser);
            txtold = FindViewById<EditText>(Resource.Id.txtold);
            txtNew = FindViewById<EditText>(Resource.Id.txtNew);
            btnchange=FindViewById<ImageButton>(Resource.Id.btnchange);

            btnchange.Click += delegate{
                localhost1.Service1 service = new localhost1.Service1();
                service.changepassword(txtUser.Text.ToString(), txtold.Text.ToString(), txtNew.Text.ToString());

                Toast.MakeText(ApplicationContext, "Password Changed", ToastLength.Long).Show();

                StartActivity(typeof(login));
                Finish();
            };
            // Create your application here
        }
    }
}