﻿using Irc.Enumerations;
using Irc.Interfaces;

namespace Irc.Commands;

internal class Isircx : Command, ICommand
{
    public Isircx() : base(0, false)
    {
    }

    public new EnumCommandDataType GetDataType()
    {
        return EnumCommandDataType.None;
    }

    public new void Execute(IChatFrame chatFrame)
    {
        chatFrame.User.Send(Raw.IRCX_ERR_NOTIMPLEMENTED(chatFrame.Server, chatFrame.User, nameof(Access)));
    }
}