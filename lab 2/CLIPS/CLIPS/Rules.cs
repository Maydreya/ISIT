using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CLIPS
{
    //Класс правил, для десерилизации json
    public class Rules
    {
        [JsonProperty("rules")]
        public RulesItem[] Rule { get; set; }
    }
    public class RulesItem
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("condition")]
        public string condition { get; set; }
        [JsonProperty("assert")]
        public string assert { get; set; }
    }
}
