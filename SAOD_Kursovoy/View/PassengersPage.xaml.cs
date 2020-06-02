using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Вкладка "Пассажиры".
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
