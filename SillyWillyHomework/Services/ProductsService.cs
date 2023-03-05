using AutoMapper;
using FluentValidation;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Models;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services.BaseService;

namespace SillyWillyHomework.Services
{
    public class ProductsService : BaseService<Product, ProductDto>, IProductsService
    {

        public ProductsService(IBaseRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
        }

    }
}
