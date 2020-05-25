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
            var mw = App.Current.MainWindow as MainWindow;
            DataContext = mw.PassengersVM;
        }
    }
}
