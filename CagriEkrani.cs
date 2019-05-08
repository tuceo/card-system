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
using System.Collections;

namespace EngelsizKartSistemi
{
    public partial class CagriEkrani : Form
    {
        SQL connect = new SQL();
        SqlCommand komut = new SqlCommand();
        DataTable dt = new DataTable();
        //SqlDataReader dr;
        SqlDataAdapter da;

        public CagriEkrani()
        {
            InitializeComponent();
        }
        public void datagridfiltesiz(string butonname)
        {
            if (butonname == "Güncel")
            {

                dataGridViewGuncel.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGridViewGuncel.DataSource = null;

                dataGridViewGuncel.Refresh();

                da = new SqlDataAdapter("Select TC [TC], adi[Adı], soyadi[Soyadı], engeli[Engeli], telefon[Telefonu], yakinTelefonu[Yakın Telefonu], durum[Durum], sonuc[Sonuç], yardimTarihiSaati[Yardım Tarihi/Saati], mahalle[Mahalle], sokakCaddeBulvar[Sokak/Cadde/Bulvar], il[İl], ilce[İlçe] " +
                   "From Yardim, Kullanici, Raspberry where Kullanici.KartID = Yardim.KartID and Yardim.raspberryID = Raspberry.raspberryID and durum not like 'Kapandi'", connect.baglantıadresi());
                da.Fill(dt);
                dataGridViewGuncel.DataSource = dt;
                dt.Dispose();
                da.Dispose();
                dataGridViewGuncel.AutoResizeColumns();
                dataGridViewGuncel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                columnHeaderStyle.BackColor = Color.Beige;
                columnHeaderStyle.Font = new Font("Arial", 9);
                dataGridViewGuncel.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            }
            else if (butonname == "Gecmis")
            {
                dataGridViewGecmis.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGridViewGecmis.DataSource = null;

                dataGridViewGecmis.Refresh();
                da = new SqlDataAdapter("Select TC [TC], adi[Adı], soyadi[Soyadı], engeli[Engeli], telefon[Telefonu], yakinTelefonu[Yakın Telefonu], durum[Durum], sonuc[Sonuç], yardimTarihiSaati[Yardım Tarihi/Saati], mahalle[Mahalle], sokakCaddeBulvar[Sokak/Cadde/Bulvar], il[İl], ilce[İlçe] " +
                    "From Yardim, Kullanici, Raspberry where Kullanici.KartID = Yardim.KartID and Yardim.raspberryID = Raspberry.raspberryID and Yardim.durum like 'Kapandi'", connect.baglantıadresi());
                da.Fill(dt);
                dataGridViewGecmis.DataSource = dt;
                dt.Dispose();
                da.Dispose();
                dataGridViewGecmis.AutoResizeColumns();
                dataGridViewGecmis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                columnHeaderStyle.BackColor = Color.Beige;
                columnHeaderStyle.Font = new Font("Arial", 9);
                dataGridViewGecmis.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            }
        }
        private void OturumAcma_Load(object sender, EventArgs e)
        {
            timer_Güncel.Start();
            connect.baglanticontrol();
            flowLayoutPanelCagri.Controls.Clear();
            flowLayoutPanelCagri.Controls.Add(pnlGuncel);

            pnlGecmis.Visible = false;
            pnlGuncel.Visible = true;

            dataGridViewGecmis.Visible = false;
            dataGridViewGuncel.Visible = true;
           
            datagridfiltesiz("Güncel");
            connect.baglantıkapamak();
        }

        private void btnCagriGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                connect.baglanticontrol();

                pnlGuncel.Visible = true;
                pnlGecmis.Visible = false;

                dataGridViewGecmis.Visible = true;
                dataGridViewGuncel.Visible = true;

                datagridfiltesiz("Güncel");
                dataGridViewGuncel.DataSource = dt;
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(dt);
                MessageBox.Show("Kayıt güncellendi!", "Bilgilendirme Penceresi", MessageBoxButtons.OK);

                
                connect.baglantıkapamak();
            }
            catch
            {
                MessageBox.Show("Hata! Kayıt Güncellenemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGecmis_Click(object sender, EventArgs e)
        {
            timer_Güncel.Start();
            connect.baglanticontrol();

            flowLayoutPanelCagri.Controls.Clear();
            flowLayoutPanelCagri.Controls.Add(pnlGecmis);

            pnlGuncel.Visible = false;
            pnlGecmis.Visible = true;

            dataGridViewGuncel.Visible = false;
            dataGridViewGecmis.Visible = true;

            datagridfiltesiz("Gecmis");
            connect.baglantıkapamak();
        }

        private void btnReturnAnaEkran_Click(object sender, EventArgs e)
        {
            timer_Güncel.Start();
            AnaEkran anaekran1 = new AnaEkran();
            anaekran1.Show();
            this.Hide();

        }

        private void CagriEkrani_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer_Güncel.Start();
            Giris giris = new Giris();
            giris.Show();
            this.Hide();
        }

        private void btnGuncel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    connect.baglanticontrol();

            //    flowLayoutPanelCagri.Controls.Clear();
            //    flowLayoutPanelCagri.Controls.Add(pnlGuncel);

            //    pnlGecmis.Visible = false;
            //    pnlGuncel.Visible = true;

            //    dataGridViewGecmis.Visible = false;
            //    dataGridViewGuncel.Visible = true;
                
            //    datagridfiltesiz("Güncel");
            //    connect.baglantıkapamak();


            //}
            //catch
            //{
                
            //}
        }

        private void timer_Güncel_Tick(object sender, EventArgs e)
        {
            try
            {
                connect.baglanticontrol();
                datagridfiltesiz("Güncel");
                connect.baglantıkapamak();

            }
            catch
            {

            }
        }
        private void dataGridViewGuncel_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            timer_Güncel.Stop();
        }
        private void dataGridViewGuncel_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            timer_Güncel.Start();
        }
    }
}
