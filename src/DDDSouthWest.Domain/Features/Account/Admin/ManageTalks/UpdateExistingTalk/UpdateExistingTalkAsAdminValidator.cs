using Dapper;
using FluentValidation;
using FluentValidation.Validators;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.UpdateExistingTalk
{
    public class UpdateExistingTalkAsAdminValidator : AbstractValidator<UpdateExistingTalk.Command>
    {
        private readonly ClientConfigurationOptions _options;

        public UpdateExistingTalkAsAdminValidator(ClientConfigurationOptions options)
        {
            _options = options;
            RuleFor(x => x.TalkTitle).NotEmpty();
            RuleFor(x => x.TalkSummary).NotEmpty();
            RuleFor(x => x.TalkBodyMarkdown).NotEmpty().WithMessage("'Talk Abstract' must not be empty");
            /*RuleFor(x => x.IsApproved).Must(TalkIsSubmitted).WithMessage("Talk must be submitted before approval");*/
        }

        private bool TalkIsSubmitted(UpdateExistingTalk.Command command, bool isApproved, PropertyValidatorContext context)
        {
            // TODO - We don't want to be able to approve talks that aren't submitted
            if (!isApproved)
                return false;
            
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                const string sql = "SELECT COUNT(*) FROM talks WHERE Id = @Id AND IsSubmitted = TRUE";
                var isSubmitted = connection.QuerySingleOrDefault<int>(sql, new
                {
                    Id = command.Id
                });

                return isSubmitted == 0;
            }
        }


    }
}