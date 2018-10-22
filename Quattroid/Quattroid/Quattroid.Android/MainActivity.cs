namespace Quattroid.Droid
{
	using Android.App;
	using Android.Content.PM;
	using Android.OS;

	[Activity(Label = "Quattroid 2", 
			  Icon = "@drawable/ic_launcher", 
			  Theme = "@style/MainTheme", 
			  MainLauncher = false, 
		      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}