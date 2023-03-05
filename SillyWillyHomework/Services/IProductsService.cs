using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public interface IProductsService : IBaseService<Product, ProductDto>
    {
    }
}
