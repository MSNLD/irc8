﻿using Irc.Enumerations;
using Irc.Objects;

namespace Irc.Interfaces;

public interface IFloodProtectionManager
{
    EnumFloodResult FloodCheck(EnumCommandDataType type, User user);

    EnumFloodResult Audit(IFloodProtectionProfile protectionProfile, EnumCommandDataType type,
        EnumUserAccessLevel level);
}