using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BarcodeTeknikMobile;

namespace BarcodeTeknikMobile
{
    [Activity(Label = "login", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class login : AppCompatActivity
    {
        ImageButton btnlogin;
        EditText txtUserId, txtPassword;
        TextView vwchangepw;
        int progress = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);

            btnlogin = FindViewById<ImageButton>(Resource.Id.btnlogin);
            txtUserId = FindViewById<EditText>(Resource.Id.txtUserId);
            txtPassword = FindViewById<EditText>(Resource.Id.txtpassword);
            vwchangepw = FindViewById<TextView>(Resource.Id.vwchangepw);

            vwchangepw.Click += delegate
            {
                StartActivity(typeof(changepassword));
                Finish();
            };

            btnlogin.Click += delegate
            {
                
                    ProgressDialog progressDialog = ProgressDialog.Show(this, "", "Loading...", true);
                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                    new Thread(new ThreadStart(delegate
                    {
                    try
                    {
                        progress = 0;
                        localhost1.Service1 service = new localhost1.Service1();
                        if (service.cekuser(txtUserId.Text.ToString(), txtPassword.Text.ToString()).count == 1)
                        {
                            Intent i = new Intent(this, typeof(MainActivity));
                            i.PutExtra("UserId", txtUserId.Text.ToString());
                            StartActivity(i);

                            progress = 1;

                            if (progress == 1)
                            {
                                progressDialog.Dismiss();
                            }

                            Finish();

                            RunOnUiThread(async () =>
                        {
                            Toast.MakeText(ApplicationContext, "Login Success", ToastLength.Long).Show();
                            await Task.Delay(50);
                        });

                        }

                        else
                        {
                            RunOnUiThread(async () =>
                            {
                                progress = 1;

                                if (progress == 1)
                                {
                                    progressDialog.Dismiss();
                                }
                                Toast.MakeText(ApplicationContext, "Please check your user or password", ToastLength.Long).Show();
                                await Task.Delay(50);
                            });
                        }
                        }
                        catch (Exception ex)
                        {
                            RunOnUiThread(async () =>
                            {
                                Toast.MakeText(this, "Network Error", ToastLength.Short).Show();
                                progressDialog.Dismiss();
                            });
                          
                        }
                    })).Start();
                };
            
            // Create your application here
        }
    }
}