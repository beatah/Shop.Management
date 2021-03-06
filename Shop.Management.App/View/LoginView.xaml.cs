﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Shop.Management.App.ViewModel;

namespace Shop.Management.App.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView 
    {
        public LoginView()
        {
            InitializeComponent();
            LoginViewModel _loginViewModel = new LoginViewModel();
            this.DataContext = _loginViewModel;
            if(_loginViewModel.CloseAction==null)
                _loginViewModel.CloseAction=new Action(this.Close);
        }
    }
}
