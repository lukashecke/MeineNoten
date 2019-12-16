using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineNoten.Model
{
    public class Grade
    {
        public Grade(String text, int value)
        {
            this.Title = text;
            this.Value = value;
        }

        public int Value { get; set; }

        public String Title { get; set; }
    }
}
