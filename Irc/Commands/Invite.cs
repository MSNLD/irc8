﻿using Irc.Enumerations;
using Irc.Interfaces;

namespace Irc.Commands;

internal class Invite : Command, ICommand
{
    public Invite() : base(1)
    {
    }

    public new EnumCommandDataType GetDataType()
    {
        return EnumCommandDataType.None;
    }

    public new void Execute(IChatFrame chatFrame)
    {
        // Invite <nick>
        // Invite <nick> <channel>

        var targetNickname = chatFrame.Message.Parameters.FirstOrDefault();
        var targetUser = chatFrame.Server.GetUserByNickname(targetNickname);

        if (targetUser == null)
        {
            chatFrame.User.Send(Raw.IRCX_ERR_NOSUCHNICK_401(chatFrame.Server, chatFrame.User, targetNickname));
            return;
        }

        if (chatFrame.Message.Parameters.Count() == 1) InviteNickToCurrentChannel(chatFrame, targetUser);

        if (chatFrame.Message.Parameters.Count() > 1) InviteNickToSpecificChannel(chatFrame, targetUser);
    }


    public static void InviteNickToCurrentChannel(IChatFrame chatFrame, IUser targetUser)
    {
        var targetChannelKvp = chatFrame.User.GetChannels().FirstOrDefault();
        var targetChannel = targetChannelKvp.Key;
        var member = targetChannelKvp.Value;

        if (targetChannel == null)
        {
            chatFrame.User.Send(Raw.IRCX_ERR_U_NOTINCHANNEL_928(chatFrame.Server, chatFrame.User));
            return;
        }

        ProcessInvite(chatFrame, member, targetChannel, targetUser);
    }

    public static void InviteNickToSpecificChannel(IChatFrame chatFrame, IUser targetUser)
    {
        var targetChannelName = chatFrame.Message.Parameters[1];
        var targetChannel = chatFrame.Server.GetChannelByName(targetChannelName);
        if (targetChannel == null)
        {
            chatFrame.User.Send(Raw.IRCX_ERR_NOSUCHCHANNEL_403(chatFrame.Server, chatFrame.User, targetChannelName));
            return;
        }

        var member = targetChannel.GetMember(chatFrame.User);

        if (member == null && chatFrame.User.GetLevel() < EnumUserAccessLevel.Guide)
            chatFrame.User.Send(Raw.IRCX_ERR_NOTONCHANNEL_442(chatFrame.Server, chatFrame.User, targetChannel));

        ProcessInvite(chatFrame, member, targetChannel, targetUser);
    }

    public static void ProcessInvite(IChatFrame chatFrame, IChannelMember member, IChannel? targetChannel,
        IUser targetUser)
    {
        if (targetChannel.InviteOnly && member.GetLevel() < EnumChannelAccessLevel.ChatHost)
        {
            chatFrame.User.Send(Raw.IRCX_ERR_CHANOPRIVSNEEDED_482(chatFrame.Server, chatFrame.User, targetChannel));
            return;
        }

        if (targetUser.IsOn(targetChannel))
        {
            chatFrame.User.Send(Raw.IRCX_ERR_USERONCHANNEL_443(chatFrame.Server, targetUser, targetChannel));
            return;
        }

        if (!targetChannel.InviteMember(targetUser))
        {
            chatFrame.User.Send(Raw.IRCX_ERR_TOOMANYINVITES_929(chatFrame.Server, chatFrame.User, targetUser,
                targetChannel));
            return;
        }

        targetUser.Send(Raw.RPL_INVITE(chatFrame.Server, chatFrame.User, targetUser, chatFrame.Server.RemoteIP,
            targetChannel));
    }
}