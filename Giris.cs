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

namespace EngelsizKartSistemi
{
    public partial class Giris : Form
    {
        SqlConnection baglanti;
        SqlCommand komut = new SqlCommand();

        public Giris()
        {
            InitializeComponent();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection("Data Source=den1.mssql6.gear.host;Initial Catalog=kartsistemi;User ID=kartsistemi; Password=123456.");
                baglanti.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("BAGLANTI OLUSTURULAMADI ! ");
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string giris, sifre, sql = "SELECT * FROM Yetkili WHERE yetkiliKullaniciAdi ='" + tbKullaniciAdi.Text + "'AND sifre='" + tbParola.Text + "'";

            SqlCommand com = new SqlCommand(sql, baglanti);
            SqlDataReader oku = com.ExecuteReader();
            giris = tbKullaniciAdi.Text;
            sifre = tbParola.Text;
            if (oku.Read())
            {
                    AnaEkran ana = new AnaEkran();
                    DialogResult di = ana.ShowDialog();
                    if (di == DialogResult.OK)
                    {
                        this.ShowDialog();
                        baglanti.Close();
                    }
                    else
                    {
                        Application.Exit();
                        baglanti.Close();
                    }
            }
            else
            {
                MessageBox.Show("Kullanıcı veya Sifre Yanlıs \nLutfen Tekrar Deneyiniz !");

            }
            oku.Close();
        }

        private void btn_parolamıunuttum_Click(object sender, EventArgs e)
        {
            ParolamıUnuttum sifremiunuttum = new ParolamıUnuttum();
            this.Hide();
            sifremiunuttum.ShowDialog();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
