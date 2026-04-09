namespace simpli.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class UpdateRoomDto
{
  [Required]
  public string? CompanyName { get; set; }
  [Required]
  [EmailAddress]
  public string? Email { get; set; }
  [Required]
  public string? ContactNumber { get; set; }
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
  public string? Password { get; set; }

}