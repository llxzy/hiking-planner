using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.QueryObjects;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services
{
    public class CrudQueryServiceBase<TEntity, TDto, TFilter> : ICrudQueryServiceBase<TDto>
        where TEntity : class, new()
        where TDto : class
        where TFilter : FilterDtoBase
        
    {
        protected IRepository<TEntity> Repository;
        protected IMapper Mapper;
        protected QueryObjectBase<TEntity, TDto, TFilter, IQuery<TEntity>> QueryObject;

        public CrudQueryServiceBase(IRepository<TEntity> repository, 
            QueryObjectBase<TEntity, TDto, TFilter, IQuery<TEntity>> qob)
        {
            Repository = repository;
            Mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMap));
            QueryObject = qob;
        }

        public async Task<TDto> GetAsync(int id)
        {
            var e = await Repository.GetByIdAsync(id);
            return e != null ? Mapper.Map<TDto>(e) : null;
        }

        public async Task CreateAsync(TDto entityDto)
        {
            await Repository.CreateAsync(Mapper.Map<TEntity>(entityDto));
        }

        public void Update(TDto entityDto)
        {
            Repository.Update(Mapper.Map<TEntity>(entityDto));
        }

        public async Task DeleteAsync(int id)
        {
            await Repository.DeleteAsync(id);
        }
    }
}
