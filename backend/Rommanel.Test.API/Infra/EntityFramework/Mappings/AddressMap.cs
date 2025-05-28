using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Rommanel.Test.API.Entities;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;

namespace Rommanel.Test.API.Infra.EntityFramework.Mappings
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(p => p.Id);

            builder
             .HasOne(p => p.Customer)
             .WithMany(p => p.Addresses)
             .HasForeignKey(p => p.CustomerId)
             .IsRequired();
        }
    }
}
