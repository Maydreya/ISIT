using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab_3
{
    class MLV
    {
        List<string> Nodes = new List<string>();
        string[] Node;
        string name;
        public void Run(bool k, RichTextBox richTextBox)
        {
            name = KnowledgeBase.startname;
            Nodes.Clear();
            Nodes.Add(name);
            if (k)
            {
                CheckNodes(richTextBox);
            }
            else
            {
                CheckNodesObr(richTextBox);
            }
        }
        public bool Check(string name)
        {
            bool k = false;
            for (int i = 0; i < KnowledgeBase.Nodes.Count; i++)
            {
                if (KnowledgeBase.Nodes[i].name == name)
                {
                    k = true;
                    KnowledgeBase.count++;
                }
            }
            return k;

        }
        //Прямой ход
        public void CheckNodes(RichTextBox text)
        {
            for (int j = 0; j < Nodes.Count; j++)
            {
                name = Nodes[j];
                for (int i = 0; i < KnowledgeBase.Nodes.Count; i++)
                {
                    if (j == 0 && KnowledgeBase.Nodes[i].name.Contains(name) && KnowledgeBase.Nodes[i].bind.Contains(KnowledgeBase.bind))
                    {
                        text.Text += KnowledgeBase.Nodes[i].name + " " + KnowledgeBase.Nodes[i].bind + " " + KnowledgeBase.Nodes[i].node + Environment.NewLine;
                        if (KnowledgeBase.Nodes[i].node.Contains(","))
                        {
                            Node = KnowledgeBase.Nodes[i].node.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int u = 0; u < Node.Count(); u++)
                            {
                                Node[u] = Node[u].TrimStart(' ');
                                Nodes.Add(Node[u]);
                            }
                        }
                        else
                        {
                            Nodes.Add(KnowledgeBase.Nodes[i].node);
                        }
                    }
                    else if(KnowledgeBase.Nodes[i].name.Contains(name) && name != KnowledgeBase.startname)
                    {
                        text.Text += KnowledgeBase.Nodes[i].name + " " + KnowledgeBase.Nodes[i].bind + " " + KnowledgeBase.Nodes[i].node + Environment.NewLine;
                        if (KnowledgeBase.Nodes[i].node.Contains(","))
                        {
                            Node = KnowledgeBase.Nodes[i].node.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int u = 0; u < Node.Count(); u++)
                            {
                                Node[u] = Node[u].TrimStart(' ');
                                Nodes.Add(Node[u]);
                            }
                        }
                        else
                        {
                            Nodes.Add(KnowledgeBase.Nodes[i].node);
                        }
                    }
                }
            }
        }
        //Обратный ход
        public void CheckNodesObr(RichTextBox text)
        {
            for (int j = 0; j < Nodes.Count; j++)
            {
                name = Nodes[j];
                for (int i = 0; i < KnowledgeBase.Nodes.Count; i++)
                {
                    if (KnowledgeBase.Nodes[i].node.Contains(name))
                    {
                        text.Text += KnowledgeBase.Nodes[i].name + " " + KnowledgeBase.Nodes[i].bind + " " + KnowledgeBase.Nodes[i].node + Environment.NewLine;
                        Nodes.Add(KnowledgeBase.Nodes[i].name);
                    }
                }
            }
        }

    }
}
