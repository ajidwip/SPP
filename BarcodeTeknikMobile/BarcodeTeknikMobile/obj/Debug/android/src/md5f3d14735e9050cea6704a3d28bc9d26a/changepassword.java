package md5f3d14735e9050cea6704a3d28bc9d26a;


public class changepassword
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("BarcodeTeknikMobile.changepassword, BarcodeTeknikMobile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", changepassword.class, __md_methods);
	}


	public changepassword ()
	{
		super ();
		if (getClass () == changepassword.class)
			mono.android.TypeManager.Activate ("BarcodeTeknikMobile.changepassword, BarcodeTeknikMobile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
