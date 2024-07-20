using Assign3Cumulative___Narmin_Gurbanli.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assign3Cumulative___Narmin_Gurbanli.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext school = new SchoolDbContext();
        //This controller accesses the teachers table from school database
        /// <summary>
        /// Returns list of teachers in the system
        /// </summary>
        /// <example>
        /// api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers, first name adn last name
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            MySqlConnection Conn = school.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            //I added search by salary and hire date
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower (@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower (@key) or lower (hiredate) like lower (@key) or lower (salary) like lower (@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> Teachers = new List<Teacher> {};
            while (ResultSet.Read())
                 {
                    
                    int TeacherId = (int)ResultSet["teacherid"];
                    string TeacherFname = ResultSet["teacherfname"].ToString();
                    string TeacherLname = ResultSet["teacherlname"].ToString();
                    string EmployeeNumber = ResultSet["employeenumber"].ToString();
                    DateTime HireDate = (DateTime)ResultSet["hiredate"];
                    string FormattedHireDate = HireDate.ToString("yyyy-MM-dd");
                    decimal Salary = ResultSet.GetDecimal("salary");
                    string FormattedSalary = Salary.ToString("00.00");






                Teacher NewTeacher = new Teacher();
                    NewTeacher.TeacherId = TeacherId;
                    NewTeacher.TeacherFname = TeacherFname;
                    NewTeacher.TeacherLname = TeacherLname;
                    NewTeacher.EmployeeNumber = EmployeeNumber;
                    NewTeacher.FormattedHireDate = FormattedHireDate;
                    NewTeacher.FormattedSalary = FormattedSalary;
                    


                    
                    Teachers.Add(NewTeacher);
                }
            Conn.Close();
            return Teachers;

        }
        [HttpGet]
        public Teacher FindTeacher(int id)
            //This shows each individual teacher
        {
            Teacher NewTeacher = new Teacher();
            MySqlConnection Conn = school.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Select * from teachers WHERE teacherid = "+id;
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            while (ResultSet.Read())
            {

                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                string FormattedHireDate = HireDate.ToString("yyyy-MM-dd");
                decimal Salary = ResultSet.GetDecimal("salary");
                string FormattedSalary = Salary.ToString("00.00");


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.FormattedHireDate = FormattedHireDate;
                NewTeacher.FormattedSalary = FormattedSalary;

            }


                return NewTeacher;
        }
    }
}
