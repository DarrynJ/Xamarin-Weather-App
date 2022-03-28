using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApplication.Models;
using TestApplication.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestApplication.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private WeatherService _weatherService;
        private WeatherResponse _weatherResponse;
        private List<WeatherResponse> _forecast;
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Properties
        public WeatherResponse Weather
        {
            get { return _weatherResponse; }
            private set
            {
                if (_weatherResponse == value)
                    return;

                _weatherResponse = value;
                OnPropertyChanged("Weather");
            }
        }
        public List<Weather> Weathers { get => Models.Weather.GetDummyData(); }
        public List<WeatherResponse> Forecast
        {
            get { return _forecast; }
            set
            {
                if (_forecast == value)
                    return;

                _forecast = value;
                OnPropertyChanged("Forecast");
            }
        }
        public ICommand GetWeatherCommand { get; }
        public ICommand GetForecastCommand { get; }
        #endregion

        #region Constructor
        public WeatherViewModel()
        {
            _weatherService = new WeatherService();
            GetWeatherCommand = new Command(async () => await DoGetWeatherCommand());
            GetForecastCommand = new Command(async () => await DoGetWeatherForecastCommand());
        }
        #endregion

        #region Public Methods
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Private Methods
        private async Task DoGetWeatherCommand()
        {
            var location = await this.GetCurrentLocationAsync();

            Weather = await _weatherService.GetLatestWeatherByPosition(location.Latitude, location.Longitude);
        }

        private async Task DoGetWeatherForecastCommand()
        {
            var location = await this.GetCurrentLocationAsync();

            Forecast = await _weatherService.GetLatestWeatherForecastByPosition(location.Latitude, location.Longitude, DateTime.Today, DateTime.Today.AddDays(7));
        }

        private async Task<Location> GetCurrentLocationAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(20));

            CancellationToken token = new CancellationToken();

            var location = await Geolocation.GetLocationAsync(request, token);
            if (location == null)
            {
                throw new Exception("Unable to retrieve location.");
            }

            return location;
        }
        #endregion
    }
}
