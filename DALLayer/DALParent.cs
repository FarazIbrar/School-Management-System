using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLayer
{
    public class DALParent
    {
        public static void AddParent(ModelParent parent)
        {
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PP_SaveParent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // Adding parameters
                cmd.Parameters.AddWithValue("@parentName", parent.parentName);
                cmd.Parameters.AddWithValue("@cnic", parent.cnic);
                cmd.Parameters.AddWithValue("@parentOccupation", parent.parentOccupation);
                cmd.Parameters.AddWithValue("@parentSalary", parent.parentSalary);
                cmd.Parameters.AddWithValue("@phoneNo", parent.phoneNo);
                // Executing the stored procedure
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public static int GetParentID(string cnic)
        {
            int parentId = 0;
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetParentIDByCNIC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // Adding parameters

                cmd.Parameters.AddWithValue("@cnic", cnic);

                SqlParameter outputParam = new SqlParameter();
                outputParam.ParameterName = "@parentID";
                outputParam.SqlDbType = SqlDbType.Int;
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                // Execute the command
                cmd.ExecuteNonQuery();

                // Retrieve the output parameter value
                parentId = Convert.ToInt32(outputParam.Value);
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return parentId;
        }



    }
    

}
