using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HastaneRandevuSistemi
{
    public partial class HastaForm : Form
    {
        private string connectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable table;

        public HastaForm()
        {
            InitializeComponent();

            // Set the connection string to the database
            connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True";

            // Create a new SqlConnection with the connection string
            connection = new SqlConnection(connectionString);

            // Create a new SqlDataAdapter with a select query and the SqlConnection
            adapter = new SqlDataAdapter("SELECT * FROM Hasta", connection);

            // Create a new DataTable and fill it with the data from the SqlDataAdapter
            table = new DataTable();
            adapter.Fill(table);

            // Bind the data from the DataTable to the DataGridView
            dataGridView1.DataSource = table;
        }

        private void HastaForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Get the selected DataRow from the DataGridView
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            DataRow selectedRow = table.Rows[selectedRowIndex];

            // Set the data from the selected DataRow to the text boxes
            txtAd.Text = selectedRow["Ad"].ToString();
            txtSoyad.Text = selectedRow["Soyad"].ToString();
            cmbCinsiyet.Text = selectedRow["Cinsiyet"].ToString();
            dtpDogumTarihi.Value = Convert.ToDateTime(selectedRow["Doğum Tarihi"].ToString());
            txtTelefon.Text = selectedRow["Telefon"].ToString();
            txtAdres.Text = selectedRow["Adres"].ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Create a new DataRow with the data from the text boxes
            DataRow newRow = table.NewRow();
            newRow["Ad"] = txtAd.Text;
            newRow["Soyad"] = txtSoyad.Text;
            newRow["Cinsiyet"] = cmbCinsiyet.Text;
            newRow["Doğum Tarihi"] = dtpDogumTarihi.Value;
            newRow["Telefon"] = txtTelefon.Text;
            newRow["Adres"] = txtAdres.Text;

            // Add the new DataRow to the DataTable
            table.Rows.Add(newRow);

            // Update the database with the changes
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update(table);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Get the selected DataRow from the DataGridView
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            DataRow selectedRow = table.Rows[selectedRowIndex];

            // Set the data from the text boxes to the selected DataRow
            selectedRow["Ad"] = txtAd.Text;
            selectedRow["Soyad"] = txtSoyad.Text;
            selectedRow["Cinsiyet"] = cmbCinsiyet.Text;
            selectedRow["Doğum Tarihi"] = dtpDogumTarihi.Value;
            selectedRow["Telefon"] = txtTelefon.Text;
            selectedRow["Adres"] = txtAdres.Text;

            // Update the database with the changes
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update(table);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int id = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "DELETE FROM Hasta WHERE HastaID = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Kayıt silindi");
                        BindData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt silinemedi");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin");
            }
        }

        private void BindData()
        {
            // Clear existing rows in the DataGridView
            dataGridView1.Rows.Clear();

            // Retrieve the data from the database and populate the DataGridView
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT * FROM Hasta";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string ad = reader.GetString(1);
                    string soyad = reader.GetString(2);
                    string cinsiyet = reader.GetString(3);
                    DateTime dogumTarihi = reader.GetDateTime(4);
                    string telefon = reader.GetString(5);
                    string adres = reader.GetString(6);

                    dataGridView1.Rows.Add(id, ad, soyad, cinsiyet, dogumTarihi, telefon, adres);
                }

                reader.Close();
            }
        }

        private void ClearInputs()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbCinsiyet.SelectedIndex = -1;
            dtpDogumTarihi.Value = DateTime.Now;
            txtTelefon.Text = "";
            txtAdres.Text = "";
        }
    }
}

