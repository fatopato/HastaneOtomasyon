
public class Ilac
{
    [Key]
    public int IlacID { get; set; }

    [Required]
    [StringLength(50)]
    public string Ad { get; set; }

    [Required]
    [StringLength(50)]
    public string Firma { get; set; }

    public double Fiyat { get; set; }

    public ICollection<Tedavi> Tedaviler { get; set; }
}