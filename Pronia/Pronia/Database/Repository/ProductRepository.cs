using Microsoft.EntityFrameworkCore;
using Pronia.Database.Interfaces;
using Pronia.Database.Models;
using Pronia.Database.ViewModels;
using Pronia.Helpers.Extentions;

namespace Pronia.Database.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ProniaDbContext _dbContext;
    private readonly IWebHostEnvironment _environment;
    private const string FOLDER_NAME = "Upload/Product";

    public ProductRepository(ProniaDbContext dbContext, IWebHostEnvironment environment)
    {
        _dbContext = dbContext;
        _environment = environment;
    }

    public List<Product> GetAll()
    {
        return _dbContext.Products.OrderBy(p => p.Id).ToList();
    }

    public async Task Insert(CreateProductViewModel model)
    {
        var product = new Product()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            ImageUrl = model.File.CreateFile(_environment.WebRootPath, FOLDER_NAME)
        };

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product> GetById(int? id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task RemoveById(int? id)
    {
        var product = await GetById(id);
        FileExtention.RemoveFile(Path.Combine(_environment.WebRootPath, FOLDER_NAME, product.ImageUrl));
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(int? id, UpdateProductViewModel model)
    {
        var product = await GetById(id);

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;

        if (model.File is not null)
            product.ImageUrl = model.File.UpdateFile(_environment.WebRootPath, FOLDER_NAME, product.ImageUrl);

        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }
}
