using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.DBUtility
{
    public static class DBProperty
    {
        public static string GetConnectionString()
        {
            return "Data Source=.\\SQLEXPRESS;Initial Catalog=PetPals;Integrated Security=True;";
        }
    }
}
