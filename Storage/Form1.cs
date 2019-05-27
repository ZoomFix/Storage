using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Storage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Склад|*.storage" };

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            var storage = new StorageModel()
            {
                Name = textBox1.Text,
                Address = textBox2.Text,
                Capacity = Convert.ToInt32(textBox3.Text),
                Items = listBox1.Items.OfType<Items>().ToList(),
                Workers = listBox2.Items.OfType<Workers>().ToList(),
            };

            switch (comboBox1.SelectedValue?.ToString())
            {
                case "A+":
                    storage.StorageType = StorageType.Aplus;
                    break;
                case "A":
                    storage.StorageType = StorageType.A;
                    break;
                case "B+":
                    storage.StorageType = StorageType.Bplus;
                    break;
                case "B":
                    storage.StorageType = StorageType.B;
                    break;
                case "C":
                    storage.StorageType = StorageType.C;
                    break;
                case "D":
                    storage.StorageType = StorageType.D;
                    break;
                default:
                    storage.StorageType = StorageType.Aplus;
                    break;
            }

            var xs = new XmlSerializer(typeof(StorageModel));
            var file = File.Create(sfd.FileName);
            xs.Serialize(file, storage);
            file.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var items = new ItemsForm() { Items = new Items() };
            if (items.ShowDialog(this) == DialogResult.OK)
            {
                listBox1.Items.Add(items.Items);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Enabled = listBox1.SelectedItem is Items;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Items)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                var item = (Items)listBox1.Items[index];
                var ff = new ItemsForm() { Items = item };
                if (ff.ShowDialog(this) == DialogResult.OK)
                {
                    listBox1.Items.Remove(item);
                    listBox1.Items.Insert(index, item);
                }
            }
        }

        private void загрузитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Склад|*.storage" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;
            var xs = new XmlSerializer(typeof(StorageModel));
            var file = File.OpenRead(ofd.FileName);
            var storage = (StorageModel)xs.Deserialize(file);
            file.Close();

            textBox1.Text = storage.Name;
            textBox2.Text = storage.Address;
            textBox3.Text = Convert.ToString(storage.Capacity);
            switch (storage.StorageType)
            {
                
                case StorageType.Aplus:
                    comboBox1.Text = "A+";
                    break;
                case StorageType.A:
                    comboBox1.Text = "A";
                    break;
                case StorageType.Bplus:
                    comboBox1.Text = "B+";
                    break;
                case StorageType.B:
                    comboBox1.Text = "B";
                    break;
                case StorageType.C:
                    comboBox1.Text = "C";
                    break;
                case StorageType.D:
                    comboBox1.Text = "D";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var items in storage.Items)
            {
                listBox1.Items.Add(items);
            }
            foreach (var items in storage.Workers)
            {
                listBox2.Items.Add(items);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var workers = new WorkersForm() { Workers = new Workers() };
            if (workers.ShowDialog(this) == DialogResult.OK)
            {
                listBox2.Items.Add(workers.Workers);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = listBox2.SelectedItem is Workers;
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox2.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                var worker = (Workers)listBox2.Items[index];
                var ff = new WorkersForm() { Workers = worker };
                if (ff.ShowDialog(this) == DialogResult.OK)
                {
                    listBox2.Items.Remove(worker);
                    listBox2.Items.Insert(index, worker);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is Workers)
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
