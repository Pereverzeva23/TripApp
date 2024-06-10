
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TripApp.Models;
using TripApp.ViewModels;

namespace TripApp
{
    [QueryProperty(nameof(FullData), "fulldata")]
    //[QueryProperty(nameof(GetData), "getData")]
    public partial class MainPage : ContentPage
    {
        private string getdata = "";
        private string fulldata;

        public string FullData
        {
            get { return fulldata; }
            set { fulldata = value; } 
        }

        /*
        public string GetData
        {
            get { return getdata; }
            set { getdata = value; }
        }
        */

        private readonly MainPageModelView _modelView;
        

        int count = 0;

        public MainPage()
        {
           
            InitializeComponent();
            datePick.MinimumDate = DateTime.Today;
            _modelView = new MainPageModelView();
            LoadPickerData();
        }

        private async void LoadPickerData()
        {
            if (_modelView!=null)
            {
                _modelView.LoadPickerData();
                streetFrom.ItemsSource = _modelView.addresses;
                streetTo.ItemsSource = _modelView.addresses;
            }
        }

        private void SetBindingContext()
        {
            if (FullData != null)
            {
                //192.168.54.128
                _modelView.LoadData(FullData);
                //DataContext = _modelView;
                entryName.Text = _modelView.Items[0].Name;
                datePick.Date = _modelView.Items[0].Date;

                var aAddBuildTo = _modelView.addressBuildings.Where(a => a.ID == _modelView.Items[0].AddressToId).ToList();
                var aAddBuildFrom = _modelView.addressBuildings.Where(a => a.ID == _modelView.Items[0].AddressFromId).ToList();

                var aStreetTo = _modelView.addresses.Where(a => a.ID == aAddBuildTo[0].AddressId).ToList();
                var aStreetFrom = _modelView.addresses.Where(a => a.ID == aAddBuildFrom[0].AddressId).ToList();

                var aHouseTo = _modelView.buildings.Where(a => a.ID == aAddBuildTo[0].BuildingId).ToList();
                var aHouseFrom = _modelView.buildings.Where(a => a.ID == aAddBuildFrom[0].BuildingId).ToList();



                for(int i=0;i<_modelView.addresses.Count;i++)
                {
                    if (_modelView.addresses[i] == aStreetTo[0])
                    {
                        streetTo.SelectedIndex = i;
                    }
                    if (_modelView.addresses[i] == aStreetFrom[0])
                    {
                        streetFrom.SelectedIndex = i;
                    }
                }

                for (int i = 0; i < _modelView.buildings.Count; i++)
                {
                    if (_modelView.buildings[i] == aHouseTo[0])
                    {
                        streetHouseTo.SelectedIndex = i;
                    }
                    if (_modelView.buildings[i] == aHouseFrom[0])
                    {
                        streetHouseFrom.SelectedIndex = i;
                    }
                }


                /*
                streetTo.SelectedItem = aStreetTo[0];
                streetFrom.SelectedItem = aStreetFrom[0];

                streetHouseTo.SelectedItem = aHouseTo[0];
                streetHouseFrom.SelectedItem = aHouseFrom[0];
                */

                /*
                streetFrom.SelectedItem = _modelView.Items[0];
                streetHouseFrom.SelectedItem = _modelView.Items[0];
                */

                /*
                var a = FullData as TripRoutesViewModel;
                entryName.Text = a.Name;
                //entryFrom.Text = FullData.AddressBuildingRouteFromId.x_coordinate.ToString();
                //entryTo.Text = FullData.AddressBuildingRouteToId.x_coordinate.ToString();
                datePick.Date = a.Date;
                */
            }

            //if (GetData != null)
            //{

            /*
            // Очистка всего списка для работы с 1 объектом
            _modelView.Items.Clear();
            // Добавление объекта из перехода
            _modelView.Items.Add(FullData);
            // Отображение на форме
            BindingContext = _modelView;
            */

            //}
            //else
            //{

            //}

            //inputViewModel.Items[0].AddTripRoute(false);
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            if (entryName.Text != null)
            {
                if (entryName.Text.Length <= 0 || streetFrom.SelectedIndex == -1 || streetHouseFrom.SelectedIndex == -1
                    || streetTo.SelectedIndex == -1 || streetHouseTo.SelectedIndex == -1)
                {
                    // Уведомление
                    await Shell.Current.DisplayAlert("Oops", "Заполните все поля", "принять");
                }
                else
                {

                    //Получить все поездки ID



                    //inputViewModel.Items.
                    //var streetToId = streetTo.SelectedItem as AddressBuildings;
                    //var houseToId = houseTo.SelectedItem as Buildings;
                    //var streetFromId = streetFrom.SelectedItem as AddressBuildings;
                    //var houseFromId = houseFrom.SelectedItem as Buildings;

                    var addToId = streetTo.SelectedItem as AddressesViewModel;
                    var addFromId = streetFrom.SelectedItem as AddressesViewModel;

                    var buildToId = streetHouseTo.SelectedItem as BuildingsViewModel;
                    var buildFromId = streetHouseFrom.SelectedItem as BuildingsViewModel;

                    var _ToList = _modelView.addressBuildings.Where(a => a.BuildingId == buildToId.ID && a.AddressId == addToId.ID).ToList();
                    var _FromList = _modelView.addressBuildings.Where(a => a.BuildingId == buildFromId.ID && a.AddressId == addFromId.ID).ToList();

                    if (FullData == null)
                    {

                        //DateTime.Parse(datePick.Date);
                        var d = new DateTime(datePick.Date.Ticks, DateTimeKind.Utc);
                        _modelView.Items.Add(new TripRoutesViewModel()
                        {
                            Name = entryName.Text,
                            Date = d,
                            AddressFromId = _ToList[0].ID,
                            //AddressBuildingFromId = streetFromId.BuildingId,
                            AddressToId = _FromList[0].ID,

                            //AddressBuildingToId = streetToId.BuildingId
                            //ID = ...
                        });
                            await _modelView.Items[_modelView.Items.Count - 1].AddTripRoute();
                    }
                    else
                    {
                        _modelView.Items[0].Name = entryName.Text;
                        _modelView.Items[0].Date = datePick.Date;
                        //_modelView.Items[0].AddressBuildingFromId = streetFromId.BuildingId;
                        _modelView.Items[0].AddressFromId = _FromList[0].ID;
                        //_modelView.Items[0].AddressBuildingToId = streetToId.BuildingId;
                        _modelView.Items[0].AddressToId = _ToList[0].ID;
                        await _modelView.Items[0].UpdateTripRoute(_modelView.Items[0]);
                    }

                    _modelView.Items.Clear();
                    FullData = null;

                    await Shell.Current.GoToAsync("//RoutePage");

                    /*
                    TripRoutesViewModel routesViewModel = new TripRoutesViewModel();

                    routesViewModel.Date = datePick.Date;
                    routesViewModel.Name = entryName.Text;
                    routesViewModel.AddressBuildingFromId = Convert.ToInt32(entryFrom.Text);
                    routesViewModel.AddressBuildingToId = Convert.ToInt32(entryTo.Text);

                    await routesViewModel.AddTripRoute(true);
                    */

                    /*
                    ViewModels.MainModelView inputViewModel = (ViewModels.MainModelView)BindingContext;
                    List<Route> routes = new List<Route>();

                    if (!string.IsNullOrEmpty(inputViewModel.RouteName))
                    {
                        Route newRoute = new Route(inputViewModel.RouteName,inputViewModel.PointFrom,inputViewModel.PointTo,inputViewModel.Date);
                        routes.Add(newRoute);
                    }
                    */

                    //await Navigation.PushAsync(new RoutePage(routes));
                }
            }
            else
                // Уведомление
                await Shell.Current.DisplayAlert("Oops", "Заполните все поля", "принять");
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            SetBindingContext();
        }

        private void streetFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(streetFrom.SelectedIndex != -1)
            {
                //var streetItem = sender as AddressesViewModel;
                var streetItem = streetFrom.SelectedItem as AddressesViewModel;
                var addressBuildingList = _modelView.addressBuildings.Where(u => u.AddressId == streetItem.ID).ToList();
                var houseList = new ObservableCollection<BuildingsViewModel>();
                foreach (var address_Building in addressBuildingList)
                {
                    var a = _modelView.buildings.Where(u => u.ID == address_Building.BuildingId).ToList();
                    houseList.Add(new BuildingsViewModel()
                    {
                        ID = a[0].ID,
                        Number = a[0].Number
                    });
                }
                streetHouseFrom.ItemsSource = houseList;
                streetHouseFrom.IsEnabled = true;
            }
            else
            {
                streetHouseFrom.SelectedIndex = -1;
                streetHouseFrom.IsEnabled = false;
            }
        }

        private void streetTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (streetTo.SelectedIndex != -1)
            {
                var streetItem = streetTo.SelectedItem as AddressesViewModel;
                var addressBuildingList = _modelView.addressBuildings.Where(u => u.AddressId == streetItem.ID);
                var houseList = new ObservableCollection<BuildingsViewModel>();
                foreach (var address_Building in addressBuildingList)
                {
                    var a = _modelView.buildings.Where(u => u.ID == address_Building.BuildingId).ToList();
                    houseList.Add(new BuildingsViewModel()
                    {
                        ID = a[0].ID,
                        Number = a[0].Number
                    });
                }
                streetHouseTo.ItemsSource = houseList;
                streetHouseTo.IsEnabled = true;
            }
            else
            {
                streetHouseTo.SelectedIndex = -1;
                streetHouseTo.IsEnabled = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }

}
