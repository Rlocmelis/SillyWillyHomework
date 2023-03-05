using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SillyWillyHomework.Exceptions;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Validation;
using System.Linq.Expressions;

namespace SillyWillyHomework.Services.BaseService
{
    public class BaseService<TEntity, TModel> : IBaseService<TEntity, TModel>
    where TEntity : class
    where TModel : class
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var model = _mapper.Map<TModel>(entity);
            return model;
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var models = _mapper.Map<IEnumerable<TModel>>(entities);
            return models;
        }

        public virtual async Task<TModel> AddAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task UpdateAsync(int id, TModel model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(model, entity);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}
