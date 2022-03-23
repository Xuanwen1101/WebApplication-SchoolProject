using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject_W2022.Models;

namespace SchoolProject_W2022.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET : /Student/List
        // showing a page of all the Students in the system
        public ActionResult List()
        {
            // connect tto our access layer
            StudentDataController controller = new StudentDataController();

            // get all the Students
            IEnumerable<Student> Students = controller.ListStudents();

            // pass the Students into view/List.cshtml
            return View(Students);
        }

        // GET : /Student/Show/{id}
        // [Route("Student/Show/{StudentId}")] // this will cause a non-nullable type 'System.Int32' error, since the pass in StudentId is a string, not a int
        public ActionResult Show(int id)
        {

            StudentDataController controller = new StudentDataController();

            Student SelectedStudent = controller.getStudent(id);

            // routes the single Student into show.cshtml
            return View(SelectedStudent);
        }
    }
}