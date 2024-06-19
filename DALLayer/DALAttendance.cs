using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLayer
{
    public  class DALAttendance
    {
        public static int SubmitAttendance(List<ModelAttendance> attendanceRecords)
        {
            int flag = 1;
            // Iterate through each attendance record in the list
            foreach (var attendanceRecord in attendanceRecords)
            {
                try
                {
                    using (SqlConnection con = DBHelper.GetConnection())
                    {
                        // Open the connection
                        con.Open();

                        // Define the stored procedure name
                        string procedureName = "InsertStudentAttendanceRecord";

                        // Create a SqlCommand object with the procedure name and connection
                        using (SqlCommand cmd = new SqlCommand(procedureName, con))
                        {
                            // Specify that the command is a stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Set the parameters for the stored procedure
                            cmd.Parameters.AddWithValue("@regID", attendanceRecord.regID);
                            cmd.Parameters.AddWithValue("@attendanceDate", attendanceRecord.attendanceDate);
                            cmd.Parameters.AddWithValue("@status", attendanceRecord.status);
                            // Execute the stored procedure
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during database operation
                    Console.WriteLine("Error inserting attendance record: " + ex.Message);
                    flag = 0;
                    // You can choose to log the error or handle it in any other way as per your requirement
                }

            }
            return flag;
        }

        public static List<ModelAttendance> GetStudentByClass(string grade,DateTime attendanceDate)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("GetClassAttendance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@grade", grade);
            cmd.Parameters.AddWithValue("@attendanceDate", attendanceDate);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ModelAttendance> listAttendance = new List<ModelAttendance>();
            while (reader.Read())
            {
                ModelAttendance attendance = new ModelAttendance();
                attendance.regID = Convert.ToInt32(reader["regID"]);
                attendance.studentName = reader["name"].ToString();
                attendance.status = reader["status"].ToString();
                
                listAttendance.Add(attendance);
            }
            con.Close();
            return listAttendance;
        }

        public static List<ModelAttendance> GetStudentAttendanceReport(int regID, int year, int month)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("GetStudentAttendanceReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@regID", regID);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ModelAttendance> listAttendance = new List<ModelAttendance>();
            while (reader.Read())
            {
                ModelAttendance attendance = new ModelAttendance();
                attendance.attendanceDate = Convert.ToDateTime(reader["attendanceDate"]);
                attendance.status = reader["status"].ToString();

                listAttendance.Add(attendance);
            }
            con.Close();
            return listAttendance;
        }


    }
}
