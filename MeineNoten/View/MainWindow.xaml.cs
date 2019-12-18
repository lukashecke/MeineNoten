using MeineNoten.Model;
using MeineNoten.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
     
        Data data = default(Data);
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

        public MainWindow()
        {
            if (File.Exists("MeineNoten.xml"))
            {
                DataSet.ReadXml("MeineNoten.xml");
            }
            data = new Data();
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();

        }

        private void Eintragen_Click(object sender, RoutedEventArgs e)
        {
            data.InsertDataRow("AP",((MainWindowViewModel)DataContext).SelectedItem.ToString(), "1"); //Wieso Cast hier nötig????

            using (DataSet)
            {
                DataSet.WriteXml("MeineNoten.xml", XmlWriteMode.WriteSchema);
                
            }

            //var pfad = Properties.Settings.Default.Pfad;
            //Properties.Settings.Default.Pfad = pfad;

            //data.APGrades.Add(((Grade)item).Value);
            //SavingToXml.Save( data, "data.xml");


        }





    }

    

    
}
