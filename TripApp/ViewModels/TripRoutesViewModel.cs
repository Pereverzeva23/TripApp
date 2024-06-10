using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using TripApp.Models;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using TripApp.StaticClass;
using Newtonsoft.Json;
using System.Net;

namespace TripApp.ViewModels
{
    public class TripRoutesViewModel :INotifyPropertyChanged
    {
        //const string apiUrl = @"http://172.18.50.92//api/TripRoutes";

        HttpClient client;
        JsonSerializerOptions serializerOptions;

        int id;
        string name;
        DateTime date;
        int addressToId; //AddressBuilding
        int addressFromId; //AddressBuilding

        private bool isDelete;
        private int isItemsTripRoutes = 0;

        private int isBuildings = 0;
        private int isAddresses = 0;
        private int isAddressBuildings = 0;

        //AddressBuildings addressBuildingsRouteToId;
        //AddressBuildings addressBuildingsRouteFromId;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<TripRoutes> tripRoutes { get; } = new();
        public ObservableCollection<AddressBuildings> addressBuildings { get; } = new();
        public ObservableCollection<Addresses> addresses { get; } = new();
        public ObservableCollection<Buildings> buildings { get; } = new();

        private ObservableCollection<TripRoutesViewModel> ItemsTripRoutes { get; set; }


        public ObservableCollection<BuildingsViewModel> getBuildings { get; private set; }
        public ObservableCollection<AddressesViewModel> getAddresses { get; private set; }
        public ObservableCollection<AddressBuildingsViewModel> getAddressBuildings { get; private set; }


        public TripRoutesViewModel()
        {
            //this.tripRoutes = new ObservableCollection<TripRoutesViewModel>();

            // устанавливаем команду добавления
            AddCommand = new Command(() =>
            {
                tripRoutes.Add(new TripRoutes(id, name, date, addressToId, addressFromId));
            });

            // устанавливаем команду удаления
            RemoveCommand = new Command((args) =>
            {
                if (args is TripRoutes trip) tripRoutes.Remove(trip);
            });

        }

        public bool IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }

        public int ID
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AddressToId
        {
            get => addressToId;
            set
            {
                if (addressToId != value)
                {
                    addressToId = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AddressFromId
        {
            get => addressFromId;
            set
            {
                if (addressFromId != value)
                {
                    addressFromId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NameAddressTo
        {
            get
            {
                GetAddressBuildingsCatalog(addressToId.ToString());
                GetAddressesCatalog(getAddressBuildings[0].AddressId.ToString());
                GetBuildingsCatalog(getAddressBuildings[0].BuildingId.ToString());
                string nameTo = getAddresses[0].Name.ToString() + " " + getBuildings[0].Number.ToString();
                return nameTo;
            }
        }

        public string NameAddressFrom
        {
            get
            {
                GetAddressBuildingsCatalog(addressFromId.ToString());
                GetAddressesCatalog(getAddressBuildings[0].AddressId.ToString());
                GetBuildingsCatalog(getAddressBuildings[0].BuildingId.ToString());
                string nameFrom = getAddresses[0].Name.ToString() + " " + getBuildings[0].Number.ToString();
                return nameFrom;
            }
        }

        public async void GetTripRoutes()
        {
            //Берем данные с Web Api
            this.ItemsTripRoutes = new ObservableCollection<TripRoutesViewModel>();
            this.ItemsTripRoutes.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadAddressesCatalogCompleted);
            string result = webClient.DownloadString(new Uri(Params.Api + "TripRoutes"));

            try
            {
                this.ItemsTripRoutes.Clear();
                if (result != null)
                {
                    var tripAddressList = JsonConvert.DeserializeObject<TripRoutes[]>(result);
                    foreach (TripRoutes tripAddress in tripAddressList)
                    {
                        this.ItemsTripRoutes.Add(new TripRoutesViewModel()
                        {
                            ID = tripAddress.id,
                            Name = tripAddress.name,
                            Date = tripAddress.date,
                            AddressFromId = tripAddress.AddressFromId,
                            AddressToId = tripAddress.AddressToId
                        });
                    }
                }
                isItemsTripRoutes = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных адреса/проверьте соединение с сервером", "принять");
                isItemsTripRoutes = 2;
            }
        }

        // Добавление маршрута
        public async Task AddTripRoute()
        {
            TripRoutes trip = new TripRoutes();
            trip.date = date;
            trip.name = name;
            trip.AddressFromId = addressFromId;
            trip.AddressToId = addressToId;

            //Получить последний ID
            GetTripRoutes();
            if (ItemsTripRoutes.Count > 0)
                trip.id = ItemsTripRoutes[ItemsTripRoutes.Count - 1].id + 1;
            else
                trip.id = 1;



            //tripRoutes.Add(trip);

            Uri uri = new Uri(string.Format(Params.Api+"TripRoutes", string.Empty));

            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //WriteIndented = true
            };

            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize<TripRoutes>(trip, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                //else
                   // response = await client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    //Всплывающее сообщение
                    Debug.WriteLine(@"\tSuccessfully saved");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tError{0}", ex.Message);
            }
        }

        // Обновление маршрута
        public async Task UpdateTripRoute(TripRoutesViewModel trip)
        {
            TripRoutes trips = new TripRoutes();
            //var d = new DateTime(date.Year, date.Month, date.Day);
            trips.date = date;
            trips.name = name;
            trips.AddressFromId = addressFromId;
            trips.AddressToId = addressToId;
            trips.id = id;

            //tripRoutes.Add(trip);

            Uri uri = new Uri(string.Format(Params.Api + "TripRoutes/" + trips.id, string.Empty));

            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //WriteIndented = true
            };

            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize<TripRoutes>(trips, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                //if (isNewItem)
                   // response = await client.PostAsync(uri, content);
               // else
                    response = await client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    //Всплывающее сообщение
                    Debug.WriteLine(@"\tSuccessfully saved");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tError{0}", ex.Message);
            }
        }

        // Удаление маршрута
        public async Task DeleteTripRoute()
        {
            Uri uri = new Uri(string.Format(Params.Api, id));

            try
            {
                client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                isDelete = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                isDelete = true;
            }
        }

        // Удаление маршрута
        public async Task DeleteTripRoute(TripRoutes trip)
        {
            Uri uri = new Uri(string.Format(Params.Api, trip.id));

            try
            {
                client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                isDelete = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                isDelete = true;
            }
        }

        // Удаление маршрута
        public async Task DeleteTripRoute(TripRoutesViewModel trip)
        {

            Uri uri = new Uri(string.Format(Params.Api + "TripRoutes/" + trip.id.ToString()));
            try
            {
                client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                    isDelete = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                isDelete = true;
            }
        }

        /*
        public string SerializeToXml<T>(T obj, JsonSerializerOptions serializerOptions)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }
        */

        public async void GetAddressesCatalog(string id)
        {
            this.getAddresses = new ObservableCollection<AddressesViewModel>();

            //Берем данные с Web Api
            this.getAddresses.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadAddressesCatalogCompleted);
            string result = webClient.DownloadString(new Uri(Params.Api + "Addresses/"+id));

            try
            {
                this.getAddresses.Clear();
                if (result != null)
                {
                    var addressList = JsonConvert.DeserializeObject<Addresses>(result);
                    this.getAddresses.Add(new AddressesViewModel()
                    {
                        ID = addressList.id,
                        Name = addressList.name
                    });
                }
                isAddresses = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных адреса/проверьте соединение с сервером", "принять");
                isAddresses = 2;
            }
        }

        public async void GetBuildingsCatalog(string id)
        {
            this.getBuildings = new ObservableCollection<BuildingsViewModel>();

            //Берем данные с Web Api
            this.getBuildings.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadBuildingsCatalogCompleted);
            string result = webClient.DownloadString(new Uri(Params.Api + "Buildings/" + id));

            try
            {
                this.getBuildings.Clear();
                if (result != null)
                {
                    var buildingList = JsonConvert.DeserializeObject<Buildings>(result);
                    this.getBuildings.Add(new BuildingsViewModel()
                    {
                        ID = buildingList.id,
                        Number = buildingList.number
                    });
                }
                isBuildings = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных здания/проверьте соединение с сервером", "принять");
                isBuildings = 2;
            }
        }

        public async void GetAddressBuildingsCatalog(string id)
        {
            this.getAddressBuildings = new ObservableCollection<AddressBuildingsViewModel>();

            //Берем данные с Web Api
            this.getAddressBuildings.Clear();

            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            // webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadAddressBuildingsCatalogCompleted);
            string result = webClient.DownloadString(new Uri(Params.Api + "AddressBuildings/" + id));

            try
            {
                this.getAddressBuildings.Clear();
                if (result != null)
                {
                    var buildingList = JsonConvert.DeserializeObject<AddressBuildings>(result);
                    this.getAddressBuildings.Add(new AddressBuildingsViewModel()
                    {
                        ID = buildingList.id,
                        X_coordinate = buildingList.x,
                        Y_coordinate = buildingList.y,
                        AddressId = buildingList.AddressId,
                        BuildingId = buildingList.BuildingId
                    });
                }
                isAddressBuildings = 1;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Oops", "Ошибка загрузки данных/проверьте соединение с сервером", "принять");
                isAddressBuildings = 2;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
