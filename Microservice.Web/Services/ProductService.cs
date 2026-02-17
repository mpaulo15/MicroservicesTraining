using Microservice.Web.Models;
using Microservice.Web.Services.IServices;
using Microservice.Web.Utils;
using System.Collections.Generic;

namespace Microservice.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/products";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            // O HttpClient combina o BaseAddress (https://localhost:5001/) 
            // com o BasePath (api/v1/product) automaticamente.
            var response = await _client.GetAsync(BasePath);

            if (!response.IsSuccessStatusCode)
            {
                // Isso vai te ajudar a ver o erro real se falhar
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Erro: {response.StatusCode} - {errorContent}");
            }

            return await response.ReadContentAs<List<ProductModel>>();
        }

        //public async Task<IEnumerable<ProductModel>> FindAllProducts()
        //{
        //    var response = await _client.GetAsync(BasePath);
        //    return await response.ReadContentAs<List<ProductModel>>();
        //}

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong when calling API");
        }

        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong when calling API");
        }
    }
}

