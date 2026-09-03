using Microsoft.EntityFrameworkCore;
using ProductManager.Core.DTOs;
using ProductManager.Core.Entities;
using ProductManager.Core.Interfaces;
using ProductManager.Infrastructure.Data;

namespace ProductManager.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product is null ? null : new ProductDto(product.Id, product.Name, product.Price, product.Stock);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return new ProductDto(product.Id, product.Name, product.Price, product.Stock);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}