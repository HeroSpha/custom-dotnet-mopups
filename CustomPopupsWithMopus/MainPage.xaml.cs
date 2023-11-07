using CustomPopupsWithMopus.ViewModels;

namespace CustomPopupsWithMopus
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}