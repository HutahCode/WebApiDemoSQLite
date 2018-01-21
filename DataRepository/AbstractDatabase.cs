using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data;


namespace WebApiDemoLite.DataRepository
{
    public class AbstractDatabase
    {
        public IDbConnection DBConnection
        {
            get
            {
                return new SQLiteConnection(@"Data Source=|DataDirectory|\sampledata.db3;Version=3;");
            }
        }
    }
}