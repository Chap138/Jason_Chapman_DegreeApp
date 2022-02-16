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
            }
        }

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