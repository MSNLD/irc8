﻿using Irc.Enumerations;
using Irc.Helpers;
using Irc.Interfaces;
using Irc.Resources;

namespace Irc.Commands
{
    public class Nick : Command, ICommand
    {
        public Nick() : base(1, false)
        {
        }

        public new EnumCommandDataType GetDataType()
        {
            return EnumCommandDataType.Standard;
        }

        public new void Execute(IChatFrame chatFrame)
        {
            var hopcount = string.Empty;
            if (chatFrame.Message.Parameters.Count > 1) hopcount = chatFrame.Message.Parameters[1];

            // Is user not registered?
            // Set nickname according to regulations (should be available in user object and changes based on what they authenticated as)
            if (!chatFrame.User.IsAuthenticated()) HandlePreauthNicknameChange(chatFrame);
            else if (!chatFrame.User.IsRegistered()) HandlePreregNicknameChange(chatFrame);
            else HandleRegNicknameChange(chatFrame);
        }

        public static bool ValidateNickname(string nickname, bool guest = false, bool oper = false, bool preAuth = false,
            bool preReg = false)
        {
            var mask = IrcStrings.PostAuthNicknameMask;

            if (preAuth) mask = IrcStrings.PreAuthNicknameMask;
            else if (oper) mask = IrcStrings.PostAuthOperNicknameMask;
            else if (guest) mask = IrcStrings.PostAuthGuestNicknameMask;

            return nickname.Length <= IrcStrings.MaxFieldLen &&
                   RegularExpressions.Match(mask, nickname, true);
        }

        public static bool HandlePreauthNicknameChange(IChatFrame chatFrame)
        {
            var nickname = chatFrame.Message.Parameters.First();
            // UTF8 / Guest / Normal / Admin/Sysop/Guide OK
            if (!ValidateNickname(nickname, preAuth: true))
            {
                chatFrame.User.Send(Raw.IRCX_ERR_ERRONEOUSNICK_432(chatFrame.Server, chatFrame.User, nickname));
                return false;
            }

            chatFrame.User.Nickname = nickname;
            return true;
        }

        public static bool HandlePreregNicknameChange(IChatFrame chatFrame)
        {
            var nickname = chatFrame.Message.Parameters.First();
            var guest = chatFrame.User.IsGuest();
            var oper = chatFrame.User.GetLevel() >= EnumUserAccessLevel.Guide;

            if (!ValidateNickname(nickname, guest, oper, false, true))
            {
                chatFrame.User.Send(Raw.IRCX_ERR_ERRONEOUSNICK_432(chatFrame.Server, chatFrame.User, nickname));
                return false;
            }

            chatFrame.User.Nickname = nickname;
            return true;
        }

        public static bool HandleRegNicknameChange(IChatFrame chatFrame)
        {
            var nickname = chatFrame.Message.Parameters.First();
            var guest = chatFrame.User.IsGuest();
            var oper = chatFrame.User.GetLevel() >= EnumUserAccessLevel.Guide;

            if (!guest && !oper)
            {
                chatFrame.User.Send(Raw.IRCX_ERR_NONICKCHANGES_439(chatFrame.Server, chatFrame.User, nickname));
                return false;
            }

            var channels = chatFrame.User.GetChannels();
            foreach (var channel in channels)
            foreach (var member in channel.Key.GetMembers())
                if (member.GetUser().Nickname == nickname)
                {
                    chatFrame.User.Send(Raw.IRCX_ERR_NICKINUSE_433(chatFrame.Server, chatFrame.User));
                    return false;
                }

            if (!ValidateNickname(nickname, guest, oper))
            {
                chatFrame.User.Send(Raw.IRCX_ERR_ERRONEOUSNICK_432(chatFrame.Server, chatFrame.User, nickname));
                return false;
            }

            chatFrame.User.ChangeNickname(nickname, false);
            return true;
        }
    }
}