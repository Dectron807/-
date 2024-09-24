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
using System.Windows.Shapes;

namespace ызщке
{
    /// <summary>
    /// Логика взаимодействия для DeleteAthleteWindow.xaml
    /// </summary>
    public partial class DeleteAthleteWindow : Window
    {
        private SportsSchoolEntities _context = new SportsSchoolEntities();
        public DeleteAthleteWindow()
        {
            InitializeComponent();
            LoadAthletes();
        }

        private void LoadAthletes()
        {
            var athletes = _context.Athlete.ToList();
            comboBoxAthletes.ItemsSource = athletes;
            comboBoxAthletes.DisplayMemberPath = "FullName";
            comboBoxAthletes.SelectedValuePath = "Id";
        }

        private void btnDeleteAthlete_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAthletes.SelectedValue != null)
            {
                int athleteId = (int)comboBoxAthletes.SelectedValue;
                var athleteToDelete = _context.Athlete.Find(athleteId);

                if (athleteToDelete != null)
                {
                    _context.Athlete.Remove(athleteToDelete);
                    _context.SaveChanges();

                    MessageBox.Show("Спортсмен удален.");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Выберите спортсмена для удаления.");
            }
        }
    }
}
