using SQLite;
using Xamarin.Forms;

namespace Jason_Chapman_MobileDev_C971
{
    public partial class App : Application
    {
        public static string FilePath;
        private int testCount;

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

            testCount = 22;
            Course courseTest = new Course()
            {
                TermID = 1,
                CourseTitle = "CourseTes" + " " + testCount,
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Today,
                Progress = "Compelted",
                InstructorName = "Mr.Mackey",
                InstructorPhone = "555-5559",
                InstructorEmail = "mackey@gmail.com"

            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Course>();
                //int addedTest = conn.Insert(courseTest);
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