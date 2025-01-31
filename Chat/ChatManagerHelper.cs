using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using API.Entity;

namespace API.Chat
{
    public abstract class ChatManagerHelper
    {
        /// <summary>
        /// Pattern for Deepl translated chat messages
        /// </summary>
        private const string TranslationPattern = @"(?<pre>.+){:(?<lang>\w{2}):(?<orig>.+)}@{(?<trans>.+)";

        /// <summary>
        /// build a sentence to be displayed in the tell
        /// </summary>
        public static void BuildTellSentence(string sender, string msg, out string result)
        {
            // If no sender name was provided, show only the msg
            var name = EntityHelper.RemoveTitleAndShardFromName(sender);

            if (sender.Length == 0)
                result = msg;
            else
            {
                // TODO special case where there is only a title, very rare case for some NPC

                // TODO Does the char have a CSR title?
                const string csr = "";

                result = $"{csr}{name} tells you: {msg}";
            }
        }

        /// <summary>
        /// build a sentence to be displayed in the chat (e.g add "you say", "you shout", "[user name] says" or "[user name] shout")
        /// </summary>
        public static void BuildChatSentence(IClient client, string sender, string msg, ChatGroupType type, out string result)
        {
            // if its a tell, then use buildTellSentence
            if (type == ChatGroupType.Tell)
            {
                BuildTellSentence(sender, msg, out result);
                return;
            }

            // If no sender name was provided, show only the msg
            if (sender.Length == 0)
            {
                result = msg;
                return;
            }

            // get the category if any. Note, in some case (chat from other player), there is not categories
            // and we do not want getStringCategory to return 'SYS' category.
            var catStr = GetStringCategory(msg, out var finalMsg);
            var cat = "";

            if (catStr.Length > 0)
                cat = $"&{catStr}&";

            if (cat.Length > 0)
            {
                result = msg;
                return;
            }

            // Format the sentence with the provided sender name
            var senderName = EntityHelper.RemoveTitleAndShardFromName(sender);

            // Does the char have a CSR title?
            var csr = CharacterTitle.IsCsrTitle(EntityHelper.GetTitleFromName(sender)) ? "(CSR) " : "";

            var userEntity = client.GetApiNetworkManager()?.GetApiEntityManager()?.GetApiUserEntity();

            if (userEntity != null && senderName == userEntity.GetDisplayName())
            {
                // The player talks
                result = type switch
                {
                    ChatGroupType.Shout => $"§c{cat}{csr}You shout: {finalMsg}",
                    _ => $"{cat}{csr}You say: {finalMsg}"
                };
            }
            else
            {
                // Special case where there is only a title, very rare case for some NPC
                //if (senderName.Length == 0)
                //{
                //    Character entity = EntitiesMngr.getEntityByName(sender, true, true) as CCharacterCL;
                //    // We need the gender to display the correct title
                //    bool bWoman = entity != null && entity.getGender() == GSGENDER.female;
                //
                //    senderName = STRING_MANAGER.CStringManagerClient.getTitleLocalizedName(CEntityCL.getTitleFromName(sender), bWoman);
                //    {
                //        // Sometimes translation contains another title
                //        int pos = senderName.IndexOf('$');
                //        if (pos != -1)
                //        {
                //            senderName = STRING_MANAGER.CStringManagerClient.getTitleLocalizedName(CEntityCL.getTitleFromName(senderName), bWoman);
                //        }
                //    }
                //}
                //
                //senderName = STRING_MANAGER.CStringManagerClient.getLocalizedName(senderName);

                result = type switch
                {
                    ChatGroupType.Shout => $"§c{cat}{csr}{senderName} shouts: {finalMsg}",
                    _ => $"{cat}{csr}{senderName} says: {finalMsg}"
                };
            }
        }

        /// <summary>
        /// get the category if any. Note, in some case (chat from other player), there is not categories
        /// and we do not want getStringCategory to return 'SYS' category.
        /// </summary>
        public static string GetStringCategory(string src, out string dest, bool alwaysAddSysByDefault = false)
        {
            var str = GetStringCategoryIfAny(src, out dest);

            if (alwaysAddSysByDefault)
                return str.Length == 0 ? "SYS" : str;

            return str;
        }

        /// <summary>
        /// Get the category from the string (src="&amp;SYS&amp;Who are you?" and dest="Who are you?" and return "SYS"), if no category, return ""
        /// </summary>
        public static string GetStringCategoryIfAny(string src, out string dest)
        {
            const int preTagSize = 5;

            var colorCode = Array.Empty<char>();

            if (src.Length >= 3)
            {
                var startPos = 0;

                // Skip <NEW> or <CHG> if present at beginning
                var preTag = "";

                const string newTag = "<NEW>";

                if (src.Length >= preTagSize && src[..preTagSize] == newTag)
                {
                    startPos = preTagSize;
                    preTag = newTag;
                }

                const string chgTag = "<CHG>";

                if (src.Length >= preTagSize && src[..preTagSize] == chgTag)
                {
                    startPos = preTagSize;
                    preTag = chgTag;
                }

                if (src[startPos] == '&')
                {
                    var nextPos = src.IndexOf('&', startPos + 1);

                    if (nextPos != -1)
                    {
                        var codeSize = nextPos - startPos - 1;
                        colorCode = new char[codeSize];

                        for (var k = 0; k < codeSize; ++k)
                        {
                            colorCode[k] = char.ToLower(src[k + startPos + 1]);
                        }

                        var destTmp = "";

                        if (startPos != 0)
                            // leave <NEW> or <CHG> in the dest string
                            destTmp = preTag;

                        destTmp += src[(nextPos + 1)..];
                        dest = destTmp;
                    }
                    else
                    {
                        dest = src;
                    }
                }
                else
                {
                    dest = src;
                }
            }
            else
            {
                dest = src;
            }

            return new string(colorCode);
        }

        /// <summary>
        /// Removes translations from the message
        /// </summary>
        /// <param name="msg">message to be processed</param>
        /// <param name="translateChat">true, if the chat should be translated</param>
        /// <returns>message without translations</returns>
        public static string GetVerbatim(string msg, bool translateChat)
        {
            Debug.Print(msg);

            var match = Regex.Match(msg, TranslationPattern, RegexOptions.Multiline & RegexOptions.IgnoreCase);

            if (!match.Success)
                return msg;

            return translateChat ?
                $"{match.Groups["pre"].Value}[{match.Groups["lang"].Value}]{match.Groups["trans"].Value}" :
                $"{match.Groups["pre"].Value}{match.Groups["orig"].Value}";
        }
    }
}