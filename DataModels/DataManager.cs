using DataModels.Repozitories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class DataManager
    {
        public IAuthorizationRep AutoriationRep { get; }
        public IUserRep UserRep { get; }
        public IHistoryRep HistoryRep { get; }

        public DataManager(IUserRep userRep, IAuthorizationRep autarizationRep, IHistoryRep historyRep)
        {
            AutoriationRep = autarizationRep;
            UserRep = userRep;
            HistoryRep = historyRep;
        }
    }
}
