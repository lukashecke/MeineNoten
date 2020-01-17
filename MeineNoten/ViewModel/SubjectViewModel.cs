using MeineNoten.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten.ViewModel
{
    public class SubjectViewModel : ViewModelBase
    {
        public SubjectViewModel(object model)
        {
            this.Model = model;

            this.MarkViewModels.CollectionChanged += MarkViewModels_CollectionChanged;
        }

        public object Model
        {
            get;set;
        }
        
        public void Refresh()
        {
            List<double> noten = new List<double>();
            foreach( var markViewModel in this.MarkViewModels)
            {
                noten.Add(markViewModel.Note);
                if (markViewModel.Gewichtung == 1)
                {
                    noten.Add(markViewModel.Note);
                }
            }
            if (noten.Count() == 0)
            {
                this.Note = 0;
            }
            else
            {
                this.Note = Enumerable.Average(noten);
            }
            if (this.SubjectMarkChanged != null)
            {
                this.SubjectMarkChanged(this, null);
            }
        }



        public event EventHandler SubjectMarkChanged;

        private void MarkViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Refresh();
        }

        public String Title
        {
            get
            {
                return MeineNoten.Class.MeineNoten.Database.Tables["Subjects"].Select(String.Format("SUID='{0}'", ((DataRow)Model)["SUID"])).First()["Title"].ToString();
            }
        }

        private Double note;
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

        private ObservableCollectionEx<MarkViewModel> markViewModels;
        public ObservableCollectionEx<MarkViewModel> MarkViewModels
        {
            get
            {
                if (this.markViewModels == null)
                {
                    this.markViewModels = new ObservableCollectionEx<MarkViewModel>();
                    foreach(DataRow dataRowGrade in MeineNoten.Class.MeineNoten.Database.Tables["Grades"].Select(String.Format("SSID='{0}'", ((DataRow)Model)["SSID"])))
                    {
                        this.markViewModels.Add(new MarkViewModel(dataRowGrade));
                    }
                }
                return this.markViewModels;
            }
        }
    }
}
