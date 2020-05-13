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
using Android.Support.V7.Widget;
using Android.Graphics;
using System.Net;
using Android.Database.Sqlite;
using Android.Database;

namespace BarcodeTeknikMobile
{
    class WOAdapter : RecyclerView.Adapter
    {

        public event EventHandler<int> ItemClick;
        List<WoGetSet> recyclelist;
        private SQLiteDatabase catalogdb;
        private sqliteHelper dbInstance;
        public WOAdapter(List<WoGetSet> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.listWo, parent, false);

            RecycleViewHolder2 vh = new RecycleViewHolder2(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolder2 vh = holder as RecycleViewHolder2;

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;
          
            vh.txtWO.Text = recyclelist[position].getWO();
            vh.vwWo.Text = recyclelist[position].getTitle();
            vh.vwWOdesc.Text = recyclelist[position].getdesc();

            vh.cb.Checked = false;
            vh.cb.Enabled = false;

            if (recyclelist[position].getflag().ToString() == "1")
            {


                vh.cb.Checked = true;
                vh.lndivider.SetBackgroundResource(Resource.Drawable.recycledivider1);

            }
            else
            {
                vh.lndivider.SetBackgroundResource(Resource.Drawable.recycledivide2);
            }

            string queryq = "select * from " + sqliteTable.TableWoTransaction + " where " + sqliteTable.Wo_Mo + "='" + recyclelist[position].getWO() + "'";
            ICursor cursor2 = catalogdb.RawQuery(queryq, null);
            if (cursor2.Count > 0)
            {
                if (recyclelist[position].getflag().ToString() != "1")
                {
                    vh.lndivider.SetBackgroundResource(Resource.Drawable.recyclerdivider3);
                }
            }
        }
        public void removeitem(int position)
        {
            recyclelist.RemoveAt(position);
            // notify item added by position
            NotifyItemRemoved(position);
        }
        public void restoreItem(WoGetSet item, int position)
        {
            recyclelist.Insert(position, item);
            // notify item added by position
            NotifyItemInserted(position);
        }
        public override int ItemCount
        {
            get { return recyclelist == null ? 0 : recyclelist.Count; }
        }
        void OnClick(int position)
        {
            ItemClick(this, position);
        }
        //void OnlongClick(int position)
        //{
        //    ItemClick2(this, position);
        //}
    }
    public class RecycleViewHolder2 : RecyclerView.ViewHolder
    {
        public int count = 0;
        public TextView txtWO{ get; private set; }
        public CheckBox cb { get; private set; }
        public TextView vwWo { get; private set; }

        public TextView vwWOdesc { get; private set; }

        public LinearLayout lndivider { get; set; }
        public RelativeLayout vwbackground;

        public RelativeLayout viewForeground;
        public RecycleViewHolder2(View itemView, Action<int> listener) : base(itemView)
        {
            lndivider = itemView.FindViewById<LinearLayout>(Resource.Id.lndivider);
            vwbackground = itemView.FindViewById<RelativeLayout>(Resource.Id.view_background);
            viewForeground = itemView.FindViewById<RelativeLayout>(Resource.Id.view_foreground);
            txtWO = itemView.FindViewById<TextView>(Resource.Id.txtWo);
            vwWo = itemView.FindViewById<TextView>(Resource.Id.vwWO);
            cb = itemView.FindViewById<CheckBox>(Resource.Id.cb);
           vwWOdesc = itemView.FindViewById<TextView>(Resource.Id.vwWOdesc);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);

           // itemView.LongClick += (sender, e) => listener2(base.LayoutPosition);

        }
    }

}