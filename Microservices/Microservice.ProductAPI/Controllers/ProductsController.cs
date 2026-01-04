using Microservice.ProductAPI.Data.ValuesObjects;
using Microservice.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.ProductAPI.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _context;
        public ProductsController(IProductRepository context)
        {
            _context = context ?? throw new
                ArgumentException(nameof(context));
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Opcional, se o repositório retornar null em caso de erro grave
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _context.FindAll();
            if (products == null)
            {
                             return NotFound();
            }
            return Ok(products);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            // Busca o objeto de domínio (Product)
            var product = await _context.FindById(id);

            // 2. Condição de verificação concisa. Retorna 404 se não encontrado.
            if (product == null)
                return NotFound();

            // Retorna 200 OK com o objeto tipado
            return Ok(product);
        }


       [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Para validação de modelo
        public async Task<ActionResult<ProductVO>> Create(ProductVO vo)
        {
            if (vo == null) return BadRequest("O corpo da requisição não pode ser nulo.");

            var product = await _context.Create(vo);
            return Ok(product);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Para validação de modelo
        public async Task<ActionResult<ProductVO>> Update(ProductVO vo)
        {
            if (vo == null) return BadRequest("O corpo da requisição não pode ser nulo.");

            var product = await _context.Create(vo);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _context.Delete(id);
            if(!status) return BadRequest();
            return Ok(status);
        }


    }
}
