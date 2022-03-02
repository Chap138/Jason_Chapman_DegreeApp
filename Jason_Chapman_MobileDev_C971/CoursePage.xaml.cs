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
            //alertID++;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                courseList = conn.Table<Course>().ToList();
            }

            for (int i = 0; i < courseList.Count; i++)
            {
                if (courseList[i].CourseID == CurrentCourseID)
                {
                    //alertID = courseList[i].NotificationID;
                    start = courseList[i].StartDate;
                    end = courseList[i].EndDate;
                    if (courseList[i].NotificationID > 0)
                    {
                        CrossLocalNotifications.Current.Cancel(courseList[i].NotificationID);
                    }
                    for (int j = 0; j < courseList.Count; j++)
                    {
                        if (courseList[j].NotificationID > 0)
                        {
                            alertID++;
                        }
                    }//for j
                    break;
                }
            }//for i

            foreach (Course row in courseList)
            {
                if (row.CourseID == CurrentCourseID)
                {
                    row.NotificationID = alertID;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        int rows = conn.Update(row);
                    }
                    break;
                }
            }//foreach

            DisplayAlert(alertID.ToString(), "Notification set!\n" + "Start date: " + start.ToString() + "\n" + "End date: " + end.ToString(), "OK");//DELETE THIS except for 'Notification set!'
            CrossLocalNotifications.Current.Show(" ", "Start date: " + start.ToString() + "\n" + "End date: " + end.ToString(), alertID, start);
            //CrossLocalNotifications.Current.Show("Alert!!!", "End date: + " + end.ToString(), alertID, end);
        }//end AlertTest_Clicked
    }
}