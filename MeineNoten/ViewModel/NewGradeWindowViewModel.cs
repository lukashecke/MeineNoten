using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeineNoten.ViewModel
{
    class NewGradeWindowViewModel : ViewModelBase
    {
        private ObservableCollection<string> subjects;
        public ObservableCollection<string> Subjects
        {
            get
            {
                if (this.subjects == null)
                {
                    this.subjects = new ObservableCollection<string>();
                    this.OnPropertyChanged("Subjects");
                }
                return this.subjects;
            }
            set
            {
                this.subjects = value;
                this.OnPropertyChanged("Subjects");

            }
        }
        private ObservableCollection<string> grades;
        public ObservableCollection<string> Grades
        {
            get
            {
                if (this.grades == null)
                {
                    this.grades = new ObservableCollection<string>();
                    this.OnPropertyChanged("Grades");
                }
                return this.grades;
            }
            set
            {
                this.grades = value;
                this.OnPropertyChanged("Grades");

            }

        }
        private string selectedGrade;
        public string SelectedGrade
        {
            get
            {
                return this.selectedGrade;
            }
            set
            {
                this.selectedGrade = value;
                this.OnPropertyChanged("SelectedGrade");

            }
        }
        private string selectedSubject;
        public string SelectedSubject
        {
            get
            {
                return this.selectedSubject;
            }
            set
            {
                this.selectedSubject = value;
                this.OnPropertyChanged("SelectedSubject");

            }
        }
        // So ist der Kalender immer schon schön auf den heutigen Tag gestellt
        private DateTime selectedDate=DateTime.Now;
        public DateTime SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
            set
            {
                this.selectedDate = value;
                this.OnPropertyChanged("SelectedDate");

            }
        }
        private bool selectedWeighting;
        public bool SelectedWeighting
        {
            get
            {
                return this.selectedWeighting;
            }
            set
            {
                this.selectedWeighting = value;
                this.OnPropertyChanged("SelectedWeighting");

            }
        }
        private string art;
        public string Art
        {
            get
            {
                return this.art;
            }
            set
            {
                this.art = value;
                this.OnPropertyChanged("Art");

            }
        }
        public NewGradeWindowViewModel()
        {
            this.Subjects.Add("Anwendungsentwicklung und Programmierung");
            this.Subjects.Add("IT-Systeme");
            this.Subjects.Add("Vernetzte Systeme");
            this.Subjects.Add("Betriebswirtschaftliche Prozesse");
            this.Subjects.Add("Sozialkunde");
            this.Subjects.Add("Deutsch");
            this.Subjects.Add("Englisch");
            this.Subjects.Add("Ethik");
            this.Subjects.Add("Sport");
            this.Subjects.Add("Crimpen und Löten");

            this.Grades.Add("1");
            this.Grades.Add("2");
            this.Grades.Add("3");
            this.Grades.Add("4");
            this.Grades.Add("5");
            this.Grades.Add("6");
            
        }
        

    }
}
