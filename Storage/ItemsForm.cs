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
    public partial class ItemsForm : Form
    {
        public ItemsForm()
        {
            InitializeComponent();
        }

        public Items Items { get; set; }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Items.Name;
            textBox2.Text = Convert.ToString(Items.Price);
            textBox3.Text = Convert.ToString(Items.Units);
            textBox4.Text = Items.Manufacturer;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Items.Name = textBox1.Text;
            Items.Price = Convert.ToInt32(textBox2.Text);
            Items.Units = Convert.ToInt32(textBox3.Text);
            Items.Manufacturer = textBox4.Text;
        }
    }
}
