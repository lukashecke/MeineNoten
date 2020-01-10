using MeineNoten.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace MeineNoten.View
{
    /// <summary>
    /// Interaktionslogik für NewGradeWindow.xaml
    /// </summary>
    public partial class NewGradeWindow : Window
    {
        #region fields
        Data data = new Data();
        #endregion

        #region entities
        string SelectedSchoolYear { get; set; }
        string Selection { get; set; }
        private DataSet dataSet;
        public DataSet DataSet
        {
            get
            {
                if (dataSet == null)
                {
                    dataSet = new DataSet();
                }
                return dataSet;
            }
        }
        #endregion

        #region constructors
        public NewGradeWindow(string selection, string selectedSchoolYear)
        {
            // Centering the Window
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            
            this.DataContext = new NewGradeWindowViewModel();
            InitializeComponent();

            LoadData();

            this.Selection = selection;
            ((NewGradeWindowViewModel)DataContext).SelectedSubject = this.Selection;
            this.SelectedSchoolYear = selectedSchoolYear;
            PART_Beschreibung.Focus();
        }
        #endregion

        #region private methods
        private void Check(string selectedSubject, string selectedGrade, string selectedDate, string art)
        {
            if (selectedSubject == null || selectedSubject.Equals("initialize") || selectedGrade == null || selectedDate == null || string.IsNullOrWhiteSpace(art))
            {
                throw new Exception();
            }

        }

        private void LoadData()
        {
            dataSet = new DataSet();
            dataSet = data.Database;
            try
            {
                dataSet.ReadXml("MeineNoten.xml");
            }
            catch (Exception)
            {
                // Bei Erststart des Programms wird das Laden hier problemlos übersprungen
            }
            data.database = dataSet;
        }

        private string ToWeight(bool boo)
        {
            if (boo)
            {
                return "2";
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// Converts a DateTime objects int to a string with DD.MM.YYYY Format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string DateTimeToDateString(DateTime dateTime)
        {
            string day = dateTime.Day.ToString();
            string month = dateTime.Month.ToString();
            string year = dateTime.Year.ToString();
            // Append 0 before Day/ Month 0-9
            if (day.Count() < 2)
            {
                day = "0" + day;
            }
            if (month.Count() < 2)
            {
                month = "0" + month;
            }
            return $"{day}.{month}.{year}";
        }
        #endregion

        #region Events (1. Commands, 2. Logik nicht in die Grafik)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Check(((NewGradeWindowViewModel)DataContext).SelectedSubject,
                    ((NewGradeWindowViewModel)DataContext).SelectedGrade,
                    ((NewGradeWindowViewModel)DataContext).SelectedDate.ToString(),
                    ((NewGradeWindowViewModel)DataContext).Art);

                data.InsertDataRow(((NewGradeWindowViewModel)DataContext).SelectedSubject,
                    ((NewGradeWindowViewModel)DataContext).SelectedGrade,
                    ToWeight(((NewGradeWindowViewModel)DataContext).SelectedWeighting),
                    DateTimeToDateString(((NewGradeWindowViewModel)DataContext).SelectedDate),
                    ((NewGradeWindowViewModel)DataContext).Art,
                    this.SelectedSchoolYear);
                using (DataSet)
                {
                    DataSet.WriteXml("MeineNoten.xml");
                }
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Auswahl ist nicht vollständig.", "Noteninformation nicht vollständig");
            }
        }
        #endregion
    }
}
