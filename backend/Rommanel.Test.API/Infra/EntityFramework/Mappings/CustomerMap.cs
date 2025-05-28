using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Rommanel.Test.API.Entities;

namespace Rommanel.Test.API.Infra.EntityFramework.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(400)
                .IsRequired();
        }
    }
}
