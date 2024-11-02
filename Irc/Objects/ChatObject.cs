﻿using Irc.Access;
using Irc.Enumerations;
using Irc.Interfaces;
using Irc.Resources;

namespace Irc.Objects;

public abstract class ChatObject
{
    public Dictionary<char, int> Modes { get; set; } = new();

    public Dictionary<string?, string?> Props { get; set; } = new()
    {
        { "NAME", null }
    };

    public IAccessList AccessList { get; set; } = new AccessList();


    public EnumUserAccessLevel Level => EnumUserAccessLevel.None;

    public Guid Id { get; } = Guid.NewGuid();

    public string ShortId => Id.ToString().Split('-').Last();

    public string? Name
    {
        get => Props["NAME"] ?? IrcStrings.Wildcard;
        set => Props["NAME"] = value;
    }

    public abstract void Send(string message);

    public abstract void Send(string message, ChatObject except = null);

    public abstract void Send(string message, EnumChannelAccessLevel accessLevel);

    public abstract bool CanBeModifiedBy(ChatObject source);

    public override string? ToString()
    {
        return Name;
    }
}