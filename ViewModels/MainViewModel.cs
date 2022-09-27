using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        string display = "";
        public string Display
        {
            get => display;
            set
            {
                display = value;
                OnPropertyChanged("Display");
            }
        }
    }
}
