using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIPS
{
    class EC
    {
        public static bool fr;
        public static int k = 1;
        public static string message, message2;
        string[] facts;
        public string Watch(RichTextBox richtextbox)
        {
            if (fr)
            {
                
                if (KnowledgeBase.count < KnowledgeBase.Rules.Count)
                {
                    message = message2 + "\n" + "     " + Convert.ToString(k) + " " + KnowledgeBase.Rules[KnowledgeBase.count].name + ": ";
                    facts = KnowledgeBase.Rules[KnowledgeBase.count].condition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < facts.Count(); i++)
                    {
                        if (WM.ActiveFacts.Count > 0)
                        {
                            for (int j = 0; j < WM.ActiveFacts.Count; j++)
                            {
                                if (j == WM.ActiveFacts.Count - 1)
                                {
                                    if (WM.ActiveFacts[j].Contains(facts[i]))
                                    {
                                        message += facts[i];
                                        break;
                                    }
                                }

                                else if (WM.ActiveFacts[j].Contains(facts[i]))
                                {
                                    message += facts[i] + ",";
                                    break;
                                }
                            }
                        }
                        if (i == facts.Count() - 1) message += "*";
                        else message += "*,";
                    }
                    k++;
                    message2 = null;
                }
            }
            return message;
        }
    }
}
