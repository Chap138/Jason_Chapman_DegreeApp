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
        [MaxLength(250)]
        public string AssessmentNotes { get; set; }
        private int notitificationID = 1;
        public int NotificationID
        {
            get { return notitificationID; }
            set { notitificationID = value; }
        }

    }
}
