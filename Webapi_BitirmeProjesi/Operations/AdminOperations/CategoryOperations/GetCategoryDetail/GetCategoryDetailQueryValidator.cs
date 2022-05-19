using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.GetCategoryDetail
{
    public class GetCategoryDetailQueryValidator:AbstractValidator<GetCategoryDetailQuery>
    {
        public GetCategoryDetailQueryValidator()
        {
            RuleFor(query => query.CategoryId).GreaterThan(0);
        }
    }
}
