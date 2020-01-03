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
        bool shutdown = false;
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
        DataSet dataSet = new DataSet();
        public MainWindowViewModel()
        {
            this.Grades.Add(new Grade("1", 1));
            this.Grades.Add(new Grade("2", 2));
            this.Grades.Add(new Grade("3", 3));
            this.Grades.Add(new Grade("4", 4));
            this.Grades.Add(new Grade("5", 5));
            this.Grades.Add(new Grade("6", 6));


            try
            {
                dataSet.ReadXml("MeineNoten.xml");
                XmlFormatCheck();
            }
            catch (Exception) // Getriggert bei erststart, weil noch keine XML-Speicher-Datei erstellt
            {
                dataSet.WriteXml("MeineNoten.xml"); //Leere Datei wird angelegt 
            }
            if (!shutdown)// Bei fehlerhafter XML wird das Programm geschlossen und es sollen keine weiteren Schritte gemacht werden
            {


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

                        grade += ((Double.Parse(row["Note"].ToString())) * (Int16.Parse(row["Gewichtung"].ToString())));
                        amountOfGrades += (Int16.Parse(row["Gewichtung"].ToString()));

                    }
                }
                // Bei Erststart 0/0 = NaN wird in GesamtnoteAnpassen abgefangen
                grade /= amountOfGrades;

#warning Gesamtnote änder sich erst bei Programmstart

                this.totalGrade = GesamtnoteAnpassen(grade);

            }
        }

        private void XmlFormatCheck()
        {
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        // Gültige Noten
                        if ((Double.Parse(row["Note"].ToString()) > 0) && (Double.Parse(row["Note"].ToString()) < 7))
                        {

                        }
                        else
                        {
                            throw new Exception("Note");
                        }

                        // Gültige Gewichtungen
                        Int16.Parse(row["Gewichtung"].ToString());

                        // Gültiges Fach
                        if ((row["Fach"].ToString()).Equals("Anwendungsentwicklung und Programmierung") ||
                            (row["Fach"].ToString()).Equals("IT-Systeme") ||
                            (row["Fach"].ToString()).Equals("Vernetzte Systeme") ||
                            (row["Fach"].ToString()).Equals("Betriebswirtschaftliche Prozesse") ||
                            (row["Fach"].ToString()).Equals("Sozialkunde") ||
                            (row["Fach"].ToString()).Equals("Deutsch") ||
                            (row["Fach"].ToString()).Equals("Englisch") ||
                            (row["Fach"].ToString()).Equals("Ethik") ||
                            (row["Fach"].ToString()).Equals("Sport") ||
                            (row["Fach"].ToString()).Equals("Crimpen und Löten"))
                        {

                        }
                        else
                        {
                            throw new Exception("Fach");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Daten konnten nicht vollständig gelesen werden." + Environment.NewLine + "Überprüfen sie die XML-Datei auf Fehler.", "Beschädigte Daten");
                        // Programm "nicht öffnen"
                        Application.Current.Shutdown();
                        shutdown = true;
                        break;
                    }
                }
            }
        }

        public string GesamtnoteAnpassen(double grade)
        {
            string retGrade = "initializing";
            if (grade % 1 == 0)
            {
                retGrade = grade.ToString() + ",00";
            }
            else if (Double.IsNaN(grade))// Bei erststart des Programmes
            {
                retGrade = "Noch keine Noten eingetragen!";
            }
            else 
            {
                //runden
                grade *= 100;
                int i = (int)grade;
                grade = i;
                grade /= 100;
                retGrade = grade.ToString();
                // Noten mit einer Nachkommastelle eine 0 anhängen
                if (retGrade.Count() != 4)
                {
                    retGrade += "0";
                }
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
