using System.ComponentModel.DataAnnotations;
using simpli.Domain;

public class VisitorAndQrCodeDataDto
{
  [Required]
  public Visitor Visitor { get; set; }
  [Required]
  public string qrCodeData { get; set; }
}