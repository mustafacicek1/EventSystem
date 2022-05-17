using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.DeleteCategory
{
    public class DeleteCategoryCommand
    {
        public int CategoryId { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public DeleteCategoryCommand(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            Category category = _dbContext.Categories.SingleOrDefault(c=>c.Id==CategoryId);
            if(category is null)
                throw new InvalidOperationException("Category not found");

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }
    }
}
