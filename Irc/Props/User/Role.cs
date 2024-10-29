using Irc.Enumerations;
using Irc.Interfaces;
using Irc.Objects;
using Irc.Objects.Server;
using Irc.Resources;

namespace Irc.Props.User
{
    public class Role : PropRule
    {
        public Role() : base(IrcStrings.UserPropRole, EnumChannelAccessLevel.None,
            EnumChannelAccessLevel.ChatMember, IrcStrings.GenericProps, "0", true)
        {
        }

        public static EnumIrcError EvaluateSet(IServer server, IChatObject source, IChatObject target, string propValue)
        {
            server.ProcessCookie((IUser)source, IrcStrings.UserPropRole, propValue);
            return EnumIrcError.NONE;
        }
    }
}