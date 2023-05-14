public class Randevu
{
    [Key]
    public int RandevuID { get; set; }

    public int HastaID { get; set; }

    public int DoktorID { get; set; }

    [Required]
    public DateTime Tarih { get; set; }

    [StringLength(200)]
    public string Aciklama { get; set; }

    public Hasta Hasta { get; set; }

    public Doktor Doktor { get; set; }

    public ICollection<Tedavi> Tedaviler { get; set; }
}