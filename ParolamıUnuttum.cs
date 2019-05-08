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
using System.Net.Mail;

namespace EngelsizKartSistemi
{
    public partial class ParolamıUnuttum : Form
    {
        SQL connect = new SQL();
        SqlConnection baglanti;

        public ParolamıUnuttum()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Giris ot = new Giris();
            ot.Show();
            this.Hide();
        }

        private void ParolamıUnuttum_Load(object sender, EventArgs e)
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
        public bool Gonder(string konu, string icerik, string icerik2)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("beseninsaat06@gmail.com");
            ePosta.To.Add(tbemail.Text);

            ePosta.Subject = konu;

            ePosta.Body = ("Kullanıcı Adınız: " + icerik + " Şifreniz: " + icerik2);
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("beseninsaat06@gmail.com", "ilkay8835");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
            }
            return kontrol;
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            string kullanıcıadı, sifre, sql = "SELECT * FROM Yetkili WHERE yetkilitcno='" + tbTc.Text.ToString() + "'AND Email='" + tbemail.Text.ToString() + "'";
            SqlCommand com = new SqlCommand(sql, baglanti);
            SqlDataReader oku = com.ExecuteReader();

            if (oku.Read())
            {
                    sifre = oku["Sifre"].ToString();
                    kullanıcıadı = oku["yetkiliKullaniciAdi"].ToString();
                    MessageBox.Show("Şifreniz Mailinize Yollanmıştır.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Gonder("Unutmuş Olduğunuz Şifreniz Ektedir", kullanıcıadı, sifre);
                    this.Hide();
                    Giris oturumacma = new Giris();
                    oturumacma.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı!", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            oku.Close();
        }
    }
}
