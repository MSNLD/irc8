﻿using Irc.Enumerations;
using Irc.Resources;

namespace Irc.Props.Channel
{
    internal class Lag : PropRule
    {
        // The LAG channel property contains a numeric value between 0 to 2 seconds.
        // The server will add an artificial delay of that length between subsequent messages from the same member.
        // All messages to the channel are affected. 
        public Lag() : base(IrcStrings.ChannelPropLag, EnumChannelAccessLevel.ChatHost,
            EnumChannelAccessLevel.ChatHost, IrcStrings.ChannelPropLagRegex, string.Empty)
        {
        }
    }
}