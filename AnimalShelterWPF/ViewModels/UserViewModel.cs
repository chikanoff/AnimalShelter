using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GUI.Data;
using GUI.Models;
using GUI.MVVMBase;
using GUI.Services;
using GUI.Views;
using Microsoft.Win32;

namespace GUI.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private UserModel _model;
        private UserData _user;
        private ObservableCollection<PetData> _userPets;
        private ObservableCollection<PhotoData> _petPhotos;
        private PetData _selectedPet;
        private PhotoData _selectedPhoto;
        private bool _visibility = false;

        public UserData User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        public ObservableCollection<PetData> UserPets
        {
            get => _userPets;
            set
            {
                _userPets = value;
                RaisePropertyChanged("UserPets");
            }
        }

        public ObservableCollection<PhotoData> PetPhotos
        {
            get => _petPhotos;
            set
            {
                _petPhotos = value;
                RaisePropertyChanged("PetPhotos");
            }
        }

        public PetData SelectedPet
        {
            get => _selectedPet;
            set
            {
                _selectedPet = value;
                _petPhotos = new ObservableCollection<PhotoData>(_selectedPet.Photos);
                _visibility = true;
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("SelectedPet");
                RaisePropertyChanged("PetPhotos");
            }
        }
        public PhotoData SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                _selectedPhoto = value;
                RaisePropertyChanged("SelectedPhoto");
            }
        }
        public bool Visibility => _visibility;



        public ICommand AddPhotoCommand { get; set; }
        public ICommand DeletePhotoCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand QuitCommand { get; set; }

        public UserViewModel()
        {
            _model = new UserModel();
            _user = _model.GetUser(AuthentificationService.Id);
            _userPets = _model.GetUserPets(_user.Id);

            AddPhotoCommand = new DelegateCommand(AddPhoto);
            DeletePhotoCommand = new DelegateCommand(DeletePhoto);
            LogoutCommand = new RelayCommand<IClosable>(Logout);
            QuitCommand = new RelayCommand<IClosable>(Quit);
        }

        private void AddPhoto()
        {
            string path = null;
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter =
                    "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };
            var show = op.ShowDialog();
            if (show != null && show.Value)
            {
                path = op.FileName;
            }

            int? result = _model.AddPhoto(_selectedPet.Id, path);
            if (result != null)
            {
                _petPhotos.Add(new PhotoData()
                {
                    Id = result.Value,
                    Path = path,
                    PetId = _selectedPet.Id
                });
            }
            RaisePropertyChanged("PetPhotos");

        }

        private void DeletePhoto()
        {
            if (_selectedPhoto == null)
            {
                MessageBox.Show("Выберите фотографию");
            } else
            {
                _model.DeletePhoto(_selectedPhoto);
                _petPhotos.Remove(_selectedPhoto);
                RaisePropertyChanged("PetPhotos");
            }
        }

        private void Logout(IClosable window)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            window.Close();
        }

        private void Quit(IClosable window)
        {
            window.Close();
        }
    }
}
