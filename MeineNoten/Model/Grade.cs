using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten.Model
{
    public class Grade
    {
        #region properties
        public int Value { get; set; }
        public String Title { get; set; }
        public string Fach { get; set; }
        public string Art { get; set; }
        public string Date { get; set; }
        public string Note { get; set; }
        public string Gewichtung { get; set; }
        #endregion

        #region constructors
        public Grade(String text, int value)
        {
            this.Title = text;
            this.Value = value;
        }
        public Grade(string fach, string art, string date, string note, string gewichtung)
        {
            this.Fach = fach;
            this.Art = art;
            this.Date = date;
            this.Note = note;
            this.Gewichtung = gewichtung;
        }
        #endregion
    }
}
