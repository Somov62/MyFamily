using MyFamily.Wpf.MVVM;
using MyFamily.Wpf.Services;

namespace MyFamily.Wpf.ViewModels.Base
{
    public abstract class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
            ServiceManager.InternetConnectionService.ConnectionChanged += ConnectionChanged;
        }
        private void ConnectionChanged(bool actualConnction)
        {
            OnPropertyChanged(nameof(IsConnectionExists));
        }

        public bool IsConnectionExists => ServiceManager.InternetConnectionService.IsConnectionExists;

        public void Set<T>(ref  T field, T value, string? property = null)
        {
            field = value;
            base.OnPropertyChanged(property);
        }

        public virtual void OnAppearing()
        {
            this.OnPropertyChanged();
        }
    }
}
