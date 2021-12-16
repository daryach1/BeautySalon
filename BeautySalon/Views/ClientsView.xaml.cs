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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeautySalon.Services;

namespace BeautySalon.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientsView.xaml
    /// </summary>
    public partial class ClientsView : Page
    {
        BeautySalonEntities context;
        public ClientsView()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            context = new BeautySalonEntities();
            clientsDataGrid.ItemsSource = context.Client.ToList();
            genderComboBox.ItemsSource = context.Gender.ToList();
        }

        /// <summary>
        /// Метод для сортировки и поиска данных в реальном времени
        /// </summary>
        private void SearchAndSortingTable()
        {
            var currentGender = (Gender)genderComboBox.SelectedItem;
            string searchFullName = fullNameTextBox.Text;
            string searchEmail = emailTextBox.Text;
            string searchPhone = phoneTextBox.Text;
            List<Client> clients = context.Client.ToList();
            if (genderComboBox.SelectedItem == null)
            {
                clients = clients.Where(x => x.LastName.ToLower().Contains(searchFullName.ToLower())).ToList();
                clients = clients.Where(x => x.Email.ToLower().Contains(searchEmail.ToLower())).ToList();
                clients = clients.Where(x => x.Phone.ToLower().Contains(searchPhone.ToLower())).ToList();
                clientsDataGrid.ItemsSource = clients;
            }
            else
            {
                clients = clients.Where(x => x.GenderCode == currentGender.Code).ToList();
                clients = clients.Where(x => x.Email.ToLower().Contains(searchEmail.ToLower())).ToList();
                clients = clients.Where(x => x.Phone.ToLower().Contains(searchPhone.ToLower())).ToList();
                clients = clients.Where(x => x.LastName.ToLower().Contains(searchFullName.ToLower())).ToList();  
                clientsDataGrid.ItemsSource = clients;
            }
            
        }

        /// <summary>
        /// Метод для очистки данных полей поиска
        /// </summary>
        private void ClearValues() 
        {
            fullNameTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            phoneTextBox.Text = string.Empty;
            genderComboBox.SelectedItem = null;
        }

        private void fullNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAndSortingTable();
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAndSortingTable();
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAndSortingTable();
        }

        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAndSortingTable();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var row = (Client)clientsDataGrid.SelectedItem;
            if (row == null)
                MessageBox.Show("Выберите клиента для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            { 
                int idClient = row.ID;
                List<ClientService> clientServices = context.ClientService.ToList();
                clientServices = clientServices.Where(x => x.ClientID == idClient).ToList();
                if (clientServices.Count != 0)
                    MessageBox.Show("У данного клиента имеются посещения. Удаление запрещено.", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить данного клиента?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        context.Client.Remove(row);
                        context.SaveChanges();
                        clientsDataGrid.ItemsSource = context.Client.ToList();
                    }
                }
            }
            
        }

        private void showVisitsButton_Click(object sender, RoutedEventArgs e)
        {
            var row = (Client)clientsDataGrid.SelectedItem;
            int idClient = row.ID;
            List<ClientService> clientServices = context.ClientService.ToList();
            clientServices = clientServices.Where(x => x.ClientID == idClient).ToList();
            if (clientServices.Count == 0)
                MessageBox.Show("У данного клиента посещений нет", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            else 
                FrameService.MainFrame.Navigate(new ClientVisitsView(clientServices));
        }
    }
}
