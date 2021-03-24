using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using GUI.Models;
using GUI.MVVMBase;
using GUI.Views;

namespace GUI.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private RegisterModel _model;
        private string _username;
        private string _password;
        private string _address;
        private string _fullName;
        private string _phone;

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
        public string Address {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged("Address");
            }
        }
        public string FullName {
            get => _fullName;
            set
            {
                _fullName = value;
                RaisePropertyChanged("FullName");
            }
        }
        public string Phone {
            get => _phone;
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }

        public ICommand RegisterCommand { get; set; }
        public ICommand BackToLoginCommand { get; set; }

        public RegisterViewModel() {
            _model = new RegisterModel();

            RegisterCommand = new RelayCommand<IClosable>(Register);
            BackToLoginCommand = new RelayCommand<IClosable>(BackToLogin);
        }

        private void Register(IClosable window)
        {
            if (!_model.CheckUsername(_username))
            {
                if (CheckTextBoxes())
                {
                    _model.CreateUser(_username, _password, _fullName, _address, _phone);
                    LoginView loginView = new LoginView();
                    loginView.Show();
                    window.Close();
                }
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
            }

        }

        private void BackToLogin(IClosable window)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            window.Close();
        }
        private bool CheckTextBoxes()
        {
            bool checker = true;
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_address) || string.IsNullOrEmpty(_fullName) || string.IsNullOrEmpty(_phone))
            {
                checker = false;
                MessageBox.Show("У вас остались незаполненные поля");
            } else if(!_phone.All(char.IsDigit))
            {
                checker = false;
                MessageBox.Show("Не правильно введен номер");
            }


            return checker;
        }
    }
}
