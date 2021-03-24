using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DAL.Entities;
using GUI.Models;
using GUI.MVVMBase;
using GUI.Views;

namespace GUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private LoginModel _loginModel;
        private string _username;
        private string _password;

        public string Username {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged("Username");
            }
        }
        public string Password {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand LoginAsGuestCommand { get; set; }
        public ICommand QuitCommand { get; set; }



        public LoginViewModel()
        {
            _loginModel = new LoginModel();
            LoginCommand = new RelayCommand<IClosable>(Login);
            RegisterCommand = new RelayCommand<IClosable>(Register);
            LoginAsGuestCommand = new RelayCommand<IClosable>(LoginAsGuest);
            QuitCommand = new RelayCommand<IClosable>(Quit);
        }

        private void Quit(IClosable window)
        {
            window.Close();
        }

        private void Login(IClosable window)
        {
            if (CheckTextBoxes())
            {
                bool login = _loginModel.IsLogin(_username, _password);
                if (login)
                {
                    string type = _loginModel.GetUserType(_username, _password);
                    switch (type)
                    {
                        case "Administrator":
                            _loginModel.LoginUser(_username, _password);
                            AdminView adminView = new AdminView();
                            adminView.Show();
                            break;
                        case "Manager":
                            _loginModel.LoginUser(_username, _password);
                            ManagerView managerView = new ManagerView();
                            managerView.Show();
                            break;
                        case "User":
                            _loginModel.LoginUser(_username, _password);
                            UserView userView = new UserView();
                            userView.Show();
                            break;
                        default:
                            MessageBox.Show("USER_TYPE_ERROR, необходимо обратиться к системному администратору.");
                            break;
                    }
                    window.Close();
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует");
                }
            }
        }

        private void Register(IClosable window)
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
            window.Close();
        }

        private void LoginAsGuest(IClosable window)
        {
            GuestView guestView = new GuestView();
            guestView.Show();
            window.Close();
        }

        private bool CheckTextBoxes()
        {
            bool checker = true;
            if (string.IsNullOrEmpty(_username))
            {
                checker = false;
                MessageBox.Show("Вы не ввели имя пользователя");
            }
            else if (string.IsNullOrEmpty(_password))
            {
                checker = false;
                MessageBox.Show("Вы не ввели пароль");
            }

            return checker;
        }
    }
}
