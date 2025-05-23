using Microsoft.EntityFrameworkCore;
using Pronia.Database.Models;
using Pronia.Database.ViewModels;

namespace Pronia.Database.Interfaces;

public interface IProductRepository
{
    List<Product> GetAll();
    Task Insert(CreateProductViewModel model);
    Task<Product> GetById(int? id);
    Task RemoveById(int? id);
    Task Update(int? id, UpdateProductViewModel model);
}
