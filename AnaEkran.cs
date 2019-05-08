using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngelsizKartSistemi
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {
            InitializeComponent();
        }

        private void btnKullaniciEkrani_Click(object sender, EventArgs e)
        {
            Kullanıcılar KullanicilarEkrani = new Kullanıcılar();
            KullanicilarEkrani.ShowDialog();
            this.Hide();
        }

        private void btnCagriEkrani_Click(object sender, EventArgs e)
        {
            CagriEkrani CagriEkranii = new CagriEkrani();
            CagriEkranii.ShowDialog();
            this.Hide();
        }
    }
}
