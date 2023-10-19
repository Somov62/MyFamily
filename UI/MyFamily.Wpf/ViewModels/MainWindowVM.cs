using MyFamily.Wpf.Models;
using MyFamily.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamily.Wpf.ViewModels
{
    public class MainWindowVM : Base.Navigation.HostPageViewModel
    {
        public MainWindowVM()
        {
            var family = SettingsService.Configuration.Family;
            if (family == null)
                base.Navigation.Navigate(new AuthVM());
        }

        internal Family CurrentFamily { get; set; }
    }
}
