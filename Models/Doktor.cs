public class Doktor
{
    [Key]
    public int DoktorID { get; set; }

    [Required]
    [StringLength(50)]
    public string Ad { get; set; }

    [Required]
    [StringLength(50)]
    public string Soyad { get; set; }

    [Required]
    [StringLength(10)]
    public string Cinsiyet { get; set; }

    [Required]
    [StringLength(50)]
    public string UzmanlikAlani { get; set; }

    [StringLength(20)]
    public string Telefon { get; set; }

    [StringLength(200)]
    public string Adres { get; set; }

    public ICollection<Randevu> Randevular { get; set; }
    public ICollection<Tedavi> Tedaviler { get; set; }
}