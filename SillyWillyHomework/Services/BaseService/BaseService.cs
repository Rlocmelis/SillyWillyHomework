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

        public async Task<TModel> GetByIdAsync(int id, string includeProperties = null)
        {
            var includes = ConvertToIncludeExpressions(includeProperties);

            var entities = await _repository.GetByIdAsync(id, includes);

            var model = _mapper.Map<TModel>(entities);

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
            ValidateModel(model);

            var entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task UpdateAsync(int id, TModel model)
        {
            ValidateModel(model);

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

        protected virtual void ValidateModel(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }

        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> ConvertToIncludeExpressions(string includeProperties)
        {
            if (string.IsNullOrEmpty(includeProperties))
            {
                return null;
            }

            var includes = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var includeExpressions = new List<Expression<Func<TEntity, object>>>();

            foreach (var include in includes)
            {
                includeExpressions.Add(GetIncludeExpression(include));
            }

            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null;

            foreach (var includeExpression in includeExpressions)
            {
                if (includeFunc == null)
                {
                    includeFunc = q => q.Include(includeExpression);
                }
                else
                {
                    includeFunc = q => includeFunc(q).Include(includeExpression);
                }
            }

            return includeFunc;
        }

        private Expression<Func<TEntity, object>> GetIncludeExpression(string include)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            Expression property = parameter;

            foreach (var propertyToInclude in include.Split('.'))
            {
                property = Expression.PropertyOrField(property, propertyToInclude);
            }

            var cast = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(cast, parameter);
        }
    }
}
