using MeineNoten.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MeineNoten.View
{
    /// <summary>
    /// Interaktionslogik für NewGradeWindow.xaml
    /// </summary>
    public partial class NewGradeWindow : Window
    {
        Data data = new Data();
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

        public NewGradeWindow()
        {
            this.DataContext = new NewGradeWindowViewModel();
            InitializeComponent();

            LoadData();

        }

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
                    ((NewGradeWindowViewModel)DataContext).Art);
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

            


            //string i = ((NewGradeWindowViewModel)DataContext).SelectedGrade;
        }

        private void Check(string selectedSubject, string selectedGrade,  string selectedDate, string art)
        {
            if (selectedSubject==null||selectedGrade==null|| selectedDate==null||string.IsNullOrWhiteSpace( art))
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

        private string DateTimeToDateString(DateTime dateTime)
        {
            return $"{dateTime.Day}.{dateTime.Month}.{dateTime.Year}";
        }
    }
}
