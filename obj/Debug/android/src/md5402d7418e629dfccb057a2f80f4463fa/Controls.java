package md5402d7418e629dfccb057a2f80f4463fa;


public class Controls
	extends md5402d7418e629dfccb057a2f80f4463fa.Activity1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("NetworkDetection.Controls, NetworkDetection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Controls.class, __md_methods);
	}


	public Controls () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Controls.class)
			mono.android.TypeManager.Activate ("NetworkDetection.Controls, NetworkDetection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

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
