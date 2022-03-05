using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jason_Chapman_MobileDev_C971
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentID { get; set; }
        public int CourseID { get; set; }
        public string AssessmentTitle { get; set; }
        public DateTime DueDate { get; set; }
        public string AssessmentType;
        private int notificationID = 1;
        public int NotificationID
        {
            get { return notificationID; }
            set { notificationID = value; }
        }

    }
}
