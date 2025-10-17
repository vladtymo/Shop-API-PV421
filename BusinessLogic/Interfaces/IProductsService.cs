using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IProductsService
    {
        Task<IList<ProductDto>> GetAll(int? filterCategoryId, string? searchTitle);
        Task<ProductDto?> Get(int id);
        Task<ProductDto> Create(CreateProductDto model);
        Task Edit(EditProductDto model);
        Task Delete(int id);
    }
}
