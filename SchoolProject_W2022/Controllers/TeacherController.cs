using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, string TeacherSalary)
        {

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherSalary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFname;
            NewTeacher.TeacherLName = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            /*NewTeacher.Salary = Convert.ToDouble(TeacherSalary);*/
            NewTeacher.SalaryString = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.addTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.getTeacher(id);


            return View(SelectedTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.deleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }

    }
}