using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PersonelKayıtSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=EJDERIYAV2\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");
        void temizle() 
        {
            txtid.Text = string.Empty;
            txtad.Text = string.Empty;
            txtsoyad.Text = string.Empty;
            txtmeslek.Text = string.Empty;
            mskmaas.Text = string.Empty;
            cmbsehir.Text= string.Empty;
            rbevli.Checked = false;
            rbbekar.Checked = false;
            txtid.Focus();
        }
        string x;//durum değişkeni
        private void Form1_Load(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbsehir.Text);
            komut.Parameters.AddWithValue("@p4", mskmaas.Text);
            komut.Parameters.AddWithValue("@p5", txtmeslek.Text);
            if (rbevli.Checked && !rbbekar.Checked) 
            {
                komut.Parameters.AddWithValue("@p6", true);
            }
            else 
            {
                komut.Parameters.AddWithValue("@p6", false);
            }
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi.");
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            cmbsehir.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            mskmaas.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            x = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            if (x == "True") 
            {
                rbevli.Checked = true;
                rbbekar.Checked = false;
            }
            else 
            {
                rbbekar.Checked = true;
                rbevli.Checked = false;
            }
            txtmeslek.Text = dataGridView1.Rows[secim].Cells[6].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("delete from Tbl_Personel where Personelid=@p1", baglanti);
            sil.Parameters.AddWithValue("@p1",txtid.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("silme işlemi tamamlandı.");
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("Update Tbl_Personel set PerAd=@p1,PerSoyad=@p2,PerSehir=@p3,PerMaas=@p4,PerDurum=@p5,PerMeslek=@p6 where Personelid=@p7",baglanti);
            guncelle.Parameters.AddWithValue("@p1", txtad.Text);
            guncelle.Parameters.AddWithValue("@p2", txtsoyad.Text);
            guncelle.Parameters.AddWithValue("@p3", cmbsehir.Text);
            guncelle.Parameters.AddWithValue("@p4", mskmaas.Text);
            if (rbevli.Checked == true && rbbekar.Checked == false)
            {
                x = "True";
            }
            else
            {
                x = "False";
            }
            guncelle.Parameters.AddWithValue("@p5", x);
            guncelle.Parameters.AddWithValue("@p6", txtmeslek.Text);
            guncelle.Parameters.AddWithValue("@p7", txtid.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("güncelleme işlemi tamamlandı.");
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();

        }
    }
}
