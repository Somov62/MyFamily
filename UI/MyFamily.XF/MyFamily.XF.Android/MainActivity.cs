using System;
using Android.Content;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace MyFamily.XF.Droid
{
    [Activity(Label = "Excel Бюджет", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#222222"));
            LoadApplication(new App("mainActivity"));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    //[IntentFilter(
    //    new[] { Intent.ActionView },
    //    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    //    DataSchemes = new[] { "http", "https" },
    //    DataHosts = new[] { "*.youtube.com", "youtube.com" }
    //)]
    //[Activity(Label = "Вступить в семью", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    //public class JoinFamilyActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    //{
    //    protected override void OnCreate(Bundle savedInstanceState)
    //    {
    //        base.OnCreate(savedInstanceState);

    //        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
    //        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    //        LoadApplication(new App("joinFamilyActivity"));
    //    }
    //    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    //    {
    //        Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

    //        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    //    }
    //}
}