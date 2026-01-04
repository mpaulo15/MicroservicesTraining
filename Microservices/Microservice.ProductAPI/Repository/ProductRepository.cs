
using AutoMapper;
using Microservice.ProductAPI.Data.ValuesObjects;
using Microservice.ProductAPI.Models;
using Microservice.ProductAPI.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Microservice.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product products = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(products);
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            Product products = _mapper.Map<Product>(vo);
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(products);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product products = _mapper.Map<Product>(vo);
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(products);
        }


        public async Task<bool> Delete(long id)
        {
            try
            {
                Product products = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (products == null)
                {
                    return false;
                }
                else
                {
                    _context.Products.Remove(products);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

    }
}
