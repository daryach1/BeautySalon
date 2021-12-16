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
    /// Логика взаимодействия для InformationClientView.xaml
    /// </summary>
    public partial class InformationClientView : Page
    {
        BeautySalonEntities context;
        public InformationClientView(BeautySalonEntities context, Client client)
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            this.context = context;
            this.DataContext = client;
            genderComboBox.ItemsSource = context.Gender.ToList();
        }
    }
}
