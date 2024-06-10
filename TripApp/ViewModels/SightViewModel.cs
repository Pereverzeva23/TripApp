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
    public class SightViewModel : INotifyPropertyChanged
    {
        int id;
        string name = "";
        string description = "";
        TimeSpan house_workTo;
        TimeSpan house_workFrom;
        float rating = 0;
        int addressBuildingId = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<Sights> sight { get; } = new();

        public void MainViewModel()
        {
            // устанавливаем команду добавления
            AddCommand = new Command(() =>
            {
                sight.Add(new Sights(id, name, description, house_workTo, house_workFrom, rating, addressBuildingId));
            });
            // устанавливаем команду удаления
            RemoveCommand = new Command((args) =>
            {
                if (args is Sights trip) sight.Remove(trip);
            });
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

        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan Hours_workTo
        {
            get => house_workTo;
            set
            {
                if (house_workTo != value)
                {
                    house_workTo = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan Hours_workFrom
        {
            get => house_workFrom;
            set
            {
                if (house_workFrom != value)
                {
                    house_workFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Rating
        {
            get => rating;
            set
            {
                if (rating != value)
                {
                    rating = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AddressBuildingId
        {
            get => addressBuildingId;
            set
            {
                if (addressBuildingId != value)
                {
                    addressBuildingId = value;
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
