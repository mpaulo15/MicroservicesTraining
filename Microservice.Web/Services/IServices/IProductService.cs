using Microservice.Web.Models;

namespace Microservice.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> findAllProducts();
        Task<ProductModel> findProductById(long id);

        Task<ProductModel> CreateProduct(ProductModel model);
        Task<ProductModel> UpdateProduct(ProductModel model);

        Task<bool> DeleteProduct(long id);

    }
}
