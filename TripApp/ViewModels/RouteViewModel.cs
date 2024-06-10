using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TripApp.Models;

namespace TripApp.ViewModels
{
    public class RouteViewModel : INotifyPropertyChanged
    {
        string name = "";
        string pointFrom = "";
        string pointTo = "";
        DateTime date = DateTime.MinValue;
        int age;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<Models.Route> route { get; } = new();

        public void MainViewModel()
        {
            // устанавливаем команду добавления
            AddCommand = new Command(() =>
            {
                route.Add(new Models.Route(Name, PointFrom, PointTo, Date));
                Name = "";

            });
            // устанавливаем команду удаления
            RemoveCommand = new Command((args) =>
            {
                if (args is Models.Route trip) route.Remove(trip);
            });
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
        public string PointFrom
        {
            get => pointFrom;
            set
            {
                if (pointFrom != value)
                {
                    pointFrom = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PointTo
        {
            get => pointTo;
            set
            {
                if (pointTo != value)
                {
                    pointTo = value;
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
        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged();
                }
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

