using TestApplication.ViewModels;
using Xamarin.Forms;

namespace TestApplication
{
    public partial class MainPage : ContentPage
    {
        #region Constructor
        public MainPage()
        {
            InitializeComponent();

            BindingContext = this.ViewModel = new WeatherViewModel();
            (BindingContext as WeatherViewModel).GetWeatherCommand.Execute(null);
            (BindingContext as WeatherViewModel).GetForecastCommand.Execute(null);
        }
        #endregion

        #region Public Properties
        public WeatherViewModel ViewModel { get; set; }
        #endregion
    }
}
