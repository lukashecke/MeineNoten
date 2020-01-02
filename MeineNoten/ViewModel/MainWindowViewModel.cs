using MeineNoten.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            dataSet.ReadXml("MeineNoten.xml");
            //dataSet.
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                       MyGrades.Add(row["Note"].ToString());
                    //Console.WriteLine(row["Note"].ToString());
                    //foreach (DataColumn column in table.Columns)
                    //{
                    //    Console.WriteLine(row[column]);
                    //}
                }
            }

            //this.myGrades.Add();



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

        private Grade totalGrade;
        public Grade TotalGrade
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
