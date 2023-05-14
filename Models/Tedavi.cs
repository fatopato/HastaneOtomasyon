// Define the Tedavi table
public class Tedavi
{
    [Key]
    public int TedaviID { get; set; }

    public int RandevuID { get; set; }

    public int DoktorID { get; set; }

    [Required]
    [StringLength(200)]
    public string Aciklama { get; set; }

    public DateTime Tarih { get; set; }

    public double Fiyat { get; set; }

    public Randevu Randevu { get; set; }

    public Doktor Doktor { get; set; }
}
