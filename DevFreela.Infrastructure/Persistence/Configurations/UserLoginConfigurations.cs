using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserLoginConfigurations : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(x => x.Id);

            builder
               .Property(x => x.Username)
               .HasMaxLength(50)
               .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasMaxLength(50)
                .IsRequired();
        }
    }

}
