using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLayer
{
    public class DALDashboard
    {
        public static int GetTotalNumberOfStudents()
        {
            int totalStudents = 0;
            SqlConnection con = DBHelper.GetConnection();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM StudentInfo", con);
                totalStudents = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the total number of students: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return totalStudents;
        }

        public static int GetTotalNumberOfTeachers()
        {
            int totalTeachers = 0;
            SqlConnection con = DBHelper.GetConnection();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM TeacherInfo", con);
                totalTeachers = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the total number of teachers: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return totalTeachers;
        }

        public static decimal GetTotalSubmittedFee()
        {
            decimal totalSubmittedFee = 0;
            SqlConnection con = DBHelper.GetConnection();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(submitedFee) FROM FeeInfo", con);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value) // Check if the result is not null
                {
                    totalSubmittedFee = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the total submitted fee: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return totalSubmittedFee;
        }

        public static decimal GetTotalTeacherSalaries()
        {
            decimal totalSalaries = 0;
            SqlConnection con = DBHelper.GetConnection();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(totalSalary) FROM TeacherSalary WHERE salaryStatus = 'Paid'", con); object result = cmd.ExecuteScalar();

                if (result != DBNull.Value) // Check if the result is not null
                {
                    totalSalaries = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the total teacher salaries: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return totalSalaries;
        }


    }
}
