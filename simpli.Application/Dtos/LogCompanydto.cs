using System.ComponentModel.DataAnnotations;

namespace simpli.Domain.Entities;

public class LogCompanydto
{
  [Required]
  public string? Email { get; set; }
  [Required]
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]

  public string? Password { get; set; }
}