using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten.Class
{
    public static class MeineNoten
    {
        private static String path = @"C:\ProgramData\Meine Noten\MeineNoten.xml";

        private static DataSet database;
        public static DataSet Database
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

        public static void LoadData()
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    MeineNoten.Database.ReadXml(path);
                }
                else
                {
                    CreateData();
                }

            }
            catch (Exception)
            {
                // Bei Erststart des Programms wird das Laden hier problemlos übersprungen
            }
        }

        public static void CreateData()
        {
            try
            {

   


                #region SchoolYears
                var dataTable = new DataTable("SchoolYears");
                var dataColumn = new DataColumn("SYID");
                dataTable.Columns.Add(dataColumn);
                dataTable.PrimaryKey = new DataColumn[] { dataColumn };
                dataTable.Columns.Add(new DataColumn("Title"));
                MeineNoten.Database.Tables.Add(dataTable);

                var dataRow = dataTable.NewRow();
                dataRow["SYID"] = 1;
                dataRow["Title"] = "2019/20";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SYID"] = 2;
                dataRow["Title"] = "2020/21";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SYID"] = 3;
                dataRow["Title"] = "2021/22";
                dataTable.Rows.Add(dataRow);
                #endregion

                #region Subjects
                dataTable = new DataTable("Subjects");
                dataColumn = new DataColumn("SUID");
                dataTable.Columns.Add(dataColumn);
                dataTable.PrimaryKey = new DataColumn[] { dataColumn };
                dataTable.Columns.Add(new DataColumn("Title"));
                MeineNoten.Database.Tables.Add(dataTable);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 1;
                dataRow["Title"] = "Anwendungsentwicklung und Programmierung";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 2;
                dataRow["Title"] = "IT-Systeme";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 3;
                dataRow["Title"] = "Vernetzte Systeme";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 4;
                dataRow["Title"] = "Betriebswirtschaftliche Prozesse";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 5;
                dataRow["Title"] = "Sozialkunde";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 6;
                dataRow["Title"] = "Deutsch";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 7;
                dataRow["Title"] = "Englisch";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 8;
                dataRow["Title"] = "Ethik";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 9;
                dataRow["Title"] = "Sport";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SUID"] = 10;
                dataRow["Title"] = "Crimpen und Löten";
                dataTable.Rows.Add(dataRow);
                
                #endregion

                #region SchoolYearSubjects
                dataTable = new DataTable("SchoolYearSubjects");
                dataColumn = new DataColumn("SSID");
                dataTable.Columns.Add(dataColumn);
                dataTable.PrimaryKey = new DataColumn[] { dataColumn };
                dataTable.Columns.Add(new DataColumn("SYID"));
                dataTable.Columns.Add(new DataColumn("SUID"));
                MeineNoten.Database.Tables.Add(dataTable);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 1;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 1;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 2;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 2;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 3;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 3;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 4;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 4;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 5;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 5;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 6;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 6;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 7;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 7;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 8;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 8;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 9;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 9;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 10;
                dataRow["SYID"] = 1;
                dataRow["SUID"] = 10;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 11;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 1;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 12;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 2;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 13;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 3;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 14;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 4;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 15;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 5;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 16;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 6;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 17;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 7;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 18;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 8;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 19;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 9;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 20;
                dataRow["SYID"] = 2;
                dataRow["SUID"] = 10;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 21;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 1;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 22;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 2;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 23;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 3;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 24;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 4;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 25;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 5;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 26;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 6;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 27;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 7;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 28;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 8;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 29;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 9;
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["SSID"] = 30;
                dataRow["SYID"] = 3;
                dataRow["SUID"] = 10;
                dataTable.Rows.Add(dataRow);
                #endregion

                dataTable = new DataTable("Grades");
                //dataColumn = new DataColumn("GRID");
                //dataTable.Columns.Add(dataColumn);
                //dataTable.PrimaryKey = new DataColumn[] { dataColumn };
                dataTable.Columns.Add(new DataColumn("SSID"));
                dataTable.Columns.Add(new DataColumn("Note", typeof(int)));
                dataTable.Columns.Add(new DataColumn("Art", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Date", typeof(DateTime)));
                dataTable.Columns.Add(new DataColumn("Gewichtung", typeof(int)));
                MeineNoten.Database.Tables.Add(dataTable);
            }
            catch (Exception ex)
            {
                // Bei Erststart des Programms wird das Laden hier problemlos übersprungen
            }
        }

        public static void SaveData()
        {
            try
            {
                if (!System.IO.Directory.Exists(@"C:\ProgramData\Meine Noten"))
                {
                CreateDirectory();
                }

                MeineNoten.Database.WriteXml(path, XmlWriteMode.WriteSchema);

            }
            catch (Exception ex)
            {
                // Bei Erststart des Programms wird das Laden hier problemlos übersprungen
            }
        }
        // [PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
        private static void CreateDirectory()
        {
            Directory.CreateDirectory(@"C:\ProgramData\Meine Noten");

        }
    }
}
