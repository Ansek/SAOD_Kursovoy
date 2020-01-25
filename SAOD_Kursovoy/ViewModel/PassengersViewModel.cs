using SAOD_Kursovoy.Service;

namespace SAOD_Kursovoy.ViewModel
{
    class PassengersViewModel
    {
        public Command FindByPassport
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Поиск по <№ паспорта>.");
            });
        }

        public Command FindByFIO
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Поиск по <ФИО>.");
            });
        }

        public Command Add
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Зарегистрировать.");
            });
        }

        public Command<int> Remove
        {
            get => new Command<int>((i) =>
            {
                System.Windows.MessageBox.Show("Удалить.");
            });
        }

        public Command Clear
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Очистить.");
            });
        }
    }
}
