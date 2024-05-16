using Amazone.Apis.Dtos.Product;
using Amazone.Core.Entities.Data;
using AutoMapper;

namespace Amazone.Apis.Helper
{
    public class PictureUrlResolver : IValueResolver<Product, ProductReturnDto, string>
    {
        private readonly IConfiguration _config;

        public PictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))

                return $"{_config["BaseApiUrl"]}/{source.PictureUrl}";
            return string.Empty;
        }
    }
}
