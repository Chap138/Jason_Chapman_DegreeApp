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
        public CoursePage(int courseID)
        {
            InitializeComponent();
        }

        private void EditCourseSaveBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void EditCourseCancelBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}