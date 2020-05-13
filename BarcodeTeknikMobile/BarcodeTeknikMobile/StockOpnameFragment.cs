using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;

using Android.Support.Design.Widget;
using Android.Support.V7.Widget;

using Android.Views;
using Android.Widget;



namespace BarcodeTeknikMobile
{
    
    public class StockOpnameFragment : Fragment
    {
        Button syncQty;
           Bundle bundle = new Bundle();
        AlertDialog.Builder _dialogBuilder;
        AlertDialog alertdialog;
        //Button btnsubmit;
         List<StockOpnameGetSet> recyclelist = new List<StockOpnameGetSet>();
        RecyclerView mRecyclerView;
         StockOpnameAdapter mAdapter;
        ISharedPreferences shared;
       // BottomNavigationView bottomNavigationView;
        // FloatingActionButton fab;
       
        // int hitung = 0;
        View view;
        EditText qtydialog, txtresult;
        int posisi = 0;
        string title = "";
        public List<string> list = new List<string>();
        EditText txtdate;
        int count = 0;
        string dept = "";
        LinearLayout btnminus, btnplus;
             int hitung = 0;
        Spinner spSite;
        EditText spRak,txtsearch;
        private SQLiteDatabase catalogdb;
        private sqliteHelper dbInstance;
        List<string> spsitelist = new List<string>();
       // List<string> spraklist = new List<string>();
        ArrayAdapter adapter;
        TextView txtRak, uom;
        string result;
        Button btnsearch;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View itemView = LayoutInflater.From(container.Context).
                    Inflate(Resource.Layout.Opname, container, false);
           
           
            view = inflater.Inflate(Resource.Layout.unissue_dialog, container, false);
            btnminus = view.FindViewById<LinearLayout>(Resource.Id.btnminus);
            btnplus = view.FindViewById<LinearLayout>(Resource.Id.btnplus);
            spRak = view.FindViewById<EditText>(Resource.Id.spRak);
            txtRak = view.FindViewById<TextView>(Resource.Id.txtRak);
            txtsearch=itemView.FindViewById<EditText>(Resource.Id.txtsearch);
            shared = Activity.GetSharedPreferences("sharedprefrences", 0);
            btnsearch = itemView.FindViewById<Button>(Resource.Id.btnseacrh);
            syncQty = itemView.FindViewById<Button>(Resource.Id.btnsync);

            syncQty.Click += delegate
            {
                ProgressDialog progressDialog = ProgressDialog.Show(Activity, "", "Loading...", true);
                progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                new Thread(new ThreadStart(delegate
                {
                    try
                    {
                        localhost1.Service1 service = new localhost1.Service1();
                        service.synqty();
                        progressDialog.Dismiss();
                    }
                    catch (Exception ex)
                    {
                        Activity.RunOnUiThread(() =>
                        {
                            Toast.MakeText(Activity, "Server Error", ToastLength.Short).Show();
                            progressDialog.Dismiss();
                        });
                    }

                })).Start();
            };

            txtsearch.TextChanged += delegate
            {
                if(txtsearch.Text=="")
                {
                    recyclelist.Clear();
                    dbInstance = CoreApplication.getInstance().getDatabase();
                    catalogdb = dbInstance.WritableDatabase;
                    string queryq = "select " + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.opnamedate + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + "," + sqliteTable.uom + " from " + sqliteTable.TableStockOpname + " where " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "' and " + sqliteTable.PartNo + " like '%" + txtsearch.Text.ToString() + "%' order by " + sqliteTable.qty + " asc";
                    ICursor cursor2 = catalogdb.RawQuery(queryq, null);
                    while (cursor2.MoveToNext())
                    {
                        recyclelist.Add(new StockOpnameGetSet(cursor2.GetString(0), cursor2.GetString(1), "1", "0", cursor2.GetString(3), cursor2.GetString(4), cursor2.GetString(5), cursor2.GetString(6)));
                    }
                    mAdapter.NotifyDataSetChanged();
                    txtresult.RequestFocus();
                }
            };

            btnsearch.Click += delegate
            {
                recyclelist.Clear();
                dbInstance = CoreApplication.getInstance().getDatabase();
                catalogdb = dbInstance.WritableDatabase;
                string queryq = "select " + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.opnamedate + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + "," + sqliteTable.uom + " from " + sqliteTable.TableStockOpname + " where " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "' and " + sqliteTable.PartNo + " like '%"+txtsearch.Text.ToString()+"%' order by " + sqliteTable.qty + " asc";
                ICursor cursor2 = catalogdb.RawQuery(queryq, null);
                while (cursor2.MoveToNext())
                {
                    recyclelist.Add(new StockOpnameGetSet(cursor2.GetString(0), cursor2.GetString(1), "1", "0", cursor2.GetString(3), cursor2.GetString(4), cursor2.GetString(5), cursor2.GetString(6)));
                }
                mAdapter.NotifyDataSetChanged();
                txtresult.RequestFocus();
            };

            ISharedPreferencesEditor editor = shared.Edit();
            editor.PutString("MenuTrans", null);
            editor.Commit();

            txtdate = itemView.FindViewById<EditText>(Resource.Id.txtTanggal);
            txtdate.Focusable = false;
            txtdate.Clickable = true;
            txtresult = itemView.FindViewById<EditText>(Resource.Id.txtresult);
            txtresult.RequestFocus();

            if (Arguments.GetString("result")!=null)
            {
                result = Arguments.GetString("result");
            }
            if(Arguments.GetString("tanggalopname") != null)
            {
                txtdate.Text = Arguments.GetString("tanggalopname");
            }
           
            txtdate.Click += delegate
            {
                try
                {
                    recyclelist.Clear();


                    DatePicker frag = DatePicker.NewInstance(delegate (DateTime time)
                    {
                        try
                        {
                            txtdate.Text = Convert.ToDateTime(time.ToShortDateString()).ToString("yyyy-MM-dd");
                            ISharedPreferencesEditor editor2 = shared.Edit();
                            editor2.PutString("tanggalopname", txtdate.Text.ToString());
                            editor2.Commit();
                            localhost1.Service1 MyClient = new localhost1.Service1();
                            localhost1.Opname emp = new localhost1.Opname();
                            DataTable dt = new DataTable();

                            emp = MyClient.GetOpname(txtdate.Text.ToString());
                            dt = emp.OpnameTable;
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    recyclelist.Add(new StockOpnameGetSet(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), "1", "0", dt.Rows[i][2].ToString(), "Terkirim", dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString()));
                                }
                            }
                            else
                            {
                                dbInstance = CoreApplication.getInstance().getDatabase();
                                catalogdb = dbInstance.WritableDatabase;
                                string queryq = "select " + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.opnamedate + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + "," + sqliteTable.uom + " from " + sqliteTable.TableStockOpname + " where " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "' order by " + sqliteTable.qty + " asc";
                                ICursor cursor2 = catalogdb.RawQuery(queryq, null);
                                while (cursor2.MoveToNext())
                                {
                                    recyclelist.Add(new StockOpnameGetSet(cursor2.GetString(0), cursor2.GetString(1), "1", "0", cursor2.GetString(3), cursor2.GetString(4), cursor2.GetString(5), cursor2.GetString(6)));
                                }
                            }
                            mAdapter.NotifyDataSetChanged();
                        }
                        catch
                        {
                            dbInstance = CoreApplication.getInstance().getDatabase();
                            catalogdb = dbInstance.WritableDatabase;
                            string queryq = "select " + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.opnamedate + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + "," + sqliteTable.uom + " from " + sqliteTable.TableStockOpname + " where " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "' order by " + sqliteTable.qty + " asc";
                            ICursor cursor2 = catalogdb.RawQuery(queryq, null);
                            while (cursor2.MoveToNext())
                            {
                                recyclelist.Add(new StockOpnameGetSet(cursor2.GetString(0), cursor2.GetString(1), "1", "0", cursor2.GetString(3), cursor2.GetString(4), cursor2.GetString(5), cursor2.GetString(6)));
                            }
                            mAdapter.NotifyDataSetChanged();
                        }
                    });
                    frag.Show(FragmentManager, DatePicker.TAG);
                }
                catch(Exception ex)
                {
                    Toast.MakeText(Activity, "Network Error", ToastLength.Short).Show();
                }
            };
            qtydialog = view.FindViewById<EditText>(Resource.Id.qtydialog);
            uom = view.FindViewById<TextView>(Resource.Id.uom);
            spSite = view.FindViewById<Spinner>(Resource.Id.spSite);

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;
            ICursor cursor = catalogdb.RawQuery("select * from " + sqliteTable.TableSite + "", null);
            while (cursor.MoveToNext())
            {
                spsitelist.Add(cursor.GetString(1));
            }

            //ICursor cursor3 = catalogdb.RawQuery("select * from " + sqliteTable.TableRak + "", null);
            //while (cursor3.MoveToNext())
            //{
            //    spraklist.Add(cursor3.GetString(1));
            //}

            adapter = new ArrayAdapter<string>(Activity, Resource.Layout.spinnercustomlayout, spsitelist);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spSite.Adapter = adapter;

            //adapter2 = new ArrayAdapter<string>(Activity, Resource.Layout.spinnercustomlayout, spraklist);
            //adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //spRak.Adapter = adapter2;
           
            //  fab = itemView.FindViewById<FloatingActionButton>(Resource.Id.fab);

            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new StockOpnameAdapter(recyclelist);
            mAdapter.ItemClick += OnItemClick;
            mAdapter.ItemLongClick += OnItemLongClick;
            prepare();
            mRecyclerView.SetAdapter(mAdapter);

            txtresult.TextChanged += delegate
            {
                if (txtdate.Text != "")
                {
                    if (txtresult.Text != "")
                    {
                        HandleScanResultBarang(txtresult.Text.ToString());
                    }
                }
                else
                {
                    if (txtresult.Text != "")
                    {
                        txtresult.Text = "";
                        txtdate.Error = "Tanggal harus diisi";
                    }
                }
            };

            btnminus.Click += delegate {
                if (hitung != 0)
                {
                    hitung--;
                    qtydialog.Text = hitung.ToString();
                }

            };
            btnplus.Click += delegate {
                hitung++;
                qtydialog.Text = hitung.ToString();
            };

            return itemView;
        }
        public void prepare()
        {
            try
            {
                ISharedPreferencesEditor editor2 = shared.Edit();
                editor2.PutString("tanggalopname", txtdate.Text.ToString());
                editor2.Commit();
                localhost1.Service1 MyClient = new localhost1.Service1();
                localhost1.Opname emp = new localhost1.Opname();
                DataTable dt = new DataTable();

                emp = MyClient.GetOpname(txtdate.Text.ToString());
                dt = emp.OpnameTable;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        recyclelist.Add(new StockOpnameGetSet(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), "1", "0", dt.Rows[i][2].ToString(), "Terkirim", dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString()));
                    }
                }
                else
                {
                    dbInstance = CoreApplication.getInstance().getDatabase();
                    catalogdb = dbInstance.WritableDatabase;
                    string queryq = "select " + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.opnamedate + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + "," + sqliteTable.uom + " from " + sqliteTable.TableStockOpname + " where " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "' order by " + sqliteTable.qty + " asc";
                    ICursor cursor2 = catalogdb.RawQuery(queryq, null);
                    while (cursor2.MoveToNext())
                    {
                        recyclelist.Add(new StockOpnameGetSet(cursor2.GetString(0), cursor2.GetString(1), "1", "0", cursor2.GetString(3), cursor2.GetString(4), cursor2.GetString(5), cursor2.GetString(6)));
                    }
                }
            }catch(Exception ex)
            {
                dbInstance = CoreApplication.getInstance().getDatabase();
                catalogdb = dbInstance.WritableDatabase;
                string queryq = "select " + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.opnamedate + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + "," + sqliteTable.uom + " from " + sqliteTable.TableStockOpname + " where " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "' order by " + sqliteTable.qty + " asc";
                ICursor cursor2 = catalogdb.RawQuery(queryq, null);
                while (cursor2.MoveToNext())
                {
                    recyclelist.Add(new StockOpnameGetSet(cursor2.GetString(0), cursor2.GetString(1), "1", "0", cursor2.GetString(3), cursor2.GetString(4), cursor2.GetString(5), cursor2.GetString(6)));
                }
            }
            mAdapter.NotifyDataSetChanged();
        }
        private void delete(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }

            try
            {
                if (txtdate.Text != "")
                {

                    localhost1.Service1 service = new localhost1.Service1();
                    service.deletestokcopname(recyclelist[posisi].getPartNo(), txtdate.Text.ToString(), recyclelist[posisi].getContract());


                    dbInstance = CoreApplication.getInstance().getDatabase();
                    catalogdb = dbInstance.WritableDatabase;
                    string query = "delete from " + sqliteTable.TableStockOpname + " where " + sqliteTable.PartNo + "='" + recyclelist[posisi].getPartNo() + "' and " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "'";
                    catalogdb.ExecSQL(query);

                    var tmp = recyclelist.Where(c => (c.PartNo == recyclelist[posisi].getPartNo()) && (c.Contract == recyclelist[posisi].getContract())).First();
                    recyclelist.Remove(tmp);

                    mAdapter.NotifyItemRemoved(posisi);

                    Toast.MakeText(Activity.ApplicationContext, "hapus sukses", ToastLength.Long).Show();
                }
                else
                {
                    txtdate.Error = "Tanggal harus diisi";
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity.ApplicationContext, "Maaf transaksi tidak dapat di proses, Mohon coba lagi atau hubungi IT!", ToastLength.Long).Show();
            }
        }

        void OnItemLongClick(object sender, int position)
        {
            using (_dialogBuilder = new AlertDialog.Builder(Activity))
            {
                posisi = position;
                title = recyclelist[position].getPartNo().ToString();
                _dialogBuilder.SetMessage("Anda yakin hapus item ini?");
                _dialogBuilder.SetCancelable(false);
                _dialogBuilder.SetPositiveButton("OK", delete);
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
                using (_dialogBuilder = new AlertDialog.Builder(Activity))
                {

                    posisi = position;
                    uom.Text = recyclelist[position].getuom();
                    qtydialog.Text = "0";
                    hitung = 0;
                    spSite.SetSelection(adapter.GetPosition(recyclelist[posisi].getContract().ToString()));
                    title = recyclelist[position].getPartNo().ToString();
                    _dialogBuilder.SetTitle(title);
                    _dialogBuilder.SetView(view);
                    _dialogBuilder.SetCancelable(false);
                    _dialogBuilder.SetPositiveButton("OK", unissued);
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
        private void unissued(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }
            try
            {
                for (int i = 0; i < recyclelist.Count; i++)
                {
                    recyclelist[i].setfirstscan("0");
                }
                int index= recyclelist.FindIndex(x => x.Contract == spSite.SelectedItem.ToString() && x.PartNo== title);
                if (index == posisi)
                {
                    localhost1.Service1 service = new localhost1.Service1();
                    service.updateqtystockopname(qtydialog.Text.ToString(), title, recyclelist[posisi].getContract(), txtdate.Text.ToString());

                    dbInstance = CoreApplication.getInstance().getDatabase();
                    catalogdb = dbInstance.WritableDatabase;
                    string query = "update " + sqliteTable.TableStockOpname + " set " + sqliteTable.qty + "=" + qtydialog.Text.ToString() + "," + sqliteTable.Contract + "='" + spSite.SelectedItem.ToString() + "'," + sqliteTable.Rakno + "='" + spRak.Text.ToString() + "'," + sqliteTable.uom + "='" + uom.Text.ToString() + "' where " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "'";

                    catalogdb.ExecSQL(query);

                    recyclelist[posisi].setQty(qtydialog.Text.ToString());
                    recyclelist[posisi].setContract(spSite.SelectedItem.ToString());
                    recyclelist[posisi].setuom(uom.Text.ToString());
                    recyclelist[posisi].setRakNo(spRak.Text.ToString());

                    mAdapter.NotifyDataSetChanged();
                    Toast.MakeText(Activity.ApplicationContext, "Edit Quantity berhasil", ToastLength.Short).Show();
                }
                else if(index==-1)
                {
                    localhost1.Service1 service = new localhost1.Service1();
                    service.updateqtystockopname(qtydialog.Text.ToString(), title, recyclelist[posisi].getContract(), txtdate.Text.ToString());

                    dbInstance = CoreApplication.getInstance().getDatabase();
                    catalogdb = dbInstance.WritableDatabase;
                    string query = "update " + sqliteTable.TableStockOpname + " set " + sqliteTable.qty + "=" + qtydialog.Text.ToString() + "," + sqliteTable.Contract + "='" + spSite.SelectedItem.ToString() + "'," + sqliteTable.Rakno + "='" + spRak.Text.ToString() + "'," + sqliteTable.uom + "='" + uom.Text.ToString() + "' where " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "'";

                    catalogdb.ExecSQL(query);

                    recyclelist[posisi].setQty(qtydialog.Text.ToString());
                    recyclelist[posisi].setContract(spSite.SelectedItem.ToString());
                    recyclelist[posisi].setuom(uom.Text.ToString());
                    recyclelist[posisi].setRakNo(spRak.Text.ToString());

                    mAdapter.NotifyDataSetChanged();
                    Toast.MakeText(Activity.ApplicationContext, "Edit Quantity berhasil", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(Activity.ApplicationContext, "Item sudah ada atau site harus berbeda untuk part sejenis", ToastLength.Short).Show();
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(Activity.ApplicationContext, "Maaf transaksi tidak dapat di proses, Mohon coba lagi atau hubungi IT!", ToastLength.Long).Show();
            }

            spRak.Text = "";
     
        }

        private void canceldialog(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }
            spRak.Text = "";
            _dialogBuilder.Dispose();
            alertdialog.Dismiss();
        }

        public void OnBackPressed()
        {
           
           
            Activity.Finish();
        }
       
       public void HandleScanResultBarang(string result)
        {
            try
            {
                string msg = "";

                if (result != null && !string.IsNullOrEmpty(result))
                {
                    localhost1.Service1 service = new localhost1.Service1();
                    localhost1.opanameitem emp = new localhost1.opanameitem();
                    DataTable dt = new DataTable();
                    emp = service.GetOpanameitem(result.ToString());
                    dt = emp.opanameitemtable;

                    string partno = dt.Rows[0][0].ToString();
                    string uom = dt.Rows[0][1].ToString();
                    ICursor cursorcekitem = catalogdb.RawQuery("select * from " + sqliteTable.TableStockOpname + " where " + sqliteTable.PartNo + "='" + partno + "' and " + sqliteTable.Contract + "='' and " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "'", null);

                    if (cursorcekitem.Count == 0)
                    {
                        List<StockOpnameGetSet> list = new List<StockOpnameGetSet>();

                        if (recyclelist.Count > 0)
                        {
                            for (int i = 0; i < recyclelist.Count; i++)
                            {
                                list.Add(recyclelist[i]);
                            }
                            recyclelist.Clear();
                            recyclelist.Insert(0, new StockOpnameGetSet(partno, "0", "0", "1", "", "Belum Terkirim", "", uom));
                            for (int j = 0; j < list.Count; j++)
                            {

                                recyclelist.Insert(j + 1, list[j]);
                            }

                        }
                        else
                        {
                            recyclelist.Insert(0, new StockOpnameGetSet(partno, "0", "0", "1", "", "Belum Terkirim", "", uom));
                        }

                        dbInstance = CoreApplication.getInstance().getDatabase();
                        catalogdb = dbInstance.WritableDatabase;
                        string query = "Insert into " + sqliteTable.TableStockOpname + "(" + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.opnamedate + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.uom + ") select '" + partno + "','0','" + txtdate.Text.ToString() + "','','Belum Terkirim','" + uom + "' where not exists(select * from " + sqliteTable.TableStockOpname + " where " + sqliteTable.PartNo + "='" + partno + "' and " + sqliteTable.Contract + "='' and  " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "')";

                        catalogdb.ExecSQL(query);
                        txtresult.Text = "";
                        mAdapter.NotifyDataSetChanged();


                    }
                    else
                    {
                        txtresult.Text = "";
                        Toast.MakeText(Activity.ApplicationContext, "Item sudah ada atau site harus berbeda untuk part sejenis", ToastLength.Short).Show();
                    }
                   
                }
                else
                    {
                        txtresult.Text = "";
                        msg = "Scanning Canceled!";
                        Toast.MakeText(Activity.ApplicationContext, msg, ToastLength.Short).Show();
                    }
               
            }catch(Exception ex)
            {
                txtresult.Text = "";
                Toast.MakeText(Activity.ApplicationContext, "Network Error", ToastLength.Short).Show();
            }
            //hitung += 1;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menusubmit:
                    {
                        try
                        {
                           
                            count = 0;

                            if (recyclelist.Count > 0)
                            {
                                localhost1.Service1 service = new localhost1.Service1();
                                //service.deleteopname(txtdate.Text.ToString());

                                for (int i = 0; i < recyclelist.Count; i++)
                            {
                                if(recyclelist[i].getContract().ToString() != "")
                                {

                                        recyclelist[i].setfirstscan("0");

                                        service.stockopname(recyclelist[i].getQty(), recyclelist[i].getPartNo(), recyclelist[i].getContract(), recyclelist[i].getRakNo(),txtdate.Text.ToString());

                                        dbInstance = CoreApplication.getInstance().getDatabase();
                                        catalogdb = dbInstance.WritableDatabase;
                                        string query = "update " + sqliteTable.TableStockOpname + " set " + sqliteTable.StatusKirim + "='Terkirim' where " + sqliteTable.PartNo + "='" + recyclelist[i].getPartNo() + "' and " + sqliteTable.Contract + "='" + recyclelist[i].getContract()+ "' and  " + sqliteTable.opnamedate + "='" + txtdate.Text.ToString() + "'";

                                        catalogdb.ExecSQL(query);


                                        recyclelist[i].setstatuskirim("Terkirim");
                                        count++;
                                }
                                //to sqlServer
                                
                            }
                            if (count > 0)
                            {
                                mAdapter.NotifyDataSetChanged();
                                Toast.MakeText(this.Activity, "sukses", ToastLength.Long).Show();
                            }
                            else
                            {
                                Toast.MakeText(this.Activity, "Qty atau Site tidak boleh kosong", ToastLength.Long).Show();
                            }
                             
                          }
                            else
                            {
                                Toast.MakeText(this.Activity, "Data Tidak Ditemukan", ToastLength.Long).Show();

                            }
                        }
                        catch (Exception ex)
                        {
                            string err = ex.ToString();
                            Toast.MakeText(this.Activity, "Maaf transaksi tidak dapat di proses, Mohon coba lagi atau hubungi IT!", ToastLength.Long).Show();
                        }
                  
                    return true;
                    }
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