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
