using Amazone.Core.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order = Amazone.Core.Entities.Order_Aggregate.Order;

namespace Amazone.Repository._Data.Config.OrderConfig
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner());
            builder.Property(o => o.Subtotal).HasColumnType("decimal(12,2)");
            builder.Property(o => o.orderStatus)
                .HasConversion(
                (OStatus) => (OStatus).ToString(),
                 (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), (OStatus))
                    );
            builder.HasMany(o => o.Items)
                .WithOne().OnDelete(DeleteBehavior.Cascade);

        }
    }

}