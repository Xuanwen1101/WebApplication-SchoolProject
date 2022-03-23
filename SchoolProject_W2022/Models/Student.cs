using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject_W2022.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public string StudentNumber { get; set; }
        public DateTime EnrollDate { get; set; }

    }
}