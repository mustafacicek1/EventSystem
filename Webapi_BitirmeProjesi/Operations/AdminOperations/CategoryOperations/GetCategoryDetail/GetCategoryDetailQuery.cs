using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.GetCategoryDetail
{
    public class GetCategoryDetailQuery
    {
        public int CategoryId { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public GetCategoryDetailQuery(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CategoriesViewModel Handle()
        {
            var category = _dbContext.Categories.SingleOrDefault(c => c.Id == CategoryId);
            if (category is null)
                throw new InvalidOperationException("Category not found");

            CategoriesViewModel vm = new CategoriesViewModel();
            vm.CategoryId = category.Id;
            vm.CategoryName = category.Name;
            return vm;
        }
    }
}
