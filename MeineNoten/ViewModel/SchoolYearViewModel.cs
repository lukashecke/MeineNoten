using MeineNoten.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten.ViewModel
{
    public class SchoolYearViewModel : ViewModelBase
    {
        public SchoolYearViewModel(object model) : base()
        {
            this.Model = model;
        }

        public object Model { get; set; }

        /// <summary>
        /// Refresh der Gesamtnote
        /// </summary>
        public void Refresh()
        {
            // Endnote verwendet die gerundeten Fachnoten
            var noten = this.SubjectViewModels.Cast<SubjectViewModel>().Where(element => element.Note > 0).Select(ele=> Math.Round(ele.Note)).AsEnumerable();


            if (noten.Count() == 0)
            {
                this.Note = 0;
            }
            else
            {
                this.Note = Enumerable.Average(noten);
            }
        }

        private void SchoolYearViewModel_SubjectMarkChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
        public String Title
        {
            get
            {
                return ((DataRow)Model)["Title"].ToString();
            }
        }


        private double note;
        public Double Note
        {
            get
            {
                return this.note;
            }
            set
            {
                this.note = value;
                this.OnPropertyChanged("Note");
            }
        }

        private ObservableCollectionEx<SubjectViewModel> subjectViewModels;
        public ObservableCollectionEx<SubjectViewModel> SubjectViewModels
        {
            get
            {
                if (this.subjectViewModels == null)
                {
                    this.subjectViewModels = new ObservableCollectionEx<SubjectViewModel>();
                    foreach(DataRow dataRowSchoolYearSubject in MeineNoten.Class.MeineNoten.Database.Tables["SchoolYearSubjects"].Select(String.Format("SYID='{0}'", ((DataRow)Model)["SYID"])))
                    {
                        var subjectViewModel = new SubjectViewModel(dataRowSchoolYearSubject);
                        subjectViewModel.SubjectMarkChanged += SchoolYearViewModel_SubjectMarkChanged;
                        this.SubjectViewModels.Add(subjectViewModel);

                        subjectViewModel.Refresh();
                    }
                }
                return this.subjectViewModels;
            }
        }

        private void SubjectViewModel_SubjectMarkChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
