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