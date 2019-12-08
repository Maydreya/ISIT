﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace decision_making
{
    public partial class Form1 : Form
    {
        models mod = new models();
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int b = Convert.ToInt32(label5.Text);
            if (b < Data.experts)
            {
                mod.Bolshinstvo(listBox1.SelectedIndex);
                for (int i = 0; i < mod.votes.Length; i++)
                    dataGridView1.Rows[i].Cells[2].Value = mod.votes[i];
                for (int i = 0; i < mod.percents.Length; i++)
                    dataGridView1.Rows[i].Cells[3].Value = (Math.Round(mod.percents[i], 4) * 100).ToString() + "%";
                b++;
                if(b <= Data.experts)
                label5.Text = b.ToString();
            }

            label13.Text = mod.wins[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1 != null && textBox2 != null)
            {
                Clear();
                Data.alts = Convert.ToInt32(textBox2.Text);
                Data.experts = Convert.ToInt32(textBox1.Text);
                for (int i = 0; i < Data.alts; i++)
                {
                    dataGridView1.Rows.Add(i + 1, "alternative " + (i+1), "0", "0%");
                    dataGridView2.Columns.Add("alternative"+(i+1),"alternative " + (i + 1));
                    dataGridView5.Columns.Add("alternative" + (i + 1), "alternative " + (i + 1));
                    dataGridView7.Columns.Add("alternative" + (i + 1), "alternative " + (i + 1));
                    dataGridView9.Columns.Add("alternative" + (i + 1), "alternative " + (i + 1));
                    listBox1.Items.Add("alternative " + (i+1));
                }
                for(int i = 0; i < Data.experts; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView5.Rows.Add();
                    dataGridView7.Rows.Add();
                    dataGridView9.Rows.Add();
                }
                mod.ClearVotes();
                mod.CreateVotesMass(listBox1.Items.Count);
                label5.Text = "1";
                label6.Text = "Победитель: ";
                label9.Text = "Победитель: ";
            }
            else
            {
                MessageBox.Show("Заполните все столбцы");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            listBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView5.Columns.Clear();
            dataGridView6.Rows.Clear();
            dataGridView7.Rows.Clear();
            dataGridView7.Columns.Clear();
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            dataGridView9.Columns.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //передача в массив
            int[,] array = new int[Data.experts, Data.alts];
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    array[i, j] = Convert.ToInt32(dataGridView2[j, i].Value);
                }
            }

            //решение
            List<string> reshenie = mod.yavn(array);

            //загружаем решение
            for (int k = 0; k < reshenie.Count; k++)
            {
                dataGridView3.Rows.Add();
                dataGridView3[0, k].Value = reshenie[k];
            }
            label6.Text = label6.Text + " " + mod.win;


        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[,] array = new int[Data.experts, Data.alts];
            for (int i = 0; i < dataGridView9.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView9.ColumnCount; j++)
                {
                    array[i, j] = Convert.ToInt32(dataGridView9[j, i].Value);
                }
            }
            int[] alter = mod.board(array);
            int max, maxi;
            for (int i = 0; i < alter.Length; i++)
            {
                dataGridView8.Rows.Add(alter[i].ToString());
            }

            mod.maximum(alter);
            for (int i = 0; i < mod.wins.Count; i++)
                label11.Text = label11.Text + "alternative " + mod.wins[i] + " ";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[,] array = new int[Data.experts, Data.alts];
            for (int i = 0; i < dataGridView7.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView7.ColumnCount; j++)
                {
                    array[i, j] = Convert.ToInt32(dataGridView7[j, i].Value);
                }
            }
            int[] alter = mod.simpson(array);

            for (int k = 0; k < alter.GetLength(0); k++)
            {
                dataGridView6.Rows.Add();
                dataGridView6[0, k].Value = alter[k];
            }

            label9.Text = label9.Text + " " + mod.win;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
