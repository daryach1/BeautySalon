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
            if (genderComboBox.SelectedItem == null)
            {
                return;
            }
            var currentGender = (Gender)genderComboBox.SelectedItem;
            List<Client> clients = context.Client.ToList();
            clients = clients.Where(x => x.GenderCode == currentGender.Code).ToList();
            clientsDataGrid.ItemsSource = clients;
            
            
        }

        private void SearchTable() 
        {
            string searchFullName = fullNameTextBox.Text;
            List<Client> clients = context.Client.ToList();
            clients = clients.Where(x => x.LastName.ToLower().Contains(searchFullName.ToLower())).ToList();
            clientsDataGrid.ItemsSource = clients;
        }

        private void fullNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTable();
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAndSortingTable();
        }
    }
}
