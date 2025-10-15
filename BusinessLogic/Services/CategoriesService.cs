using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Helpers;
using DataAccess.Repositories;
using System.Net;

namespace BusinessLogic.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Category> repo;

        public CategoriesService(IMapper mapper, IRepository<Category> repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        public async Task<IList<CategoryDto>> Get(int pageNumber = 1)
        {
            var items = await repo.GetAllAsync(pageNumber, 5);

            return mapper.Map<IList<CategoryDto>>(items);
        }

        public async Task<CategoryDto> Create(CategoryDto model)
        {
            var entity = mapper.Map<Category>(model);

            await repo.AddAsync(entity);

            return mapper.Map<CategoryDto>(entity);
        }

        public async Task Delete(int id)
        {
            var item = await GetEntityById(id);

            await repo.DeleteAsync(id);
        }

        public async Task Edit(CategoryDto model)
        {
            var entity = mapper.Map<Category>(model);

            await repo.UpdateAsync(entity);
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var item = await GetEntityById(id);

            return mapper.Map<CategoryDto>(item);
        }

        private async Task<Category> GetEntityById(int id)
        {
            if (id < 0)
                throw new HttpException("Id can not be negative.", HttpStatusCode.BadRequest); // 400

            var item = await repo.GetByIdAsync(id);

            if (item == null)
                throw new HttpException($"Category with id:{id} not found.", HttpStatusCode.NotFound); // 404

            return item;
        }
    }
}
