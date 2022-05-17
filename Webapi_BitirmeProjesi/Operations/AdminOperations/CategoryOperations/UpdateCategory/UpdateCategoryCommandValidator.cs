using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.UpdateCategory
{
    public class UpdateCategoryCommandValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.Model.CategoryName).NotEmpty().MinimumLength(2).MaximumLength(30);
            RuleFor(command => command.CategoryId).GreaterThan(0);
        }
    }
}
