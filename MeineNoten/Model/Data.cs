using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten
{
    public class Data
    {
        public DataSet database;
        public DataSet Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataSet("MeineNoten");
                }

                return database;
            }
        }

        public DataTable dataTable=new DataTable();

        private List<int> apGrades;
        public List<int> APGrades
        {
            get
            {
                if (apGrades == null)
                {
                    apGrades = new List<int>();
                }

                return apGrades;
            }
        }

        // Umschreiben!
        public List<int> ITGrades { get; set; }
        public List<int> VSGrades { get; set; }
        public List<int> BWGrades { get; set; }
        public List<int> SKGrades { get; set; }
        public List<int> DEGrades { get; set; }
        public List<int> ENGrades { get; set; }
        public List<int> ETGrades { get; set; }
        public List<int> CLGrades { get; set; }

        public Data()
        {
            var dataTable = CreateTable();
            Database.Tables.Add(dataTable);
        }

        public DataRow InsertDataRow(String fach, String note, String gewichtung)
        {

            var dataRow = this.dataTable.NewRow();
            dataRow["Fach"] = fach;
            dataRow["Note"] = note;
            dataRow["Gewichtung"] = gewichtung;
            this.dataTable.Rows.Add(dataRow);

            return dataRow;
        }

        public DataTable CreateTable()
        {
            this.dataTable = new DataTable("DataTable");
            dataTable.Columns.Add("Fach");
            dataTable.Columns.Add("Note");
            dataTable.Columns.Add("Gewichtung");

            return dataTable;
        }



    }
}
