using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public enum Gender
        {
            Male,
            Female,
            Other
        }

        public List<Gender> AvailableGenders { get; set; } = new()
        {
            Gender.Male,
            Gender.Female,
            Gender.Other
        };

        public List<string> AvailableCities { get; set; } = new()
        {
            "Linz",
            "Wels",
            "Kefermarkt",
            "Hofkirchen",
            "Froschberger Ghetto"
        };

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public double Salary { get; set; }
            public Gender Gender { get; set; }
        }

        private Person _currentPerson = new();

        public Person CurrentPerson
        {
            get => _currentPerson;
            set
            {
                PropertyChanged?.Invoke(this, new(nameof(CurrentPerson)));
                _currentPerson = value;
            }
        }

        public ObservableCollection<Person> Persons { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Persons = new()
            {
                new()
                {
                    FirstName = "Gerks",
                    LastName = "Khnifs",
                    Salary = 3
                },
                new()
                {
                    FirstName = "Mäcksl",
                    LastName = "Finekner",
                    Salary = 69420
                }
            };
            Recalculate();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = CurrentPerson;
            var newInstance = new Person()
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Salary = obj.Salary,
                City = obj.City,
                Gender = obj.Gender,
                Street = obj.Street
            };

            Persons.Add(newInstance);

            Recalculate();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            Persons.Remove(CurrentPerson);
            CurrentPerson = new();
            Recalculate();
        }

        private void Recalculate()
        {
            HighestSalary.Text = Persons.Max(p => p.Salary).ToString();
            AverageSalary.Text = Persons.Average(p => p.Salary).ToString();
        }
    }
}
