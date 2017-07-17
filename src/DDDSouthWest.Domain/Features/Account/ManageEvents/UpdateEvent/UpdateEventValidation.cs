﻿using FluentValidation;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateEvent
{
    public class UpdateEventValidation : AbstractValidator<UpdatEvent.Command>
    {
        public UpdateEventValidation()
        {
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.EventFilename).NotEmpty();
        }
    }
}