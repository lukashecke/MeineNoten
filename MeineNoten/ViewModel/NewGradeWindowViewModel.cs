using MeineNoten.Commands;
using MeineNoten.Model;
using MeineNoten.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MeineNoten.ViewModel
{
    public class NewGradeWindowViewModel : ViewModelBase
    {
        private String selectedDescription;
        public String SelectedDescription
        {
            get
            {
                return this.MarkViewModel.Art;
            }
            set
            {
                this.MarkViewModel.Art = value;
                this.OnPropertyChanged("SelectedDescription");
            }
        }

        private DateTime selectedDate = DateTime.Now; // So ist der Kalender immer schon schön auf den heutigen Tag gestellt
        public DateTime SelectedDate
        {
            get
            {
                return this.MarkViewModel.Date;
            }
            set
            {
                this.MarkViewModel.Date = value;
                this.OnPropertyChanged("SelectedDate");

            }
        }

        private int selectedWeighting;
        public int SelectedWeighting
        {
            get
            {
                return this.MarkViewModel.Gewichtung ;
            }
            set
            {
                this.MarkViewModel.Gewichtung = value;
                this.OnPropertyChanged("SelectedWeighting");

            }
        }

        private Grades selectedGrade;
        public Grades SelectedGrade
        {
            get
            {
                return (Grades)this.MarkViewModel.Note;
            }
            set
            {
                this.MarkViewModel.Note = (int)value;
                this.OnPropertyChanged("SelectedGrade");

            }
        }

        #region constructors
        public NewGradeWindowViewModel(SchoolYearViewModel schoolYearViewModel, MarkViewModel markViewModel)
        {
            this.SchoolYearViewModel = schoolYearViewModel;
            this.MarkViewModel = markViewModel;
            this.SaveCommand = new RelayCommand(CommandSave, CanExecuteSave);
        }
        #endregion
        public ICommand SaveCommand
        {
            get; set;
        }
        public void CommandSave(object param)
        {
            MeineNoten.Class.MeineNoten.Database.Tables["Grades"].Rows.Add((DataRow)this.MarkViewModel.Model);
            this.SchoolYearViewModel.SubjectViewModels.Current.MarkViewModels.Add(this.MarkViewModel);
            // Closes the current opened NewGradeWindow
            Application.Current.Windows.OfType<NewGradeWindow>().First().Close();
        }
        

        public bool CanExecuteSave(object param)
        {
            return true;
        }

        public SchoolYearViewModel SchoolYearViewModel
        {
            get;set;
        }

        public MarkViewModel MarkViewModel
        {
            get; set;
        }

        public Array Grades
        {
            get
            {
                return  Enum.GetValues(typeof(Grades));
            }
        }
    }

    public enum Grades
    {
        sehr_gut = 1,
        gut=2,
        befriedigend=3,
        ausreichend=4,
        mangelhaft=5,
        ungenügend=6
    }
}
