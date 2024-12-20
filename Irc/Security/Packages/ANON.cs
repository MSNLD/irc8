﻿using Irc.Enumerations;
using Irc.Interfaces;

namespace Irc.Security.Packages;

public class ANON : SupportPackage
{
    public ANON()
    {
        Guest = true;
        Authenticated = true;
        Listed = true;
    }

    public override EnumSupportPackageSequence InitializeSecurityContext(string data, string ip)
    {
        return EnumSupportPackageSequence.SSP_AUTHENTICATED;
    }

    public override EnumSupportPackageSequence AcceptSecurityContext(string data, string ip)
    {
        return EnumSupportPackageSequence.SSP_AUTHENTICATED;
    }

    public override string GetDomain()
    {
        return nameof(ANON);
    }

    public override string GetPackageName()
    {
        return nameof(ANON);
    }

    public override Credential? GetCredentials() => new()
    {
        Level = EnumUserAccessLevel.Member,
        Domain = GetType().Name,
        Username = null,
        Guest = true
    };

    public override SupportPackage CreateInstance(ICredentialProvider credentialProvider)
    {
        return new ANON();
    }

    public string CreateSecurityChallenge(EnumSupportPackageSequence stage)
    {
        return null;
    }
}