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
        public string CourseStatus { get; set; }//In Progress, Completed, Dropped, Plan to Take 
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        [MaxLength(250)]
        public string CourseNotes { get; set; }
        private int notificationID = 1;
        public int NotificationID
        {
            get { return notificationID; }
            set { notificationID = value; }
        }

    }
}
