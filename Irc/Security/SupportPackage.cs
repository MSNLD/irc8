﻿using Irc.Enumerations;
using Irc.Interfaces;

namespace Irc.Security;

public class SupportPackage
{
    protected Credential? _credentials;
    public bool Guest;
    public bool Listed = true;
    public EnumSupportPackageSequence ServerSequence;

    public uint ServerVersion;
    public bool Authenticated { get; protected set; }

    public virtual SupportPackage CreateInstance(ICredentialProvider? credentialProvider)
    {
        throw new NotImplementedException();
    }

    public virtual string? CreateSecurityChallenge()
    {
        throw new NotImplementedException();
    }

    public virtual EnumSupportPackageSequence InitializeSecurityContext(string token, string? ip)
    {
        throw new NotImplementedException();
    }

    public virtual EnumSupportPackageSequence AcceptSecurityContext(string token, string? ip)
    {
        throw new NotImplementedException();
    }

    public virtual string GetDomain()
    {
        return GetPackageName();
    }

    public virtual string GetPackageName()
    {
        return GetType().Name;
    }

    public virtual Credential? GetCredentials()
    {
        return _credentials;
    }

    public bool IsAuthenticated()
    {
        return Authenticated;
    }

    public void SetChallenge(byte[] new_challenge)
    {
        throw new NotImplementedException();
    }
}