﻿using Irc.Enumerations;

namespace Irc.Access
{
    public class ServerAccess : AccessList
    {
        public ServerAccess()
        {
            accessEntries = new Dictionary<EnumAccessLevel, List<AccessEntry>>
            {
                { EnumAccessLevel.OWNER, new List<AccessEntry>() },
                { EnumAccessLevel.HOST, new List<AccessEntry>() },
                { EnumAccessLevel.VOICE, new List<AccessEntry>() },
                { EnumAccessLevel.DENY, new List<AccessEntry>() },
                { EnumAccessLevel.GRANT, new List<AccessEntry>() }
            };
        }
    }
}