using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject_W2022.Models;

namespace SchoolProject_W2022.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        // GET : /Class/List
        // showing a page of all the Classes in the system
        public ActionResult List()
        {
            // connect tto our access layer
            ClassDataController controller = new ClassDataController();

            // get all the Classes
            IEnumerable<Class> Classes = controller.ListClasses();

            // pass the Classes into view/List.cshtml
            return View(Classes);
        }

        // GET : /Class/Show/{id}
        // [Route("Class/Show/{ClassId}")] // this will cause a non-nullable type 'System.Int32' error, since the pass in ClassId is a string, not a int
        public ActionResult Show(int id)
        {

            ClassDataController controller = new ClassDataController();

            Class SelectedClass = controller.getClass(id);

            // routes the single Class into show.cshtml
            return View(SelectedClass);
        }
    }
}