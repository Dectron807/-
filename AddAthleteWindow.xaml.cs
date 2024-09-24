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
    public partial class AddAthleteWindow : Window
    {
        private SportsSchoolEntities _context = new SportsSchoolEntities();

        public AddAthleteWindow()
        {
            InitializeComponent();
            LoadSections();
        }

        private void LoadSections()
        {
            var sections = _context.Section.ToList();
            comboBoxSections.ItemsSource = sections;
            comboBoxSections.DisplayMemberPath = "Name";
            comboBoxSections.SelectedValuePath = "Id";
        }

        private void btnAddAthlete_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            int age = int.Parse(txtAge.Text);
            string achievements = txtAchievements.Text;
            int sectionId = (int)comboBoxSections.SelectedValue;

            Athlete newAthlete = new Athlete
            {
                FullName = fullName,
                Age = age,
                Achievements = achievements,
                SectionId = sectionId
            };

            _context.Athlete.Add(newAthlete);
            _context.SaveChanges();

            MessageBox.Show("Спортсмен добавлен!");
            this.Close();
        }
    }
}
