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
using AppPan_PanaderoCliente.Constants;
using Plugin.CurrentActivity;

namespace AppPan_PanaderoCliente.Droid
{
    class MainAplication
    {
#if DEBUG
        [Application(Debuggable = true)]
#else
	        [Application(Debuggable = false)]
#endif
        [MetaData("com.google.android.maps.v2.API_KEY", Value = AppConstants.GoogleMapsApiKey)]
        public class MainApplication : Application
        {
            public MainApplication(IntPtr handle, JniHandleOwnership transer)
              : base(handle, transer)
            {
            }
            public override void OnCreate()
            {
                base.OnCreate();
                CrossCurrentActivity.Current.Init(this);
            }
        }
    }
}