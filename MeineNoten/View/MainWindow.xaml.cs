using MeineNoten.Model;
using MeineNoten.View;
using MeineNoten.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace MeineNoten
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Data data = new Data();
        private DataSet dataSet;


        private string selection;
        public string Selection
        {
            get
            {
                if (selection == null)
                {
                    selection = "initializing";
                }
                return selection;
            }
            set
            {
                this.selection = value;
            }
        }


        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();

            LoadData();
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            Selection = lbi.Content.ToString();
            listView.ItemsSource = CreateGrades();
#warning Eingetragene Noten erscheinen erst nach SelectinChange
            //Vorerst Notlösung für Refreshen
            DataSet refresh = new DataSet();
            refresh.ReadXml("MeineNoten.xml");
            dataSet = refresh;
        }
        private IEnumerable CreateGrades()
        {
            List<Grade> list = new List<Grade>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["Fach"].ToString().Equals(Selection))
                    {
                        try
                        {
                            list.Add(new Grade(row["Fach"].ToString(), row["Art"].ToString(), row["Datum"].ToString(), row["Note"].ToString(), row["Gewichtung"].ToString()));
                            ////list.Add("Note: "+row["Note"].ToString() + " " + "Gewichtung: " + row["Gewichtung"].ToString() + " " + "Datum: " + row["Datum"].ToString());
                            //list.Add(row["Art"].ToString()+" "+ row["Datum"].ToString()+" Note: "+ row["Note"].ToString() + " (Gew.: " + row["Gewichtung"].ToString() + ")");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewGradeWindow win2 = new NewGradeWindow();
            win2.Show();
        }
    }
}
