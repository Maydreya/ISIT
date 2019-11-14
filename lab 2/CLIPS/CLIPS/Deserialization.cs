using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIPS
{
    class Deserialization
    {
        
        //Чтение из json файла
        public void Deserealize(string filename, Rules myCollection)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                KnowledgeBase.Rules.Clear();
                KnowledgeBase.Results.Clear();
                myCollection.Rule = null;
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);
                myCollection = JsonConvert.DeserializeObject<Rules>(json);
                for (int i = 0; i < myCollection.Rule.Length; i++)
                {
                    KnowledgeBase.Rules.Add(myCollection.Rule[i]);
                    if (myCollection.Rule[i].assert.Contains("repair")) KnowledgeBase.Results.Add(myCollection.Rule[i]);
                    
                }
            }
        }
    }
}
