﻿using BeautySalon.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautySalon.Views
{
    /// <summary>
    /// Логика взаимодействия для InformationClientView.xaml
    /// </summary>
    public partial class InformationClientView : Page
    {
        BeautySalonEntities context;
        Client newClient;
        string pathPhotoClient = string.Empty;
        public InformationClientView(BeautySalonEntities context, Client client)
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            this.context = context;
            this.newClient = client;
            this.DataContext = newClient;
            genderComboBox.ItemsSource = context.Gender.ToList();
        }

        private string CheckGender()
        {
            string nameGender;
            string gender = string.Empty;
            
            nameGender = genderComboBox.Text;
            switch (nameGender)
            {
                 case "Женский":
                     gender = "ж";
                     break;
                 case "Мужской":
                     gender = "м";
                    break;
                 default:
                     gender = string.Empty;
                     break;
            }  
            return gender;
        }

        private void saveDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                newClient.GenderCode = CheckGender();
                newClient.Birthday = (DateTime)birthdayDatePicker.SelectedDate;
                newClient.RegistrationDate = DateTime.Now;
                newClient.PhotoPath = pathPhotoClient;
                context.SaveChanges();
                FrameService.MainFrame.Navigate(new ClientsView());
            }
        }

        private bool CheckData()
        {
            try
            {
                var check = this.DataContext as Client;
                string error = string.Empty;
                if (!Regex.IsMatch(check.LastName, "^[a-zA-Zа-яА-Я ]*$") || check.LastName == string.Empty || check.LastName.Length > 50)
                    error += "Неверный ввод поля Фамилия \n";
                if (!Regex.IsMatch(check.FirstName, "^[a-zA-Zа-яА-Я ]*$") || check.LastName == string.Empty || check.FirstName.Length > 50)
                    error += "Неверный ввод поля Имя \n";
                if (!Regex.IsMatch(check.Patronymic, "^[a-zA-Zа-яА-Я ]*$") || check.LastName == string.Empty || check.Patronymic.Length > 50)
                    error += "Неверный ввод поля Отчество \n";
                if (!Regex.IsMatch(check.Email, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$") || check.Email == string.Empty)
                    error += "Неверный ввод e-mail \n";
                if (genderComboBox.SelectedItem == null)
                    error += "Не введен пол клиента";
                if (!Regex.IsMatch(check.Phone, @"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\-\d\d) | (\d{ 7})| (\d{ 3}\-\d\-\d{ 3})|)") || check.Phone == string.Empty)
                    error += "Неверный ввод поля Телефон";
                if (error == string.Empty)
                    return true;
                else
                {
                    MessageBox.Show(error, "Ошибка добавления клиента", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить клиента, проверьте введенные данные", "Ошибка сохранения данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
            
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            FrameService.MainFrame.Navigate(new ClientsView());
        }

        private void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                clientPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathPhotoClient = System.IO.Path.GetFileName(openFileDialog.FileName);
            } 
        }
    }
}
