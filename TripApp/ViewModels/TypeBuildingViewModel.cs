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
    public class TypeBuildingViewModel : INotifyPropertyChanged
    {
        int id;
        string name = "";

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<TypeBuildings> typeBuildings { get; } = new();

        public void MainViewModel()
        {
            // устанавливаем команду добавления
            AddCommand = new Command(() =>
            {
                typeBuildings.Add(new TypeBuildings(id, name));
            });
            // устанавливаем команду удаления
            RemoveCommand = new Command((args) =>
            {
                if (args is TypeBuildings type) typeBuildings.Remove(type);
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
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
