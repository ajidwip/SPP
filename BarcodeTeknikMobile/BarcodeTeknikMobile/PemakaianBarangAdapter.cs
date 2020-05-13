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
    class PemakaianBarangAdapter : RecyclerView.Adapter
    {

        public event EventHandler<int> ItemClick,ItemLongClick;
        List<PemakaianBarangGetterSetter> recyclelist;
       
         
       
            public PemakaianBarangAdapter(List<PemakaianBarangGetterSetter> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.listPemakaianBarang, parent, false);

            RecycleViewHolder vh = new RecycleViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {     

            RecycleViewHolder vh = holder as RecycleViewHolder;

          
            vh.txtpartNo.Text = recyclelist[position].getPartNo();
            vh.Site.Text = recyclelist[position].getContract();

            vh.editqty.Text = recyclelist[position].getQty();
            vh.txtstatus.Text = recyclelist[position].getstatuskirim();
            vh.editrak.Text = recyclelist[position].getRakNo();

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
    public class RecycleViewHolder : RecyclerView.ViewHolder
    {
        public int count = 0;
        public TextView txtpartNo { get; private set; }
        public TextView editqty { get; private set; }
        public TextView txtstatus { get; private set; }
        public TextView Site { get; private set; }
        public TextView editrak { get; private set; }
        public LinearLayout lndivider { get; private set; }

        public RecycleViewHolder(View itemView, Action<int> listener,Action<int>listener2) : base(itemView)
        {
            //lndivider = itemView.FindViewById<LinearLayout>(Resource.Id.lndivider);
            txtpartNo = itemView.FindViewById<TextView>(Resource.Id.txtpartNo);
            editqty = itemView.FindViewById<TextView>(Resource.Id.editqty);
            txtstatus = itemView.FindViewById<TextView>(Resource.Id.txtstatus);
            Site = itemView.FindViewById<TextView>(Resource.Id.editSite);
            editrak = itemView.FindViewById<TextView>(Resource.Id.editrak);



            itemView.Click += (sender, e) => listener(base.LayoutPosition);

            itemView.LongClick+= (sender, e) => listener2(base.LayoutPosition);
        }
    }

}