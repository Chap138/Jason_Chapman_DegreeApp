using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jason_Chapman_MobileDev_C971
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseID { get; set; }
        public int TermID { get; set; }
        public string CourseTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Progress { get; set; }//In Progress, Completed, Dropped, Plan to Take 
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }

        //public Course(int termID, string title, DateTime start, DateTime end, string prog, string name, string phone, string email)
        //{
        //    TermID = termID;
        //    CourseTitle = title;
        //    StartDate = start;
        //    EndDate = end;
        //    Progress = prog;
        //    InstructorName = name;
        //    InstructorPhone = phone;
        //    InstructorEmail = email;
        //}
    }
}
