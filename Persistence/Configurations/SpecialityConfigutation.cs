using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
    {
        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            builder.HasData(
                new Speciality
                {
                    Name = "title",
                    Description = "Sample Content",
                    Id = Guid.NewGuid()
                },
                new Speciality
                {
                    Name = "tiltle 2",
                    Description = "Sample Content 2",
                    Id = Guid.NewGuid()
                }
               );

        }
    }

}
