using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для FindByPassportPage.xaml
    /// </summary>
    public partial class FindByPassportPage : Page
    {
        public FindByPassportPage()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).PassengersVM;
        }
    }
}
