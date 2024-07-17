﻿using Wino.Core.Domain.Entities;

namespace Wino.Messaging.Server
{
    public record AccountCreatedMessage(MailAccount Account) : ServerMessageBase<AccountCreatedMessage>;
}
