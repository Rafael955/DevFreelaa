using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Fullname)
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.BirthDate)
                .IsRequired();

            builder
                .HasMany(x => x.Skills)
                .WithOne()
                .HasForeignKey(x => x.IdSkill)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.UserLogin)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
