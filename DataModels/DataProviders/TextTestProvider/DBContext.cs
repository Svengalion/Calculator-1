using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DataModels.DataProviders.TextTestProvider
{
    class DBContext
    {
        readonly string MainPath; //
        readonly string UserFile; //= ;
        readonly string HistoryFile;
        readonly string AutoFile;

        public DBContext()
        {
            MainPath = Assembly.GetExecutingAssembly().Location;
            UserFile = Path.Combine(MainPath, "users.csv");
            HistoryFile = Path.Combine(MainPath, "history.csv");
            AutoFile = Path.Combine(MainPath, "autos.csv");

            if (!File.Exists(UserFile))
            {
                using (StreamWriter sw = new StreamWriter(File.Create(UserFile)))
                {

                }
            }
        }
    }
}
