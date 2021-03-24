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

namespace GUI.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private AdminModel _model;
        private ObservableCollection<UserData> _users;
        private ObservableCollection<UserTypeData> _userTypes;
        private UserData _userToAdd;
        private UserData _selectedUser;
        private UserTypeData _selectedUserType;
        private bool _visibility;
        private int _tabSelectionChanged;

        public bool Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }
        public int TabSelectionChanged
        {
            get => _tabSelectionChanged;
            set
            {
                _tabSelectionChanged = value;
                ClearSelected();
                UpdateUsers();
                _visibility = false;
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("TabSelectionChanged");
            }
        }

        public UserData UserToAdd
        {
            get => _userToAdd;
            set
            {
                _userToAdd = value;
                RaisePropertyChanged("UserToAdd");
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

        public ObservableCollection<UserTypeData> UserTypes
        {
            get => _userTypes;
            set
            {
                _userTypes = value;
                RaisePropertyChanged("UserTypes");
            }
        }

        public UserData SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                _visibility = true;
                _selectedUserType = _selectedUser.UserType;
                RaisePropertyChanged("Visibility");
                RaisePropertyChanged("SelectedUser");
                RaisePropertyChanged("SelectedUserType");
            }
        }

        public UserTypeData SelectedUserType
        {
            get => _selectedUserType;
            set
            {
                _selectedUserType = value;
                RaisePropertyChanged("SelectedUserType");
            }
        }

        public ICommand AddUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand QuitCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public AdminViewModel()
        {
            _model = new AdminModel();
            _users = _model.GetUsers();
            _userTypes = _model.GetUserTypes();
            _userToAdd = new UserData();

            AddUserCommand = new DelegateCommand(AddUser);
            EditUserCommand = new DelegateCommand(EditUser);
            DeleteUserCommand = new DelegateCommand(DeleteUser);

            QuitCommand = new RelayCommand<IClosable>(Quit);
            LogoutCommand = new RelayCommand<IClosable>(Logout);
        }

        private void AddUser()
        {
            if (string.IsNullOrEmpty(_userToAdd.Username) || string.IsNullOrEmpty(_userToAdd.Password) ||
                string.IsNullOrEmpty(_userToAdd.Address) || string.IsNullOrEmpty(_userToAdd.FullName) ||
                string.IsNullOrEmpty(_userToAdd.PhoneNumber))
            {
                MessageBox.Show("Вы заполнили не все поля");
            } else if (!_userToAdd.PhoneNumber.All(char.IsDigit))
            {
                MessageBox.Show("Номер телефона должен состоять из цифр");
            }
            else
            {
                _model.AddUser(_userToAdd);
                ClearSelected();
                UpdateUsers();
                ClearUserToAdd();
            }
        }

        private void EditUser()
        {

            if (AuthentificationService.Id == _selectedUser.Id)
            {
                MessageBox.Show("Вы не можете поменять себе тип");
            }
            else
            {
                _selectedUser.UserType = _selectedUserType;
                _model.EditUser(_selectedUser);
                ClearSelected();
                UpdateUsers();
            }
        }

        private void DeleteUser()
        {
            if (AuthentificationService.Id == _selectedUser.Id)
            {
                MessageBox.Show("Вы не можете удалить сами себя");
            }
            else
            {
                _model.DeleteUser(_selectedUser);
                ClearSelected();
                UpdateUsers();
            }
        }

        private void ClearUserToAdd()
        {
            _userToAdd = null;
            RaisePropertyChanged("UserToAdd");
        }

        private void ClearSelected()
        {
            _selectedUser = null;
            _selectedUserType = null;
            _visibility = false;
            RaisePropertyChanged("SelectedUserType");
            RaisePropertyChanged("SelectedUser");
            RaisePropertyChanged("Visibility");
        }

        private void UpdateUsers()
        {
            _users = _model.GetUsers();
            RaisePropertyChanged("Users");
        }

        private void Quit(IClosable window)
        {
            window.Close();
        }

        private void Logout(IClosable window)
        {
            LoginView login = new LoginView();
            login.Show();
            window.Close();
        }
    }
}
