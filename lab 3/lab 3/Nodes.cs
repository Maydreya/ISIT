using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lab_3
{
    public class Nodes
    {
        [JsonProperty("semka")]
        public NodesItem[] Node { get; set; }
    }
    public class NodesItem
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("bind")]
        public string bind{ get; set; }
        [JsonProperty("node")]
        public string node { get; set; }
    }
}

