using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для FindByFIOPage.xaml
    /// </summary>
    public partial class FindByFIOPage : Page
    {
        public FindByFIOPage()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).PassengersVM;
        }
    }
}
