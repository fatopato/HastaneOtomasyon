using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneRandevuSistemi
{
    public partial class DoktorForm : Form
    {
        MyDbContext context = new MyDbContext();
        Doktor selectedDoktor = null;

        public DoktorForm()
        {
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            // Get all doktors from the database
            List<Doktor> doktors = context.Doktorlar.ToList();

            // Bind the doktors to the DataGridView
            dataGridView1.DataSource = doktors;
        }

        private void ClearInputs()
        {
            // Clear the text boxes
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtCinsiyet.Text = "";
            txtUzmanlikAlani.Text = "";
            txtTelefon.Text = "";
            txtAdres.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Get the selected doktor from the DataGridView
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            selectedDoktor = context.Doktorlar.Find(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);

            // Set the data from the selected doktor to the text boxes
            txtAd.Text = selectedDoktor.Ad;
            txtSoyad.Text = selectedDoktor.Soyad;
            txtCinsiyet.Text = selectedDoktor.Cinsiyet;
            txtUzmanlikAlani.Text = selectedDoktor.UzmanlikAlani;
            txtTelefon.Text = selectedDoktor.Telefon;
            txtAdres.Text = selectedDoktor.Adres;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Create a new doktor object with the input data
            Doktor doktor = new Doktor()
            {
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                Cinsiyet = txtCinsiyet.Text,
                UzmanlikAlani = txtUzmanlikAlani.Text,
                Telefon = txtTelefon.Text,
                Adres = txtAdres.Text
            };

            // Add the doktor to the database
            context.Doktorlar.Add(doktor);
            context.SaveChanges();

            // Bind the updated data to the DataGridView and clear the inputs
            BindData();
            ClearInputs();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (selectedDoktor != null)
            {
                // Update the selected doktor object with the input data
                selectedDoktor.Ad = txtAd.Text;
                selectedDoktor.Soyad = txtSoyad.Text;
                selectedDoktor.Cinsiyet = txtCinsiyet.Text;
                selectedDoktor.UzmanlikAlani = txtUzmanlikAlani.Text;
                selectedDoktor.Telefon = txtTelefon.Text;
                selectedDoktor.Adres = txtAdres.Text;

                // Update the doktor in the database
                context.SaveChanges();

                // Bind the updated data to the DataGridView and clear the inputs
                BindData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek doktoru seçin.");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Get the selected doktor from the DataGridView
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            int doktorID = (int)dataGridView1.Rows[selectedRowIndex].Cells["DoktorID"].Value;

            using (var db = new MyDbContext())
            {
                // Find the doktor to delete
                Doktor doktorToDelete = db.Doktorlar.Find(doktorID);

                // Delete the doktor from the database
                db.Doktorlar.Remove(doktorToDelete);
                db.SaveChanges();
            }

            // Refresh the DataGridView
            BindData();
            ClearInputs();
        }
    }
}