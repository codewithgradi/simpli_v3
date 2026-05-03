using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simpli.Domain;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
  public void Configure(EntityTypeBuilder<Log> builder)
  {
    builder.HasKey(x => x.Id);
  }
}