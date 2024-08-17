using Assign3Cumulative___Narmin_Gurbanli.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    decimal Salary = (decimal)ResultSet["salary"];







                Teacher NewTeacher = new Teacher();
                    NewTeacher.TeacherId = TeacherId;
                    NewTeacher.TeacherFname = TeacherFname;
                    NewTeacher.TeacherLname = TeacherLname;
                    NewTeacher.EmployeeNumber = EmployeeNumber;
                    NewTeacher.HireDate = HireDate;
                    NewTeacher.Salary = Salary;
                    


                    
                    Teachers.Add(NewTeacher);
                }
            Conn.Close();
            return Teachers;

        }

        /// <summary>
        /// Retrieves a teacher's details from the database based on the provided teacher ID.
        /// </summary>
        /// <param name="id">The id of the teacher being retrived</param>
        /// <returns>
        /// Return a web page containing the details of the teacher retrived.
        /// </returns>
        /// <example> GET: /api/TeacherData/FindTeacher/3 </example>

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
                decimal Salary = (decimal)ResultSet["salary"];



                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

            }


                return NewTeacher;
        }

        /// <summary>
        /// Deletes a teacher record from the database based on the provided teacher ID.
        /// </summary>
        /// <param name="id">The id of the teacher being deleted</param>
        /// <return> This method does not return a value. </return>
        /// <example>POST : /api/TeacherData/DeleteTeacher/3</example>

        [HttpPost]
        
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = school.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Delete from Teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();
            //dont need to return anything, as it is a void method
        }

        /// <summary>
        /// Adds a new teacher record to the database.
        /// </summary>
        /// <param name="NewTeacher">Object that contains the details of the new teacher to be added</param>
        /// <example>POST: /api/TeacherData/AddTeacher</example>

        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            if (!NewTeacher.IsValid()) return;

            MySqlConnection Conn = school.AccessDatabase();
            Debug.WriteLine(NewTeacher.TeacherFname);
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherId,@TeacherFname,@TeacherLname,@EmployeeNumber,@HireDate,@Salary)";
            cmd.Parameters.AddWithValue("@TeacherId", NewTeacher.TeacherId);
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();

        }

        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            MySqlConnection Conn = school.AccessDatabase();
            Debug.WriteLine(TeacherInfo.TeacherFname);
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@HireDate, salary=@Salary where teacherid=@TeacherID";
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
