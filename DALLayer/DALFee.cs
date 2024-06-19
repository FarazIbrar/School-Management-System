using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ModelLayer;
using DALLayer;

namespace ModelLayer
{
    public class DALFee
    {
        public static int AddFeeInfo(ModelFee fee)
        {
            int flag = 0;
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertFeeInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                cmd.Parameters.AddWithValue("@regID", fee.regID);
                cmd.Parameters.AddWithValue("@totalFee", fee.totalFee);
                cmd.Parameters.AddWithValue("@submitedFee", fee.submitedFee);
                cmd.Parameters.AddWithValue("@status", fee.status);
                cmd.Parameters.AddWithValue("@feeMonth", fee.feeMonth);
                cmd.Parameters.AddWithValue("@feeYear", fee.feeYear);
                cmd.Parameters.AddWithValue("@submissionDate", fee.submissionDate);
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


        public static List<ModelFee> GetStudentFeeInfo(int regID, int feeYear)
        {
            List<ModelFee> feeInfoList = new List<ModelFee>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetStudentFeeInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@regID", regID);
                    cmd.Parameters.AddWithValue("@feeYear", feeYear);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelFee fee = new ModelFee();
                            fee.regID = Convert.ToInt32(reader["regID"]);
                            fee.feeMonth = Convert.ToInt32(reader["feeMonth"]);
                            fee.totalFee = Convert.ToDecimal(reader["totalFee"]);
                            fee.submitedFee = Convert.ToDecimal(reader["submitedFee"]);
                            fee.status = reader["status"].ToString();
                            if (!reader.IsDBNull(reader.GetOrdinal("submissionDate")))
                            {
                                fee.submissionDate = reader.GetDateTime(reader.GetOrdinal("submissionDate"));
                            }
                            else
                            {
                                // Handle the case where submissionDate is null
                                fee.submissionDate = DateTime.MinValue; // or null, depending on how you want to handle null dates
                            }
                            feeInfoList.Add(fee);
                        }
                    }
                }
            }

            return feeInfoList;
        }


    }
}
