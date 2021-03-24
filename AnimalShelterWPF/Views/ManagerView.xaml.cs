using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GUI.MVVMBase;

namespace GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window, IClosable
    {
        public ManagerView()
        {
            InitializeComponent();
        }
    }
}
