using AutoMapper;
using Microservice.ProductAPI.Data.ValuesObjects;
using Microservice.ProductAPI.Models;

namespace Microservice.ProductAPI.Configs
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();
            });
            return mappingConfig;
        }
    }
}
