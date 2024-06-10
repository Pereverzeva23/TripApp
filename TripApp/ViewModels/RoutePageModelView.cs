using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TripApp.Models;
using TripApp.StaticClass;

namespace TripApp.ViewModels
{
    class RoutePageModelView : INotifyPropertyChanged
    {
        //const string apiUrl = @"http://172.18.50.92//api/Sights";

        public RoutePageModelView()
        {
            this.Items = new ObservableCollection<TripRoutesViewModel>();
        }

        public ObservableCollection<TripRoutesViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            if(this.IsDataLoaded == false)
            {
                this.Items.Clear();
                /*
                this.Items.Add(new TripRoutesViewModel()
                {
                    ID = 0,
                    Name = "PleaseWait...",
                    //Date = "Please wait while the catalog is downloaded from the server.",
                    //Website = null
                });
                */
                WebClient webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);
                webClient.DownloadStringAsync(new Uri(Params.Api + "TripRoutes"));
                //webClient.Headers
            }
        }

        private void webClient_DownloadCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.Items.Clear();
                if (e.Result != null)
                {
                    var trips = JsonConvert.DeserializeObject<TripRoutes[]>(e.Result);
                    foreach (TripRoutes trip in trips)
                    {
                        this.Items.Add(new TripRoutesViewModel()
                        {
                            ID = trip.id,
                            Name = trip.name,
                            Date = trip.date,
                            AddressFromId = trip.AddressFromId,
                            AddressToId = trip.AddressToId
                        });
                    }
                    this.IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                this.Items.Add(new TripRoutesViewModel()
                {
                    ID = 0,
                    Name = "An Error Occurred",
                    //Description = String.Format("The following exception occured: {0}", ex.Message),
                    //Website = String.Format("Additional inner exception information: {0}", ex.InnerException.Message)
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /*
        public string _routeName;
        public string _pointFrom;
        public string _pointTo;
        public DateTime _date;
        public string RouteName
        {
            get => _routeName;
            set
            {
                if (_routeName != value)
                {
                    _routeName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PointFrom
        {
            get => _pointFrom;
            set
            {
                if (_pointFrom != value)
                {
                    _pointFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PointTo
        {
            get => _pointTo;
            set
            {
                if (_pointTo != value)
                {
                    _pointTo = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}
