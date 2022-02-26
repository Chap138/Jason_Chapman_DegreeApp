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
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
