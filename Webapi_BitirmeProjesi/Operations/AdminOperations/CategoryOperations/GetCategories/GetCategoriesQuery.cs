﻿using System.Collections.Generic;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.GetCategories
{
    public class GetCategoriesQuery
    {
        private readonly EventSystemDbContext _dbContext;

        public GetCategoriesQuery(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<CategoriesViewModel> Handle()
        {
            var categories = _dbContext.Categories.Select(c=>new CategoriesViewModel
            {
                CategoryName=c.Name,
                CategoryId=c.Id,
            }).ToList();
            
            return categories;
        }
    }
}
