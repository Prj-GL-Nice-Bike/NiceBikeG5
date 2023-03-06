﻿using System;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Projet_Beau_vélo
{
    public class Bike : INotifyPropertyChanged
    {
        int size;
        string color;
        double quantity;
        string type;
        ImageSource image;
        public event PropertyChangedEventHandler PropertyChanged;

        public Bike(int size, string color, int quantity, string type, ImageSource image)
        {
            this.size = size;
            this.color = color;
            this.quantity = quantity;
            this.type = type;
            this.image = image;
        }


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
    public partial class ViewBike: INotifyPropertyChanged
    {

        //public List<Bike> Bikes { get; set; } = new();
        //public ObservableCollection<Bike> Bikes { get; set; } = new ();
        public IList<Bike> Bikes { get; } = new ObservableCollection<Bike>();
        Random random = new Random();
        string[] Colours = { "red", "blue" };
        string[] Types = { "City", "Adventure", "Explorer" };
        
        public ImageSource Image = ImageSource.FromFile("redtc.jpeg");
        
         public ICommand DeleteCommand { private set; get; }

        //string tape;
        public ViewBike()
        {
            Bikes.Add(new Bike(random.Next(26, 28), Colours[random.Next(Colours.Length)], random.Next(1, 20), Types[random.Next(Types.Length)], Image));
            Bikes.Add(new Bike(random.Next(26, 28), Colours[random.Next(Colours.Length)], random.Next(1, 20), Types[random.Next(Types.Length)], Image));
            Bikes.Add(new Bike(random.Next(26, 28), Colours[random.Next(Colours.Length)], random.Next(1, 20), Types[random.Next(Types.Length)], Image));
            //base.OnAppearing();
            //Bikes.ToList();

            DeleteCommand = new Command(
            execute: () =>
            {
                Bikes.Add(new Bike(random.Next(26, 28), Colours[random.Next(Colours.Length)], random.Next(1, 20), Types[random.Next(Types.Length)], Image));
                //Bikes.Clear();
                RefreshCanExecutes();
            });
        }

        void RefreshCanExecutes()
        {
            (DeleteCommand as Command).ChangeCanExecute();

        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

