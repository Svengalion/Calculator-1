using DataModels.Repozitories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class DataManager
    {
        public IAutarizationRep AutarizationRep { get; }

        public DataManager(IUserRep userRep, IAutarizationRep autarizationRep, IHistoryRep historyRep)
        {
            AutarizationRep = autarizationRep;
            UserRep = userRep;
            HistoryRep = historyRep;
        }

        public IUserRep UserRep { get; }
        public IHistoryRep HistoryRep { get; }
    }
}
