using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DALLayer
{
    public  class DBHelper
    {
        
            public static SqlConnection GetConnection()
            {
                SqlConnection con = new SqlConnection("Data Source=FARAZ\\SQLEXPRESS;Initial Catalog=SchoolManagementSystem;Integrated Security=True;Encrypt=False");
                return con;
            }
        
    }
}
