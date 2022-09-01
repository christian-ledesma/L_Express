using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LExpress.Core.Entities;
using LExpress.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products
                                    .Include(p => p.ProductBrand)
                                    .Include(p => p.ProductType)
                                    .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                    .Include(p => p.ProductBrand)
                                    .Include(p => p.ProductType)
                                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
