using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten
{
    public class Data
    {
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


      

    }
}
