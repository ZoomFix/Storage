using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage
{
    public partial class WorkersForm : Form
    {
        public WorkersForm()
        {
            InitializeComponent();
        }

        public Workers Workers { get; set; }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Workers.Name = textBox1.Text;
            Workers.Position = textBox2.Text;
            Workers.Experience = Convert.ToInt32(textBox3.Text);
        }

        private void WorkersForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Workers.Name;
            textBox2.Text = Workers.Position;
            textBox3.Text = Convert.ToString(Workers.Experience);
        }
    }
}
