using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> repo;
        //private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductsService(IRepository<Product> repo, IMapper mapper)
        {
            this.repo = repo;
            //this.ctx = ctx;
            this.mapper = mapper;
        }

        public async Task<ProductDto> Create(CreateProductDto model)
        {
            var entity = mapper.Map<Product>(model);

            //ctx.Products.Add(entity);
            //ctx.SaveChanges(); // generate id (execute INSERT SQL command)
            await repo.AddAsync(entity);

            return mapper.Map<ProductDto>(entity);
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new HttpException("Id can not be negative.", HttpStatusCode.BadRequest); // 400

            var item = await repo.GetByIdAsync(id);

            if (item == null)
                throw new HttpException($"Product with id:{id} not found.", HttpStatusCode.NotFound); // 404

            //ctx.Products.Remove(item);
            //ctx.SaveChanges(true);
            await repo.DeleteAsync(item);
        }

        public async Task Edit(EditProductDto model)
        {
            //ctx.Products.Update(mapper.Map<Product>(model));
            //ctx.SaveChanges();
            await repo.UpdateAsync(mapper.Map<Product>(model));
        }

        public async Task<ProductDto?> Get(int id)
        {
            if (id < 0)
                return null; // TODO: throw exceptions

            var item = await repo.GetByIdAsync(id);

            if (item == null)
                return null;

            return mapper.Map<ProductDto>(item);
        }

        public async Task<IList<ProductDto>> GetAll(int? filterCategoryId, string? searchTitle) // iPhone 17
        {
            // IQuerable - it's command only (without data)
            // List, Array... (ToList()...) - get data from DB
            //IQueryable<Product> query = ctx.Products
            //    .Include(x => x.Category); // perform LEF JOIN

            //if (filterCategoryId != null)
            //    query = query.Where(x => x.CategoryId == filterCategoryId);

            //if (searchTitle != null)
            //    query = query.Where(x => x.Title.ToLower().Contains(searchTitle.ToLower()));

            //// current query: select * from Products left join Categories where CategoryId = 4

            //var items = query.ToList(); // load data

            var filterEx = PredicateBuilder.New<Product>(true);

            if (filterCategoryId != null)
                filterEx = filterEx.And(x => x.CategoryId == filterCategoryId);

            if (!string.IsNullOrWhiteSpace(searchTitle))
                filterEx = filterEx.And(x => x.Title.ToLower().Contains(searchTitle.ToLower()));

            var items = await repo.GetAllAsync(filtering: filterEx, includes: nameof(Product.Category));

            return mapper.Map<IList<ProductDto>>(items);
        }
    }
}
