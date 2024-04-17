﻿using System;
using Wino.Core.Domain.Models.Menus;

namespace Wino.MenuFlyouts.Context
{
    public class MailOperationMenuFlyoutItem : WinoOperationFlyoutItem<MailOperationMenuItem>
    {
        public MailOperationMenuFlyoutItem(MailOperationMenuItem operationMenuItem, Action<MailOperationMenuItem> clicked) : base(operationMenuItem, clicked)
        {
        }
    }
}
