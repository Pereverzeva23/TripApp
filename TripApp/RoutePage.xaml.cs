using Npgsql;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http.Json;
using MauiLibrary;
using TripApp.Models;
using TripApp.ViewModels;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace TripApp;
public partial class RoutePage : ContentPage
{
    //string sql = "Server=localhost; Port=5432; Database=TripApp; User Id=postgres; Password=admin;";
    //public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();



    //public List<TripRoutes> tripRoutes { get; private set;}

    private ViewModels.RoutePageModelView inputViewModel;

    

    public RoutePage()
	{
        InitializeComponent();

        
        /* NpgsqlConnection conn = new NpgsqlConnection(sql);
         conn.Open();
         NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM city", conn);
         NpgsqlDataReader reader = cmd.ExecuteReader();

         while (reader.Read())
         {
            Cities.Add(new City { CityName = reader["name_city"].ToString() }); // �������� "CityName" �� ��� ������� � ��������� ������
         }

         ListPlan.ItemsSource = Cities;
         conn.Dispose();
         conn.Close();*/

    }

    private async void DataLoad()
    {
        inputViewModel = new ViewModels.RoutePageModelView();
        inputViewModel.LoadData();
        ListRoute.ItemsSource = inputViewModel.Items;
        //ListRoute.
    }

    private async void PrintAsync()
    {/*
        try
        {
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
            using var client = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = delegate { return true; },
            });
            var response = await MauiLibrary.ApiClient.GetAsync("/");
            if (response.IsSuccessStatusCode)
            {
                //var cities = await response.Content.ReadFromJsonAsync<ObservableCollection<City>>();
                var cities = await response.Content.ReadAsStringAsync();
              //  ListPlan.ItemsSource = cities;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
#if DEBUG
            throw;
#endif
        }*/
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        // ���������� ������
        //������� �� �������� MainPage
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void ToolbarItem2_Clicked(object sender, EventArgs e)
    {
        // �����������
        bool result = await Shell.Current.DisplayAlert("��������", "�� �������?", "��", "���");
        if (result)
        {

            // �������� ������
            if (ListRoute.SelectedItem != null)
            {
                var data = ListRoute.SelectedItem as TripRoutesViewModel;
                // ������ �� ��������
                await inputViewModel.Items[0].DeleteTripRoute(data);
                while (!inputViewModel.Items[0].IsDelete)
                {

                }
                await Shell.Current.GoToAsync("//MainPage");
                await Shell.Current.GoToAsync("//RoutePage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Oops", "�������� �������", "�������");
            }
        }
    }

    private async void ToolbarItem3_Clicked(object sender, EventArgs e)
    {
        // ��������� ������
        if (ListRoute.SelectedItem != null)
        {
            var data = ListRoute.SelectedItem as TripRoutesViewModel;

            // �������� ������ �� ��������
            await Shell.Current.GoToAsync($"//MainPage?fulldata={data.ID}");
        }
        else
        {
            // �����������
            await Shell.Current.DisplayAlert("Oops", "�������� �������", "�������");
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            DataLoad();
        }
        catch { }
        PrintAsync();
    }

    private async void ListRoute_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // �����������
        bool result = await Shell.Current.DisplayAlert("�����������", "���������� ������� �� �����?", "��", "���");
        if (result)
        {
            //�������� ������ �� �����
            var data = ListRoute.SelectedItem as TripRoutesViewModel;
            await Shell.Current.GoToAsync($"//MapsPage?fulldata={data.ID}");
        }
    }
    /*
public class City
{
public int id_city { get; set; }
public string name_city { get; set; }
public string description { get; set;}
}
*/
}