using System.Windows;
using SAOD_Kursovoy.Model.Data;
using SAOD_Kursovoy.Service;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для InputFlight.xaml
    /// </summary>
    public partial class InputFlight : Window 
    {
        public InputFlight()
        {
            InitializeComponent();
            DataContext = new Flight();
            OK.Command = new Command(() =>
            {
                DialogResult = true;
            }, () => (DataContext as Flight).Error == string.Empty &&
                (DataContext as Flight).IsFieldsNotNull);
        }
    }
}
