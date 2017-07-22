using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage
{
    public class CreatePageValidation : AbstractValidator<CreatePage.Command>
    {
        public CreatePageValidation()
        {
            RuleFor(x => x.PageTitle).NotEmpty();
            RuleFor(x => x.PageFilename).NotEmpty();
            RuleFor(x => x.PageBody).NotEmpty();
        }
    }
}