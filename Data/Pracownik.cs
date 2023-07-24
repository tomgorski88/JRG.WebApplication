using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JRG.WebApplication.Data;

public class Pracownik
{
    [Key]
    public int Id { get; set; }
	[Required]
	public string? Nazwa { get; set; }
    // [Required]
    // public string? Zmiana { get; set; }
    [Required]
    public string? Notatka { get; set; }
    [Required]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	[DataType(DataType.Date)]
    public DateOnly DataUrodzenia { get; set; }
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	[DataType(DataType.Date)]
    [Required]
    public DateOnly DataZatrudnienia { get; set; }
    [Required]
    public string? Telefon { get; set; }
    [Required]
    public string? Adres { get; set; }
    [Required(ErrorMessage = "Please enter Description")]

    public int? StopienId { get; set; }
    public Stopien? Stopien { get; set; }

    public int? ZmianaId { get; set; }
    public Zmiana? Zmiana { get; set; }

    public ICollection<Uprawnienie>? Uprawnienia { get; set; }
    public ICollection<UkonczonyKurs>? UkonczoneKursy { get; set; }


}
