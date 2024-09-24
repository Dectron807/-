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

namespace ызщке
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSections_Click(object sender, RoutedEventArgs e)
        {
            SectionsWindow sectionsWindow = new SectionsWindow();
            sectionsWindow.Show();
        }

        private void btnAddAthlete_Click(object sender, RoutedEventArgs e)
        {
            AddAthleteWindow addAthleteWindow = new AddAthleteWindow();
            addAthleteWindow.Show();
        }

        private void btnDeleteAthlete_Click(object sender, RoutedEventArgs e)
        {
            DeleteAthleteWindow deleteAthleteWindow = new DeleteAthleteWindow();
            deleteAthleteWindow.Show();
        }
    }
}
