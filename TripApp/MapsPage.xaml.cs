using Map = Microsoft.Maui.Controls.Maps.Map;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using TripApp.ViewModels;

namespace TripApp
{
    [QueryProperty(nameof(FullData), "fulldata")]
    public partial class MapsPage : ContentPage
    {
        private string fulldata;

        private int load;

        private string linePath = "";

        public string FullData
        {
            get { return fulldata; }
            set { fulldata = value; }
        }

        private readonly MainPageModelView _modelView;

        IDispatcherTimer timer;

        public MapsPage()
        {
            InitializeComponent();
            _modelView = new MainPageModelView();

            this.SetValue(Shell.FlyoutBehaviorProperty, FlyoutBehavior.Disabled);

            Location location = new Location(55.254589, 61.39116);
            MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
            //mapOnPage = new Map(mapSpan);
            mapOnPage.MoveToRegion(mapSpan);

            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                mapOnPage.IsShowingUser = true;
            }
        }

        private void DoSometing()
        {
            Layer_MapClicked(null, new MapClickedEventArgs(new Location()));
            timer.Stop();
            FullData = "";
            FullData = null;
        }

        private async void Layer_MapClicked(object sender, Microsoft.Maui.Controls.Maps.MapClickedEventArgs e)
        {
            //Хз что тут за debug
            System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");

            var a = mapOnPage.MapElements.Count;


            //Создаем пин для карты
            var pin2 = new Pin
            {
                Label = "Location",
                Address = "You Clicked",
                Location = new Microsoft.Maui.Devices.Sensors.Location(e.Location.Latitude, e.Location.Longitude),
                Type = PinType.Place
            };

            //Создаем событие клик для каждого маркера
            pin2.MarkerClicked += Pin2_markerClicked;

            //Добавление пина на карту
            mapOnPage.Pins.Add(pin2);


            if (mapOnPage.Pins.Count >= 2)
            {
                string route = await GetPath(mapOnPage.Pins[0].Location.Longitude, mapOnPage.Pins[0].Location.Latitude,
                  mapOnPage.Pins[1].Location.Longitude, mapOnPage.Pins[1].Location.Latitude);

                await Draw(route);
            }
            else
            {
                //await Draw();
            }
        }

        private async Task Draw(string route)
        {
            //string json = LoadMauiAsset().Result;
            string json = route;
            //string json = File.ReadAllText("..\\TextFile1.txt");
            //var array= JsonConvert.DeserializeObject<Item>(json);
            var data = JArray.Parse(json);
            string line = data.First.Last.Previous.ToString();

            //Получение координат построчно
            string[] line2 = line.Split(new char[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string[] coord;
            double longtitude, lattitude;

            double[][] pol = new double[0][];
            //Очистка карты
            //Layer.Pins.Clear();
            int ii = 0;
            for (int i = 0; i < line2.Length; i++)
            {
                string l = line2[i];
                coord = l.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                coord[0] = coord[0].Replace(".", ",");
                if (Double.TryParse(coord[0], out longtitude) == true)
                {
                    coord[1] = coord[1].Replace(".", ",");
                    lattitude = Convert.ToDouble(coord[1]);
                    pol = IncreaseArraySize(pol);
                    pol[ii][0] = lattitude;
                    pol[ii][1] = longtitude;
                    ii++;

                    /*
                    Layer.Pins.Add(new Pin()
                    {
                        Label = "Location",
                        Location = new Microsoft.Maui.Devices.Sensors.Location(lattitude, longtitude),
                    });
                    */
                }
            }

            var polyline = new Polyline
            {
                StrokeColor = Colors.Red,
                StrokeWidth = 20,

            };

            for (int i = 0; i < pol.Length; i++)
            {
                polyline.Geopath.Add(new Location(pol[i][0], pol[i][1]));
            }
            mapOnPage.MapElements.Add(polyline);
        }

        private async Task<string> GetPath(double lon1, double lat1, double lon2, double lat2)
        {
            string line = "";
            PostOut post = new PostOut(lon1, lat1, lon2, lat2);
            // PostOut post = new PostOut(61.39405310153961, 55.25588260432099, 61.39956068247556, 55.25344494722934);
            //PostOut post = new PostOut();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string JsonString = JsonSerializer.Serialize(post);
            //label1.Text = JsonString;
            StringContent _cont = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://routing.api.2gis.com");
            //Убрать комменты для получения маршрута



            using (HttpResponseMessage response = await client.PostAsync("/get_pairs/1.0/car?key=61e1eb8b-74ef-414b-985b-695302435ad5", _cont))
            {
                try
                {
                    // textBox3.Text = _cont.ToString();
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContent responseContent = response.Content;

                        using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                        {
                            line += await reader.ReadToEndAsync();
                            //textBox1.Text = line;
                        }
                        string js = response.Content.ToString();
                        // textBox4.Text = await response.Content.ReadAsStringAsync();

                        //var json = await response.Content.ReadFromJsonAsync(,);
                        //textBox2.Text = "Верно";
                    }
                    else
                    {
                        string error = response.ToString();
                        //textBox2.Text = "Неверно";
                    }
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                }
            }
            return line;
        }

        private async void GetPath2(double lon1, double lat1, double lon2, double lat2)
        {
            PostOut post = new PostOut(lon1, lat1, lon2, lat2);
            // PostOut post = new PostOut(61.39405310153961, 55.25588260432099, 61.39956068247556, 55.25344494722934);
            //PostOut post = new PostOut();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string JsonString = JsonSerializer.Serialize(post);
            //label1.Text = JsonString;
            StringContent _cont = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://routing.api.2gis.com");
            //Убрать комменты для получения маршрута



            using (HttpResponseMessage response = await client.PostAsync("/get_pairs/1.0/car?key=61e1eb8b-74ef-414b-985b-695302435ad5", _cont))
            {
                try
                {
                    // textBox3.Text = _cont.ToString();
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContent responseContent = response.Content;

                        using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                        {
                            linePath += await reader.ReadToEndAsync();
                            //textBox1.Text = line;
                        }
                        string js = response.Content.ToString();
                        // textBox4.Text = await response.Content.ReadAsStringAsync();

                        //var json = await response.Content.ReadFromJsonAsync(,);
                        //textBox2.Text = "Верно";

                        /*
                        while (linePath == "")
                        {

                        }
                        */

                        await Draw(linePath);
                    }
                    else
                    {
                        string error = response.ToString();
                        //textBox2.Text = "Неверно";
                    }
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                }
            }
        }

        private void Pin2_markerClicked(object sender, Microsoft.Maui.Controls.Maps.PinClickedEventArgs e)
        {
            //Явное приведение типов
            Pin pin = (Pin)sender;

            mapOnPage.MapElements[1].MapElementId = "pl0";
            bool a = mapOnPage.MapElements[0] == mapOnPage.MapElements[1];
            // Удаление
            for (int i = 0; i < mapOnPage.Pins.Count; i++)
            {
                if (mapOnPage.Pins[i].Location == pin.Location)
                {
                    //Удаляет из карты выбранный маркер
                    mapOnPage.Pins.RemoveAt(i);
                    //Удаляет маршрут
                    mapOnPage.MapElements.Clear();
                }
            }
        }

        private double[][] IncreaseArraySize(double[][] array)
        {
            double[][] newPol = new double[array.Length + 1][];
            Array.Copy(array, 0, newPol, 0, array.Length);
            double[] subArray = new double[3];
            newPol[array.Length] = subArray;
            return newPol;
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (load != 0)
            {
                if (FullData != "" && FullData!=null)
                {
                    SetBindingContext();
                    timer = Application.Current.Dispatcher.CreateTimer();
                    timer.Interval = TimeSpan.FromSeconds(3);
                    timer.Tick += (s, e) => DoSometing();
                    timer.Start();
                }
            }
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            load++;
            if (FullData != "" && FullData!=null)
            {
                SetBindingContext();
                timer = Application.Current.Dispatcher.CreateTimer();
                timer.Interval = TimeSpan.FromSeconds(5);
                timer.Tick += (s, e) => DoSometing();
                timer.Start();
            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            
        }

        private async void SetBindingContext()
        {
            var a = load;
            if (FullData != null)
            {
                //192.168.54.128
                _modelView.LoadPickerData();
                _modelView.LoadData(FullData);

                var aAddBuildTo = _modelView.addressBuildings.Where(a => a.ID == _modelView.Items[0].AddressToId).ToList();
                var aAddBuildFrom = _modelView.addressBuildings.Where(a => a.ID == _modelView.Items[0].AddressFromId).ToList();

                //Location loc = new Location(aAddBuildFrom[0].X_coordinate, aAddBuildFrom[0].Y_coordinate);

                //Создаем пин для карты
                var pin2 = new Pin
                {
                    Label = "Location",
                    Address = "You Clicked",
                    Location = new Microsoft.Maui.Devices.Sensors.Location(aAddBuildFrom[0].X_coordinate, aAddBuildFrom[0].Y_coordinate),
                    Type = PinType.Place
                };

                //Создаем событие клик для каждого маркера
                pin2.MarkerClicked += Pin2_markerClicked;


                

                //Создаем пин для карты
                var pin3 = new Pin
                {
                    Label = "Location",
                    Address = "You Clicked",
                    Location = new Microsoft.Maui.Devices.Sensors.Location(aAddBuildTo[0].X_coordinate, aAddBuildTo[0].Y_coordinate),
                    Type = PinType.Place
                };

                //Создаем событие клик для каждого маркера
                pin3.MarkerClicked += Pin2_markerClicked;

                //Добавление пина на карту
                mapOnPage.Pins.Add(pin2);
                mapOnPage.Pins.Add(pin3);


                if (mapOnPage.Pins.Count >= 2)
                {
                    await Task.Run(() =>
                    {
                        GetPath2(aAddBuildFrom[0].Y_coordinate, aAddBuildFrom[0].X_coordinate,
                            aAddBuildTo[0].Y_coordinate, aAddBuildTo[0].X_coordinate);
                    });
                }
                else
                {
                    //await Draw();
                }
            }
        }

        //Класс для маршрута
        public class PostOut
        {
            public ObjPoints[] points { get; set; }

            public string type { get; set; }

            public string output { get; set; }


            /*
            public PostOut()
            {
                points = new ObjPoints[1];
                points[0] = new ObjPoints();
                type = "jam";
                output = "full";
            }
            */


            public PostOut(double lon1, double lat1, double lon2, double lat2)
            {
                this.points = new ObjPoints[1];
                points[0] = new ObjPoints(lon1, lat1, lon2, lat2);
                this.type = "jam";
                this.output = "full";
            }
        }

        //Класс объект маршрута
        public class ObjPoints
        {
            public double lon1 { get; set; }
            public double lat1 { get; set; }
            public double lon2 { get; set; }
            public double lat2 { get; set; }


            /*
            public ObjPoints()
            {
                lon1 = 82.933328;
                lat1 = 55.102268;
                lon2 = 82.958722;
                lat2 = 55.032594;
            }
            */


            public ObjPoints(double lon1, double lat1, double lon2, double lat2)
            {
                this.lon1 = lon1;
                this.lat1 = lat1;
                this.lon2 = lon2;
                this.lat2 = lat2;
            }
        }

        public class Item
        {
            //public int distance { get; set; }
            //public int duration { get; set; }
            //public double lat1 { get; set; }
            //public double lat2 { get; set; }
            // public double lon1 { get; set; }
            // public double lon2 { get; set; }
            //  public string status { get; set; }
            public wkt wkt { get; set; }
            public Item() { }
        }
        public class wkt
        {
            public double LINESTRING { get; set; }
            public wkt() { }
        }
    }
}