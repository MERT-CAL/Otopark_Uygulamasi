using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using IronOcr;
using System.IO;

namespace OTOPARK
{
    public partial class aracgirisi : Form
    {
        public aracgirisi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="" | textBox1.Text == "" | textBox2.Text == "" )
            {
                MessageBox.Show("lütfen tüm alanları doldurunuz");
            }
            else
            {
                OleDbConnection con = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;data source=otopark.accdb");
                con.Open();

                OleDbCommand cmd = new OleDbCommand("insert into aracgiris(tip,plaka,saat) values (@1,@2,@3) ", con);
                cmd.Parameters.AddWithValue("@1", comboBox1.Text);
                cmd.Parameters.AddWithValue("@2", textBox1.Text);
                cmd.Parameters.AddWithValue("@3", textBox2.Text);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("kayıt başaryla oluşturuldu");

                }
                else
                {
                    MessageBox.Show("başarısız");
                }
                con.Close();
                comboBox1.Text = "";


                string kaynak = pictureBox1.ImageLocation;
                string hedef = "resimler\\ " + textBox1.Text + ".jpg";
                File.Copy(kaynak, hedef, true);
                textBox1.Clear();
            }
           
        }

        private void aracgirisi_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void aracgirisi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToShortTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ocr = new AutoOcr();
            var sonuc = ocr.Read(pictureBox1.Image);
            textBox1.Text = sonuc.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            button2.Enabled = true;
            button3.Enabled = false;
        }
    }
}
