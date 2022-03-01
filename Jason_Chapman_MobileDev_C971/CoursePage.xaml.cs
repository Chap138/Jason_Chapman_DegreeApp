using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jason_Chapman_MobileDev_C971
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {

        int alertID;
        int CurrentCourseID;
        List<Course> courseList;
        DateTime start;
        DateTime end;
        public CoursePage(int courseID)
        {
            InitializeComponent();
            CurrentCourseID = courseID;
        }

        private void EditCourseSaveBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void EditCourseCancelBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void AlertTest_Clicked(object sender, EventArgs e/*, DateTime start, DateTime end*/)
        {
            alertID++;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();
            }

            for(int i = 0; i < courseList.Count; i++)
            {
                if(courseList[i].CourseID == CurrentCourseID)
                {
                    start = courseList[i].StartDate;
                    end = courseList[i].EndDate;
                }
            }
            
            DisplayAlert(" ", "Notification set!", "OK");
            CrossLocalNotifications.Current.Show("Alert!!!", "This is the notification.", alertID, start);
            CrossLocalNotifications.Current.Show("Alert!!!", "This is the notification.", alertID, end);
        }//end AlertTest_Clicked
    }
}