using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace khokhlov_trpo_wpf4
{
    public class Course : INotifyPropertyChanged
    {
        private int _id = 0;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _title = "Название курса";
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _teacher = "Преподаватель";
        public string Teacher
        {
            get => _teacher;
            set
            {
                if (_teacher != value)
                {
                    _teacher = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _pricePerHour = 1000;
        public decimal PricePerHour
        {
            get => _pricePerHour;
            set
            {
                if (_pricePerHour != value)
                {
                    _pricePerHour = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalCost));
                }
            }
        }

        private int _hours = 10;
        public int Hours
        {
            get => _hours;
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalCost));
                }
            }
        }
        public decimal TotalCost => PricePerHour * Hours;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
