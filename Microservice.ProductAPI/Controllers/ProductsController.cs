using Microservice.ProductAPI.Data.ValuesObjects;
using Microservice.ProductAPI.Repository;
using Microservice.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.ProductAPI.Controllers
{
    //[Route("api/V1/Controller")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository context)
        {
            _repository = context ?? throw new
                ArgumentException(nameof(context));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _repository.FindById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create(ProductVO vo)
        {
            if (vo == null) return BadRequest();
            var product = await _repository.Create(vo);
            return Ok(product);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update(ProductVO vo)
        {
            if (vo == null) return BadRequest();
            var product = await _repository.Update(vo);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}