using AutoMapper;
using BusinessLogic;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace Shop_Api_Tests
{
    public class CategoriesServiceTests
    {
        // ------------- unit tests for GetById -------------
        // naming convention: MethodName_Condition_ExpectedResult
        [Fact]
        public async Task GetById_UserExists_ReturnUserDto()
        {
            // Arrange
            var categoryId = 10;
            var categoryEntity = new Category { Id = categoryId, Name = "Electronics" };
            var categoryDto = new CategoryDto { Id = categoryId, Name = "Electronics" };

            var mapper = new Mock<IMapper>();
            var repo = new Mock<IRepository<Category>>();

            repo.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(categoryEntity);
            mapper.Setup(m => m.Map<CategoryDto>(It.IsAny<Category>())).Returns(categoryDto);

            var service = new CategoriesService(mapper.Object, repo.Object);

            // Act
            var result = await service.GetById(categoryId);

            // Assert
            result.Should().BeEquivalentTo(categoryDto);
        }

        [Fact]
        public async Task GetById_NegativeId_ThrowException()
        {
            // Arrange
            var categoryId = -1;
        
            var mapper = new Mock<IMapper>();
            var repo = new Mock<IRepository<Category>>();

            var service = new CategoriesService(mapper.Object, repo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpException>(() => service.GetById(categoryId));
        }
    }
}