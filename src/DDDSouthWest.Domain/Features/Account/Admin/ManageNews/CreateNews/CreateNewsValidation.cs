using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageNews.CreateNews
{
    public class CreateNewsValidation : AbstractValidator<Admin.ManageNews.CreateNews.CreateNews.Command>
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