using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.GetCityDetail
{
    public class GetCityDetailQueryValidator : AbstractValidator<GetCityDetailQuery>
    {
        public GetCityDetailQueryValidator()
        {
            RuleFor(query => query.CityId).GreaterThan(0);
        }

    }
}
