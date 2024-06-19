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
    public class DALTeacher
    {
        public static void AddTeacher(ModelTeacher teacher)
        {
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("TP_SaveTeacher", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                cmd.Parameters.AddWithValue("@teacherName", teacher.teacherName);
                cmd.Parameters.AddWithValue("@teacherFatherName", teacher.teacherFatherName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@joiningDate", teacher.joiningDate != DateTime.MinValue ? teacher.joiningDate : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherGender", teacher.teacherGender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherDOB", teacher.teacherDOB != DateTime.MinValue ? teacher.teacherDOB : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherCnic", teacher.teacherCnic ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherPhoneNo", teacher.teacherPhoneNo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherAddress", teacher.teacherAddress ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherEducation", teacher.teacherEducation ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherPastExperience", teacher.teacherPastExperience ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@teacherEmail", teacher.teacherEmail);
                cmd.Parameters.AddWithValue("@teacherPassword", teacher.teacherPassword);

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


        public static ModelTeacher GetTeacherNameByEmailAndPassword(string email, string password)
        {
            ModelTeacher teacher = new ModelTeacher();
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetTeacherNameByEmailAndPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                // Execute the command and read the result
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    teacher.teacherID = Convert.ToInt32(reader["teacherID"]);
                    teacher.teacherName = reader["teacherName"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception
                teacher.teacherCnic= "11111";
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return teacher;
        }
        public static List<ModelTeacher> GetAllTeachers()
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GetTeacherInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            List<ModelTeacher> listTeachers = new List<ModelTeacher>();
            while (reader.Read())
            {
                ModelTeacher teacher = new ModelTeacher();
                teacher.teacherID = Convert.ToInt32(reader["teacherID"]);
                teacher.teacherName = reader["teacherName"].ToString();
                teacher.teacherFatherName = reader["teacherFatherName"].ToString();
                teacher.joiningDate = Convert.ToDateTime(reader["joiningDate"]);
                teacher.teacherCnic = reader["teacherCnic"].ToString();
                teacher.teacherGender = reader["teacherGender"].ToString();
                listTeachers.Add(teacher);
            }
            con.Close();
            return listTeachers;
        }

        public static void DeleteTeacher(int teacherID)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_DeleteTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@teacherID", teacherID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void UpdateRecord(int teacherID, ModelTeacher teacher)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("UpdateTeacherInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@teacherID", teacherID);
            cmd.Parameters.AddWithValue("@teacherName", teacher.teacherName);
            cmd.Parameters.AddWithValue("@teacherFatherName", teacher.teacherFatherName);
            cmd.Parameters.AddWithValue("@teacherAddress", teacher.teacherAddress);
            cmd.Parameters.AddWithValue("@teacherEducation", teacher.teacherEducation);
            cmd.Parameters.AddWithValue("@teacherPhoneNo", teacher.teacherPhoneNo);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static ModelTeacher GetTeacherByID(int teacherID)
        {
            ModelTeacher teacher = null;

            using (SqlConnection con = DBHelper.GetConnection())
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetTeacherByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@teacherID", teacherID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teacher = new ModelTeacher
                            {
                                teacherID = Convert.ToInt32(reader["teacherID"]),
                                teacherName = reader.IsDBNull(reader.GetOrdinal("teacherName")) ? null : reader.GetString(reader.GetOrdinal("teacherName")),
                                teacherFatherName = reader.IsDBNull(reader.GetOrdinal("teacherFatherName")) ? null : reader.GetString(reader.GetOrdinal("teacherFatherName")),
                                joiningDate = reader.IsDBNull(reader.GetOrdinal("joiningDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("joiningDate")),
                                teacherGender = reader.IsDBNull(reader.GetOrdinal("teacherGender")) ? null : reader.GetString(reader.GetOrdinal("teacherGender")),
                                teacherDOB = reader.IsDBNull(reader.GetOrdinal("teacherDOB")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("teacherDOB")),
                                teacherCnic = reader.IsDBNull(reader.GetOrdinal("teacherCnic")) ? null : reader.GetString(reader.GetOrdinal("teacherCnic")),
                                teacherPhoneNo = reader.IsDBNull(reader.GetOrdinal("teacherPhoneNo")) ? null : reader.GetString(reader.GetOrdinal("teacherPhoneNo")),
                                teacherAddress = reader.IsDBNull(reader.GetOrdinal("teacherAddress")) ? null : reader.GetString(reader.GetOrdinal("teacherAddress")),
                                teacherEducation = reader.IsDBNull(reader.GetOrdinal("teacherEducation")) ? null : reader.GetString(reader.GetOrdinal("teacherEducation")),
                                teacherPastExperience = reader.IsDBNull(reader.GetOrdinal("teacherPastExperience")) ? null : reader.GetString(reader.GetOrdinal("teacherPastExperience")),
                                teacherEmail = reader.IsDBNull(reader.GetOrdinal("teacherEmail")) ? null : reader.GetString(reader.GetOrdinal("teacherEmail")),
                                teacherPassword = reader.IsDBNull(reader.GetOrdinal("teacherPassword")) ? null : reader.GetString(reader.GetOrdinal("teacherPassword"))
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return teacher;
        }

        public static List<ModelTeacher> GetTeacherByName(string name)
        {
            List<ModelTeacher> teachers = new List<ModelTeacher>();
            
            using (SqlConnection con = DBHelper.GetConnection())
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetTeacherNames", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@teacherName", name);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelTeacher teacher = new ModelTeacher
                            {
                                teacherID = Convert.ToInt32(reader["teacherID"]),
                                teacherName = reader.IsDBNull(reader.GetOrdinal("teacherName")) ? null : reader.GetString(reader.GetOrdinal("teacherName")),
                                teacherFatherName = reader.IsDBNull(reader.GetOrdinal("teacherFatherName")) ? null : reader.GetString(reader.GetOrdinal("teacherFatherName"))
                            };

                            teachers.Add(teacher);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return teachers;
        }

        public static string GetTeacherNameByID(int teacherID)
        {
            string teacherName = string.Empty;

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SP_GetTeacherName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@teacherID", teacherID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teacherName = reader["teacherName"].ToString();
                        }
                    }
                }
            }

            return teacherName;
        }


    }


}
