using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для PassengersPage.xaml
    /// </summary>
    public partial class PassengersPage : Page
    {
        public PassengersPage()
        {
            InitializeComponent();
            DataContext = new ViewModel.PassengersViewModel();
        }
    }
}
