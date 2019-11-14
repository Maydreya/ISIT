using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIPS
{
    static class KnowledgeBase
    {
        
        //Список результатов для обратного хода
        public static List<RulesItem> Results = new List<RulesItem>();
        //Список правил
        public static List<RulesItem> Rules = new List<RulesItem>();
        //Переменная, в которую будет записан результат
        public static string resultat;
        public static int count = 0, res;
        public static bool ready = false, result = false, okay = false, obr = false;
    }
}
