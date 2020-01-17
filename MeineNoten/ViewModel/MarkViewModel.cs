using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten.ViewModel
{
    public class MarkViewModel : ViewModelBase
    {
        
        public int Note
        {
            get
            {
                if (((DataRow)Model)["Note"] == DBNull.Value)
                    return 0;
                else
                    return (int)((DataRow)Model)["Note"];
            }
            set
            {
                ((DataRow)Model)["Note"] = value;
                this.OnPropertyChanged("Note");
            }
        }

        private string art;
        public string Art
        {
            get
            {
                if (((DataRow)Model)["Art"] == DBNull.Value)
                    return String.Empty;
                else
                    return ((DataRow)Model)["Art"].ToString(); }
            set
            {
                ((DataRow)Model)["Art"] = value;
                this.OnPropertyChanged("Art");
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                if (((DataRow)Model)["Date"] == DBNull.Value)
                    return DateTime.Now;
                else
                    return Convert.ToDateTime(((DataRow)Model)["Date"]);
            }
            set
            {
                ((DataRow)Model)["Date"] = value;
                this.OnPropertyChanged("Date");
            }
        }

        private int gewichtung;
        public int Gewichtung
        {
            get
            {
                if (((DataRow)Model)["Gewichtung"] == DBNull.Value)
                    return 0;
                else
                    return (int)((DataRow)Model)["Gewichtung"];
            }
            set
            {
                ((DataRow)Model)["Gewichtung"] = value;
                this.OnPropertyChanged("Gewichtung");
            }
        }

        public MarkViewModel(object model)
        {
            this.Model = model;
        }

        public object Model { get; set; }
    }
}
