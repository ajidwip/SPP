using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Graphics;
using BarcodeTeknikMobile.localhost1;
using System;
using System.Collections.Generic;
using Android.Views;
using System.Data;
using Android.Support.Design.Widget;
using Android.Content;
using Android.Database.Sqlite;
using Firebase;
using Firebase.Storage;
using ZXing.Mobile;

using Android.Text;
using Android.Text.Style;
using Android.Content.PM;

namespace BarcodeTeknikMobile
{
    [Activity(Label="",Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        ISharedPreferences shared;
        BottomNavigationView bottomNavigationView;
        Bundle bundle = new Bundle();
        public static MobileBarcodeScanner scanner;
        //ImageButton btnscan;
        string Departemen = "";
        string UserId = "";
        Fragment fragment;
        string WOTrans = "";
        string result = "";
        ISharedPreferencesEditor editor;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
         
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigationView.NavigationItemSelected += BottomNavigation_NavigationItemSelected;

            shared = GetSharedPreferences("sharedprefrences", 0);
             editor = shared.Edit();
            if (Intent.GetStringExtra("UserId")!=null)
            {
                UserId = Intent.GetStringExtra("UserId");
            }
            else
            {
                UserId = shared.GetString("UserId", null);
            }

            if (Departemen=="")
            {
                try
                {
                    localhost1.Service1 service = new localhost1.Service1();
                    Departemen = service.getdept(UserId).dept;

                }
                catch(Exception ex)
                {
                    Departemen = shared.GetString("Dept", null);
                }
            }

            // ISharedPreferencesEditor editor = shared.Edit();
            editor.PutString("UserId", UserId);
            editor.PutString("Dept", Departemen);
            editor.Commit();

           // btnscan = toolbar.FindViewById<ImageButton>(Resource.Id.btnscan);
           

            //if (Intent.GetStringExtra("result")!=null)
            //{
            //    result = Intent.GetStringExtra("result");
            //}

            if (Intent.GetStringExtra("dept") != null)
            {
                Departemen = Intent.GetStringExtra("dept");
            }
            else
            {
                Departemen = shared.GetString("Dept", null);
            }

            if (Intent.GetStringExtra("trans") != null)
            {
                WOTrans = Intent.GetStringExtra("trans");
            }
            else
            {
                WOTrans =shared.GetString("WO",null);
            }

            //btnscan.Click += delegate  {

            //    fragment = new scanact();
             
            //    if (fragment != null)
            //    {
            //        FragmentManager fragmentManager2 = this.FragmentManager;
            //        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
            //        fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
            //        fragmenttransaction.Commit();
            //    }             
            //};
           
            if (Departemen == "Teknik")
            {
                //if (result != "" && result!=null)
                //{
                //    fragment = new PemakaianBarangFragment();
                //    //bundle.PutString("result", result);
                //    bundle.PutString("WO", WOTrans);
                //    fragment.Arguments = bundle;
                //    if (fragment != null)
                //    {
                //        FragmentManager fragmentManager2 = this.FragmentManager;
                //        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                //        fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
                //        fragmenttransaction.Commit();
                //    }
                //}
                //else{
                    fragment = new WoActivity();
                    bundle.PutString("Dept", Departemen);
                    bundle.PutString("trans", WOTrans);
                    fragment.Arguments = bundle;
                    if (fragment != null)
                    {
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
                        fragmenttransaction.Commit();
                    }
                //}
            }
            else
            {
              
                if (shared.GetString("MenuTrans", null) == "PemakaianBarang")
                {
                    fragment = new WoActivity();
                    bundle.PutString("Dept", Departemen);
                    bundle.PutString("trans", WOTrans);
                    fragment.Arguments = bundle;
                    if (fragment != null)
                    {
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
                        fragmenttransaction.Commit();
                    }
                   
                }
                else if (shared.GetString("MenuTrans", null) == "StockOpname")
                {
                    fragment = new StockOpnameFragment();
                    bundle.PutString("tanggalopname", shared.GetString("tanggalopname", null));
                    
                    bundle.PutString("Dept", "");
                    bundle.PutString("trans", WOTrans);
                    fragment.Arguments = bundle;
                    if (fragment != null)
                    {
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
                        fragmenttransaction.Commit();
                    }
                }
                else if (shared.GetString("MenuTrans", null) == null)
                {
                 
                    editor.PutString("MenuTrans", "StockOpname");
                    editor.Commit();


                    fragment = new StockOpnameFragment();
                    bundle.PutString("Dept", "");
                    bundle.PutString("trans", WOTrans);
                    fragment.Arguments = bundle;
                    if (fragment != null)
                    {
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
                        fragmenttransaction.Commit();
                    }

                }

              
            }
        }
       public void getdata(string WO1,string Contract)
        {
            ISharedPreferencesEditor editor = shared.Edit();
            editor.PutString("WO", WO1);
            editor.Commit();

            //WOTrans = WO1;
            bundle.PutString("WO", WO1);
            bundle.PutString("Contract", Contract);
            fragment = new PemakaianBarangFragment();
            fragment.Arguments = bundle;

            if (fragment != null)
            {
                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
                fragmenttransaction.CommitAllowingStateLoss();
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.menu_submit, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Fragment currentFragment = FragmentManager.FindFragmentById(Resource.Id.containerbody);
            string a = currentFragment.ToString();

            if (a.Contains("PemakaianBarangFragment"))
            {
                ((PemakaianBarangFragment)currentFragment).OnBackPressed();
            }
            else if (a.Contains("StockOpnameFragment"))
            {
                ISharedPreferencesEditor editor = shared.Edit();
                editor.PutString("MenuTrans", null);
                editor.Commit();
                ((StockOpnameFragment)currentFragment).OnBackPressed();
            }
            else if (a.Contains("WoActivity"))
            {
                ISharedPreferencesEditor editor = shared.Edit();
                editor.PutString("MenuTrans", null);
                editor.Commit();
                ((WoActivity)currentFragment).OnBackPressed();
            }
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            if (Departemen != "Teknik")
            {
                LoadFragment(e.Item.ItemId);
            }
        }
        void LoadFragment(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
               
                case Resource.Id.action_StockOpname:
                 
                    editor.PutString("MenuTrans", "StockOpname");
                    editor.Commit();
                    fragment = new StockOpnameFragment();
                    break;
                case Resource.Id.action_pemakaian:
                    editor.PutString("MenuTrans", "PemakaianBarang");
                    editor.Commit();
                    fragment = new WoActivity();
                    break;
                case Resource.Id.action_Upload:
                    editor.PutString("MenuTrans",null);
                    editor.Commit();
                    fragment = new uploadphotofragment();
                    break;
            }
            bundle.PutString("Dept", "");
            fragment.Arguments = bundle;
            if (fragment != null)
            {
                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Replace(Resource.Id.containerbody, fragment);
                fragmenttransaction.CommitAllowingStateLoss();
            }
        }
    }
}

