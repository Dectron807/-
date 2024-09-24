using System;
using System.IO;
using System.Linq;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ызщке
{
    public partial class SectionsWindow : Window
    {
        private SportsSchoolEntities _context = new SportsSchoolEntities();
        public SectionsWindow()
        {
            InitializeComponent();
            LoadSections();
        }

        private void LoadSections()
        {
            var sections = _context.Section.Include("Coach").ToList();
            comboBoxSections.ItemsSource = sections;
            comboBoxSections.DisplayMemberPath = "Name";
            comboBoxSections.SelectedValuePath = "Id";
        }

        private void btnPrintPdf_Click(object sender, RoutedEventArgs e)
        {
            var selectedSection = (Section)comboBoxSections.SelectedItem;

            if (selectedSection != null)
            {
                GeneratePdf(selectedSection);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите секцию.");
            }
        }

        private void comboBoxSections_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxSections.SelectedValue != null)
            {
                int sectionId = (int)comboBoxSections.SelectedValue;
                var athletes = _context.Athlete.Where(a => a.SectionId == sectionId).ToList();
                listBoxAthletes.ItemsSource = athletes;
                listBoxAthletes.DisplayMemberPath = "FullName";
                var section = _context.Section.Include("Coach")
                        .FirstOrDefault(s => s.Id == sectionId);

                if (section != null && section.Coach != null)
                {
                    textBlockTrainerInfo.Text = $"Тренер: {section.Coach.FullName}";
                }
                else
                {
                    textBlockTrainerInfo.Text = "Информация о тренере недоступна.";
                }
            }
        }

        private void GeneratePdf(Section section)
        {
            string filePath = "SectionInfo.pdf";

            using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                var document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                try
                {
                    string fontPath = "C:\\Windows\\Fonts\\arialbd.ttf"; 
                    var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var font = new iTextSharp.text.Font(baseFont, 12);
                    var titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);

                    var titleParagraph = new Paragraph("Информация о секции", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    document.Add(titleParagraph);
                    document.Add(new Paragraph("\n"));

                    document.Add(new Paragraph("Секция: " + section.Name, font));
                    if (section.Coach != null)
                    {
                        document.Add(new Paragraph("Тренер: " + section.Coach.FullName, font));
                    }
                    else
                    {
                        document.Add(new Paragraph("Тренер: информация недоступна.", font));
                    }

                    var athletes = _context.Athlete.Where(a => a.SectionId == section.Id).ToList();
                    document.Add(new Paragraph("Спортсмены:", font));

                    PdfPTable table = new PdfPTable(1); 
                    table.WidthPercentage = 100;

                    foreach (var athlete in athletes)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(athlete.FullName, font))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            Padding = 5
                        };
                        table.AddCell(cell);
                    }

                    document.Add(table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании PDF: {ex.Message}");
                }
                finally
                {
                    document.Close();
                    writer.Close();
                }
            }

            System.Diagnostics.Process.Start(filePath);
        }
    }
}
