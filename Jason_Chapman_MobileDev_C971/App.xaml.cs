using SQLite;
using Xamarin.Forms;

namespace Jason_Chapman_MobileDev_C971
{
    public partial class App : Application
    {
        public static string FilePath;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
        public App(string filePath)
        {
            InitializeComponent();
            MainPage = new AppShell();

            FilePath = filePath;

            //For testing
            Term termTest = new Term()
            {
                TermTitle = "Term 1",
                CreateDate = System.DateTime.Now
            };
            Course courseTest = new Course()
            {
                TermID = 1,
                CourseTitle = "Course",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Today,
                Progress = "Completed",
                InstructorName = "Mr.Mackey",
                InstructorPhone = "555-5559",
                InstructorEmail = "mackey@gmail.com"

            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                conn.Insert(termTest);
                //int termAddedTest = conn.Insert(termTest);

                conn.CreateTable<Course>();
                conn.Insert(courseTest);
                //int crsAddedTest = conn.Insert(courseTest);
                //conn.Insert()

                //Course coursePrepop; {
                //coursePrepop(1, "Math 101", DateTime.Today, DateTime.Today, "In Progress", "Mr. Mackey",  "555-3508", "mackey@hotmail.com" ),
                //new Course(1, "English 201", DateTime.Today, DateTime.Today, "Completed", "Mrs. Streibel", "555-0241", "streibel@hotmail.com" ),
                //new Course(1, "History", DateTime.Today, DateTime.Today, "Plan to take", "Mr. Adler", "555-8824", "adler@hotmail.com" ),
                //new Course(1, "Sociology", DateTime.Today, DateTime.Today, "Completed", "Mr. Derp", "555-7600", "derp@hotmail.com" ),
                //new Course(1, "Basketweaving", DateTime.Today, DateTime.Today, "Dropped", "Ms. Bronski", "555-2855", "bronski@hotmail.com" ),
                //new Course(1, "Astronomy", DateTime.Today, DateTime.Today, "In Progress", "Mr. Garrison", "555-7637", "garrison@hotmail.com"
                //};
            }//end conn


        }//end overloaded Constructor

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}