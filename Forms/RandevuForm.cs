public partial class RandevuForm : Form
{
    private MyDbContext dbContext;
    private Randevu selectedRandevu;

    public RandevuForm()
    {
        InitializeComponent();
        dbContext = new MyDbContext();
        BindData();
    }

    private void BindData()
    {
        var randevular = dbContext.Randevular.Include(r => r.Hasta).Include(r => r.Doktor).ToList();
        dataGridView1.DataSource = randevular;
        dataGridView1.Columns["RandevuID"].HeaderText = "Randevu ID";
        dataGridView1.Columns["Tarih"].HeaderText = "Tarih";
        dataGridView1.Columns["Hasta"].HeaderText = "Hasta";
        dataGridView1.Columns["Doktor"].HeaderText = "Doktor";
        dataGridView1.Columns["HastaID"].Visible = false;
        dataGridView1.Columns["DoktorID"].Visible = false;
    }

    private void ClearInputs()
    {
        txtRandevuID.Clear();
        cmbHasta.SelectedIndex = -1;
        cmbDoktor.SelectedIndex = -1;
        dtpTarih.Value = DateTime.Today;
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            selectedRandevu = dataGridView1.Rows[e.RowIndex].DataBoundItem as Randevu;
            if (selectedRandevu != null)
            {
                txtRandevuID.Text = selectedRandevu.RandevuID.ToString();
                cmbHasta.SelectedValue = selectedRandevu.HastaID;
                cmbDoktor.SelectedValue = selectedRandevu.DoktorID;
                dtpTarih.Value = selectedRandevu.Tarih;
            }
        }
    }

    private void btnEkle_Click(object sender, EventArgs e)
    {
        var randevu = new Randevu
        {
            HastaID = (int)cmbHasta.SelectedValue,
            DoktorID = (int)cmbDoktor.SelectedValue,
            Tarih = dtpTarih.Value
        };

        dbContext.Randevular.Add(randevu);
        dbContext.SaveChanges();

        BindData();
        ClearInputs();
    }

    private void btnGuncelle_Click(object sender, EventArgs e)
    {
        if (selectedRandevu != null)
        {
            selectedRandevu.HastaID = (int)cmbHasta.SelectedValue;
            selectedRandevu.DoktorID = (int)cmbDoktor.SelectedValue;
            selectedRandevu.Tarih = dtpTarih.Value;

            dbContext.SaveChanges();

            BindData();
            ClearInputs();
        }
    }

    private void btnSil_Click(object sender, EventArgs e)
    {
        if (selectedRandevu != null)
        {
            dbContext.Randevular.Remove(selectedRandevu);
            dbContext.SaveChanges();

            BindData();
            ClearInputs();
        }
    }
}
