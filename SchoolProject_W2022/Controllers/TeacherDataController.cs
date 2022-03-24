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
    public class TeacherDataController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Teachers table of our School database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teacher objects (including first names, last names, id, employee number, hire date, and salary.)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM teachers";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                NewTeacher.Salary = Convert.ToDouble(ResultSet["salary"]);

                Teachers.Add(NewTeacher);
            }

            Conn.Close();

            return Teachers;
        }

        /// <summary>
        /// Return the information list of the selected Teacher
        /// </summary>
        /// <param name="teacherId">The selected teacher's Id number</param>
        /// <returns>A list of the selected Teacher information (including first names, last names, id, employee number, hire date, and salary.)</returns>
        /// <example>GET api/TeacherData/getTeacher/1</example>
        [HttpGet]
        [Route("api/TeacherData/getTeacher/{teacherId}")]
        public Teacher getTeacher(int teacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            // cmd.CommandText = "SELECT * FROM Teachers where teacherId=" + teacherId;
            // parameterize the queries to avoid sql injection attacks
            cmd.CommandText = "SELECT * FROM Teachers where teacherId = @id";
            cmd.Parameters.AddWithValue("@id", teacherId);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();


            Teacher SelectedTeacher = new Teacher();

            ResultSet.Read();

            SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
            SelectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
            SelectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
            SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
            SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
            SelectedTeacher.Salary = Convert.ToDouble(ResultSet["salary"]);

            Conn.Close();

            // Return the information list of the selected Teacher
            return SelectedTeacher;
        }

    }

}
