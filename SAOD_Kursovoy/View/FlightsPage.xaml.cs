using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для FlightsPage.xaml
    /// </summary>
    public partial class FlightsPage : Page
    {
        public FlightsPage()
        {
            InitializeComponent();
            DataContext = new ViewModel.FlightsViewModel();
        }
    }
}
