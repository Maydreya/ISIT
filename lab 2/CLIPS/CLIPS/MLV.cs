using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CLIPS
{
    class MLV
    {
        EC ec = new EC();
        List<string> Conditions = new List<string>();
        List<string> rulConditions = new List<string>();
        string[] rulCondition;
        string[] andrulConditions;
        string[] orrulConditions;
        string[] orConditions;
        string[] Condition;
        string[] andConditions;
        public string Text(TextBox textbox)
        {
            string answer = textbox.Text;
            return answer;
        }
        public void ChangeState(int state, RichTextBox rich, TextBox textBox)
        {
            switch (state)
            {
                case 0:
                    RunState(rich, textBox);
                    break;
                case 1:
                    ResetState();
                    break;
                case 2:
                    ObrState(rich);
                    break;
            }
        }
        //Проверяем прошло правило проверку или нет, если да, то добавляем, если с вопросом, то выводим на экран
        public void RunState(RichTextBox richTextBox, TextBox textBox)
        {
            if (KnowledgeBase.count < KnowledgeBase.Rules.Count)
            {
                if (CheckRule(KnowledgeBase.count) == true)
                {
                    if (KnowledgeBase.Rules[KnowledgeBase.count].assert.Contains("repair"))
                    {
                        KnowledgeBase.result = true;
                        Match match = Regex.Match(KnowledgeBase.Rules[KnowledgeBase.count].assert, @"(?<=')[^']*(?=')");
                        if (match.Success)
                        {
                            KnowledgeBase.resultat = match.Groups[0].Value + Environment.NewLine;
                        }
                    }
                    //else if (KnowledgeBase.Rules[KnowledgeBase.count].assert.Contains("?")) Write(richTextBox, textBox);
                    else if (KnowledgeBase.Rules[KnowledgeBase.count].assert.Contains("?") == false) WM.ActiveFacts.Add(KnowledgeBase.Rules[KnowledgeBase.count].assert);
                    CheckRules();
                    Write(richTextBox, textBox);
                    KnowledgeBase.count++;
                }
                else
                {
                    KnowledgeBase.count++;
                    RunState(richTextBox, textBox);
                }
            }
            else
            {
                CheckRules();
                Write(richTextBox, textBox);
            }
        }
        //Проверка правила на условия, для вывода его на экран
        public bool CheckRule(int count)
        {
            int i = 0;
            rulConditions.Clear();
            //Добавляем условия в rulConditions
            if (KnowledgeBase.Rules[count].condition.Contains(","))
            {
                rulCondition = KnowledgeBase.Rules[count].condition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int u = 0; u < rulCondition.Count(); u++)
                {
                    rulCondition[u] = rulCondition[u].TrimStart(' ');
                    rulConditions.Add(rulCondition[u]);
                }
            }
            else rulConditions.Add(KnowledgeBase.Rules[count].condition);
            //Сверяем условия факта с ограничениями
            for (int f = 0; f < rulConditions.Count(); f++)
            {
                if (rulConditions[f].Contains("not"))
                {
                    rulConditions[f].Remove(0, 4);
                    if (WM.ActiveFacts.Contains(rulConditions[f]))
                    {
                        i++;
                    }
                    else
                    {
                        rulConditions[f].Remove(0, 4);
                        if (WM.ActiveFacts.Contains(rulConditions[f]) == false) i++;
                    }
                }
                else if (rulConditions[f].Contains("or"))
                {
                    orrulConditions = null;
                    rulConditions[f] = rulConditions[f].Remove(0, 3);
                    rulConditions[f] = rulConditions[f].Trim(')');
                    orrulConditions = rulConditions[f].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int q = 0; q < orrulConditions.Count(); q++)
                    {
                        orrulConditions[q] = orrulConditions[q].TrimStart(' ');
                        if (WM.ActiveFacts.Contains(orrulConditions[q]))
                        {

                            i++;
                            break;
                        }
                        else if (WM.ActiveFacts.Contains(orrulConditions[q]))
                        {
                            i++;
                            break;
                        }
                    }
                }
                else if (rulConditions[f].Contains("and"))
                {
                    rulConditions[f] = rulConditions[f].Remove(0, 4);
                    rulConditions[f] = rulConditions[f].Trim(')');
                    andrulConditions = rulConditions[f].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int q = 0; q < andrulConditions.Count(); q++)
                    {
                        andrulConditions[q] = andrulConditions[q].TrimStart(' ');
                        if (WM.ActiveFacts.Contains(andrulConditions[q])) KnowledgeBase.okay = true;
                        else if (WM.ActiveFacts.Contains(andrulConditions[q])) KnowledgeBase.okay = true;
                        else
                        {
                            KnowledgeBase.okay = false;
                            break;
                        }
                    }
                    if (KnowledgeBase.okay)
                    {
                        i++; KnowledgeBase.okay = false;
                    }
                }
                else if (rulConditions[f].Contains("yes") || rulConditions[f].Contains("no"))
                {
                    for (int t = 0; t < WM.ActiveFacts.Count; t++)
                    {
                        if (WM.ActiveFacts[t].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(0).Contains(rulCondition[f].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(0)) == true)
                        {
                            if (WM.ActiveFacts.Contains(rulConditions[f]))
                            {

                                i++;
                            }
                        }
                    }
                }
                else if (rulConditions[f].Contains("<="))
                {
                    for (int t = 0; t < WM.ActiveFacts.Count; t++)
                    {
                        double num;
                        if (double.TryParse(WM.ActiveFacts[t].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(1), out num))
                        {
                            if (Convert.ToInt32(WM.ActiveFacts[t].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(1)) <= Convert.ToInt32(rulConditions[f].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(2))) i++;
                            break;
                        }
                    }
                }
                else if (rulConditions[f].Contains(">"))
                {
                    for (int t = 0; t < WM.ActiveFacts.Count; t++)
                    {
                        double num;
                        if (double.TryParse(WM.ActiveFacts[t].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(1), out num))
                        {
                            if (Convert.ToInt32(WM.ActiveFacts[t].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(1)) > Convert.ToInt32(rulConditions[f].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(2))) i++;
                            break;
                        }
                    }
                }
                else if (rulConditions[f].Contains("")) i++;
            }
            if (i == rulConditions.Count && WM.ActiveFacts.Contains(KnowledgeBase.Rules[count].assert) == false)
            {
                return true;
            }
            else return false;

        }
        //Проверка правил, после занесения нового факта, на условия
        public void CheckRules()
        {
            for (int i = 0; i < KnowledgeBase.Rules.Count; i++)
            {
                if (CheckRule(i))
                    if (KnowledgeBase.Rules[i].assert.Contains("repair"))
                    {
                        KnowledgeBase.result = true;
                        Match match = Regex.Match(KnowledgeBase.Rules[i].assert, @"(?<=')[^']*(?=')");
                        if (match.Success)
                        {
                            KnowledgeBase.resultat = match.Groups[0].Value + Environment.NewLine;
                            break;
                        }
                    }
                    //else if (KnowledgeBase.Rules[KnowledgeBase.count].assert.Contains("?")) Write(richTextBox, textBox);
                    else if (KnowledgeBase.Rules[i].assert.Contains("?") == false) WM.ActiveFacts.Add(KnowledgeBase.Rules[i].assert);
            }
        }
        //Вывод на экран
        public void Write(RichTextBox richTextBox, TextBox textBox)
        {
            if (KnowledgeBase.result != true && KnowledgeBase.count < KnowledgeBase.Rules.Count)
            {
                Match match = Regex.Match(KnowledgeBase.Rules[KnowledgeBase.count].assert, @"(?<=')[^']*(?=')");
                if (match.Success)
                {
                    richTextBox.Text += match.Groups[0].Value + Environment.NewLine;
                }
                KnowledgeBase.ready = false;
                Text(textBox);
            }
            else if (KnowledgeBase.result)
            {
                richTextBox.Text += "Suggested: " + Environment.NewLine + KnowledgeBase.resultat + Environment.NewLine;
            }
            else KnowledgeBase.count = 0;
        }

        //Кнопка Reset - обнуление всех данных
        public void ResetState()
        {
            WM.ActiveFacts.Clear();
            KnowledgeBase.count = 0;
            KnowledgeBase.ready = false;
            KnowledgeBase.result = false;
            KnowledgeBase.resultat = null;
            EC.k = 1;
        }
        //Обрытный ход
        public void ObrState(RichTextBox rich)
        {
            Condition = KnowledgeBase.Results[KnowledgeBase.res - 1].condition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int u = 0; u < Condition.Count(); u++)
            {
                if (Condition[u].Contains("or"))
                {
                    Condition[u] = Condition[u].Remove(0, 3);
                    Condition[u] = Condition[u].Trim(')');
                    orConditions = Condition[u].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < orConditions.Count(); k++)
                        Conditions.Add(orConditions[k]);
                }
                else if (Condition[u].Contains("and"))
                {
                    Condition[u] = Condition[u].Remove(0, 4);
                    Condition[u] = Condition[u].Trim(')');
                    andConditions = Condition[u].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < andConditions.Count(); k++)
                        Conditions.Add(Conditions[k]);
                }
                else
                {
                    Condition[u] = Condition[u].TrimStart(' ');
                    Conditions.Add(Condition[u]);
                }

            }
            for (int i = 0; i < KnowledgeBase.Rules.Count; i++)
            {
                for (int j = 0; j < Conditions.Count; j++)
                {

                    if (KnowledgeBase.Rules[i].assert.Contains(Conditions[j]))
                    {
                        rulCondition = KnowledgeBase.Rules[i].condition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int u = 0; u < rulCondition.Count(); u++)
                        {
                            rulCondition[u] = rulCondition[u].TrimStart(' ');
                            rulConditions.Add(rulCondition[u]);
                        }
                    }
                }
            }
            for (int h = 0; h < rulConditions.Count; h++)
            {
                rich.Text += rulConditions[h] + "\n";
            }
            rulConditions.Clear();
            Conditions.Clear();
        }
    }
}
