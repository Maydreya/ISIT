using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_3
{
    public partial class Form1 : Form
    {
        string filename;
        Nodes myCollection = new Nodes();
        Deserialization deserial = new Deserialization();
        MLV mlv = new MLV();
        public Form1()
        {
            InitializeComponent();
            foreach (ToolStripMenuItem m in menuStrip1.Items)
            {
                SetWhiteColor(m);
            }
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new Cols());
        }

        private void SetWhiteColor(ToolStripMenuItem item)
        {
            item.ForeColor = Color.White;
            foreach (ToolStripMenuItem it in item.DropDownItems)
            {
                SetWhiteColor(it);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void загрузитьБазуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                deserial.Deserealize(filename, myCollection);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                richTextBox1.Clear();
                KnowledgeBase.startname = "problem";
                mlv.Run(mlv.Check(KnowledgeBase.startname), richTextBox1);
            }
            else
            {
                richTextBox1.Clear();
                KnowledgeBase.startname = "motherboard";
                mlv.Run(mlv.Check(KnowledgeBase.startname), richTextBox1);
            }
        }
    }
    public class Cols : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            // 51, 153, 255 - устанавливаем голубой цвет выбранного элемента
            // (или задаете свой)
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(64, 64, 80); }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.FromArgb(64, 64, 80); }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.FromArgb(64, 64, 80); }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.FromArgb(64, 64, 80); }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(51, 153, 255); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(51, 153, 255); }
        }


    }
}
