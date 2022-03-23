using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject_W2022.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public string TeacherName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

    }
}