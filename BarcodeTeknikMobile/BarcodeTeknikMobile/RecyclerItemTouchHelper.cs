using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;

namespace BarcodeTeknikMobile
{
    class RecyclerItemTouchHelper: ItemTouchHelper.SimpleCallback
    {
        private RecyclerItemTouchHelperListener listener;

        public RecyclerItemTouchHelper(int dragDirs, int swipeDirs, RecyclerItemTouchHelperListener listener) : base(dragDirs, swipeDirs)
        {
         
            this.listener = listener;
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            return true;
        }
        
    public override void OnChildDrawOver(Canvas c, RecyclerView recyclerView,
                                RecyclerView.ViewHolder viewHolder, float dX, float dY,
                                int actionState, bool isCurrentlyActive)
        {
            View foregroundView = ((RecycleViewHolder2)viewHolder).viewForeground;
           DefaultUIUtil.OnDrawOver(c, recyclerView, foregroundView, dX, dY,
                    actionState, isCurrentlyActive);
        }

        
    public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            View foregroundView = ((RecycleViewHolder2)viewHolder).viewForeground;
            DefaultUIUtil.ClearView(foregroundView);
        }

        
    public override void OnSelectedChanged(RecyclerView.ViewHolder viewHolder, int actionState)
        {
            if (viewHolder != null)
            {
                View foregroundView = ((RecycleViewHolder2)viewHolder).viewForeground;

                DefaultUIUtil.OnSelected(foregroundView);
            }
        }
        public override void OnChildDraw(Canvas c, RecyclerView recyclerView,
                            RecyclerView.ViewHolder viewHolder, float dX, float dY,
                            int actionState, bool isCurrentlyActive)
        {
            View foregroundView = ((RecycleViewHolder2)viewHolder).viewForeground;

            DefaultUIUtil.OnDraw(c, recyclerView, foregroundView, dX, dY,
                    actionState, isCurrentlyActive);
        }
        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            listener.onSwiped(viewHolder, direction, viewHolder.AdapterPosition);
        }
        
      public override int ConvertToAbsoluteDirection(int flags, int layoutDirection)
        {
            return base.ConvertToAbsoluteDirection(flags, layoutDirection);
        }
        public interface RecyclerItemTouchHelperListener
        {
            void onSwiped(RecyclerView.ViewHolder viewHolder, int direction, int position);
        }
    }
}