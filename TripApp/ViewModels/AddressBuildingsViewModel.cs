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
    public class AddressBuildingsViewModel
    {
        int id;
        float x_coordinate;
        float y_coordinate;
        int addressId;
        int buildingId;

        //Addresses address;
        //Buildings addressBuildingsRouteFromId;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<AddressBuildings> addressBuildings { get; } = new();
        public ObservableCollection<Addresses> address { get; } = new();
        public ObservableCollection<Buildings> buildings { get; } = new();

        public void MainViewModel()
        {

            // устанавливаем команду добавления
            AddCommand = new Command(() =>
            {
                addressBuildings.Add(new AddressBuildings(id, x_coordinate, y_coordinate, addressId, buildingId));
            });
            // устанавливаем команду удаления
            RemoveCommand = new Command((args) =>
            {
                if (args is AddressBuildings addressBuild) addressBuildings.Remove(addressBuild);
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

        public float X_coordinate
        {
            get => x_coordinate;
            set
            {
                if (x_coordinate != value)
                {
                    x_coordinate = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Y_coordinate
        {
            get => y_coordinate;
            set
            {
                if (y_coordinate != value)
                {
                    y_coordinate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AddressId
        {
            get => addressId;
            set
            {
                if (addressId != value)
                {
                    addressId = value;
                    OnPropertyChanged();
                }
            }
        }

        public int BuildingId
        {
            get => buildingId;
            set
            {
                if (buildingId != value)
                {
                    buildingId = value;
                    OnPropertyChanged();
                }
            }
        }

        public Addresses AddressName
        {
            get
            {
                var list = address.Where(u => u.id == addressId).ToList();
                return list[0];
            }
        }

        public Buildings BuildingNumber
        {
            get
            {
                var list = buildings.Where(u => u.id == buildingId).ToList();
                return list[0];
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
