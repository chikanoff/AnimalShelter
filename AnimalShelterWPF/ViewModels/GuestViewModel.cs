using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using GUI.Data;
using GUI.Models;
using GUI.MVVMBase;
using GUI.Views;

namespace GUI.ViewModels
{
    public class GuestViewModel : ViewModelBase
    {
        private GuestModel _model;
        private PetData _selectedPet;
        private ObservableCollection<PetData> _pets;
        private bool _visibility = false;

        public ObservableCollection<PetData> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                RaisePropertyChanged("Pets");
            }
        }


        public PetData SelectedPet
        {
            get => _selectedPet;
            set
            {
                _selectedPet = value;
                _visibility = true;
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("SelectedPet");
            }
        }

        public bool Visibility => _visibility;

        public ICommand LogoutCommand { get; }
        public ICommand QuitCommand { get; }

        public GuestViewModel()
        {
            _model = new GuestModel();
            _pets = _model.GetPets();

            LogoutCommand = new RelayCommand<IClosable>(Logout);
            QuitCommand = new RelayCommand<IClosable>(Quit);
        }

        private void Logout(IClosable window)
        {
            LoginView login = new LoginView();
            login.Show();
            window.Close();
        }
        private void Quit(IClosable window)
        {
            window.Close();
        }
    }
}
