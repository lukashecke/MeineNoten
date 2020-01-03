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


        public Data()
        {
            var dataTable = CreateTable();
            Database.Tables.Add(dataTable);
        }

        public DataRow InsertDataRow(String fach, String note, String gewichtung, String date, String art)
        {

            var dataRow = this.dataTable.NewRow();
            dataRow["Fach"] = fach;
            dataRow["Note"] = note;
            dataRow["Gewichtung"] = gewichtung;
            dataRow["Art"] = art;
            dataRow["Datum"] = date;
            this.dataTable.Rows.Add(dataRow);

            return dataRow;
        }

        public DataTable CreateTable()
        {
            this.dataTable = new DataTable("Noteninfo");
            dataTable.Columns.Add("Fach");
            dataTable.Columns.Add("Note");
            dataTable.Columns.Add("Gewichtung");
            dataTable.Columns.Add("Art");
            dataTable.Columns.Add("Datum");

            return dataTable;
        }
    }
}
