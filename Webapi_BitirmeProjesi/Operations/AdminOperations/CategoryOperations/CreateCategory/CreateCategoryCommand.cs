using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.CreateCategory
{
    public class CreateCategoryCommand
    {
        public CreateCategoryModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public CreateCategoryCommand(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            Category category = _dbContext.Categories.SingleOrDefault(c=>c.Name==Model.CategoryName);
            if (category is not null)
                throw new InvalidOperationException("Category already exist");

            Category newCategory = new Category();
            newCategory.Name = Model.CategoryName;
            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();
        }
    }
}
