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
    public  class DALSalary
    {
        public static int AddSalaryInfo(ModelSalary salary)
        {
            int flag = 0;
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertTeacherSalary", con); // Assuming the name of the stored procedure is "InsertSalaryInfo"
                cmd.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                cmd.Parameters.AddWithValue("@teacherID", salary.teacherID);
                cmd.Parameters.AddWithValue("@totalSalary", salary.totalSalary);
                cmd.Parameters.AddWithValue("@salaryStatus", salary.salaryStatus);
                cmd.Parameters.AddWithValue("@salaryMonth", salary.salaryMonth);
                cmd.Parameters.AddWithValue("@salaryYear", salary.salaryYear);
                cmd.Parameters.AddWithValue("@payDate", salary.payDate);

                // Executing the stored procedure
                cmd.ExecuteNonQuery();
                flag = 1;
            }
            catch (Exception ex)
            {
                // Handle the exception
                flag = -1;
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return flag;
        }
        public static List<ModelSalary> GetTeacherSalaryInfo(int teacherID, int salaryYear)
        {
            List<ModelSalary> salaryInfoList = new List<ModelSalary>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetTeacherSalaryInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@teacherID", teacherID);
                    cmd.Parameters.AddWithValue("@salaryYear", salaryYear);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelSalary salary = new ModelSalary();
                            salary.teacherID = Convert.ToInt32(reader["teacherID"]);
                            salary.salaryMonth = Convert.ToInt32(reader["salaryMonth"]);
                            salary.totalSalary = Convert.ToDecimal(reader["totalSalary"]);
                            salary.salaryStatus = reader["salaryStatus"].ToString();

                            // Check if payDate is null before attempting to read it
                            if (!reader.IsDBNull(reader.GetOrdinal("payDate")))
                            {
                                salary.payDate = reader.GetDateTime(reader.GetOrdinal("payDate"));
                            }
                            else
                            {
                                // Handle the case where submissionDate is null
                                salary.payDate = DateTime.MinValue; // or null, depending on how you want to handle null dates
                            }

                            salaryInfoList.Add(salary);
                        }
                    }
                }
            }

            return salaryInfoList;
        }

    }
    

}
