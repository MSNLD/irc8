﻿using Irc.Enumerations;
using Irc.Interfaces;
using Irc.Modes;
using Irc.Resources;

namespace Irc.Extensions.Modes.Channel;

public class Clone : ModeRuleChannel, IModeRule
{
    public Clone() : base(IrcStrings.ChannelModeClone)
    {
    }

    public new EnumIrcError Evaluate(IChatObject source, IChatObject target, bool flag, string parameter)
    {
        return EvaluateAndSet(source, target, flag, parameter);
    }
}