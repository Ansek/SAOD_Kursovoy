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
            var mw = App.Current.MainWindow as MainWindow;
            DataContext = mw.FlightsVM;
        }
    }
}
