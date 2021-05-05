using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        //create constuctor
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

       //get product by id
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

    //get product all
        public async Task<IReadOnlyList<Product>> GetProductAsync()
        {
           return await _context.Products
           .Include(p => p.ProductType)
           .Include(p => p.ProductBrand)
           .ToListAsync();
        }

        // get product brand
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

    //get product types
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
         return await _context.ProductTypes.ToListAsync();
        }
    }
}