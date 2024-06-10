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
    public class AddressesViewModel : INotifyPropertyChanged
    {
        int id;
       // int x;
       // int y;
        string name = "";
        //string house_street = "";
       // int TypeBuildingId = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<Addresses> address { get; } = new();

        public void MainViewModel()
        {
            
            // устанавливаем команду добавления
            AddCommand = new Command(() =>
            {
                address.Add(new Addresses(id, name));
            });
            // устанавливаем команду удаления
            RemoveCommand = new Command((args) =>
            {
                if (args is Addresses adr) address.Remove(adr);
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

        /*
        public int X
        {
            get => x;
            set
            {
                if (x != value)
                {
                    x = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Y
        {
            get => y;
            set
            {
                if (y != value)
                {
                    y = value;
                    OnPropertyChanged();
                }
            }
        }
        */



        /*
        public string HouseStreet
        {
            get => house_street;
            set
            {
                if (house_street != value)
                {
                    house_street = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TypeBuildingID
        {
            get => TypeBuildingId;
            set
            {
                if (TypeBuildingId != value)
                {
                    TypeBuildingId = value;
                    OnPropertyChanged();
                }
            }
        }
        */

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
