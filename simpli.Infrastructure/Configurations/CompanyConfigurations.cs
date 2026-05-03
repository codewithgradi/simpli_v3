using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simpli.Domain;

namespace simpli.Infrastructure.Configuration;


public class CompanyConfigurations : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> builder)
  {
    builder.HasKey(c => c.Id);


    //One company have many notifications
    builder.HasMany(c => c.Notifications)
    .WithOne(c => c.Company)
    .HasForeignKey(x => x.CompanyId);

    //One company have many rooms
    builder.HasMany(x => x.Rooms)
    .WithOne(x => x.Company)
    .HasForeignKey(x => x.CompanyId);
  }
}