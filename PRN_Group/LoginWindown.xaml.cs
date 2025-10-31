﻿using BLL;
using DAL.Entities;
using System;
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

namespace PRN_Group
{
    /// <summary>
    /// Interaction logic for LoginWindown.xaml
    /// </summary>
    public partial class LoginWindown : Window
    {
        public LoginWindown()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();

            var user = UserService.Login(Email.Text, Password.Password);


            if (user != null && user.RoleId == 1)
            {
                ((App)Application.Current).CurrentUserId = user.Id;
                ((App)Application.Current).CurrentUser = user;

                //if(((App)Application.Current).CurrentUser != null)
                //{
                //    MessageBox.Show("User " + ((App)Application.Current).CurrentUser.Email + ((App)Application.Current).CurrentUser.Id);
                //}

                MessageBox.Show("Login successful!");

                DashboardWindow d = new();
                d.Show();
                this.Close();
            }
            else if (user != null && user.RoleId == 2)
            {
                ((App)Application.Current).CurrentUserId = user.Id;
                ((App)Application.Current).CurrentUser = user;

                MessageBox.Show("Login successful!");

                HomePageWindow h = new();
                h.Show();
                this.Close();
            }
            else MessageBox.Show("Invalid email or password.");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
