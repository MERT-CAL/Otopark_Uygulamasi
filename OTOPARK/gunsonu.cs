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
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace OTOPARK
{
    public partial class gunsonu : Form
    {
        public gunsonu()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("provider=microsoft.ACE.OLEDB.12.0;Data source = otopark.accdb");
        OleDbDataAdapter dr;
        OleDbCommand cmd;
        DataSet ds;

        //araccikis tablosunu listele
        void listele()
        {
            dr = new OleDbDataAdapter("select *from araccikis", con);
            ds = new DataSet();
            con.Open();
            dr.Fill(ds, "araccikis");
            dataGridView1.DataSource = ds.Tables["araccikis"];
            con.Close();
        }

        // araccıkıs tablosunu temizleme
        void temizle()
        {
            cmd = new OleDbCommand("delete *from araccikis");
            con.Open();
            cmd.Connection = con;
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("gün sonu alındı");
            }
            else
            {
                MessageBox.Show("başarısız");
            }
            con.Close();
            listele();
            label1.Text = "0";
            label2.Text = "0";
        }

        private void gunsonu_Load(object sender, EventArgs e)
        {
            listele();
            label2.Text = (dataGridView1.RowCount).ToString();

            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            label1.Text = toplam.ToString();
        }

        private void gunsonu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // PDF DOSYASI OLARAK GÜN SONU DÖKÜMANI ALDIĞIM KISIM 

            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "PDF Dosyaları";
            save.DefaultExt = "pdf";
            save.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar(*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);


                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 80;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTable.DefaultCell.BorderWidth = 1;


                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdfTable.AddCell(cell);
                }
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdfTable.AddCell(cell.Value.ToString());
                        }
                    }
                }
                catch (NullReferenceException)
                {
                }
                using (FileStream stream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
            }
            temizle();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
