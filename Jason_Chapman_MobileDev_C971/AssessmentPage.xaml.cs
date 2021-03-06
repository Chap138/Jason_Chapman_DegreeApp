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
    public partial class AssessmentPage : ContentPage
    {
        int alertID;
        private int CurrentAssmtID;
        List<Assessment> assmtList;
        List<Course> courseList;
        private string CurrentAssmtTitle;
        private string CurrentAssmtType;
        private DateTime CurrentAssmtDueDate;
        private DateTime dueDate;

        public AssessmentPage(int assmtID)
        {
            InitializeComponent();
            CurrentAssmtID = assmtID;
            //DropAddAssmtTable();
        }
        protected override void OnAppearing()
        {
            GetAssessment();
            //DeleteButtons();
            //AddAssmtFromDB();
        }//end OnAppearing
        public void GetAssessment()//Update assessment info when page appears
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                assmtList = conn.Table<Assessment>().ToList();
            }

            foreach (Assessment row in assmtList)
            {
                if (row.AssessmentID == CurrentAssmtID)
                {
                    CurrentAssmtTitle = row.AssessmentTitle;
                    CurrentAssmtType = row.AssessmentType;
                    CurrentAssmtDueDate = row.DueDate;
                }
            }
            AssmtLabel.Text = CurrentAssmtTitle;
            AssmtTypeLabel.Text = CurrentAssmtType;
            AssmtDueDatePicker.Date = CurrentAssmtDueDate;
        }//end GetAssessment()

        private void EditAssessment_Clicked(object sender, EventArgs e)
        {
            EditAssmtTitleEntry.IsVisible = true;
            EditAssmtTitleEntry.Focus();
            EditAssmtTypePicker.IsVisible = true;
            EditAssmtSaveBtn.IsVisible = true;
            EditAssmtCancelBtn.IsVisible = true;
        }//end EditAssessment_Clicked

        private void EditAssmtSaveBtn_Clicked(object sender, EventArgs e)
        {
            bool pa = false;
            bool oa = false;
            bool containsSpec = EditAssmtTitleEntry.Text.Any(char.IsPunctuation) || EditAssmtTitleEntry.Text.Any(char.IsSymbol);//Check for special characters

            foreach (Assessment row in assmtList)
            {
                if (row.AssessmentID == CurrentAssmtID)
                {
                    if (EditAssmtTitleEntry.Text == null)
                    {
                        row.AssessmentTitle = CurrentAssmtTitle;
                    }
                    else if (containsSpec)
                    {
                        DisplayAlert(" ", "Please use letters and numbers only.", "OK");
                    }
                    else
                    {
                        row.AssessmentTitle = EditAssmtTitleEntry.Text;
                        AssmtLabel.Text = EditAssmtTitleEntry.Text;
                    }

                    if (EditAssmtTypePicker.SelectedItem == null)
                    {
                        row.AssessmentType = CurrentAssmtType;
                    }
                    else if (row.AssessmentType == EditAssmtTypePicker.SelectedItem.ToString())
                    {
                        row.AssessmentType = EditAssmtTypePicker.SelectedItem.ToString();
                        AssmtTypeLabel.Text = EditAssmtTypePicker.SelectedItem.ToString();
                    }
                    else if (row.AssessmentType != EditAssmtTypePicker.SelectedItem.ToString())
                    {
                        if (row.AssessmentType == "Performance Assessment")
                        {
                            pa = true;
                        }
                        else if (row.AssessmentType == "Objective Assessment")
                        {
                            oa = true;
                        }
                        if (pa || oa)
                        {
                            DisplayAlert(" ", "Only one Performance Assessment and one Objective Assessment allowed per course.", "OK");
                        }
                    }


                    row.DueDate = AssmtDueDatePicker.Date;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        int rows = conn.Update(row);
                    }
                    break;
                }
            }
            EditAssmtTitleEntry.IsVisible = false;
            EditAssmtTypePicker.IsVisible = false;
            EditAssmtSaveBtn.IsVisible = false;
            EditAssmtCancelBtn.IsVisible = false;
        }//end EditAssmtSaveBtn_Clicked

        private void EditAssmtCancelBtn_Clicked(object sender, EventArgs e)
        {
            EditAssmtTitleEntry.IsVisible = false;
            EditAssmtTypePicker.IsVisible = false;
            EditAssmtSaveBtn.IsVisible = false;
            EditAssmtCancelBtn.IsVisible = false;
        }//end EditAssmtCancelBtn_Clicked

        private void DeleteButtons()//Delete buttons to replace refreshed
        {
            for (int i = 20; i < layout.Children.Count;)
            {
                layout.Children.RemoveAt(i);
            }
        }//end DeleteButtons

        private void SetDueDateAlertButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                assmtList = conn.Table<Assessment>().ToList();
            }
            for (int i = 0; i < assmtList.Count; i++)
            {
                if (assmtList[i].AssessmentID == CurrentAssmtID)
                {
                    dueDate = assmtList[i].DueDate;
                    if (assmtList[i].NotificationID > 0)
                    {
                        CrossLocalNotifications.Current.Cancel(assmtList[i].NotificationID);
                    }
                    for (int j = 0; j < assmtList.Count; j++)
                    {
                        if (assmtList[i].NotificationID > 0)
                        {
                            alertID++;
                        }
                    }//for j
                    break;
                }
            }//for i

            foreach (Assessment row in assmtList)
            {
                if (row.AssessmentID == CurrentAssmtID)
                {
                    row.NotificationID = alertID;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        int rows = conn.Update(row);
                    }
                    break;
                }
            }//foreach

            DisplayAlert(" ", "Notification set!", "OK");
            CrossLocalNotifications.Current.Show(" ", "Due date: " + dueDate.ToString() + "\n", alertID, dueDate);
        }//end SetDueDateAlertButton_Clicked

        private async void DeleteAssmt_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Alert!", "Are you sure you want to delete this assessment?", "Yes", "No");
            int currentCourseID;

            if (answer)
            {
                foreach (Assessment row in assmtList)
                {
                    if (row.AssessmentID == CurrentAssmtID)
                    {
                        currentCourseID = row.CourseID;
                        using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                        {
                            conn.Delete(row);
                        }
                        await DisplayAlert(" ", "Assessment deleted.", "OK");
                        await Navigation.PopAsync();//Goes back to previous page

                        break;
                    }
                }
            }
            else return;
        }//end DeleteAssmt_Clicked

    }
}

