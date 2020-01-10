using MeineNoten.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace MeineNoten.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region fields
        bool shutdown = false;
        DataSet dataSet = new DataSet();
        #endregion

        #region eintities
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

        private ObservableCollection<Grade> totalGrades;
        public ObservableCollection<Grade> TotalGrades
        {
            get
            {
                if (this.totalGrades == null)
                {
                    this.totalGrades = new ObservableCollection<Grade>();
                    this.OnPropertyChanged("TotalGrades");
                }

                return this.totalGrades;
            }
            set
            {
                this.totalGrades = value;
                this.OnPropertyChanged("TotalGrades");

            }
        }

        private ObservableCollection<string> schoolYear;
        public ObservableCollection<string> SchoolYear
        {
            get
            {
                if (this.schoolYear == null)
                {
                    this.schoolYear = new ObservableCollection<string>();
                    this.OnPropertyChanged("SchoolYear");
                }
                return this.schoolYear;
            }
            set
            {
                this.schoolYear = value;
                this.OnPropertyChanged("SchoolYear");
                

            }

        }

        private string selectedSchoolYear;
        public string SelectedSchoolYear
        {
            get
            {
                if (this.selectedSchoolYear == null)
                {
                    this.selectedSchoolYear = "2019/20"; // default
                    this.OnPropertyChanged("SelectedSchoolYear");
                }
                return this.selectedSchoolYear;
            }
            set
            {
                this.selectedSchoolYear = value;
                this.OnPropertyChanged("SelectedSchoolYear");
            }
        }
        #endregion

        #region constructors
        public MainWindowViewModel()
        {
            this.Grades.Add(new Grade("1", 1));
            this.Grades.Add(new Grade("2", 2));
            this.Grades.Add(new Grade("3", 3));
            this.Grades.Add(new Grade("4", 4));
            this.Grades.Add(new Grade("5", 5));
            this.Grades.Add(new Grade("6", 6));

            this.SchoolYear.Add("2019/20");
            this.SchoolYear.Add("2020/21");
            this.SchoolYear.Add("2021/22");

            this.TotalGrades.Add(new Grade("Anwendungsentwicklung und Programmierung", "Gesamtnote", DateTime.Now.ToString(), "-", "1","initialize"));
            this.TotalGrades.Add(new Grade("IT-Systeme", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Vernetzte Systeme", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Betriebswirtschaftliche Prozesse", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Sozialkunde", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Deutsch", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Englisch", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Ethik", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Sport", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));
            this.TotalGrades.Add(new Grade("Crimpen und Löten", "Gesamtnote", DateTime.Now.ToString(), "-", "1", "initialize"));

            try
            {
                dataSet.ReadXml("MeineNoten.xml");
                XmlFormatCheck();
            }
            catch (Exception) // Getriggert bei erststart, weil noch keine XML-Speicher-Datei erstellt
            {
                dataSet.WriteXml("MeineNoten.xml"); //Leere Datei wird angelegt 
            }

            CalculateTotalGrades();

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
                        if (row["Schuljahr"].ToString().Equals(SelectedSchoolYear))
                        {
                            grade += ((Double.Parse(row["Note"].ToString())) * (Int16.Parse(row["Gewichtung"].ToString())));
                            amountOfGrades += (Int16.Parse(row["Gewichtung"].ToString()));
                        }
                    }
                }
                // Bei Erststart 0/0 = NaN wird in GesamtnoteAnpassen abgefangen
                grade /= amountOfGrades;
                this.TotalGrade = GesamtnoteBerechnen(); //GesamtnoteAnpassen(grade);
            }
        }
        #endregion

        #region private methods
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
                            // for debugging only
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
                            // for debugging only
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
        #endregion

        #region public methods
        public string GesamtnoteBerechnen()
        {
            string ret;
            double temp = 0.0;
            int amount = 0;
            foreach (var item in TotalGrades)
            {
                try
                {
                    if (!item.Note.Equals("-"))
                    {

                    temp += Double.Parse(item.Note);
                    amount++;
                    }
                }
                catch (Exception)
                {
                    break; // wenn noch keine Note eingetragen, hier einfach übersprungen
                }
            }
            temp /= amount;
            //runden
            temp *= 100;
            int i = (int)temp;
            temp = i;
            temp /= 100;
            ret = temp.ToString();
            // Noten mit Nachkommastellen versorgen
            if (ret.Count() < 2)
            {
                ret += ",00";
            }
            else if (ret.Count() < 4)
            {
                ret += "0";
            }
            if (Double.IsNaN(temp) || (temp == (-21474836.48))) // -2147...,48 kommt raus bei leeren Noten
            {
                ret = "Keine Noten eingetragen!";
            }
            this.TotalGrade = ret;
            this.OnPropertyChanged("TotalGrade");
            this.OnPropertyChanged("TotalGrades"); // Bugfix for refreshing the last Grade in the DataSet
            return ret;
        }

        public void CalculateTotalGrades()
        {
            //aktualisieren
            try
            {
                dataSet = new DataSet();
                dataSet.ReadXml("MeineNoten.xml");
                XmlFormatCheck();
                
            } catch
            {

            }

            foreach (var item in this.TotalGrades)
            {
                string fach = item.Fach;
                double temps = 0.0;
                int amountOfGrades = 0;
                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (row["Fach"].ToString().Equals(fach) && row["Schuljahr"].ToString().Equals(SelectedSchoolYear))
                        {
                            temps += ((Double.Parse(row["Note"].ToString())) * (Int16.Parse(row["Gewichtung"].ToString())));
                            amountOfGrades += (Int16.Parse(row["Gewichtung"].ToString()));
                        }
                    }
                    temps /= amountOfGrades;
                    //runden
                    temps *= 100;
                    int i = (int)temps;
                    temps = i;
                    temps /= 100;
                }
                if (Double.IsNaN(temps) || (temps == (-21474836.48))) // -2147...,48 kommt raus bei leeren Noten
                {
                    item.Note = "-";
                }
                else
                {
                    item.Note = temps.ToString();
                    // Noten mit Nachkommastellen versorgen
                    if (item.Note.Count() < 2)
                    {
                        if (item.Note.Equals("0")) // Damit, wenn noch keine Noten eingetragen auch alles auf "-" und nicht auf 0
                        {
                            item.Note = "-";
                            break;
                        }
                        item.Note += ",00";
                    }
                    else if (item.Note.Count() < 4)
                    {
                        item.Note += "0";
                    }
                }
                // this.TotalGrades = item.Note;
            }
            this.OnPropertyChanged("TotalGrades");
        }
        public void RefreshWindow()
        {
            this.CalculateTotalGrades();
            this.GesamtnoteBerechnen();
        }
        #endregion
    }
}
