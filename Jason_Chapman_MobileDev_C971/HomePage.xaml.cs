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
    public partial class HomePage : ContentPage
    {

        //StackLayout parent = new StackLayout();
        public HomePage()
        {
            InitializeComponent();
        }

        private void AddTermBtn_Clicked(object sender, EventArgs e)
        {
            AddTermEntry.IsVisible = true;
            AddTermEntry.Focus();
            AddTermSaveBtn.IsVisible = true;
        }//end AddTermBtn_Clicked

        private void AddTermSaveBtn_Clicked(object sender, EventArgs e)
        {
            AddTermEntry.IsVisible = false;
            AddTermSaveBtn.IsVisible = false;

            Term termTest = new Term()
            {
                TermTitle = AddTermEntry.Text,
                CreateDate = System.DateTime.Now
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                conn.Insert(termTest);
            }

            Button newTab = new Button()
            {
                Text = AddTermEntry.Text,
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                Margin = 30,
                BackgroundColor = Color.White
            };

            // Creating a binding
            //newTab.SetBinding(FlyoutItem.TitleProperty, new Binding("ViewModelProperty"));
            //newTab.SetBinding(FlyoutItem.TitleProperty, "Title");


            // Set the binding context after SetBinding method calls for performance reasons
            //newTab.BindingContext = new { Title = AddTermEntry.Text };

            // Set StackLayout in XAML to the class field
            // parent = layout;

            // Add the new button to the StackLayout
            Layout.Children.Add(newTab);

        }//end AddTermSaveBtn_Clicked

        //public void Addbutton(object sender, EventArgs e)
        //{
        //    // Define a new button
        //    Button newButton = new Button { Text = "New Button" };

        //    // Creating a binding
        //    newButton.SetBinding(Button.CommandProperty, new Binding("ViewModelProperty"));

        //    // Set the binding context after SetBinding method calls for performance reasons
        //    newButton.BindingContext = viewModel;

        //    // Set StackLayout in XAML to the class field
        //    parent = layout;

        //    // Add the new button to the StackLayout
        //    parent.Children.Add(newButton);
        //}
    }
}