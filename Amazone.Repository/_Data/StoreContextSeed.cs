using Amazone.Core.Entities.Data;
using Amazone.Core.Entities.Order_Aggregate;
using System.Text.Json;

namespace Amazone.Repository.Context
{
    public static class StoreContextSeed
    {
        public static async Task ApplySeedingAsync(StoreDbContext context)
        {
            if (context.ProductBrands.Count() == 0)
            {
                var brandData = File.ReadAllText(@"E:\ROUTE\06 API\ProjectApi\API\Amazone.Repository\_Data\SeedData\brand.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brand?.Count > 0)
                {
                    await context.Set<ProductBrand>().AddRangeAsync(brand);
                }
                await context.SaveChangesAsync();
            }

            if (context.ProductTypes.Count() == 0)
            {
                var typeData = File.ReadAllText(@"E:\ROUTE\06 API\ProjectApi\API\Amazone.Repository\_Data\SeedData\type.json");
                var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                if (type?.Count > 0)
                {
                    await context.Set<ProductType>().AddRangeAsync(type);
                }
                await context.SaveChangesAsync();
            }

            if (context.Products.Count() == 0)
            {
                var productData = File.ReadAllText(@"E:\ROUTE\06 API\ProjectApi\API\Amazone.Repository\_Data\SeedData\product.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products?.Count > 0)
                {
                    await context.Set<Product>().AddRangeAsync(products);
                }
                await context.SaveChangesAsync();
            }



            if (context.deliveryMethods.Count() == 0)
            {
                var deliveryData = File.ReadAllText(@"E:\ROUTE\06 API\ProjectApi\API\Amazone.Repository\_Data\SeedData\delivery.json");
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (delivery?.Count > 0)
                {
                    await context.Set<DeliveryMethod>().AddRangeAsync(delivery);
                }

            }
            await context.SaveChangesAsync();

        }
 
    }
}
