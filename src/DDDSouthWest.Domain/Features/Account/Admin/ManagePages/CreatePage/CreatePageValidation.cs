using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManagePages.CreatePage
{
    public class CreatePageValidation : AbstractValidator<Admin.ManagePages.CreatePage.CreatePage.Command>
    {
        public CreatePageValidation()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Filename).NotEmpty();
            RuleFor(x => x.BodyMarkdown).NotEmpty();
        }
    }
}