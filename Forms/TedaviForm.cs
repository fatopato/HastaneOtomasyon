using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi
{
    public partial class TedaviForm : Form
    {
        private MyDbContext db;

        public TedaviForm()
        {
            InitializeComponent();
            db = new MyDbContext();
        }

        private void TedaviForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            var tedaviler = db.Tedaviler
                .Include(t => t.Randevu)
                .Include(t => t.Doktor)
                .ToList();

            dgvTedaviler.DataSource = tedaviler;
            dgvTedaviler.Columns["TedaviID"].HeaderText = "ID";
            dgvTedaviler.Columns["TedaviAdi"].HeaderText = "Tedavi Adı";
            dgvTedaviler.Columns["TedaviTarihi"].HeaderText = "Tedavi Tarihi";
            dgvTedaviler.Columns["Randevu"].Visible = false;
            dgvTedaviler.Columns["Doktor"].Visible = false;

            cmbRandevu.DataSource = db.Randevular.ToList();
            cmbRandevu.DisplayMember = "RandevuTarihi";
            cmbRandevu.ValueMember = "RandevuID";

            cmbDoktor.DataSource = db.Doktorlar.ToList();
            cmbDoktor.DisplayMember = "AdSoyad";
            cmbDoktor.ValueMember = "DoktorID";

            ClearInputs();
        }

        private void ClearInputs()
        {
            txtTedaviAdi.Text = "";
            dtpTedaviTarihi.Value = DateTime.Now;
            cmbRandevu.SelectedIndex = -1;
            cmbDoktor.SelectedIndex = -1;
        }

        private void dgvTedaviler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTedaviler.Rows[e.RowIndex];
                int tedaviID = Convert.ToInt32(row.Cells["TedaviID"].Value);

                Tedavi tedavi = db.Tedaviler
                    .Include(t => t.Randevu)
                    .Include(t => t.Doktor)
                    .FirstOrDefault(t => t.TedaviID == tedaviID);

                txtTedaviAdi.Text = tedavi.TedaviAdi;
                dtpTedaviTarihi.Value = tedavi.TedaviTarihi;
                cmbRandevu.SelectedValue = tedavi.RandevuID;
                cmbDoktor.SelectedValue = tedavi.DoktorID;
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtTedaviAdi.Text == "" || cmbRandevu.SelectedIndex == -1 || cmbDoktor.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            int randevuID = (int)cmbRandevu.SelectedValue;
            int doktorID = (int)cmbDoktor.SelectedValue;

            Tedavi tedavi = new Tedavi()
            {
                TedaviAdi = txtTedaviAdi.Text,
                TedaviTarihi = dtpTedaviTarihi.Value.Date,
                RandevuID = randevuID,
                DoktorID = doktorID
            };

            db.Tedaviler.Add(tedavi);
            db.SaveChanges();
            MessageBox.Show("Tedavi eklendi.");
            BindData();
            ClearInputs();
        }
    }
}