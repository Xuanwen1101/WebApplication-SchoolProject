using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject_W2022.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;

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
        [EnableCors(origins: "*", methods: "*", headers: "*")]
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
        [EnableCors(origins: "*", methods: "*", headers: "*")]
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


        /// <summary>
        /// Add a new Teacher into the system with the given information.
        /// </summary>
        /// <param name="NewTeacher"> The Teacher information that we will add to the DB</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	    "TeacherFName":"Test",
        ///	    "TeacherLName":"Teacher",
        ///	    "EmployeeNumber":"T366",
        ///	    "Salary":"52.11"
        /// }
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/addTeacher")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void addTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFName);
            Debug.WriteLine(NewTeacher.SalaryString);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@EmployeeNumber, CURRENT_DATE(), @TeacherSalary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherSalary", Convert.ToDouble(NewTeacher.SalaryString));
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Delete the selected Teacher and the classes pointing to this teacher from the system.
        /// </summary>
        /// <param name="id">The primary key: TeacherId</param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        [Route("api/TeacherData/deleteTeacher/{id}")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void deleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for deleting the classes pointing to the selected Teacher from the classes table.
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from classes where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Establish a new command (query) for deleting the selected Teacher from the teachers table.
            MySqlCommand cmd_2 = Conn.CreateCommand();

            //SQL QUERY
            cmd_2.CommandText = "Delete from teachers where teacherid=@id";
            cmd_2.Parameters.AddWithValue("@id", id);
            cmd_2.Prepare();

            cmd_2.ExecuteNonQuery();

            Conn.Close();

        }


        /// <summary>
        /// Updates an Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="id">The primary key: TeacherId</param>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the Teacher's table.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/22 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	    "TeacherFName":"Test",
        ///	    "TeacherLName":"Teacher",
        ///	    "EmployeeNumber":"T366",
        ///	    "Salary":"52.11"
        /// }
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/updateTeacher/{id}")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void updateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(TeacherInfo.TeacherFName);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, salary=@TeacherSalary where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherSalary", Convert.ToDouble(TeacherInfo.SalaryString));
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }


    }

}
