using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DataViewModel:ViewModelBase
    {
        public DataManager model = DataManager.Get(DataProvider.SqLite);
    }
}
