using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject_W2022.Models;
using MySql.Data.MySqlClient;

namespace SchoolProject_W2022.Controllers
{
    public class ClassDataController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Classes table of our School database.
        /// <summary>
        /// Returns a list of Classs in the system
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of Class objects (including class names, class code, class id, teacher name, startdate, and finishdate.)
        /// </returns>
        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public IEnumerable<Class> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT CONCAT(teacherfname, ' ', teacherlname) AS 'teachername', classes.* FROM classes JOIN teachers ON classes.teacherid = teachers.teacherid";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Class NewClass = new Class();
                NewClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClass.ClassName = ResultSet["classname"].ToString();
                NewClass.ClassCode = ResultSet["classcode"].ToString();
                NewClass.TeacherName = ResultSet["teachername"].ToString();
                NewClass.StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                NewClass.FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                Classes.Add(NewClass);
            }

            Conn.Close();

            return Classes;
        }

        /// <summary>
        /// Return the information list of the selected Class name
        /// </summary>
        /// <param name="classId">The selected class's Id number</param>
        /// <returns>A list of the selected Class information (including class names, class code, class id, teacher name, startdate, and finishdate.)</returns>
        /// <example>GET api/ClassData/getClass/1</example>
        [HttpGet]
        [Route("api/ClassData/getClass/{classId}")]
        public Class getClass(int classId)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT CONCAT(teacherfname, ' ', teacherlname) AS 'teachername', classes.* FROM classes JOIN teachers ON classes.teacherid = teachers.teacherid WHERE classId=" + classId;

            MySqlDataReader ResultSet = cmd.ExecuteReader();


            Class SelectedClass = new Class();

            ResultSet.Read();

            SelectedClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
            SelectedClass.ClassName = ResultSet["classname"].ToString();
            SelectedClass.ClassCode = ResultSet["classcode"].ToString();
            SelectedClass.TeacherName = ResultSet["teachername"].ToString();
            SelectedClass.StartDate = Convert.ToDateTime(ResultSet["startdate"]);
            SelectedClass.FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

            Conn.Close();

            // Return the information list of the selected Class
            return SelectedClass;
        }

    }
}
