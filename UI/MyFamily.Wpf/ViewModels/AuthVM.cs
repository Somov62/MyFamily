using MyFamily.Wpf.Models;
using MyFamily.Wpf.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test_requests_mobile;

namespace MyFamily.Wpf.ViewModels
{
    internal class AuthVM : Base.Navigation.PageViewModel
    {
        public AuthVM()
        {
            CreateFamilyCommand = new AsyncCommand(() =>
            {
                ApiTool.Post("", RegFamily);
            });
            LoginFamilyCommand = new AsyncCommand();
        }

        public ICommand CreateFamilyCommand { get; }
        public ICommand LoginFamilyCommand { get; }

        public Family LoginFamily { get; } = new Family();
        public Family RegFamily { get; } = new Family();
    }
}
