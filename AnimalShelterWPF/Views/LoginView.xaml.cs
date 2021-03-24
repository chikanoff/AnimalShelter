using System;
using System.Windows;
using DAL.Initializer;
using GUI.MVVMBase;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window, IClosable
    {
        public LoginView()
        {
            InitializeComponent();
        }
    }
}
