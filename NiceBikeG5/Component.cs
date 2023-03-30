using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NiceBikeG5
{
    public class Component : INotifyPropertyChanged
    {
        double quantity;
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Explorer { get; set; }
        public string Adventure { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public double Quantity
        {
            set { SetProperty(ref quantity, value); }
            get { return quantity; }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
