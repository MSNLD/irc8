﻿using Irc.Enumerations;
using Irc.Interfaces;

namespace Irc.Security;

public class Credential
{
    public string? Domain { get; set; }
    public string? Username { get; set; }
    public string Password { get; set; }
    public string? Nickname { get; set; }
    public string UserGroup { get; set; }
    public string Modes { get; set; }
    public bool Guest { get; set; }
    public long IssuedAt { get; set; }
    public EnumUserAccessLevel Level { get; set; }

    public string? GetDomain()
    {
        return Domain;
    }

    public string? GetUsername()
    {
        return Username;
    }

    public string GetPassword()
    {
        return Password;
    }

    public string? GetNickname()
    {
        return Nickname;
    }

    public string GetUserGroup()
    {
        return UserGroup;
    }

    public string GetModes()
    {
        return Modes;
    }

    public long GetIssuedAt()
    {
        return IssuedAt;
    }

    public EnumUserAccessLevel GetLevel()
    {
        return Level;
    }
}