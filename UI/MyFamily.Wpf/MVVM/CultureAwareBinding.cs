using System.Globalization;
using System.Windows.Data;

namespace MyFamily.Wpf.MVVM
{
    public class CultureAwareBinding : Binding
    {
        public CultureAwareBinding()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
    }
}
