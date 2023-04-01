using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using System.Windows.Input;
using System.Collections.ObjectModel;

namespace NiceBikeG5
{
    public class Bike : INotifyPropertyChanged
    {
        int size;
        string color;
        double quantity;
        string type;
        ImageSource image;
        public event PropertyChangedEventHandler PropertyChanged;

       
        public int Id { get; set; }

        public string OrderNumber { get; set; }
        public string AssignedAssembler { get; set; }
        public string Color { get { return color; } set { color = value; } }
        public int Size { get { return size; } set { size = value; } }
        public double Quantity
        {
            set { SetProperty(ref quantity, value); }
            get { return quantity; }
        }
        public string Type { get { return type; } set { type = value; } }
        public ImageSource Image { get { return image; } set { image = value; } }
        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
        
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}

