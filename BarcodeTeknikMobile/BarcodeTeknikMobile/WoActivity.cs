using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using ZXing.Mobile;

namespace BarcodeTeknikMobile
{
   
    public class WoActivity : Fragment, RecyclerItemTouchHelper.RecyclerItemTouchHelperListener
    {
        ISharedPreferences shared;
        Bundle bundle = new Bundle();
        View view;
        private SQLiteDatabase catalogdb;
        RecyclerView mRecyclerView;
        WOAdapter mAdapter;
        List<WoGetSet> recyclelist = new List<WoGetSet>();
        //ISharedPreferences sharedPreferences;
        public List<string> list = new List<string>();
        int posisi = 0;
        string title = "";
        ZXing.Result result;
        private sqliteHelper dbInstance;
        Android.Support.V7.App.AlertDialog.Builder _dialogBuilder;
        Android.Support.V7.App.AlertDialog alertdialog;
        string date = "";
        EditText wo1,txtsearch;
        string WoTrans = "";
        Button btnsync;
        int progress = 0,flag=0;
        public static MobileBarcodeScanner scanner;
        string dept = "";
        BottomNavigationView bottomNavigationView;
        Spinner spcontract;
        ArrayAdapter adapter;
        Button btnsearch;
        WoGetSet removeitem;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);
            // Create your fragment here
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View itemView = LayoutInflater.From(container.Context).
                    Inflate(Resource.Layout.WO, container, false);
            date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();

            MobileBarcodeScanner.Initialize(this.Activity.Application);
            scanner = new MobileBarcodeScanner();

            shared = Activity.GetSharedPreferences("sharedprefrences", 0);
            ISharedPreferencesEditor editor = shared.Edit();
            editor.PutString("MenuTrans", null);
            editor.Commit();

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;

            view = inflater.Inflate(Resource.Layout.updateWO_dialog, container, false);


            ICursor cursor = catalogdb.RawQuery("select * from " + sqliteTable.TableSite + "", null);
            while (cursor.MoveToNext())
            {
                list.Add(cursor.GetString(1));
            }

            spcontract = itemView.FindViewById<Spinner>(Resource.Id.spcontract);

            adapter = new ArrayAdapter<string>(Activity, Resource.Layout.spinnercustomlayout, list);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spcontract.Adapter = adapter;

            wo1 = view.FindViewById<EditText>(Resource.Id.Wo);
            txtsearch=itemView.FindViewById<EditText>(Resource.Id.txtsearch);
            btnsearch = itemView.FindViewById<Button>(Resource.Id.btnseacrh);
            //sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);

            dept = Arguments.GetString("Dept");


            if (Arguments.GetString("trans") !=null)
            {
                WoTrans = Arguments.GetString("trans");
            }
           

            btnsync = itemView.FindViewById<Button>(Resource.Id.btnsync);
            btnsync.Visibility = ViewStates.Visible;
            btnsync.Click += delegate
            {
                ProgressDialog progressDialog = ProgressDialog.Show(Activity, "", "Please Wait...", true);
                progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                new Thread(new ThreadStart(delegate
                {
                    Activity.RunOnUiThread(async () =>
                    {
                        progress = 0;
                        localhost1.Service1 service = new localhost1.Service1();
                        service.synwo();
                        prepare();
                        progress = 1;

                        if (progress == 1)
                        {
                            progressDialog.Dismiss();
                        }
                        await Task.Delay(50);
                    });
                })).Start();
            };

            spcontract.ItemSelected += delegate
            {
                ISharedPreferencesEditor editor2 = shared.Edit();
                editor2.Remove("RakNo");
                editor2.Commit();
                prepare();
            };

            btnsearch.Click += delegate
            {
               
                    prepare();
                
             
            };

            txtsearch.TextChanged += delegate
            {
                if(txtsearch.Text=="")
                {
                    prepare();
                }
            };

            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerView.SetItemAnimator(new DefaultItemAnimator());
            mRecyclerView.AddItemDecoration(new DividerItemDecoration(Activity, DividerItemDecoration.Vertical));
            mAdapter = new WOAdapter(recyclelist);
           
            mAdapter.ItemClick += OnItemClick;
             //mAdapter.ItemClick2 += OnLongItemclick;
            prepare();
            mRecyclerView.SetAdapter(mAdapter);


            ItemTouchHelper.SimpleCallback itemTouchHelperCallback = new RecyclerItemTouchHelper(0, ItemTouchHelper.Left, this);
            new ItemTouchHelper(itemTouchHelperCallback).AttachToRecyclerView(mRecyclerView);
            return itemView;
        }
            public void prepare()
        {

            try
            {
                recyclelist.Clear();
                DataTable dt = new DataTable();
                localhost1.Contract emp = new localhost1.Contract();
                localhost1.Service1 MyClient = new localhost1.Service1();
                emp = MyClient.GetContract(txtsearch.Text.ToString(), spcontract.SelectedItem.ToString());
                dt = emp.ContracTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    recyclelist.Add(new WoGetSet(dt.Rows[i][0].ToString(), "WO No", dt.Rows[i][2].ToString(), dt.Rows[i][1].ToString()));
                }
                mAdapter.NotifyDataSetChanged();
            }catch(Exception ex)
            {
                Toast.MakeText(Activity, "Network Error", ToastLength.Short).Show();
            }

        }
        public void OnBackPressed()
        {
           
           
            Activity.Finish();
        }

        public void onSwiped(RecyclerView.ViewHolder viewHolder, int direction, int position)
        {
            posisi = position;
            removeitem = recyclelist[position];
            mAdapter.removeitem(position);
          
            using (_dialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(Activity))
            {
                _dialogBuilder.SetMessage("Tandai sebagai tanpa pemakaian sparepart?");
                _dialogBuilder.SetCancelable(false);
                _dialogBuilder.SetPositiveButton("OK", closeWO);
                _dialogBuilder.SetNegativeButton("Cancel", canceldialog);
                //_dialogBuilder.SetNegativeButton("Cancel", BatalAction);
                alertdialog = _dialogBuilder.Create();

                alertdialog.Show();

            }
        }
            void OnItemClick(object sender, int position)
        {

            try
            {
                //if(sharedPreferences.GetString("Site",null)!="" && sharedPreferences.GetString("Site", null) != null)
                //{
                if(recyclelist[position].getflag()!="1")
                {
                    ((MainActivity)Activity).getdata(recyclelist[position].getWO().ToString(), spcontract.SelectedItem.ToString());
                }
                else
                {
                    Toast.MakeText(Activity, "Sudah di tandai sebagai WO tanpa pemakaian part", ToastLength.Long).Show();
                }
                //}
                
                //else
                //{
                //    int flag = position;
                //    scan();
                //}
              
              

            }

            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Long).Show();
            }

        }

        void OnLongItemclick(object sender, int position)
        {

            try
            {

                posisi = position;
                using (_dialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(Activity))
                {
                    _dialogBuilder.SetMessage("Tandai sebagai tanpa pemakaian sparepart?");
                    _dialogBuilder.SetCancelable(false);
                    _dialogBuilder.SetPositiveButton("OK", closeWO);
                    _dialogBuilder.SetNegativeButton("Cancel", canceldialog);
                    //_dialogBuilder.SetNegativeButton("Cancel", BatalAction);
                    alertdialog = _dialogBuilder.Create();

                    alertdialog.Show();

                }
            }

            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Long).Show();
            }

        }
        private void closeWO(object sender, DialogClickEventArgs e)
        {
            try
            {
                dbInstance = CoreApplication.getInstance().getDatabase();
                catalogdb = dbInstance.WritableDatabase;
                //string query = "select * from " + sqliteTable.TableWoTransaction + " where " + sqliteTable.Wo_Mo + "='" + recyclelist[posisi].getWO() + "'";
                //ICursor cursor = catalogdb.RawQuery(query, null);

                //if (cursor.MoveToNext())
                //{
                removeitem.setflag("1");


                localhost1.Service1 service = new localhost1.Service1();
                service.closeWO(removeitem.getWO(), spcontract.SelectedItem.ToString());
                mAdapter.restoreItem(removeitem, posisi);
                mAdapter.NotifyDataSetChanged();
                //}
                //else
                //{
                Toast.MakeText(Activity, "Berhasil ditandai", ToastLength.Long).Show();
                //}
            }catch(Exception ex)
            {
                Toast.MakeText(Activity, "Network Error", ToastLength.Long).Show();
            }


        }
            private void updateWO(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }
            try
            {
                localhost1.Service1 service = new localhost1.Service1();
                service.updateWoNumber(recyclelist[posisi].getWO(), wo1.Text.ToString());

                string TransactionId = recyclelist[posisi].getWO();

                dbInstance = CoreApplication.getInstance().getDatabase();
                catalogdb = dbInstance.WritableDatabase;

                string query = "update " + sqliteTable.TableWoTransaction + " set " + sqliteTable.Wo_Mo + "=" + wo1.Text.ToString() + "," + sqliteTable.TransactionID + "=" + wo1.Text.ToString() + " where " + sqliteTable.Wo_Mo + "='' and " + sqliteTable.TransactionID + "='" + TransactionId + "'";
                catalogdb.ExecSQL(query);

              
                string query2 = "delete from " + sqliteTable.TableTrx + " where " + sqliteTable.TransactionID + " = '" + TransactionId + "'";
                catalogdb.ExecSQL(query2);

                mAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity.ApplicationContext, "Sorry We Cannot Proccess Your Transaction, Please Try Again or Contact IT Admin!", ToastLength.Long).Show();
            }


            mAdapter.NotifyDataSetChanged();
            Toast.MakeText(Activity.ApplicationContext, "Transaction moved to Wo "+ wo1.Text.ToString() + "", ToastLength.Long).Show();
        }

        private void canceldialog(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }
            _dialogBuilder.Dispose();
            alertdialog.Dismiss();

            mAdapter.restoreItem(removeitem, posisi);
        }
        //public void HandleScanResultBarang(ZXing.Result result)
        //{
        //    string msg = "";

        //    if (result != null && !string.IsNullOrEmpty(result.Text))
        //    {
        //        localhost1.Service1 service = new localhost1.Service1();
        //        int a = service.ceksite1(result.Text.ToString()).count1;
        //        if (a > 0)
        //        {
        //            ISharedPreferencesEditor editor = sharedPreferences.Edit();
        //            editor.PutString("Site", result.Text.ToString().ToUpper());
        //            editor.Commit();
        //            Activity.RunOnUiThread(() => ((MainActivity)Activity).getdata(recyclelist[flag].getWO().ToString()));
        //        }
        //        else
        //        {
        //            msg = "Mohon untuk scan site!";
        //            Activity.RunOnUiThread(() => Toast.MakeText(Activity.ApplicationContext, msg, ToastLength.Short).Show());
        //        }
        //    }

        //    else
        //    {

        //        msg = "Scanning Canceled!";
        //        Activity.RunOnUiThread(() => Toast.MakeText(Activity.ApplicationContext, msg, ToastLength.Short).Show());
        //    }

        //}
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
    }
}