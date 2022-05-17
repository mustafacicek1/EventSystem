using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.UpdateCategory
{
    public class UpdateCategoryCommand
    {
        public UpdateCategoryModel Model { get; set; }
        public int CategoryId { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public UpdateCategoryCommand(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            Category category = _dbContext.Categories.SingleOrDefault(c=>c.Id == CategoryId);
            if (category is null)
                throw new InvalidOperationException("Category not found");

            category.Name = Model.CategoryName;
            _dbContext.SaveChanges();
        }
    }
}
