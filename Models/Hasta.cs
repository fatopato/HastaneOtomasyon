public class Hasta
{
    [Key]
    public int HastaID { get; set; }

    [Required]
    [StringLength(50)]
    public string Ad { get; set; }

    [Required]
    [StringLength(50)]
    public string Soyad { get; set; }

    [Required]
    [StringLength(10)]
    public string Cinsiyet { get; set; }

    public DateTime DogumTarihi { get; set; }

    [StringLength(20)]
    public string Telefon { get; set; }

    [StringLength(200)]
    public string Adres { get; set; }

    public ICollection<Randevu> Randevular { get; set; }
}