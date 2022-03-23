using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject_W2022.Models;

namespace SchoolProject_W2022.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET : /Teacher/List
        // showing a page of all the Teachers in the system
        public ActionResult List()
        {
            // connect tto our access layer
            TeacherDataController controller = new TeacherDataController();

            // get all the Teachers
            IEnumerable<Teacher> Teachers = controller.ListTeachers();

            // pass the Teachers into view/List.cshtml
            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}
        // [Route("Teacher/Show/{TeacherId}")] // this will cause a non-nullable type 'System.Int32' error, since the pass in TeacherId is a string, not a int
        public ActionResult Show(int id)
        {

            TeacherDataController controller = new TeacherDataController();

            Teacher SelectedTeacher = controller.getTeacher(id);

            // routes the single Teacher into show.cshtml
            return View(SelectedTeacher);
        }

    }
}