using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageNews.CreateNews
{
    public class CreateNewsValidation : AbstractValidator<CreateNews.Command>
    {
        public CreateNewsValidation()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Filename).NotEmpty();
            RuleFor(x => x.BodyHtml).NotEmpty();
            RuleFor(x => x.BodyMarkdown).NotEmpty();
        }
    }
}