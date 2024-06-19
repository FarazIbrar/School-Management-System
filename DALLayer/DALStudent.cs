using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using static System.Collections.Specialized.BitVector32;
using System.Reflection;
using System.Xml.Linq;

namespace DALLayer
{
    public class DALStudent
    {
        public static void AddStudent(ModelStudent student,int parentID)
        {
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_SaveStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // Adding parameters
                cmd.Parameters.AddWithValue("@name", student.name);
                cmd.Parameters.AddWithValue("@grade", student.grade);
                cmd.Parameters.AddWithValue("@section", student.section);
                cmd.Parameters.AddWithValue("@gender", student.gender);
                cmd.Parameters.AddWithValue("@DOB", student.DOB);
                cmd.Parameters.AddWithValue("@cnic", student.cnic);
                cmd.Parameters.AddWithValue("@admissionNo", student.admissionNo);
                cmd.Parameters.AddWithValue("@admissionDate", student.admissionDate);
                cmd.Parameters.AddWithValue("@address", student.address);
                cmd.Parameters.AddWithValue("@parentID", parentID);
                cmd.Parameters.AddWithValue("@studentEmail", student.studentEmail);
                cmd.Parameters.AddWithValue("@studentPassword", student.studentPassword);

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

        public static ModelStudent GetStudentNameByEmailAndPassword(string email, string password)
        {
            ModelStudent student = new ModelStudent();
            SqlConnection con = DBHelper.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetStudentNameByEmailAndPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                // Execute the command and read the result
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student.regID = Convert.ToInt32(reader["regID"]);
                    student.name = reader["name"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception
                student.cnic = "11111";
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return student;
        }


        public static List<ParentStudent> GetAllStudents()
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GetAllStudents", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            List<ParentStudent> listShows = new List<ParentStudent>();
            while (reader.Read())
            {
                ParentStudent student = new ParentStudent();
                student.regID = Convert.ToInt32(reader["regID"]);
                student.name = reader["name"].ToString();
                student.parentName = reader["parentName"].ToString();
                student.cnic = reader["cnic"].ToString();
                student.grade = reader["grade"].ToString();
                student.section = reader["section"].ToString();
                student.gender = reader["gender"].ToString();
                student.admissionDate = Convert.ToDateTime(reader["admissionDate"]);
                listShows.Add(student);
            }
            con.Close();
            return listShows;
        }

        

            public static List<ParentStudent> GetStudentByClass(string grade)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GetStudentsByClass", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@grade", grade);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ParentStudent> listShows = new List<ParentStudent>();
            while (reader.Read())
            {
                ParentStudent student = new ParentStudent();
                student.regID = Convert.ToInt32(reader["regID"]);
                student.name = reader["name"].ToString();
                student.parentName = reader["parentName"].ToString();
                student.cnic = reader["cnic"].ToString();
                student.grade = reader["grade"].ToString();
                student.section = reader["section"].ToString();
                student.gender = reader["gender"].ToString();
                student.admissionDate = Convert.ToDateTime(reader["admissionDate"]);
                listShows.Add(student);
            }
            con.Close();
            return listShows;
        }

        public static string GetStudentNameByRegID(int regID)
        {
            string studentName = string.Empty;

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SP_GetStudentName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@regID", regID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            studentName = reader["StudentName"].ToString();
                        }
                    }
                }
            }

            return studentName;
        }


        public static void DeleteStudent(int regID)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("DeleteParentAndStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@regID", regID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void UpdateRecord(int regID, ParentStudent ps)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("UpdateParentAndStudentInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@regID", regID);
            cmd.Parameters.AddWithValue("@name", ps.name);
            cmd.Parameters.AddWithValue("@parentName", ps.parentName);
            cmd.Parameters.AddWithValue("@grade", ps.grade);
            cmd.Parameters.AddWithValue("@section", ps.section);
            cmd.Parameters.AddWithValue("@phoneNo", ps.phoneNo);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void PromoteStudent(int regID, ParentStudent ps)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("PromoteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@regID", regID);           
            cmd.Parameters.AddWithValue("@grade", ps.grade);
            cmd.Parameters.AddWithValue("@section", ps.section);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static ParentStudent GetStudentandParentByID(int regID)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SPP_GetStudentandParentInfoByID ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@regID", regID);
            SqlDataReader reader = cmd.ExecuteReader();
            
            ParentStudent student = new ParentStudent();
            
            while (reader.Read())
            {
                student.regID = Convert.ToInt32(reader["regID"]);
                student.name = reader.GetString(reader.GetOrdinal("name"));
                student.grade = reader.GetString(reader.GetOrdinal("grade"));
                student.section = reader.GetString(reader.GetOrdinal("section"));
                student.gender = reader.GetString(reader.GetOrdinal("gender"));
                student.DOB = reader.GetDateTime(reader.GetOrdinal("DOB"));
                student.cnic = reader.GetString(reader.GetOrdinal("cnic"));
                student.admissionNo = reader.GetString(reader.GetOrdinal("admissionNo"));
                student.admissionDate = reader.GetDateTime(reader.GetOrdinal("admissionDate"));
                student.address = reader.GetString(reader.GetOrdinal("address"));
                student.parentID = reader.GetInt32(reader.GetOrdinal("parentID"));
                student.parentName = reader.GetString(reader.GetOrdinal("parentName"));
                student.parentcnic = reader.GetString(reader.GetOrdinal("cnic"));
                student.parentOccupation = reader.GetString(reader.GetOrdinal("parentOccupation"));
                student.parentSalary = reader.GetInt32(reader.GetOrdinal("parentSalary"));
                student.phoneNo = reader.GetString(reader.GetOrdinal("phoneNo"));
                student.studentEmail = reader.GetString(reader.GetOrdinal("studentEmail"));
                student.studentPassword = reader.GetString(reader.GetOrdinal("studentPassword"));
            }
                       
            con.Close();
            return student;
        }



        public static List<ParentStudent> GetStudentandParentByName(string name)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GetStudentRegIDAndParentName ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ParentStudent> listShows = new List<ParentStudent>();
            while (reader.Read())
            {
                ParentStudent student = new ParentStudent();
                student.regID = Convert.ToInt32(reader["regID"]);
                student.name = reader.GetString(reader.GetOrdinal("name"));
                student.parentName = reader.GetString(reader.GetOrdinal("parentName"));
                listShows.Add(student);
            }
            con.Close();
            return listShows;
        }


    }
}
