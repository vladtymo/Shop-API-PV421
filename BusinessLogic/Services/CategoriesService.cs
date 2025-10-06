using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public CategoriesService(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public async Task<IList<CategoryDto>> Get(int pageNumber = 1)
        {
            var items = await PagedList<Category>.CreateAsync(ctx.Categories, pageNumber, 5);

            return mapper.Map<IList<CategoryDto>>(items);
        }

        public async Task<CategoryDto> Create(CategoryDto model)
        {
            var entity = mapper.Map<Category>(model);

            ctx.Categories.Add(entity);
            await ctx.SaveChangesAsync();

            return mapper.Map<CategoryDto>(entity);
        }

        public async Task Delete(int id)
        {
            var item = await GetEntityById(id);

            ctx.Categories.Remove(item);
            await ctx.SaveChangesAsync(true);
        }

        public async Task Edit(CategoryDto model)
        {
            ctx.Categories.Update(mapper.Map<Category>(model));
            await ctx.SaveChangesAsync();
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

            var item = await ctx.Categories.FindAsync(id);

            if (item == null)
                throw new HttpException($"Category with id:{id} not found.", HttpStatusCode.NotFound); // 404

            return item;
        }
    }
}
