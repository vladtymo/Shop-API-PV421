﻿using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IProductsService
    {
        IList<ProductDto> GetAll(int? filterCategoryId, string? searchTitle);
        ProductDto? Get(int id);
        ProductDto Create(CreateProductDto model);
        void Edit(EditProductDto model);
        void Delete(int id);
    }
}
