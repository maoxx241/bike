using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace bike
{
    public class ConfigService
    {
        public static string ConnectionString
        {
            get
            {
                return "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["SQLiteDB"].ConnectionString;

            }
        }

    }
}
