using Microsoft.EntityFrameworkCore;
using ORM_Project.Context;
using ORM_Project.Exceptions;
using ORM_Project.Models;

namespace ORM_Project.Services
{
    public class ProductService
    {
        public async Task CreateAsync(Product product)
        {
            AppDbContext context = new AppDbContext();
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            AppDbContext context = new AppDbContext();
            var DbProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (DbProduct == null)
            {
                throw new NotFoundException("Product not found...");
            }
            DbProduct.Name = product.Name;
            DbProduct.Price = product.Price;
            DbProduct.Stock = product.Stock;
            DbProduct.Description = product.Description;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            AppDbContext context = new AppDbContext();
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new NotFoundException("Product not found...");
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetProductAsync()
        {
            AppDbContext context = new AppDbContext();
            return await context.Products.ToListAsync();
        }
        public async Task<List<Product>> SearchProductAsync(string name)
        {
            AppDbContext context = new AppDbContext();
            var products = await context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
            return products;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            AppDbContext context = new AppDbContext();
            var product = await context.Products.FirstOrDefaultAsync(p=>p.Id==id);
            if (product == null)
            {
                throw new NotFoundException("Product not found...");
            }
            return product;
        }


    }
}
