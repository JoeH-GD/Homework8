using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace TrueFalseGame
{
    public class TrueFalse
    {
        #region fields
        private string fileName;
        private List<Questions> list;
        #endregion

        #region Properties
        public int Count { get { return list.Count; } }

        public Questions this[int index] { get { return list[index]; } }
        #endregion 

        #region constructors
        public TrueFalse(string fileName)
        {
            this.fileName = fileName;
            list = new List<Questions>();
            
        }
        #endregion 

        #region public methods
        public void Add (string text, bool trueFalse)
        {
            list.Add(new Questions(text, trueFalse));
        }

        public void Remove (int index)
        {
            if (list != null && index < list.Count && index>=0)
            {
                list.RemoveAt(index);
            }
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Questions>));
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            list = (List<Questions>)serializer.Deserialize(stream);
            stream.Close();
        }
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Questions>));
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            serializer.Serialize(stream, list);
            stream.Close();
        }

        #endregion 
    }

}
