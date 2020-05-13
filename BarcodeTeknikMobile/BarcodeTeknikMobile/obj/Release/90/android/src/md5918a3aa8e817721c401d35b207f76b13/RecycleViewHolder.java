package md5918a3aa8e817721c401d35b207f76b13;


public class RecycleViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BarcodeTeknikMobile.RecycleViewHolder, BarcodeTeknikMobile", RecycleViewHolder.class, __md_methods);
	}


	public RecycleViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == RecycleViewHolder.class)
			mono.android.TypeManager.Activate ("BarcodeTeknikMobile.RecycleViewHolder, BarcodeTeknikMobile", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
