using System.Text.Json.Serialization;

namespace simpli.Application.Dtos
{
  public class CreateNotificationDto
  {
    public string? VisitorName { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VisitorStatus Status { get; set; }
  }
}