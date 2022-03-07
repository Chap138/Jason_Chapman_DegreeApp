using SQLite;
using System;
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

            //TestDataForEval();
        }

        private void TestDataForEval()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DeleteAll<Assessment>();
                conn.DeleteAll<Course>();
                conn.DeleteAll<Term>();

                conn.CreateTable<Term>();
                conn.CreateTable<Course>();
                conn.CreateTable<Assessment>();

                Term termTest = new Term()
                {
                    TermTitle = "Term 1",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddMonths(6)
                };
                conn.Insert(termTest);

                Course courseTest = new Course()
                {
                    TermID = termTest.ID,
                    CourseTitle = "Mobile Development",
                    InstructorName = "Jason Chapman",
                    InstructorPhone = "253-736-4078",
                    InstructorEmail = "jchap71@wgu.edu",
                    CourseStatus = "In Progress",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(2),
                    CourseNotes = "Mobile development is fun :D"
                };
                conn.Insert(courseTest);

                Assessment assmtTest1 = new Assessment()
                {
                    CourseID = courseTest.CourseID,
                    AssessmentTitle = "PA 1",
                    AssessmentType = "Performance Assessment",
                    DueDate = DateTime.Now.AddMonths(1)
                };
                conn.Insert(assmtTest1);

                Assessment assmtTest2 = new Assessment()
                {
                    CourseID = courseTest.CourseID,
                    AssessmentTitle = "OA 1",
                    AssessmentType = "Objective Assessment",
                    DueDate = DateTime.Now.AddMonths(2)
                };
                conn.Insert(assmtTest2);

            }
        }//end TestDataForEval

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