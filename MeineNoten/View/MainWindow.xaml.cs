using MeineNoten.Model;
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

        private string selection;
        public string Selection
        {
            get
            {
                if (selection == null)
                {
                    selection = "VS";
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

        private IEnumerable CreateGrades()
        {
            List<double> list = new List<double>();
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["Fach"].ToString().Equals(Selection))
                    {
                        list.Add(Double.Parse(row["Note"].ToString()));
                    }
                }
            }
            return list;
        }


        private void Eintragen_Click(object sender, RoutedEventArgs e)
        {
            data.InsertDataRow(Selection,((MainWindowViewModel)DataContext).SelectedItem.Title, "0"); //Wieso Cast hier nötig????

            using (DataSet)
            {
                DataSet.WriteXml("MeineNoten.xml");
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            Selection = lbi.Content.ToString();
            listView.ItemsSource = CreateGrades();
        }
    }
}
