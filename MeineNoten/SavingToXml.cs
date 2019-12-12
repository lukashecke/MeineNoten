using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace MeineNoten
{
    class SavingToXml
    {
        public static void Save(object obj, string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            TextWriter textWriter = new StreamWriter(filename);
            xmlSerializer.Serialize(textWriter, obj);
            textWriter.Close();
        }
    }
}
