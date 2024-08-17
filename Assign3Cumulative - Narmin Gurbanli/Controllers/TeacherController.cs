using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Assign3Cumulative___Narmin_Gurbanli.Models;
using System.Diagnostics;

namespace Assign3Cumulative___Narmin_Gurbanli.Controllers
{
    public class TeacherController : Controller
    {
        // GET : Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teachers/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            //controller.ListTeachers();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }


        //GET : /Teachers/Show/{id}
        public ActionResult Show(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            //SelectedTeacher.TeacherFname = "Narmin";
            //SelectedTeacher.TeacherLname = "Gurbanli";
            return View(SelectedTeacher);
        }


        //GET : /Teachers/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Debug.WriteLine("Hi there");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;
            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);


            return RedirectToAction("List");
            
        }


        /// <summary>
        /// Routes to "Teacher Update" page and gathers information from database
        /// </summary>
        /// <param name="id">ID of a teacher</param>
        /// <returns>
        /// Un "Update Teacher" webpage which provides current information of the teacher and also asks the
        /// user for new information as part of the form
        /// </returns>
        /// <example> GET : /Teacher/Update/{id} </example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a POST request containing information about existing teacher in the database with new values. 
        /// Conveys this nformation to the API and redirects to "Teacher/Show" page of the updated teacher.
        /// </summary>
        /// <param name="id">id of the teacher to update</param>
        /// <param name="TeacherFname">the uodated first name of the teacher</param>
        /// <param name="TeacherLname">the updated last name of the teacher</param>
        /// <param name="EmployeeNumber">the updated employee number of the teacher</param>
        /// <param name="HireDate">the updated hire date of the teacher</param>
        /// <param name="Salary">the updated salary of the teacher</param>
        /// <returns>
        /// A web page that shows the current information of the teacher
        /// </returns>
        /// <example> POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        /// "TeacherFname":"Narmin" 
        /// "TeacherLname":"Gurbanli"
        /// "EmployeeNumber":"T123"
        /// "HireDate":"01/01/2023"
        /// "Salary":"100.00"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}