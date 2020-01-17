using MeineNoten.Commands;
using MeineNoten.Model;
using MeineNoten.View;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MeineNoten.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.DeleteCommand = new RelayCommand(CommandDelete, CanExecuteDelete);
            this.NewCommand = new RelayCommand(CommandNew, CanExecuteNew);
        }

        private ObservableCollectionEx<SchoolYearViewModel> schoolYearViewModels;
        public ObservableCollectionEx<SchoolYearViewModel> SchoolYearViewModels
        {
            get
            {
                if (this.schoolYearViewModels == null)
                {
                    this.schoolYearViewModels = new ObservableCollectionEx<SchoolYearViewModel>();
                    foreach(DataRow dataRowSchoolYear in MeineNoten.Class.MeineNoten.Database.Tables["SchoolYears"].Rows)
                    {
                        var schoolYearViewModel = new SchoolYearViewModel(dataRowSchoolYear);
                        this.SchoolYearViewModels.Add(schoolYearViewModel);
                        schoolYearViewModel.Refresh();

                    }


                }
                return this.schoolYearViewModels;
            }
        }

        public ICommand DeleteCommand
        {
            get;set;
        }

        public ICommand NewCommand
        {
            get; set;
        }

        public void CommandDelete(object param)
        {
            MeineNoten.Class.MeineNoten.Database.Tables["Grades"].Rows.Remove((DataRow)((MarkViewModel)param).Model);
            MeineNoten.Class.MeineNoten.Database.Tables["Grades"].AcceptChanges();
            this.SchoolYearViewModels.Current.SubjectViewModels.Current.MarkViewModels.Remove((MarkViewModel)param);
        }

        public bool CanExecuteDelete(object param)
        {
            return param != null;
        }

        public void CommandNew(object param)
        {
            var schoolYearSubject = (DataRow)SchoolYearViewModels.Current.SubjectViewModels.Current.Model;


            var dataRowMarkViewModel = MeineNoten.Class.MeineNoten.Database.Tables["Grades"].NewRow();
            dataRowMarkViewModel["SSID"] = schoolYearSubject["SSID"];

            var markViewModel = new MarkViewModel(dataRowMarkViewModel);

            Window newGradeWindow = new NewGradeWindow(SchoolYearViewModels.Current, markViewModel);
            newGradeWindow.ShowDialog();
        }

        public bool CanExecuteNew(object param)
        {
            return true;
        }
    }
}
