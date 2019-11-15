using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lab_3
{
    class Deserialization
    {
        //Чтение из json файла
        public void Deserealize(string filename, Nodes myCollection)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                KnowledgeBase.Nodes.Clear();
                myCollection.Node = null;
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);
                myCollection = JsonConvert.DeserializeObject<Nodes>(json);
                for (int i = 0; i < myCollection.Node.Length; i++)
                {
                    KnowledgeBase.Nodes.Add(myCollection.Node[i]);
                }
            }
        }
    }
}
