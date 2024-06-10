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
    class MainPageModelView : INotifyPropertyChanged
    {
        //const string apiUrl = @"http://172.18.50.92//api/Sights";

        public MainPageModelView()
        {
            this.Items = new ObservableCollection<TripRoutesViewModel>();

            this.buildings = new ObservableCollection<BuildingsViewModel>();
            this.addresses = new ObservableCollection<AddressesViewModel>();
            this.addressBuildings = new ObservableCollection<AddressBuildingsViewModel>();
        }

        private int isBuildings = 0;
        private int isAddresses = 0;
        private int isAddressBuildings = 0;

        public ObservableCollection<TripRoutesViewModel> Items { get; private set; }

        public ObservableCollection<BuildingsViewModel> buildings { get; private set; }

        public ObservableCollection<AddressesViewModel> addresses { get; private set; }

        public ObservableCollection<AddressBuildingsViewModel> addressBuildings { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public async void LoadPickerData()
        {
            //Видимо ошибка при ассинхонном выполнении

            GetAddressesCatalog();
            GetBuildingsCatalog();
            GetAddressBuildingsCatalog();
            while (isBuildings == 0 || isAddressBuildings == 0 || isAddresses == 0)
            {

            }
            string a = "";
        }

        

        public async void GetAddressesCatalog()
        { 
            //Берем данные с Web Api
            this.addresses.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadAddressesCatalogCompleted);
            string result = webClient.DownloadString(new Uri(Params.Api + "Addresses"));

            try
            {
                this.addresses.Clear();
                if (result != null)
                {
                    var addressList = JsonConvert.DeserializeObject<Addresses[]>(result);
                    foreach (Addresses address in addressList)
                    {
                        this.addresses.Add(new AddressesViewModel()
                        {
                            ID = address.id,
                            Name = address.name
                        });
                    }
                }
                isAddresses = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных адреса/проверьте соединение с сервером", "принять");
                isAddresses = 2;
            }
        }

        public async void GetBuildingsCatalog()
        {
            //Берем данные с Web Api
            this.buildings.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadBuildingsCatalogCompleted);
            string result = webClient.DownloadString(new Uri(Params.Api + "Buildings"));

            try
            {
                this.buildings.Clear();
                if (result != null)
                {
                    var buildingList = JsonConvert.DeserializeObject<Buildings[]>(result);
                    foreach (Buildings building in buildingList)
                    {
                        this.buildings.Add(new BuildingsViewModel()
                        {
                            ID = building.id,
                            Number = building.number
                        });
                    }
                }
                isBuildings = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных здания/проверьте соединение с сервером", "принять");
                isBuildings = 2;
            }
        }

        public async void GetAddressBuildingsCatalog()
        {
            //Берем данные с Web Api
            this.addressBuildings.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
           // webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadAddressBuildingsCatalogCompleted);
            string result = webClient.DownloadString(new Uri(Params.Api + "AddressBuildings"));

            try
            {
                this.addressBuildings.Clear();
                if (result != null)
                {
                    var buildingList = JsonConvert.DeserializeObject<AddressBuildings[]>(result);
                    foreach (AddressBuildings address_building in buildingList)
                    {
                        this.addressBuildings.Add(new AddressBuildingsViewModel()
                        {
                            ID = address_building.id,
                            X_coordinate = address_building.x,
                            Y_coordinate = address_building.y,
                            AddressId = address_building.AddressId,
                            BuildingId = address_building.BuildingId
                        });
                    }
                }
                isAddressBuildings = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных/проверьте соединение с сервером", "принять");
                isAddressBuildings = 2;
            }
        }


        public async void LoadData(string id)
        {
            //Берем данные с Web Api
            this.Items.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            
            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);
            
            //webClient.DownloadStringAsync(new Uri(Params.Api + "TripRoutes/" + id));
            string result = webClient.DownloadString(new Uri(Params.Api + "TripRoutes/" + id));


            try
            {
                this.Items.Clear();
                if (result != null)
                {
                    var trips = JsonConvert.DeserializeObject<TripRoutes>(result);

                    this.Items.Add(new TripRoutesViewModel()
                    {
                        ID = trips.id,
                        Name = trips.name,
                        Date = trips.date,
                        AddressFromId = trips.AddressFromId,
                        AddressToId = trips.AddressToId
                    });

                    this.IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                /*
                this.Items.Add(new TripRoutesViewModel()
                {
                    ID = 0,
                    Name = "An Error Occurred",
                    //Description = String.Format("The following exception occured: {0}", ex.Message),
                    //Website = String.Format("Additional inner exception information: {0}", ex.InnerException.Message)
                });
                */
                this.IsDataLoaded = true;
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных/проверьте соединение с сервером", "принять");
            }


            /*
            if(Items == null) 
            {
                TripRoutesViewModel viewModel = new TripRoutesViewModel();
                Items.Add(viewModel);
            }
            else
            {
                //this.Items.
            }
            */
            /*
            if (this.IsDataLoaded == false)
            {
                this.Items.Clear();
                this.Items.Add(new TripRoutesViewModel()
                {
                    ID = 0,
                    Name = "PleaseWait...",
                    //Date = "Please wait while the catalog is downloaded from the server.",
                    //Website = null
                });
                WebClient webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);
                webClient.DownloadStringAsync(new Uri(Params.Api + "TripRoutes"));
                //webClient.Headers
            }
            */
        }

        private async void webClient_DownloadCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.Items.Clear();
                if (e.Result != null)
                {
                    var trips = JsonConvert.DeserializeObject<TripRoutes>(e.Result);
                    
                        this.Items.Add(new TripRoutesViewModel()
                        {
                            ID = trips.id,
                            Name = trips.name,
                            Date = trips.date,
                            AddressFromId = trips.AddressFromId,
                            AddressToId = trips.AddressToId
                        });
                    
                    this.IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                /*
                this.Items.Add(new TripRoutesViewModel()
                {
                    ID = 0,
                    Name = "An Error Occurred",
                    //Description = String.Format("The following exception occured: {0}", ex.Message),
                    //Website = String.Format("Additional inner exception information: {0}", ex.InnerException.Message)
                });
                */
                this.IsDataLoaded = true;
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных/проверьте соединение с сервером", "принять");
            }
        }

        private async void webClient_DownloadAddressesCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.addresses.Clear();
                if (e.Result != null)
                {
                    var addressList = JsonConvert.DeserializeObject<Addresses[]>(e.Result);
                    foreach (Addresses address in addressList)
                    {
                        this.addresses.Add(new AddressesViewModel()
                        {
                            ID = address.id,
                            Name = address.name
                        });
                    }
                }
                isAddresses = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных адреса/проверьте соединение с сервером", "принять");
                isAddresses = 2;
            }
        }

        private async void webClient_DownloadBuildingsCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.addressBuildings.Clear();
                if (e.Result != null)
                {
                    var addressBuildingList = JsonConvert.DeserializeObject<AddressBuildings[]>(e.Result);
                    foreach (AddressBuildings addressbuilding in addressBuildingList)
                    {
                        this.addressBuildings.Add(new AddressBuildingsViewModel()
                        {
                            ID = addressbuilding.id,
                            X_coordinate = addressbuilding.x,
                            Y_coordinate = addressbuilding.y,
                            AddressId = addressbuilding.id,
                            BuildingId = addressbuilding.id
                        });
                    }
                }
                isBuildings = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных здания/проверьте соединение с сервером", "принять");
                isBuildings = 2;
            }
        }

        private async void webClient_DownloadAddressBuildingsCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.addressBuildings.Clear();
                if (e.Result != null)
                {
                    var buildingList = JsonConvert.DeserializeObject<Buildings[]>(e.Result);
                    foreach (Buildings building in buildingList)
                    {
                        this.addressBuildings.Add(new AddressBuildingsViewModel()
                        {
                            ID = building.id
                            //Number = building.number
                        });
                    }
                }
                isAddressBuildings = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных/проверьте соединение с сервером", "принять");
                isAddressBuildings = 2;
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

    }
}
