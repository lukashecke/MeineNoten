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
        

        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();

            dataSet = new DataSet();
            try
            {
                dataSet.ReadXml("MeineNoten.xml");
            }
            catch (Exception)
            {

            }
            
        }

        private void Eintragen_Click(object sender, RoutedEventArgs e)
        {
            data.InsertDataRow("VS",((MainWindowViewModel)DataContext).SelectedItem.Title, "2"); //Wieso Cast hier nötig????

            dataSet = data.Database;

            using (DataSet)
            {
               

                DataSet.WriteXml("MeineNoten.xml");

            }
            


        }





    }

    

    
}
