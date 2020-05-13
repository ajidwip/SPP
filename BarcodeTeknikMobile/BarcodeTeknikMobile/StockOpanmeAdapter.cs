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
namespace BarcodeTeknikMobile
{
    class StockOpnameAdapter : RecyclerView.Adapter
    {

        public event EventHandler<int> ItemClick, ItemLongClick;
        List<StockOpnameGetSet> recyclelist;
        //int count = 0;
        //int cek = 0;
        
        public StockOpnameAdapter(List<StockOpnameGetSet> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.liststockopname, parent, false);

            RecycleViewHolder1 vh = new RecycleViewHolder1(itemView, OnClick, OnLongClick);
            return vh;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolder1 vh = holder as RecycleViewHolder1;


            vh.txtpartNo.Text = recyclelist[position].getPartNo();
            vh.editqty.Text = recyclelist[position].getQty();
            vh.Site.Text = recyclelist[position].getContract();
            vh.txtStatusKirim.Text= recyclelist[position].getstatuskirim();
            vh.editrak.Text = recyclelist[position].getRakNo();
            vh.uom.Text= recyclelist[position].getuom();
        }


        public override int ItemCount
        {
            get { return recyclelist == null ? 0 : recyclelist.Count; }
        }
        void OnClick(int position)
        {
            ItemClick(this, position);
        }
        void OnLongClick(int position)
        {
            ItemLongClick(this, position);
        }
    }
    public class RecycleViewHolder1 : RecyclerView.ViewHolder
    {
        public int count = 0;
        public TextView txtpartNo { get; private set; }
        public TextView editrak { get; private set; }
        public TextView txtStatusKirim { get; private set; }
        public TextView editqty { get; private set; }
        public TextView uom { get; private set; }
        public LinearLayout lndivider { get; private set; }
        public TextView Site { get; private set; }
        public RecycleViewHolder1(View itemView, Action<int> listener, Action<int> listener2) : base(itemView)
        {
            //lndivider = itemView.FindViewById<LinearLayout>(Resource.Id.lndivider);
           
            txtpartNo = itemView.FindViewById<TextView>(Resource.Id.txtpartNo);
            editqty = itemView.FindViewById<TextView>(Resource.Id.editqty);
            Site = itemView.FindViewById<TextView>(Resource.Id.editSite);
            editrak = itemView.FindViewById<TextView>(Resource.Id.editrak);
            txtStatusKirim = itemView.FindViewById<TextView>(Resource.Id.txtstatuskirim1);
            uom = itemView.FindViewById<TextView>(Resource.Id.uom);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);

            itemView.LongClick += (sender, e) => listener2(base.LayoutPosition);
        }
    }

}