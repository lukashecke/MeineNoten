using MeineNoten.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeineNoten.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<String> myGrades;
        public ObservableCollection<String> MyGrades
        {
            get
            {
                if (this.myGrades == null)
                {
                    this.myGrades = new ObservableCollection<String>();
                    this.OnPropertyChanged("MyGrades");
                }
                return this.myGrades;
            }
            set
            {
                this.myGrades = value;
            }
        }
        public MainWindowViewModel()
        {
            this.Grades.Add(new Grade("1", 1));
            this.Grades.Add(new Grade("2", 2));
            this.Grades.Add(new Grade("3", 3));
            this.Grades.Add(new Grade("4", 4));
            this.Grades.Add(new Grade("5", 5));
            this.Grades.Add(new Grade("6", 6));

            DataSet dataSet = new DataSet();
            try
            {
                dataSet.ReadXml("MeineNoten.xml");
            }
            catch (Exception)
            {
                // Bei Erststart des Programms wird das Laden hier problemlos übersprungen
            }
            int amountOfGrades = 0;
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                       MyGrades.Add(row["Note"].ToString());
                }
            }
            double grade = 0;

            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    try
                        {
                        grade += ((Double.Parse(row["Note"].ToString())) * (Int16.Parse(row["Gewichtung"].ToString())));
                        amountOfGrades+= (Int16.Parse(row["Gewichtung"].ToString()));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Gesamtnote konnte nicht ermittelt werden." + Environment.NewLine + "Überprüfen sie die XML-Datei auf Fehler.", "Beschädigte Daten");
                        // Damit TotalGrade auch 0...
                        grade = 0; 
                        break;
                        }
                }
            }
            grade /= amountOfGrades;
            try
            {
#warning Gesamtnote änder sich erst bei Programmstart

                this.totalGrade = GesamtnoteAnpassen(grade);
            }
            catch (Exception)
            {
                this.totalGrade = "initializing";
                MessageBox.Show("Gesamtnote konnte nicht ermittelt werden." + Environment.NewLine + "Überprüfen sie die XML-Datei auf Fehler.", "Beschädigte Daten");
            }
        }
        public string GesamtnoteAnpassen(double grade)
        {
            string retGrade="initializing";
            if (grade%1==0)
            {
                retGrade = grade.ToString() + ",0";
            }
            else
            {
                //runden
                grade *= 100;
                int i = (int)grade;
                grade = i;
                grade /= 100;
                retGrade = grade.ToString();
            }
            return retGrade;
        }
        private ObservableCollection<Grade> grades;
        public ObservableCollection<Grade> Grades
        {
            get
            {
                if (this.grades == null)
                {
                    this.grades = new ObservableCollection<Grade>();
                    this.OnPropertyChanged("Grades");
                }
                return this.grades;
            }
            set
            {
                this.grades = value;
            }
        }

        private Grade selectedItem;
        public Grade SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
                this.OnPropertyChanged("SelectedItem");

            }
        }

        private string totalGrade;
        public string TotalGrade
        {
            get
            {
                
                
                return this.totalGrade;
            }
            set
            {
                this.totalGrade = value;
                this.OnPropertyChanged("TotalGrade");

            }
        }
    }
}
