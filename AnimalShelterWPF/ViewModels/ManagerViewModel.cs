using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GUI.Data;
using GUI.Models;
using GUI.MVVMBase;
using GUI.Services;
using GUI.Views;
using Microsoft.Win32;

namespace GUI.ViewModels
{
    public class ManagerViewModel : ViewModelBase
    {
        private ManagerModel _model;
        private UserData _user;
        private PetData _selectedPet;
        private KindData _selectedKind;
        private KindData _selectedKindToAdd;
        private BreedData _selectedBreed;
        private BreedData _selectedBreedToAdd;
        private PhotoData _selectedPhoto;
        private UserData _selectedUser;
        private PetData _selectedAvailablePet;
        private DateTime _selectedDate;
        private string _breedName;
        private string _newBreedName;
        private string _kindName;
        private string _newKindName;
        private PetData _petToAdd;
        private int _tabSelectionChanged;
        private ObservableCollection<BreedData> _breeds;
        private ObservableCollection<KindData> _kinds;
        private ObservableCollection<PetData> _pets;
        private ObservableCollection<PhotoData> _petPhotos;
        private ObservableCollection<UserData> _users;
        private ObservableCollection<PetData> _availablePets;
        private bool _visibility;

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }

        }
        public PetData SelectedAvailablePet
        {
            get => _selectedAvailablePet;
            set
            {
                _selectedAvailablePet = value;
                RaisePropertyChanged("SelectedAvailablePet");
            }
        }

        public UserData SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                RaisePropertyChanged("SelectedUser");
            }
        }
        public ObservableCollection<UserData> Users
        {
            get => _users;
            set
            {
                _users = value;
                RaisePropertyChanged("Users");
            }
        }

        public ObservableCollection<PetData> AvailablePets
        {
            get => _availablePets;
            set
            {
                _availablePets = value;
                RaisePropertyChanged("AvailablePets");
            }
        }
        public UserData User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }
        public PetData PetToAdd
        {
            get => _petToAdd;
            set
            {
                _petToAdd = value;
                RaisePropertyChanged("PetToAdd");
            }
        }
        public ObservableCollection<PetData> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                RaisePropertyChanged("Pets");
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
        public ObservableCollection<BreedData> Breeds
        {
            get => _breeds;
            set
            {
                _breeds = value;
                RaisePropertyChanged("Breeds");
            }
        }
        public ObservableCollection<KindData> Kinds
        {
            get => _kinds;
            set
            {
                _kinds = value;
                RaisePropertyChanged("Kinds");
            }
        }

        public int TabSelectionChanged
        {
            get => _tabSelectionChanged;
            set
            {
                _tabSelectionChanged = value;
                ClearData();
                UpdateAll();
                RaisePropertyChanged("TabSelectionChanged");
            }
        }
        public PetData SelectedPet
        {
            get => _selectedPet;
            set
            {
                _selectedPet = value;
                if (_selectedPet != null)
                {
                    _petPhotos = new ObservableCollection<PhotoData>(_selectedPet.Photos);
                }
                _visibility = true;
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("PetPhotos");
                RaisePropertyChanged("SelectedPet");
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
        public BreedData SelectedBreed
        {
            get => _selectedBreed;
            set
            {
                _selectedBreed = value;
                _visibility = true;
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("SelectedBreed");
            }
        }
        public BreedData SelectedBreedToAdd
        {
            get => _selectedBreedToAdd;
            set
            {
                _selectedBreedToAdd = value;
                RaisePropertyChanged("SelectedBreedToAdd");
            }
        }
        public KindData SelectedKind
        {
            get => _selectedKind;
            set
            {
                _selectedKind = value;
                _visibility = true;
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("SelectedKind");
            }
        }
        public KindData SelectedKindToAdd
        {
            get => _selectedKindToAdd;
            set
            {
                _selectedKindToAdd = value;
                RaisePropertyChanged("SelectedKindToAdd");
            }
        }

        public string BreedName
        {
            get => _breedName;
            set
            {
                _breedName = value;
                RaisePropertyChanged("BreedName");
            }
        }
        public string NewBreedName
        {
            get => _newBreedName;
            set
            {
                _newBreedName = value;
                RaisePropertyChanged("NewBreedName");
            }
        }
        public string KindName
        {
            get => _kindName;
            set
            {
                _kindName = value;
                RaisePropertyChanged("KindName");
            }
        }
        public string NewKindName
        {
            get => _newKindName;
            set
            {
                _newKindName = value;
                RaisePropertyChanged("NewKindName");
            }
        }
        public bool Visibility => _visibility;


        public ICommand AddOperationCommand { get; set; }
        public ICommand AddPhotoCommand { get; set; }
        public ICommand DeletePhotoCommand { get; set; }
        public ICommand DeleteBreedCommand { get; set; }
        public ICommand AddPetCommand { get; set; }
        public ICommand EditPetCommand { get; set; }
        public ICommand DeletePetCommand { get; set; }
        public ICommand AddBreedCommand { get; set; }
        public ICommand RenameBreedCommand { get; set; }
        public ICommand AddKindCommand { get; set; }
        public ICommand RenameKindCommand { get; set; }
        public ICommand DeleteKindCommand { get; set; }
        public ICommand QuitCommand { get; set; }
        public ICommand LogoutCommand { get; set; }


        public ManagerViewModel()
        {
            _model = new ManagerModel();
            _user = _model.GetUser(AuthentificationService.Id);
            _pets = _model.GetPets();
            _breeds = _model.GetBreeds();
            _kinds = _model.GetKinds();
            _petToAdd = new PetData{ArrivalDate = DateTime.Now};
            _availablePets = new ObservableCollection<PetData>(_model.GetAvailablePets());
            _users = new ObservableCollection<UserData>(_model.GetUsers());

            AddOperationCommand = new DelegateCommand(AddOperation);

            AddPetCommand = new DelegateCommand(AddPet);
            AddPhotoCommand = new DelegateCommand(AddPhoto);
            DeletePhotoCommand = new DelegateCommand(DeletePhoto);
            DeleteBreedCommand = new DelegateCommand(DeleteBreed);
            EditPetCommand = new DelegateCommand(EditPet);
            DeletePetCommand = new DelegateCommand(DeletePet);

            AddBreedCommand = new DelegateCommand(AddBreed);
            RenameBreedCommand = new DelegateCommand(RenameBreed);
            AddKindCommand = new DelegateCommand(AddKind);
            RenameKindCommand = new DelegateCommand(RenameKind);
            DeleteKindCommand = new DelegateCommand(DeleteKind);
            QuitCommand = new RelayCommand<IClosable>(Quit);
            LogoutCommand = new RelayCommand<IClosable>(Logout);
        }


        private void AddOperation()
        {
            if (_selectedAvailablePet == null || _selectedUser == null || DateTime.Compare(_petToAdd.ArrivalDate, DateTime.Now) >= 0)
            {
                MessageBox.Show("Не правильно заполнены поля");
            } else if (DateTime.Compare(_selectedDate, DateTime.Now) < 0 &&
                       DateTime.Compare(_selectedDate, new DateTime(2000, 1, 1)) < 0)
            {
                MessageBox.Show("Проверьте дату");
            }
            else
            {
                Mapping.AddOperation(_selectedAvailablePet, _selectedDate, _selectedUser);

                UpdateAll();
                _selectedDate = DateTime.Now;
                _selectedUser = null;
                _selectedAvailablePet = null;
                RaisePropertyChanged("SelectedDate");
                RaisePropertyChanged("SelectedUser");
                RaisePropertyChanged("SelectedAvailablePet");
            }
        }
        private void AddPet()
        {
            if (CheckBoxesToAdd())
            {
                _petToAdd.BreedId = _selectedBreedToAdd.Id;
                _petToAdd.KindId = _selectedKindToAdd.Id;
                _petToAdd.Breed = _selectedBreedToAdd.Name;
                _petToAdd.Kind = _selectedKindToAdd.Name;
                _petToAdd.Photos = new List<PhotoData>();
                _model.AddPet(_petToAdd);
                _pets.Add(_petToAdd);
                _petToAdd = new PetData{ArrivalDate = DateTime.Now};
                ClearData();

                UpdatePets();
                RaisePropertyChanged("PetToAdd");
                RaisePropertyChanged("Pets");
            }
        }
        private void DeletePet()
        {
            _model.DeletePet(_selectedPet);
            _pets.Remove(_selectedPet);
            _selectedPet = null;
            _visibility = false;
            _petPhotos = null;
            UpdatePets();
            RaisePropertyChanged("PetPhotos");
            RaisePropertyChanged("Visibility");
            RaisePropertyChanged("Pets");
        }
        private void DeleteBreed()
        {
            _model.DeleteBreed(_selectedBreed);
            _breeds.Remove(_selectedBreed);
            _selectedBreed = null;
            _visibility = false;

            UpdateBreeds();
            RaisePropertyChanged("Visibility");
            RaisePropertyChanged("Breeds");
        }
        private void EditPet()
        {
            if (DateTime.Compare(_selectedPet.ArrivalDate, DateTime.Now) < 0 && DateTime.Compare(_selectedPet.ArrivalDate, new DateTime(2000, 1, 1)) >= 0)
            {
                _model.EditPet(_selectedPet);
                int id = _pets.ToList().FindIndex(x => x.Id == _selectedPet.Id);
                _pets[id] = _selectedPet;
                _selectedPet = null;
                _visibility = false;
                UpdatePets();
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("Pets");
            }
            else
            {
                MessageBox.Show("Вы выбрали неверную дату");
            }
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

            if (path != null)
            {
                _model.AddPhoto(_selectedPet.Id, path);
                _pets = _model.GetPets();
                RaisePropertyChanged("Pets");
                ClearData();
            }
            else
            {
                MessageBox.Show("Вы не выбрали фотографию");
            }

        }
        private void DeletePhoto()
        {
            if (_selectedPhoto == null)
            {
                MessageBox.Show("Выберите фотографию");
            }
            else
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

        private void ClearData()
        {
            _visibility = false;
            _selectedPet = null;
            _selectedBreed = null;
            _selectedKind = null;
            _selectedBreedToAdd = null;
            _selectedKindToAdd = null;

            RaisePropertyChanged("SelectedPet");
            RaisePropertyChanged("SelectedBreed");
            RaisePropertyChanged("SelectedKind");
            RaisePropertyChanged("SelectedBreedToAdd");
            RaisePropertyChanged("SelectedKindToAdd");
            RaisePropertyChanged("Visibility");
        }

        private void AddBreed()
        {
            if (!string.IsNullOrEmpty(_breedName))
            {
                _model.AddBreed(_breedName);
                _breeds = _model.GetBreeds();
                _breedName = null;
                RaisePropertyChanged("BreedName");
                RaisePropertyChanged("Breeds");
            }
            else
            {
                MessageBox.Show("Введите название породы");
            }
        }

        private void RenameBreed()
        {
            if (!string.IsNullOrEmpty(_newBreedName))
            {
                _model.RenameBreed(_selectedBreed, _newBreedName);
                _newBreedName = null;
                _selectedBreed = null;
                _visibility = false;
                _breeds = _model.GetBreeds();
                RaisePropertyChanged("SelectedBreed");
                RaisePropertyChanged("NewBreedName");
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("Breeds");
            }
            else
            {
                MessageBox.Show("Введите название породы");
            }
        }
        private void AddKind()
        {
            if (!string.IsNullOrEmpty(_kindName))
            {
                _model.AddKind(_kindName);
                _kinds = _model.GetKinds();
                _kindName = null;
                RaisePropertyChanged("KindName");
                RaisePropertyChanged("Kinds");
            }
            else
            {
                MessageBox.Show("Введите название вида");
            }
        }

        private void RenameKind()
        {
            if (!string.IsNullOrEmpty(_newKindName))
            {
                _model.RenameKind(_selectedKind, _newKindName);
                _newKindName = null;
                _selectedKind = null;
                _visibility = false;
                _kinds = _model.GetKinds();
                RaisePropertyChanged("SelectedKind");
                RaisePropertyChanged("NewKindName");
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("Kinds");
            }
            else
            {
                MessageBox.Show("Введите название вида");
            }
        }
        private void DeleteKind()
        {
            _model.DeleteKind(_selectedKind);
            _kinds.Remove(_selectedKind);
            _selectedKind = null;
            _visibility = false;

            UpdateKinds();
            RaisePropertyChanged("Visibility");
            RaisePropertyChanged("Kinds");
        }

        private void UpdatePets()
        {
            _pets = _model.GetPets();
        }

        private void UpdateBreeds()
        {
            _breeds = _model.GetBreeds();
        }
        private void UpdateKinds()
        {
            _kinds = _model.GetKinds();
        }

        private void UpdateAll()
        {
            _kinds = _model.GetKinds();
            _breeds = _model.GetBreeds();
            _pets = _model.GetPets();
            _availablePets = new ObservableCollection<PetData>(_model.GetAvailablePets());
            _users = new ObservableCollection<UserData>(_model.GetUsers());

            RaisePropertyChanged("Users");
            RaisePropertyChanged("AvailablePets");
            RaisePropertyChanged("Kinds");
            RaisePropertyChanged("Breeds");
            RaisePropertyChanged("Pets");
        }
        private bool CheckBoxesToAdd()
        {
            bool checker = _petToAdd != null;
            if (!checker || string.IsNullOrEmpty(_petToAdd.HealthStatus) || string.IsNullOrEmpty(_petToAdd.Color) ||
                string.IsNullOrEmpty(_petToAdd.Conditions) || string.IsNullOrEmpty(_petToAdd.Nickname) || 
                _selectedKindToAdd == null || _selectedBreedToAdd == null)
            {
                checker = false;
                MessageBox.Show("Вы заполнили не все поля");
            }

            if (_petToAdd != null && DateTime.Compare(_petToAdd.ArrivalDate, DateTime.Now) >= 0)
            {
                checker = false;
                MessageBox.Show("Вы выбрали неверную дату");
            }

            return checker;
        }
    }
}
