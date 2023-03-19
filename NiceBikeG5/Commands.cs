using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;

using Mysqlx.Prepare;


namespace NiceBikeG5
{

    public class Commands : INotifyPropertyChanged
    {
        public ICommand Bikecommand { private set; get; }
        public ICommand Componentcommand { private set; get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Commands()
        {
            Bikecommand = new Command(
                execute: () =>
                { },
                canExecute: () =>
                {
                    return false;
                });

            Componentcommand = new Command(
                execute: () =>
                {  },
                canExecute: () =>
                {
                    return false;
                });
        }
        void RefreshCanExecutes()
        {
            (Componentcommand as Command).ChangeCanExecute();
            (Bikecommand as Command).ChangeCanExecute();
        }

    }
}
   

