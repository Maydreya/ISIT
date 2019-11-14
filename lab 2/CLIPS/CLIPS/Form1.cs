using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CLIPS
{
    public partial class Form1 : Form
    {
        string filename;
        Rules myCollection = new Rules();
        Deserialization deserial = new Deserialization();
        MLV mlv = new MLV();
        EC ec = new EC();
        double num;
        public Form1()
        {
            InitializeComponent();
        }

        private void загрузитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string result = string.Empty;
                deserial.Deserealize(filename, myCollection);
            }

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "(run)" + Environment.NewLine;
            richTextBox1.Text += ec.Watch(richTextBox1) + Environment.NewLine;
            mlv.ChangeState(0, richTextBox1, textBox2);
            KnowledgeBase.obr = false;
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            textBox2.Select();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            richTextBox1.Text += "(reset)" + Environment.NewLine;
            mlv.ChangeState(1,richTextBox1, textBox2);
            KnowledgeBase.obr = false;
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            textBox2.Select();
        }

        private void stepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "(Обратный ход)" + Environment.NewLine + "Введите номер результата, который хотите получить" + Environment.NewLine;
            KnowledgeBase.obr = true;
            for (int i = 0; i < KnowledgeBase.Results.Count; i++)
                richTextBox1.Text += Convert.ToString(i+1) + ") " + KnowledgeBase.Results[i].name + "\n";
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            textBox2.Select();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (KnowledgeBase.obr == false)
                {
                    if (textBox2.Text == "yes" || textBox2.Text == "no")
                    {
                        //Добавление факта с yes, no
                        WM.ActiveFacts.Add(KnowledgeBase.Rules[KnowledgeBase.count - 1].assert.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(0) + " " + textBox2.Text);
                        richTextBox1.Text += textBox2.Text + Environment.NewLine;
                        richTextBox1.Text += ec.Watch(richTextBox1) + Environment.NewLine;
                        mlv.ChangeState(0, richTextBox1, textBox2);
                    }
                    else if (double.TryParse(textBox2.Text, out num))
                    {
                        //Добавление факта с числом
                        WM.ActiveFacts.Add(KnowledgeBase.Rules[KnowledgeBase.count - 1].assert.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ElementAt(0) + " " + textBox2.Text);
                        richTextBox1.Text += textBox2.Text + Environment.NewLine;
                        richTextBox1.Text += ec.Watch(richTextBox1) + Environment.NewLine;
                        mlv.ChangeState(0, richTextBox1, textBox2);
                    }
                    else MessageBox.Show("Ввести можно только число, yes и no");
                }
                else
                {
                    //Обратный ход
                    KnowledgeBase.res = Convert.ToInt32(textBox2.Text);
                    mlv.ChangeState(2, richTextBox1, textBox2);
                }
                textBox2.Clear();
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
                textBox2.Select();
            }
        }

        private void watchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }
    }
}
