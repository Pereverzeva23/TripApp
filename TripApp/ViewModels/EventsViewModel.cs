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
    public class EventsViewModel : INotifyPropertyChanged
    {
        int id;
        string name;
        string description;
        double cost;
        DateTime dateStart;
        DateTime dateEnd;
        TimeSpan timeStart;
        TimeSpan timeEnd;
        int addressBuildingId;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<Events> events { get; } = new();

        public void MainViewModel()
        {
            // устанавливаем команду добавления
            AddCommand = new Command(() =>
            {
                events.Add(new Events(id, name, description, cost, addressBuildingId, dateStart, dateEnd, timeStart, timeEnd));
            });
            // устанавливаем команду удаления
            RemoveCommand = new Command((args) =>
            {
                if (args is Events adr) events.Remove(adr);
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

        public double Cost
        {
            get => cost;
            set
            {
                if (cost != value)
                {
                    cost = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateStart
        {
            get => dateStart;
            set
            {
                if (dateStart != value)
                {
                    dateStart = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateEnd
        {
            get => dateEnd;
            set
            {
                if (dateEnd != value)
                {
                    dateEnd = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan TimeStart
        {
            get => timeStart;
            set
            {
                if (timeStart != value)
                {
                    timeStart = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan TimeEnd
        {
            get => timeEnd;
            set
            {
                if (timeEnd != value)
                {
                    timeEnd = value;
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
