using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using CalculationsModel;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase
    {
         Calculations model = new Calculations();
        
        string display = "0";
        public string Display
        {
            get => display;
            set
            {
                display = value;
                OnPropertyChanged("Display");
                BackSpace.RaiseCanExecuteChanged();
                DotPress.RaiseCanExecuteChanged();
            }
        }

        void SetInfo()
        {
            if(string.IsNullOrEmpty(lastOperation))
            {
                Info = Display;
                return;
            }
            if(model.IsAtomar)
            {
                Info = lastOperation + "(" + display + ")";
                return;
            }
            Info += $" {lastOperation} {display}";
            Display = "0";
        }



        string info = "";
        public string Info
        {
            get => info;
            set
            {
                info = value;
                OnPropertyChanged("Info");
                
            }
        }
        string lastOperation = "";
        string LastOperatin
        {
            get => lastOperation;
            set
            {
                
            }
        }

        public string Dot => CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public  Command<string> DigitPress { get; }
        public  Command<string> OperationPress { get; }
        public  Command BackSpace { get; }
        public  Command PlusMinus { get; }
        public  Command DotPress { get; }

        public MainViewModel()
        {
            DigitPress = new Command<string>(digitPress);
            OperationPress = new Command<string>(operationPress, _=> string.IsNullOrEmpty(lastOperation));
            BackSpace = new Command(backSpace, () => Display != "0");
            PlusMinus = new Command(() =>
            Display = Display[0] == '-' ? Display.Remove(0, 1) : '-' + Display) ;
            DotPress = new Command(()=> Display += Dot, ()=> !Display.Contains(Dot));
        }

        private void operationPress(string obj)
        {
            lastOperation = obj;
            model = new Calculations(display, "0", obj);
            SetInfo();
           // if (model.IsAtomar)
        }

        private void backSpace()
        {
            if (Display == "-0" ||
                (Display[0] == '-' && Display.Length == 2) ||
                Display.Length == 1)
            {
                Display = "0";
                return;
            }
            Display = Display.Remove(Display.Length - 1);    
        }

        private void digitPress(string obj)
        {
            if(Display=="0" || Display=="-0")
            {
                Display = Display.Trim('0') + obj;
            }
            else
            {
                Display = Display + obj;
            }
        }
    }
}
