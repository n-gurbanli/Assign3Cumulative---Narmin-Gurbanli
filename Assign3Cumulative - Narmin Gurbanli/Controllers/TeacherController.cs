using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Assign3Cumulative___Narmin_Gurbanli.Models;

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
            Teacher NewTeacher = controller.FindTeacher(id);
            //NewTeacher.TeacherFname = "Narmin";
            //NewTeacher.TeacherLname = "Gurbanli";
            return View(NewTeacher);
        }
    }
}