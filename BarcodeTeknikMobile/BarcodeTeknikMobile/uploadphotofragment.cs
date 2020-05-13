using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;
using Com.Bumptech.Glide.Request;
using Com.Bumptech.Glide.Signature;
using Java.IO;
using Square.Picasso;

namespace BarcodeTeknikMobile
{
    public class uploadphotofragment : Fragment
    {
        public static ProgressDialog progressDialog;
        public static ProgressBar progressbar;
        public static Activity context;
        ISharedPreferences shared;
        Bundle bundle = new Bundle();
        //BottomNavigationView bottomNavigationView;
        Button btnupload;
        ImageView imgpart, imgpart2, imgpart3;
          public static   AutoCompleteTextView autoPartNo;
        Java.IO.File output,output2,output3;
        Bitmap myBitmap,mybitmap3,mybitmap2;
        public  Android.Net.Uri uri;
        int count = 0,count2=0,count3=0;
       public static List<string> partno1 = new List<string>();
        public override void OnCreate(Bundle savedInstanceState)
        {
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);
            // Create your fragment here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View itemView = LayoutInflater.From(container.Context).
                  Inflate(Resource.Layout.photoupload, container, false);
            context = Activity;

            // Permission is not granted
            // Should we show an explanation?


            autoPartNo = itemView.FindViewById<AutoCompleteTextView>(Resource.Id.autoPartNo);

           
           
            btnupload = itemView.FindViewById<Button>(Resource.Id.btnupload);
            shared = Activity.GetSharedPreferences("sharedprefrences", 0);
            ISharedPreferencesEditor editor = shared.Edit();
            editor.PutString("MenuTrans", null);
            editor.Commit();

            progressbar = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar);

            imgpart = itemView.FindViewById<ImageView>(Resource.Id.imgpart);
            imgpart2 = itemView.FindViewById<ImageView>(Resource.Id.imgpart2);
            imgpart3 = itemView.FindViewById<ImageView>(Resource.Id.imgpart3);

            imgpart.Click += delegate {
                if (autoPartNo.Text != "")
                {
                    Intent intent = new Intent(MediaStore.ActionImageCapture);
                    Java.IO.File dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim + "/Camera");
                    output = new Java.IO.File(dir, autoPartNo.Text.ToString() + "1.jpg");
                    intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(output));

                    int RESULT_IMAGE_CLICK = 1;
                    count++;
                    StartActivityForResult(intent, RESULT_IMAGE_CLICK);
                }
                else
                {
                    Toast.MakeText(Activity, "Harap Isi PartNo", ToastLength.Short).Show();
                }
               
            };

            imgpart2.Click += delegate
            {
                if (autoPartNo.Text != "")
                {
                    Intent intent = new Intent(MediaStore.ActionImageCapture);
                    Java.IO.File dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim + "/Camera");
                    output2 = new Java.IO.File(dir, autoPartNo.Text.ToString() + "2.jpg");
                    intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(output2));
                    int RESULT_IMAGE_CLICK = 2;
                    count2++;
                    StartActivityForResult(intent, RESULT_IMAGE_CLICK);
                }
                else
                {
                    Toast.MakeText(Activity, "Harap Isi PartNo", ToastLength.Short).Show();
                }
            };

            imgpart3.Click += delegate
            {
                if (autoPartNo.Text != "")
                {
                    Intent intent = new Intent(MediaStore.ActionImageCapture);
                    Java.IO.File dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim + "/Camera");
                    output3 = new Java.IO.File(dir, autoPartNo.Text.ToString() + "3.jpg");
                    intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(output3));
                    int RESULT_IMAGE_CLICK = 3;
                    count3++;
                    StartActivityForResult(intent, RESULT_IMAGE_CLICK);
                }
                else
                {
                    Toast.MakeText(Activity, "Harap Isi PartNo", ToastLength.Short).Show();
                }
            };

            btnupload.Click += delegate
            {
                progressbar.Visibility = ViewStates.Visible;
                btnupload.Visibility = ViewStates.Gone;
                Interval.SetIntervalAsync(Upload, 1000);
            };
            new LoadDataForActivity().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            return itemView;
        }
        public class Interval
        {
            public static async Task SetIntervalAsync(Action action, int delay)
            {
                try
                {
                    await Task.Delay(delay);
                    action();
                }
                catch (TaskCanceledException) { }
            }
        }
        public void Upload()
        {
            try
            {
                localhost1.Service1 service = new localhost1.Service1();

                MemoryStream baos = new MemoryStream();
                myBitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, baos);
                byte[] imageInByte = baos.ToArray();

                MemoryStream baos2 = new MemoryStream();
                mybitmap2.Compress(Bitmap.CompressFormat.Jpeg, 50, baos2);
                byte[] imageInByte2 = baos2.ToArray();

                MemoryStream baos3 = new MemoryStream();
                mybitmap3.Compress(Bitmap.CompressFormat.Jpeg, 50, baos3);
                byte[] imageInByte3 = baos3.ToArray();

                service.uploadphoto(imageInByte, autoPartNo.Text.ToString());
                service.uploadphoto2(imageInByte2, autoPartNo.Text.ToString());
                service.uploadphoto3(imageInByte3, autoPartNo.Text.ToString());
                progressbar.Visibility = ViewStates.Gone;
                btnupload.Visibility = ViewStates.Visible;
                autoPartNo.Text = "";
                Toast.MakeText(Activity.ApplicationContext, "Upload Success", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                progressbar.Visibility = ViewStates.Gone;
                btnupload.Visibility = ViewStates.Visible;
                Toast.MakeText(Activity.ApplicationContext, "Upload Failed", ToastLength.Long).Show();
            }
        }
        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
           
                if (requestCode == 1)
                {
                    if(output.Exists())
                    {
                    Java.IO.File imgFile = new Java.IO.File(output.AbsolutePath);
                    if (imgFile.Exists())
                    {
                        myBitmap = BitmapFactory.DecodeFile(imgFile.AbsolutePath);
                        
                        Glide.With(Activity).Load(imgFile.AbsolutePath).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(count))).Into(imgpart);
                    };
                }
                }
                else if (requestCode == 2)
                {
                    Java.IO.File imgFile2 = new Java.IO.File(output2.AbsolutePath);
                    if (imgFile2.Exists())
                    {
                    
                        mybitmap2 = BitmapFactory.DecodeFile(imgFile2.AbsolutePath);
                        Glide.With(Activity).Load(imgFile2.AbsolutePath).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(count2))).Into(imgpart2);
                };
              }

              else if (requestCode == 3)
              {
                Java.IO.File imgFile3 = new Java.IO.File(output3.AbsolutePath);
                if (imgFile3.Exists())
                {
                   
                    mybitmap3 = BitmapFactory.DecodeFile(imgFile3.AbsolutePath);
                    Glide.With(Activity).Load(imgFile3.AbsolutePath).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(count3))).Into(imgpart3);
                };
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
             
                case Resource.Id.menulogout:
                    {
                        Intent i = new Intent(Activity, typeof(login));
                        Activity.StartActivity(i);
                        Activity.Finish();
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }

        private class LoadDataForActivity : AsyncTask
        {
            protected override void OnPreExecute()
            {
                progressDialog = ProgressDialog.Show(context, "", "Loading...", true);
                progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            }

            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                try
                {
                    localhost1.Service1 MyClient = new localhost1.Service1();
                    localhost1.PartNoAutoComplete emp = new localhost1.PartNoAutoComplete();
                    DataTable dt = new DataTable();

                    emp = MyClient.GetPartNoAutoComplete();
                    dt = emp.PartNoAutoCompleteTable;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            partno1.Add(dt.Rows[i][0].ToString());
                        }
                      
                    }

                }
                catch (Exception ex)
                {

                }

                return null;
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                progressDialog.Dismiss();
                autoPartNo.Adapter = new ArrayAdapter<string>(context, Android.Resource.Layout.SimpleListItem1, partno1);

                //Log.WriteLine(LogPriority.Debug, "CurrentItem:", viewPager.CurrentItem.ToString());


                //progressBar.setVisibility(View.GONE);
            }
        }
    }
}