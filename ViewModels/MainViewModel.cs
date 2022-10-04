using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        string display = "0";
        public string Display
        {
            get => display;
            set
            {
                display = value;
                OnPropertyChanged("Display");
            }
        }

        public string Dot => CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
    }
}
