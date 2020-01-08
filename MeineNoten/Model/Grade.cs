using MeineNoten.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten.Model
{
    public class Grade : ViewModelBase
    {
        #region properties
        public int Value { get; set; }
        public String Title { get; set; }
        public string Fach { get; set; }
        public string Art { get; set; }
        public string Date { get; set; }
        public string Gewichtung { get; set; }
        public string Schuljahr { get; set; }
        #endregion

        #region entities
        private string totalGrade;
        public string TotalGrade
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

        private string note;
        public string Note
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
        #endregion

        #region constructors
        public Grade(String text, int value)
        {
            this.Title = text;
            this.Value = value;
        }
        public Grade(string fach, string art, string date, string note, string gewichtung, string schuljahr)
        {
            this.Schuljahr = schuljahr;
            this.Fach = fach;
            this.Art = art;
            this.Date = date;
            this.Note = note;
            this.Gewichtung = gewichtung;
        }
        #endregion
    }
}
