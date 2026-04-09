namespace simpli.Domain.Entities;

using Riok.Mapperly.Abstractions;

[Mapper]

public partial class LogMappers
{
  public partial LogDto MapToDto(Log log);
}