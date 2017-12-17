using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage
{
    public class CreatePageValidation : AbstractValidator<CreatePage.Command>
    {
        public CreatePageValidation()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Filename).NotEmpty();
            RuleFor(x => x.BodyMarkdown).NotEmpty();
        }
    }
}