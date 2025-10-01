using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductsService(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public ProductDto Create(CreateProductDto model)
        {
            var entity = mapper.Map<Product>(model);

            ctx.Products.Add(entity);
            ctx.SaveChanges(); // generate id (execute INSERT SQL command)

            return mapper.Map<ProductDto>(entity);
        }

        public void Delete(int id)
        {
            if (id < 0)
                throw new HttpException("Id can not be negative.", HttpStatusCode.BadRequest); // 400

            var item = ctx.Products.Find(id);

            if (item == null)
                throw new HttpException($"Product with id:{id} not found.", HttpStatusCode.NotFound); // 404

            ctx.Products.Remove(item);
            ctx.SaveChanges(true);
        }

        public void Edit(EditProductDto model)
        {
            ctx.Products.Update(mapper.Map<Product>(model));
            ctx.SaveChanges();
        }

        public ProductDto? Get(int id)
        {
            if (id < 0)
                return null; // TODO: throw exceptions

            var item = ctx.Products.Find(id);

            if (item == null)
                return null;

            return mapper.Map<ProductDto>(item);
        }

        public IList<ProductDto> GetAll()
        {
            var items = ctx.Products
                .Include(x => x.Category) // perform LEF JOIN
                .ToList();

            return mapper.Map<IList<ProductDto>>(items);
        }
    }
}
