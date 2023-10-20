using Android;
using AndroidX;
using Android.OS;
using MyFamily.XF.Droid;
using MyFamily.XF.Services;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(PathService))]
namespace MyFamily.XF.Droid
{
    public class PathService : IPathService
    {
        public string InternalFolder
        {
            get
            {
                return Application.Context.FilesDir.AbsolutePath;
            }
        }

        public string PublicExternalFolder
        {
            get
            {
                return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments).AbsolutePath;
            }
        }

        public string PrivateExternalFolder
        {
            get
            {
                return Application.Context.GetExternalFilesDir(null).AbsolutePath;
            }
        }

        
    }
}