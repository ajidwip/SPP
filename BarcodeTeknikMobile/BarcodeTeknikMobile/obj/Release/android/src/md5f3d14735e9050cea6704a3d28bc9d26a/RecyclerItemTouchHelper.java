package md5f3d14735e9050cea6704a3d28bc9d26a;


public class RecyclerItemTouchHelper
	extends android.support.v7.widget.helper.ItemTouchHelper.SimpleCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMove:(Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$ViewHolder;Landroid/support/v7/widget/RecyclerView$ViewHolder;)Z:GetOnMove_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_ViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"n_onChildDrawOver:(Landroid/graphics/Canvas;Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$ViewHolder;FFIZ)V:GetOnChildDrawOver_Landroid_graphics_Canvas_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_ViewHolder_FFIZHandler\n" +
			"n_clearView:(Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$ViewHolder;)V:GetClearView_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"n_onSelectedChanged:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnSelectedChanged_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onChildDraw:(Landroid/graphics/Canvas;Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$ViewHolder;FFIZ)V:GetOnChildDraw_Landroid_graphics_Canvas_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_ViewHolder_FFIZHandler\n" +
			"n_onSwiped:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnSwiped_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_convertToAbsoluteDirection:(II)I:GetConvertToAbsoluteDirection_IIHandler\n" +
			"";
		mono.android.Runtime.register ("BarcodeTeknikMobile.RecyclerItemTouchHelper, BarcodeTeknikMobile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RecyclerItemTouchHelper.class, __md_methods);
	}


	public RecyclerItemTouchHelper (int p0, int p1)
	{
		super (p0, p1);
		if (getClass () == RecyclerItemTouchHelper.class)
			mono.android.TypeManager.Activate ("BarcodeTeknikMobile.RecyclerItemTouchHelper, BarcodeTeknikMobile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean onMove (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1, android.support.v7.widget.RecyclerView.ViewHolder p2)
	{
		return n_onMove (p0, p1, p2);
	}

	private native boolean n_onMove (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1, android.support.v7.widget.RecyclerView.ViewHolder p2);


	public void onChildDrawOver (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.ViewHolder p2, float p3, float p4, int p5, boolean p6)
	{
		n_onChildDrawOver (p0, p1, p2, p3, p4, p5, p6);
	}

	private native void n_onChildDrawOver (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.ViewHolder p2, float p3, float p4, int p5, boolean p6);


	public void clearView (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1)
	{
		n_clearView (p0, p1);
	}

	private native void n_clearView (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1);


	public void onSelectedChanged (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onSelectedChanged (p0, p1);
	}

	private native void n_onSelectedChanged (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);


	public void onChildDraw (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.ViewHolder p2, float p3, float p4, int p5, boolean p6)
	{
		n_onChildDraw (p0, p1, p2, p3, p4, p5, p6);
	}

	private native void n_onChildDraw (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.ViewHolder p2, float p3, float p4, int p5, boolean p6);


	public void onSwiped (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onSwiped (p0, p1);
	}

	private native void n_onSwiped (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);


	public int convertToAbsoluteDirection (int p0, int p1)
	{
		return n_convertToAbsoluteDirection (p0, p1);
	}

	private native int n_convertToAbsoluteDirection (int p0, int p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
