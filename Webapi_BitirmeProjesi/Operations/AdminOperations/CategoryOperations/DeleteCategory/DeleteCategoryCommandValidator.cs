using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.DeleteCategory
{
    public class DeleteCategoryCommandValidator:AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(command => command.CategoryId).GreaterThan(0);
        }
    }
}
