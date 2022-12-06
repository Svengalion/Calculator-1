using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DataViewModel:ViewModelBase
    {
        private DataManager model = DataManager.Get(DataProvider.SqLite);
    }
}
