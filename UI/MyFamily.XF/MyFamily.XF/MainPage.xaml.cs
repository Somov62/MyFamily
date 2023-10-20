using MyFamily.XF.Database.Excel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyFamily.XF
{
    public partial class MainPage : ContentPage
    {
        public MainPage() => InitializeComponent();

        private  void ContentPage_Appearing(object sender, EventArgs e)
        {
            //var readStorage= await Permissions.RequestAsync<Permissions.StorageRead>();
            //var writeStorage = await Permissions.RequestAsync<Permissions.StorageWrite>();
            //if (readStorage == PermissionStatus.Granted && writeStorage == PermissionStatus.Granted)
            {
                BindingContext = new MainPageModel(this);
                OnPropertyChanged(nameof(BindingContext));
            }
        }

       
    }

    public sealed class MainPageModel : INotifyPropertyChanged
    {
        private readonly ExcelService excelService = new ExcelService();

        public MainPageModel(ContentPage owner)
        {
            var categories = excelService.GetCategories();

            TransactionTemplate = new Transaction()
            {
                Guid = Guid.NewGuid(),
                Date = DateTime.Now
            };

            SelectCategoryCommand = new Command(async () =>
            {
                var action = await owner.DisplayActionSheet("Категория", "Отмена", "", categories.Select(p => p.Item1).ToArray());
                var selected = categories.FirstOrDefault(p => p.Item1 == action);
                if (selected == default)
                    return;

                Category = selected.Item1;
                if (selected.Item2 == null || selected.Item2.Count == 0)
                    return;

                if (selected.Item2.Count == 1)
                    SubCategory = selected.Item2[0];
            });

            SelectSubCategoryCommand = new Command(async () =>
            {
                var currentCategory = categories.FirstOrDefault(p => p.Item1 == Category);
                if (currentCategory == default)
                    return;

                var action = await owner.DisplayActionSheet("Подкатегория", "Отмена", "", currentCategory.Item2.ToArray());
                if (!currentCategory.Item2.Contains(action))
                    return;

                SubCategory = action;
                DoPropertyChanged(nameof(TransactionTemplate.SubCategory));

            });

            SaveCommand = new Command(() =>
            {
                if (!string.IsNullOrWhiteSpace(Category))
                {
                    if (!categories.Select(p => p.Item1).Contains(Category))
                        excelService.AddCategory(Category);
                }

                if (!string.IsNullOrWhiteSpace(Category))
                {
                    var currentCategory = categories.FirstOrDefault(p => p.Item1 == Category);
                    if (currentCategory != default)
                    {
                        if (!currentCategory.Item2.Contains(SubCategory))
                            excelService.AddSubCategory(Category, SubCategory);
                    }
                    else
                        excelService.AddSubCategory(Category, SubCategory);
                }

                TransactionTemplate.Category = Category;
                TransactionTemplate.SubCategory = SubCategory;
                TransactionTemplate.Amount = Amount ?? 0;
                if (IsExpensive)
                {
                    TransactionTemplate.Amount = Math.Abs(TransactionTemplate.Amount) * (-1);
                }
                TransactionTemplate.Date.AddSeconds(Time.TotalSeconds);

                excelService.InsertDataIntoTransactionsSheet(TransactionSheetStructure.Get(TransactionTemplate));
                owner.DisplayAlert("", "Сохранено!", "ОK");
                categories = excelService.GetCategories();
                TransactionTemplate = new Transaction()
                {
                    Guid = Guid.NewGuid(),
                    Date = DateTime.Now
                };
                DoPropertyChanged(nameof(TransactionTemplate));
                Amount = null;
                DoPropertyChanged(nameof(Amount));
                IsExpensive = true;
                DoPropertyChanged(nameof(IsExpensive));
                DoPropertyChanged(nameof(IsExpensive));
                Category = null;
                SubCategory = null;
                Time = DateTime.Now.TimeOfDay;
                DoPropertyChanged(nameof(Time));
            });
        }

        public Command SaveCommand { get; }
        public Command SelectCategoryCommand { get; }
        public Command SelectSubCategoryCommand { get; }

        public TimeSpan Time { get; set; } = DateTime.Now.TimeOfDay;

        public decimal? Amount { get; set; }

        private string _category;
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                DoPropertyChanged();
            }
        }

        private string _subcategory;
        public string SubCategory
        {
            get => _subcategory;
            set
            {
                _subcategory = value;
                DoPropertyChanged();
            }
        }

        public bool IsExpensive { get; set; } = true;

        public Transaction TransactionTemplate { get; set; }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void DoPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
