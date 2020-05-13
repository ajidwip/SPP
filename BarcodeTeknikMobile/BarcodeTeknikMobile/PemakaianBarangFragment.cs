using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;

using Android.Support.Design.Widget;
using Android.Support.V7.Widget;

using Android.Views;
using Android.Widget;

using ZXing.Mobile;

namespace BarcodeTeknikMobile
{
    [Activity(WindowSoftInputMode = SoftInput.StateHidden | SoftInput.AdjustResize)]

    public class PemakaianBarangFragment : Fragment
    {
        AlertDialog.Builder _dialogBuilder;
        AlertDialog alertdialog;
        //Button btnsubmit;
        List<PemakaianBarangGetterSetter> recyclelist = new List<PemakaianBarangGetterSetter>();
        RecyclerView mRecyclerView;
        PemakaianBarangAdapter mAdapter;
        RadioButton rdpemakaian, rdpengembalian;
        AutoCompleteTextView txtsearch;
        int count = 0;
        ISharedPreferences sharedPreferences;
        // FloatingActionButton fab;
        string result;
        // int hitung = 0;
        private SQLiteDatabase catalogdb;
        private sqliteHelper dbInstance;
        string Contract = "";
        Button syncQty, btnCopy;
        View view,view2;
        EditText qtydialog, txtautentikasi,txtresult, txtcopy, spRak;
        int posisi = 0,cpunt=0;
        string title = "";
        public List<string> list = new List<string>();
        string dept = "";

        string Wo = "";
        string TrxID = "";
        TextView txtStatusMenu, txtsitedialog,txtRak;
       // Switch menuswitch;
        LinearLayout btnminus, btnplus;
        int hitung = 0;
        Spinner spSite;
        List<string> spsitelist = new List<string>();
        ArrayAdapter adapter;
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
                    Inflate(Resource.Layout.PemakaianBarang, container, false);

            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);

            if (Arguments.GetString("Contract") != null)
            {
                Contract = Arguments.GetString("Contract");
            }
            view = inflater.Inflate(Resource.Layout.unissue_dialog, container, false);
            view2 = inflater.Inflate(Resource.Layout.autentikasidialog, container, false);
            txtautentikasi = view2.FindViewById<EditText>(Resource.Id.txtautentikasi);
            //txtketerangan=itemView.FindViewById<TextView>(Resource.Id.txtketerangan);
           // txtketerangan2= itemView.FindViewById<TextView>(Resource.Id.txtketerangan2);
            rdpemakaian = itemView.FindViewById<RadioButton>(Resource.Id.rdPemakaianbarang);
            rdpengembalian = itemView.FindViewById<RadioButton>(Resource.Id.rdPengembalianbarang);

            btnminus = view.FindViewById<LinearLayout>(Resource.Id.btnminus);
            btnplus = view.FindViewById<LinearLayout>(Resource.Id.btnplus);
            spSite = view.FindViewById<Spinner>(Resource.Id.spSite);
            spSite.Visibility = ViewStates.Gone;
            txtsitedialog = view.FindViewById<TextView>(Resource.Id.txtsitedialog);
            txtresult = itemView.FindViewById<EditText>(Resource.Id.txtresult);
            txtcopy = itemView.FindViewById<EditText>(Resource.Id.txtcopy);
            txtresult.RequestFocus();
            txtsitedialog.Visibility = ViewStates.Gone;
            spRak = view.FindViewById<EditText>(Resource.Id.spRak);
            spRak.Visibility = ViewStates.Gone;
            txtRak = view.FindViewById<TextView>(Resource.Id.txtRak);
            txtRak.Visibility = ViewStates.Gone;
            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb=dbInstance.WritableDatabase;
            syncQty = itemView.FindViewById<Button>(Resource.Id.btnsync);
            btnCopy = itemView.FindViewById<Button>(Resource.Id.btncopy);

            btnCopy.Click += delegate
              {
                  txtresult.Text = txtcopy.Text;
              };
            syncQty.Click+=delegate
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
            //ICursor cursor=catalogdb.RawQuery("select * from " + sqliteTable.TableSite + "",null);
            //while(cursor.MoveToNext())
            //{
            //    spsitelist.Add(cursor.GetString(1));
            //}


            //adapter = new ArrayAdapter<string>(Activity, Resource.Layout.spinnercustomlayout, spsitelist);
            //adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //spSite.Adapter = adapter;

            dept = Arguments.GetString("Dept");

            txtStatusMenu = itemView.FindViewById<TextView>(Resource.Id.txtStatusMenu);

            //menuswitch = itemView.FindViewById<Switch>(Resource.Id.menuswitch);
            rdpemakaian.Checked = true;

            if (rdpemakaian.Checked == true)
            {
                txtStatusMenu.Text = "Pemakaian Barang";
            }
            else
            {
                txtStatusMenu.Text = "Pengembalian Barang";
            }

           
            qtydialog = view.FindViewById<EditText>(Resource.Id.qtydialog);
            txtsearch = itemView.FindViewById<AutoCompleteTextView>(Resource.Id.txtsearch);
            txtsearch.Enabled = false;

            rdpemakaian.Click += delegate
            {
                rdpengembalian.Checked = false;
                txtStatusMenu.Text = "Pemakaian Barang";
                qtydialog.Text = "0";
            };

            rdpengembalian.Click += delegate
            {
                rdpemakaian.Checked = false;
                txtStatusMenu.Text = "Pengembalian Barang";
                qtydialog.Text = "0";
            };

            //menuswitch.CheckedChange += delegate
            //{
            //    if (menuswitch.Checked == true)
            //    {
            //        txtStatusMenu.Text = "Pemakaian Barang";
            //        qtydialog.Text = "0";
            //    }
            //    else
            //    {
            //        txtStatusMenu.Text = "Pengembalian Barang";
            //        qtydialog.Text = "0";
            //    }
            //};
            qtydialog.TextChanged += delegate
            {
                if(qtydialog.Text=="")
                {
                    hitung = 0;

                }
                else
                {
                    hitung = Int32.Parse(qtydialog.Text.ToString());
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


            if (Arguments.GetString("WO") != null)
            {
                if (Arguments.GetString("WO").Contains("NW"))
                {
                    TrxID = Arguments.GetString("WO");
                    
                }
                else
                {
                    Wo = Arguments.GetString("WO");
                    TrxID= Arguments.GetString("WO");
                    txtsearch.Text = Wo;
                }
            }

            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new PemakaianBarangAdapter(recyclelist);

            mAdapter.ItemClick += OnItemClick;
            mAdapter.ItemLongClick += OnItemLongClick;
            prepare();

            mRecyclerView.SetAdapter(mAdapter);

            txtresult.TextChanged += delegate
            {
                if(txtresult.Text!="")
                {
                    HandleScanResultBarang(txtresult.Text.ToString());
                }
               
            };
              
            
           
            
            return itemView;
        }

   

        void OnItemClick(object sender, int position)
        {

            try
            {
                using (_dialogBuilder = new AlertDialog.Builder(Activity))
                {
                   

                    posisi = position;
                    qtydialog.Text = "0";
                   // spSite.SetSelection(adapter.GetPosition(recyclelist[position].getContract().ToString()));
                    title = recyclelist[position].getPartNo().ToString();
                    _dialogBuilder.SetTitle(title+ " " +txtStatusMenu.Text);
                    _dialogBuilder.SetView(view);
                    _dialogBuilder.SetCancelable(false);
                    if (txtStatusMenu.Text == "Pemakaian Barang")
                    {
                        _dialogBuilder.SetPositiveButton("OK", unissued);
                    }
                    else
                    {
                        _dialogBuilder.SetPositiveButton("Kembalikan", unissued);
                    }
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
        private void delete(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }

            try
            {
              
                
                localhost1.Service1 service = new localhost1.Service1();
                string[] rak = recyclelist[posisi].getRakNo().Split('-');
                service.deletepemakaian(Wo,title,TrxID, recyclelist[posisi].getContract(), rak[1]);


                dbInstance = CoreApplication.getInstance().getDatabase();
                catalogdb = dbInstance.WritableDatabase;
                string query = "delete from " + sqliteTable.TableWoTransaction + " where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.TransactionID + "='" + TrxID + "' and " + sqliteTable.Rakno + "='"+ recyclelist[posisi].getRakNo() + "'";
                catalogdb.ExecSQL(query);

                 var tmp= recyclelist.Where(c => (c.PartNo == recyclelist[posisi].getPartNo()) && (c.Contract == recyclelist[posisi].getContract()) && (c.RakNO == recyclelist[posisi].getRakNo())).First();
                recyclelist.Remove(tmp);

                mAdapter.NotifyItemRemoved(posisi);

                Toast.MakeText(Activity.ApplicationContext, "hapus sukses", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity.ApplicationContext, "Maaf transaksi tidak dapat di proses, Mohon coba lagi atau hubungi IT!", ToastLength.Long).Show();
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

                mAdapter.NotifyDataSetChanged();


                if (txtStatusMenu.Text == "Pemakaian Barang")
                {
                    dbInstance = CoreApplication.getInstance().getDatabase();
                    catalogdb = dbInstance.WritableDatabase;

                    ICursor cursor = catalogdb.RawQuery("select * from " + sqliteTable.TableWoTransaction + "  where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.TransactionID + "='" + TrxID + "' and " + sqliteTable.Rakno + "='" + recyclelist[posisi].getRakNo() + "'", null);
                    string[] rak = recyclelist[posisi].getRakNo().Split('-');
                    if (cursor.MoveToNext())
                    {
                        if (cursor.GetString(7) != "Terkirim")
                        {

                            dbInstance = CoreApplication.getInstance().getDatabase();
                            catalogdb = dbInstance.WritableDatabase;
                            string query = "update " + sqliteTable.TableWoTransaction + " set " + sqliteTable.qty + "=" + sqliteTable.qty + "+" + qtydialog.Text.ToString() + " where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.TransactionID + "='" + TrxID + "' and " + sqliteTable.Rakno + "='" + recyclelist[posisi].getRakNo() + "'";
                            catalogdb.ExecSQL(query);

                            int a = int.Parse(recyclelist[posisi].getQty()) + int.Parse(qtydialog.Text.ToString());
                            recyclelist[posisi].setQty(a.ToString());
                            recyclelist[posisi].setContract(recyclelist[posisi].getContract());
                            mAdapter.NotifyDataSetChanged();

                            Toast.MakeText(Activity.ApplicationContext, "Jumlah Pakai berhasil update", ToastLength.Long).Show();
                        }
                        else
                        {
                            localhost1.Service1 service = new localhost1.Service1();
                            service.updatejumlahpakai(qtydialog.Text.ToString(), Wo, recyclelist[posisi].getContract(), title, TrxID, rak[1]);


                            dbInstance = CoreApplication.getInstance().getDatabase();
                            catalogdb = dbInstance.WritableDatabase;
                            string query = "update " + sqliteTable.TableWoTransaction + " set " + sqliteTable.qty + "=" + sqliteTable.qty + "+" + qtydialog.Text.ToString() + " where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.TransactionID + "='" + TrxID + "' and " + sqliteTable.Rakno + "='" + recyclelist[posisi].getRakNo() + "'";
                            catalogdb.ExecSQL(query);

                            int a = int.Parse(recyclelist[posisi].getQty()) + int.Parse(qtydialog.Text.ToString());
                            recyclelist[posisi].setQty(a.ToString());
                            recyclelist[posisi].setContract(recyclelist[posisi].getContract());
                            mAdapter.NotifyDataSetChanged();

                            Toast.MakeText(Activity.ApplicationContext, "Jumlah Pakai berhasil update", ToastLength.Long).Show();
                        }
                    }
                }
                else
                {
                    dbInstance = CoreApplication.getInstance().getDatabase();
                    catalogdb = dbInstance.WritableDatabase;

                    ICursor cursor = catalogdb.RawQuery("select * from " + sqliteTable.TableWoTransaction + "  where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.TransactionID + "='" + TrxID + "' and " + sqliteTable.Rakno + "='" + recyclelist[posisi].getRakNo() + "'", null);
                    string[] rak = recyclelist[posisi].getRakNo().Split('-');
                    if (cursor.MoveToNext())
                    {
                        if (cursor.GetString(7) == "Terkirim")
                        {
                            string[] rak2 = recyclelist[posisi].getRakNo().Split('-');
                            localhost1.Service1 service = new localhost1.Service1();
                            service.unissued(qtydialog.Text.ToString(), Wo, recyclelist[posisi].getContract(), title, TrxID, rak2[1]);

                         
                            string query = "update " + sqliteTable.TableWoTransaction + " set " + sqliteTable.qty + "=" + sqliteTable.qty + "-" + qtydialog.Text.ToString() + " where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.TransactionID + "='" + TrxID + "' and " + sqliteTable.Rakno + "='" + recyclelist[posisi].getRakNo() + "'";
                            catalogdb.ExecSQL(query);

                            int a = int.Parse(recyclelist[posisi].getQty()) - int.Parse(qtydialog.Text.ToString());
                            recyclelist[posisi].setQty(a.ToString());
                            mAdapter.NotifyDataSetChanged();

                            Toast.MakeText(Activity.ApplicationContext, "Pengembalian Barang berhasil", ToastLength.Long).Show();
                        }
                        else
                        {
                            string query = "update " + sqliteTable.TableWoTransaction + " set " + sqliteTable.qty + "=" + sqliteTable.qty + "-" + qtydialog.Text.ToString() + " where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + title + "' and " + sqliteTable.Contract + "='" + recyclelist[posisi].getContract() + "' and " + sqliteTable.TransactionID + "='" + TrxID + "' and " + sqliteTable.Rakno + "='" + recyclelist[posisi].getRakNo() + "'";
                            catalogdb.ExecSQL(query);

                            int a = int.Parse(recyclelist[posisi].getQty()) - int.Parse(qtydialog.Text.ToString());
                            recyclelist[posisi].setQty(a.ToString());
                            mAdapter.NotifyDataSetChanged();

                            Toast.MakeText(Activity.ApplicationContext, "Pengembalian Barang berhasil", ToastLength.Long).Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Quantity tidak cukup"))
                {
                    using (_dialogBuilder = new AlertDialog.Builder(Activity))
                    {


                        _dialogBuilder.SetMessage(ex.Message.ToString());
                        _dialogBuilder.SetCancelable(false);
                        _dialogBuilder.SetPositiveButton("OK", canceldialog);
                        //_dialogBuilder.SetNegativeButton("Cancel", BatalAction);
                        alertdialog = _dialogBuilder.Create();

                        alertdialog.Show();
                    }

                }
                else
                {
                    Toast.MakeText(Activity.ApplicationContext, "Maaf transaksi tidak dapat di proses", ToastLength.Long).Show();
                }
            }
        }
        private void submit(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }
            ViewGroup parentViewGroup2 = (ViewGroup)view2.Parent;
            if (parentViewGroup2 != null)
            {
                parentViewGroup2.RemoveAllViews();
            }
            if (txtautentikasi.Text.ToString() != "")
            {
                try
                {
                    if (recyclelist.Count > 0)
                    {
                       
                            localhost1.Service1 service = new localhost1.Service1();
                            //service.deleteWo(txtsearch.Text.ToString());
                            count = 0;
                            for (int i = 0; i < recyclelist.Count; i++)
                            {
                                if(recyclelist[i].getstatuskirim()=="Belum Terkirim")
                                {
                                    recyclelist[i].setfirstscan("0");

                                    string qty = recyclelist[i].getQty();
                                    string contract = recyclelist[i].getContract();
                                    string partNo = recyclelist[i].getPartNo();
                                    string[] a = recyclelist[i].getRakNo().Split('-');
                                    service.InsertWOTransaction(qty, Wo, contract, partNo, TrxID, txtautentikasi.Text.ToString(), a[1]);


                                    //to sqlite
                                    dbInstance = CoreApplication.getInstance().getDatabase();
                                    catalogdb = dbInstance.WritableDatabase;
                                    string query = "update " + sqliteTable.TableWoTransaction + " set " + sqliteTable.StatusKirim + "='Terkirim' where " + sqliteTable.PartNo + "='" + partNo + "' and " + sqliteTable.Wo_Mo + "='" + txtsearch.Text.ToString() + "' and  " + sqliteTable.Contract + "='" + contract + "' and " + sqliteTable.Rakno + "='" + recyclelist[i].getRakNo() + "'";

                                    catalogdb.ExecSQL(query);

                                    recyclelist[i].setstatuskirim("Terkirim");
                                }
                                count++;

                            }
                            if (count > 0)
                            {
                                mAdapter.NotifyDataSetChanged();
                                using (_dialogBuilder = new AlertDialog.Builder(Activity))
                                {


                                    _dialogBuilder.SetMessage("Sukses");
                                    _dialogBuilder.SetCancelable(false);
                                    _dialogBuilder.SetPositiveButton("OK", canceldialog);
                                    //_dialogBuilder.SetNegativeButton("Cancel", BatalAction);
                                    alertdialog = _dialogBuilder.Create();

                                    alertdialog.Show();
                                }
                            }
                            else 
                            {
                                Toast.MakeText(this.Activity, "Qty atau Site tidak boleh kosong", ToastLength.Long).Show();

                            }
                        }
                      
                    
                    else
                    {
                        Toast.MakeText(this.Activity, "Data Tidak ditemukan", ToastLength.Long).Show();
                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Quantity tidak cukup"))
                    {
                      //  Toast.MakeText(Activity.ApplicationContext, ex.Message.ToString(), ToastLength.Long).Show();
                        using (_dialogBuilder = new AlertDialog.Builder(Activity))
                        {


                            _dialogBuilder.SetMessage(ex.Message.ToString());
                            _dialogBuilder.SetCancelable(false);
                            _dialogBuilder.SetPositiveButton("OK", canceldialog);
                            //_dialogBuilder.SetNegativeButton("Cancel", BatalAction);
                            alertdialog = _dialogBuilder.Create();

                            alertdialog.Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(Activity.ApplicationContext, "Maaf transaksi tidak dapat di proses", ToastLength.Long).Show();

                    }
                   
                }
            }
            else
            {

                using (_dialogBuilder = new AlertDialog.Builder(Activity))
                {


                    _dialogBuilder.SetMessage("User Id Harus diisi");
                    _dialogBuilder.SetCancelable(false);
                    _dialogBuilder.SetPositiveButton("OK", canceldialog);
                    //_dialogBuilder.SetNegativeButton("Cancel", BatalAction);
                    alertdialog = _dialogBuilder.Create();

                    alertdialog.Show();
                }
            }
        }
        private void canceldialog(object sender, DialogClickEventArgs e)
        {
            ViewGroup parentViewGroup = (ViewGroup)view.Parent;
            if (parentViewGroup != null)
            {
                parentViewGroup.RemoveAllViews();
            }
            ViewGroup parentViewGroup2 = (ViewGroup)view2.Parent;
            if (parentViewGroup2 != null)
            {
                parentViewGroup2.RemoveAllViews();
            }
            _dialogBuilder.Dispose();
            alertdialog.Dismiss();
        }

        public void prepare()
        {
            
            recyclelist.Clear();
           

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;
            string queryq = "select " + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.flag + "," + sqliteTable.Contract + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + " from " + sqliteTable.TableWoTransaction + " where "+sqliteTable.Wo_Mo+"='"+Wo+ "' and " + sqliteTable.TransactionID + "='" + TrxID + "'";
            ICursor cursor = catalogdb.RawQuery(queryq, null);
            while (cursor.MoveToNext())
            {
                recyclelist.Add(new PemakaianBarangGetterSetter(cursor.GetString(0), cursor.GetString(1), cursor.GetString(2),"0", cursor.GetString(3), cursor.GetString(4),cursor.GetString(5)));
            }
            mAdapter.NotifyDataSetChanged();
        }
     
     public  void HandleScanResultBarang(string result)
        {
            try
            {
                string msg = "";

                if (result != null && !string.IsNullOrEmpty(result))
                {

                    localhost1.Service1 service = new localhost1.Service1();

                        int b = service.cekrak1(result.ToString()).count2;
                        if (b > 0)
                        {
                            string[] a = result.ToString().Split('-');
                            if(a[0].ToUpper() == Contract)
                            {
                                ISharedPreferencesEditor editor = sharedPreferences.Edit();
                                editor.PutString("RakNo", result.ToString().ToUpper());
                                editor.Commit();
                                Toast.MakeText(Activity, "Rak Tersimpan, silahkan scan barcode spare part yang dibutuhkan", ToastLength.Long).Show();
                            }
                            else
                            {
                            Toast.MakeText(Activity, "site hasil scan harus sama dengan site No Wo", ToastLength.Long).Show();
                             }
                              txtresult.Text = "";
                        }
                        else
                        {
                            string partno = service.getpart(result.ToString()).PartNo1;
                            if (partno != "")
                            {
                                string[] splitstring = sharedPreferences.GetString("RakNo", "*").Split('-');
                                if (result.ToString().ToUpper().Contains(splitstring[0]))
                                {
                                   
                                        string rak = service.getrakno(result.ToString()).rakno1;
                                        if (rak == sharedPreferences.GetString("RakNo", "*"))
                                        {
                                            ICursor cursorcek = catalogdb.RawQuery("select * from " + sqliteTable.TableWoTransaction + " where " + sqliteTable.Wo_Mo + "='" + Wo + "' and " + sqliteTable.PartNo + "='" + partno + "' and " + sqliteTable.Contract + "='"+ splitstring[0] + "' and " + sqliteTable.Rakno + "='" + sharedPreferences.GetString("RakNo", "*") + "'", null);
                                            if(cursorcek.Count==0)
                                            {
                                               // txtketerangan.Text = "Rak " + sharedPreferences.GetString("RakNo", "*") + "";
                                                //txtketerangan2.Text = "Lanjutkan scan barang atau scan rak kembali";
                                                recyclelist.Add(new PemakaianBarangGetterSetter(partno, "0", "0", "1", splitstring[0], "Belum Terkirim", sharedPreferences.GetString("RakNo", "*")));

                                                dbInstance = CoreApplication.getInstance().getDatabase();
                                                catalogdb = dbInstance.WritableDatabase;
                                                string query = "Insert into " + sqliteTable.TableWoTransaction + "(" + sqliteTable.Wo_Mo + "," + sqliteTable.PartNo + "," + sqliteTable.qty + "," + sqliteTable.Contract + "," + sqliteTable.flag + "," + sqliteTable.TransactionID + "," + sqliteTable.StatusKirim + "," + sqliteTable.Rakno + ") select '" + Wo + "','" + partno + "','0','" + splitstring[0] + "','0','" + TrxID + "','Belum Terkirim','" + sharedPreferences.GetString("RakNo", "*") + "' where not exists(select * from " + sqliteTable.TableWoTransaction + " where " + sqliteTable.PartNo + "='" + partno + "' and " + sqliteTable.Wo_Mo + "='" + txtsearch.Text.ToString() + "' and  " + sqliteTable.Contract + "='" + splitstring[0] + "')";

                                                catalogdb.ExecSQL(query);
                                                txtresult.Text = "";
                                                mAdapter.NotifyDataSetChanged();
                                            }
                                            else
                                            {
                                                txtresult.Text = "";
                                                Toast.MakeText(Activity, "Part sudah tersedia", ToastLength.Short).Show();
                                              }
                                        }
                                        else
                                        {
                                            txtresult.Text = "";
                                            Toast.MakeText(Activity, "Part tidak berada pada rak ini,mohon untuk scan rak!", ToastLength.Short).Show();
                                        }
                                }
                                else
                                 {
                                    txtresult.Text = "";
                                    Toast.MakeText(Activity, "Mohon untuk scan rak terlebih dahulu!", ToastLength.Short).Show();
                                }
                            }
                        }
                    //txtresult.Text = "";
                }

                else
                {
                    txtresult.Text = "";
                    msg = "Scanning Canceled!";
                    Activity.RunOnUiThread(() => Toast.MakeText(Activity.ApplicationContext, msg, ToastLength.Short).Show());
                }
               
            }catch(Exception ex)
            {
                txtresult.Text = "";
                Toast.MakeText(Activity, "Network Error", ToastLength.Long).Show();
            }
        }
        public  void OnBackPressed()
        {
            Intent i = new Intent(Activity,typeof(MainActivity));
            i.PutExtra("dept", dept);
            i.PutExtra("trans", Wo);
            Activity.StartActivity(i);
            Activity.Finish();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menusubmit:
                    {
                        try
                        {
                            using (_dialogBuilder = new AlertDialog.Builder(Activity))
                            {
                                
                                _dialogBuilder.SetView(view2);
                                //_dialogBuilder.SetMessage("Apakah yakin untuk submit?");
                                _dialogBuilder.SetCancelable(false);

                                _dialogBuilder.SetPositiveButton("OK", submit);
                                _dialogBuilder.SetNegativeButton("Cancel", canceldialog);
                                //_dialogBuilder.SetNegativeButton("Cancel", BatalAction);
                                alertdialog = _dialogBuilder.Create();

                                alertdialog.Show();
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