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
    public class StudentDataController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Students table of our School database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Student objects (including first names, last names, id, student number, and enroll date.)
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM students";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Student NewStudent = new Student();
                NewStudent.StudentId = Convert.ToInt32(ResultSet["Studentid"]);
                NewStudent.StudentFName = ResultSet["Studentfname"].ToString();
                NewStudent.StudentLName = ResultSet["Studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrollDate = Convert.ToDateTime(ResultSet["enroldate"]);

                Students.Add(NewStudent);
            }

            Conn.Close();

            return Students;
        }

        /// <summary>
        /// Return the information list of the selected Student name
        /// </summary>
        /// <param name="studentId">The selected Student's Id number</param>
        /// <returns>A list of the selected Student information (including first names, last names, id, student number, and enroll date.)</returns>
        /// <example>GET api/StudentData/getStudent/1</example>
        [HttpGet]
        [Route("api/StudentData/getStudent/{studentId}")]
        public Student getStudent(int studentId)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM Students where studentId=" + studentId;

            MySqlDataReader ResultSet = cmd.ExecuteReader();


            Student SelectedStudent = new Student();

            ResultSet.Read();

            SelectedStudent.StudentId = Convert.ToInt32(ResultSet["Studentid"]);
            SelectedStudent.StudentFName = ResultSet["Studentfname"].ToString();
            SelectedStudent.StudentLName = ResultSet["Studentlname"].ToString();
            SelectedStudent.StudentNumber = ResultSet["studentnumber"].ToString();
            SelectedStudent.EnrollDate = Convert.ToDateTime(ResultSet["enroldate"]);

            Conn.Close();

            // Return the information list of the selected Student
            return SelectedStudent;
        }

    }
}
