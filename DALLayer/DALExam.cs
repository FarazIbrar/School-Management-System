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
    public class DALExam
    {
        public static int AddExamInfo(ModelExam exam)
        {
            int flag = 0;
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EP_SaveExam", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                cmd.Parameters.AddWithValue("@regID", exam.regID);
                cmd.Parameters.AddWithValue("@examType", exam.examType);
                cmd.Parameters.AddWithValue("@obtainedMarks", exam.obtainedMarks);
                cmd.Parameters.AddWithValue("@totalMarks", exam.totalMarks);

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


        public static List<ModelExam> GetStudentMarksReport(int regID)
        {
            List<ModelExam> examInfoList = new List<ModelExam>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("EP_GetStudentMarksReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@regID", regID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelExam exam = new ModelExam();
                            exam.examID = Convert.ToInt32(reader["examID"]);
                            exam.examType = reader["examType"].ToString();
                            exam.obtainedMarks = Convert.ToDecimal(reader["obtainedMarks"]);
                            exam.totalMarks = Convert.ToDecimal(reader["totalMarks"]);
                            examInfoList.Add(exam);
                        }
                    }
                }
            }

            return examInfoList;
        }

    }
}
