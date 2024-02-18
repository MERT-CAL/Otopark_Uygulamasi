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
using System.IO;

namespace OTOPARK
{
    public partial class araccikisi : Form
    {
        public araccikisi()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("provider=microsoft.ACE.OLEDB.12.0;Data source = otopark.accdb");
        OleDbCommand cmd;
        OleDbDataAdapter dr;
        DataSet ds;
        void listele()
        {
            dr = new OleDbDataAdapter("select *from aracgiris", con);
            ds = new DataSet();
            con.Open();
            dr.Fill(ds, "aracgiris");
            dataGridView1.DataSource = ds.Tables["aracgiris"];
            con.Close();
        }
        void cikis_ekle()
        {
            con.Open();
            cmd = new OleDbCommand("insert into araccikis(tip,plaka,giris,cikis,ucret,zaman) values (@1,@2,@3,@4,@5,@6) ", con);
            cmd.Parameters.AddWithValue("@1", comboBox1.Text);
            cmd.Parameters.AddWithValue("@2", textBox1.Text);
            cmd.Parameters.AddWithValue("@3", textBox2.Text);
            cmd.Parameters.AddWithValue("@4", textBox3.Text);
            cmd.Parameters.AddWithValue("@5", label5.Text);
            cmd.Parameters.AddWithValue("@6", label6.Text);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("fiyat belirlendi ");
                con.Close();
            }
            else
            {
                MessageBox.Show("başarısız");
            }
        }
        void giris_sil()
        {
            cmd = new OleDbCommand("delete from aracgiris where plaka='" + textBox1.Text + "'");
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            listele();
            comboBox1.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            label5.Text = "ücret";
            label6.Text = "zaman";
            textBox3.Text = DateTime.Now.ToShortTimeString();
            button1.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete("resimler\\ " + textBox1.Text + ".jpg");
            giris_sil();
 
        }

        private void araccikisi_Load(object sender, EventArgs e)
        {
            listele();
            timer1.Start();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

            string giris = textBox2.Text;
            string cikis = textBox3.Text;
            TimeSpan fark = DateTime.Parse(cikis).Subtract(DateTime.Parse(giris));
            int toplamzaman = fark.Hours;

            if (comboBox1.Text=="Sedan")
            {
                if (toplamzaman < 1)
                {
                    TimeSpan fark1 = DateTime.Parse(cikis).Subtract(DateTime.Parse(giris));
                    int toplamzaman1 = fark1.Minutes;
                    label6.Text = toplamzaman1.ToString();
                    label5.Text = "40";
                }
                else if (toplamzaman >= 1 & toplamzaman <= 2)
                {
                    label5.Text = "50";
                    label6.Text = toplamzaman.ToString();
                }
                else if (toplamzaman >= 2 & toplamzaman <= 3)
                {
                    label5.Text = "60";
                    label6.Text = toplamzaman.ToString();
                }
                else if (toplamzaman >= 3 & toplamzaman <= 4)
                {
                    label5.Text = "70";
                    label6.Text = toplamzaman.ToString();
                }
                else
                {
                    label5.Text = "85";
                    label6.Text = toplamzaman.ToString();
                }
                cikis_ekle();

            }
            if (comboBox1.Text == "Minibüs")
            {
                if (toplamzaman < 1)
                {
                    TimeSpan fark1 = DateTime.Parse(cikis).Subtract(DateTime.Parse(giris));
                    int toplamzaman1 = fark1.Minutes;
                    label6.Text = toplamzaman1.ToString();
                    label5.Text = "50";
                }
                else if (toplamzaman >= 1 & toplamzaman <= 2)
                {
                    label5.Text = "60";
                    label6.Text = toplamzaman.ToString();
                }
                else if (toplamzaman >= 2 & toplamzaman <= 3)
                {
                    label5.Text = "70";
                    label6.Text = toplamzaman.ToString();
                }
                else if (toplamzaman >= 3 & toplamzaman <= 4)
                {
                    label5.Text = "80";
                    label6.Text = toplamzaman.ToString();
                }
                else
                {
                    label5.Text = "100";
                    label6.Text = toplamzaman.ToString();
                }
                cikis_ekle();

            }
            if (comboBox1.Text == "Kamyon")
            {
                if (toplamzaman < 1)
                {
                    TimeSpan fark1 = DateTime.Parse(cikis).Subtract(DateTime.Parse(giris));
                    int toplamzaman1 = fark1.Minutes;
                    label6.Text = toplamzaman1.ToString();
                    label5.Text = "70";
                }
                else if (toplamzaman >= 1 & toplamzaman <= 2)
                {
                    label5.Text = "80";
                    label6.Text = toplamzaman.ToString();
                }
                else if (toplamzaman >= 2 & toplamzaman <= 3)
                {
                    label5.Text = "90";
                    label6.Text = toplamzaman.ToString();
                }
                else if (toplamzaman >= 3 & toplamzaman <= 4)
                {
                    label5.Text = "100";
                    label6.Text = toplamzaman.ToString();
                }
                else
                {
                    label5.Text = "120";
                    label6.Text = toplamzaman.ToString();
                }
                cikis_ekle();

            }





        }

        private void araccikisi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToShortTimeString();
        }
    }
}
