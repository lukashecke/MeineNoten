using MeineNoten.Model;
using MeineNoten.View;
using MeineNoten.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MeineNoten
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region fields
        /// <summary>
        /// needed to fix selection changed on selectedschoolyear
        /// </summary>
        bool start = true;
        Data data = new Data();
        private DataSet dataSet;
        #endregion

        #region entities
        private Grade gradeSelection;
        public Grade GradeSelection
        {
            get
            {
                if (gradeSelection == null)
                {
                    // gradeSelection = new Grade("null",0) ;
                }
                return gradeSelection;
            }
            set
            {
                this.gradeSelection = value;
            }
        }
        private string selection;
        public string Selection
        {
            get
            {
                if (selection == null)
                {
                    selection = "initialize";
                }
                return selection;
            }
            set
            {
                this.selection = value;
            }
        }
        #endregion

        #region constructors
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();

            LoadData();
        }
        #endregion

        #region public methods
        public IEnumerable CreateGrades()
        {
            List<Grade> list = new List<Grade>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["Fach"].ToString().Equals(Selection) && row["Schuljahr"].ToString().Equals(SchoolYearComboBox.SelectedValue))
                    {
                        try
                        {
                            list.Add(new Grade(row["Fach"].ToString(), row["Art"].ToString(), row["Datum"].ToString(), row["Note"].ToString(), "(" + row["Gewichtung"].ToString() + ")", row["Schuljahr"].ToString()));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Datensatz konnte nicht vollständig geladen wedern." + Environment.NewLine + "Überprüfen sie die XML-Datei auf Fehler.", "Beschädigte Daten");
                        }
                    }

                }

            }
            return list;
        }
        #endregion

        #region private methods
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
        #endregion

        #region Events (muss weg)
        private void TotalGradesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).CalculateTotalGrades();
            ((MainWindowViewModel)this.DataContext).GesamtnoteBerechnen();
            Grade selectedGrade = (Grade)((sender as ListView).SelectedItem);
            Selection = selectedGrade.Fach.ToString();
            listView.ItemsSource = CreateGrades();
            //Vorerst Notlösung für Refreshen
            DataSet refresh = new DataSet();
            refresh.ReadXml("MeineNoten.xml");
            dataSet = refresh;
            DeleteButton.Visibility = Visibility.Hidden;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grade grade = (Grade)((sender as ListView).SelectedItem);
            GradeSelection = grade;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewGradeWindow win2 = new NewGradeWindow(this.Selection, ((MainWindowViewModel)DataContext).SelectedSchoolYear);
            win2.ShowDialog(); // Waits until window closes
            //Vorerst Notlösung für Refreshen
            DataSet refresh = new DataSet();
            refresh.ReadXml("MeineNoten.xml");
            dataSet = refresh;
            //listview refreshen
            listView.ItemsSource = CreateGrades();
            // fächerauswahl samt durchschnittsnote refreshen
            ((MainWindowViewModel)DataContext).RefreshWindow();
        }
        
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["Fach"].ToString().Equals(GradeSelection.Fach) &&
                        row["Note"].ToString().Equals(GradeSelection.Note) &&
                        ("("+row["Gewichtung"].ToString()+")").Equals(GradeSelection.Gewichtung) &&
                        row["Art"].ToString().Equals(GradeSelection.Art) &&
                        row["Datum"].ToString().Equals(GradeSelection.Date) &&
                        row["Schuljahr"].ToString().Equals(SchoolYearComboBox.SelectedItem))
                    {
                        row.Delete();
                        dataSet.WriteXml("MeineNoten.xml");
                        // ((MainWindowViewModel)DataContext).TotalGrades.Remove(new Grade( row["Fach"].ToString(), row["Art"].ToString(), row["Datum"].ToString(), row["Note"].ToString(), row["Gewichtung"].ToString(), row["Schuljahr"].ToString()));
                        break;
                    }
                }
            }
            //Vorerst Notlösung für Refreshen
            DataSet refresh = new DataSet();
            refresh.ReadXml("MeineNoten.xml");
            dataSet = refresh;
            listView.ItemsSource = CreateGrades();
            //listview refreshen muss vor der Fächerwahl refresht werden, sonst aktualisiert die Letzte Note nicht korrekt
            // fächerauswahl samt durchschnittsnote refreshen
            ((MainWindowViewModel)DataContext).RefreshWindow();
        }

        private void SchoolYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!start)
            {
            //listview refreshen muss vor der Fächerwahl refresht werden, sonst aktualisiert die Letzte Note nicht korrekt
            listView.ItemsSource = CreateGrades();
            // fächerauswahl samt durchschnittsnote refreshen
            ((MainWindowViewModel)DataContext).RefreshWindow();
            }
            start=false;
        }
        #endregion
    }
}
