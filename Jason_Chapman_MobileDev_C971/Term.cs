using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jason_Chapman_MobileDev_C971
{
    [Table("Term")]
    public class Term
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }
        [MaxLength(30)]
        public string TermTitle { get; set; }
        public DateTime CreateDate { get; set; }

        //public string CurrentTerm { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        //public List<Course> Courses { get; set; }//on TermPage get the courses from the DB
    }
}
