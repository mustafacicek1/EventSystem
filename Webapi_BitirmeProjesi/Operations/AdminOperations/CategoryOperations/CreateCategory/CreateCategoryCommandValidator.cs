using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.CreateCategory
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(command=>command.Model.CategoryName).NotEmpty().MinimumLength(2).MaximumLength(30);
        }
    }
}
