// ReSharper disable InconsistentNaming

using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace API.Chat
{
    /// <summary>
    /// All supported color values for chat
    /// </summary>
    public class ChatColor
    {
        /// <summary>
        /// Represents black
        /// </summary>
        public static string BLACK = "§0";

        /// <summary>
        /// Represents dark blue
        /// </summary>
        public static string DARK_BLUE = "§1";

        /// <summary>
        /// Represents dark green
        /// </summary>
        public static string DARK_GREEN = "§2";

        /// <summary>
        /// Represents dark blue (aqua)
        /// </summary>
        public static string DARK_AQUA = "§3";

        /// <summary>
        /// Represents dark red
        /// </summary>
        public static string DARK_RED = "§4";

        /// <summary>
        /// Represents dark purple
        /// </summary>
        public static string DARK_PURPLE = "§5";

        /// <summary>
        /// Represents gold
        /// </summary>
        public static string GOLD = "§6";

        /// <summary>
        /// Represents gray
        /// </summary>
        public static string GRAY = "§7";

        /// <summary>
        /// Represents dark gray
        /// </summary>
        public static string DARK_GRAY = "§8";

        /// <summary>
        /// Represents blue
        /// </summary>
        public static string BLUE = "§9";

        /// <summary>
        /// Represents green
        /// </summary>
        public static string GREEN = "§a";

        /// <summary>
        /// Represents aqua
        /// </summary>
        public static string AQUA = "§b";

        /// <summary>
        /// Represents red
        /// </summary>
        public static string RED = "§c";

        /// <summary>
        /// Represents light purple
        /// </summary>
        public static string LIGHT_PURPLE = "§d";

        /// <summary>
        /// Represents yellow
        /// </summary>
        public static string YELLOW = "§e";

        /// <summary>
        /// Represents white
        /// </summary>
        public static string WHITE = "§f";

        /// <summary>
        /// Represents magical characters that change around randomly
        /// </summary>
        public static string MAGIC = "§k";

        /// <summary>
        /// Makes the text bold.
        /// </summary>
        public static string BOLD = "§l";

        /// <summary>
        /// Makes a line appear through the text.
        /// </summary>
        public static string STRIKETHROUGH = "§m";

        /// <summary>
        /// Makes the text appear underlined.
        /// </summary>
        public static string UNDERLINE = "§n";


        private const string _minecraftColorPattern = @"\@\{[0-9ABCDEFabcdef]{4}\}";

        /// <summary>
        /// Tries to replace Ryzom colors (e.g. @{F0FF}) with Minecraft colors
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ReplaceRyzomColors(string msg)
        {
            try
            {
                if (msg.Length < 7)
                    return msg;

                // Instantiate the regular expression object.
                var r = new Regex(_minecraftColorPattern, RegexOptions.IgnoreCase);

                // Match the regular expression pattern against a text string.
                var m = r.Match(msg);

                while (m.Success)
                {
                    var colorcode = m.Value[2..^1];

                    var color = Color.FromArgb(255, // hardcoded opaque
                        int.Parse(colorcode.Substring(0, 1) + colorcode.Substring(0, 1), NumberStyles.HexNumber),
                        int.Parse(colorcode.Substring(1, 1) + colorcode.Substring(1, 1), NumberStyles.HexNumber),
                        int.Parse(colorcode.Substring(2, 1) + colorcode.Substring(2, 1), NumberStyles.HexNumber));

                    var consoleColor = FromColor(color);

                    msg = msg.Replace(m.Value, "§" + GetMinecraftColorFromConsoleColor(consoleColor));
                    m = m.NextMatch();
                }
            }
            catch
            {
                // ignored
            }

            return msg;
        }

        /// <summary>
        /// Makes the text italic.
        /// </summary>
        public static string ITALIC = "§o";

        /// <summary>
        /// Resets all previous chat colors or formats.
        /// </summary>
        public static string RESET = "§r";

        /// <summary>
        /// Get the console color from a color specified
        /// </summary>
        public static ConsoleColor FromColor(Color c)
        {
            var index = (c.R > 128) | (c.G > 128) | (c.B > 128) ? 8 : 0; // Bright bit
            index |= c.R > 64 ? 4 : 0; // Red bit
            index |= c.G > 64 ? 2 : 0; // Green bit
            index |= c.B > 64 ? 1 : 0; // Blue bit
            return (ConsoleColor)index;
        }

        /// <summary>
        /// returns the console color code for a minecraft channel type
        /// </summary>
        public static string GetMinecraftColorForChatGroupType(ChatGroupType mode)
        {
            var color = mode switch
            {
                ChatGroupType.DynChat => AQUA,
                ChatGroupType.Shout => RED,
                ChatGroupType.Team => BLUE,
                ChatGroupType.Guild => GREEN,
                ChatGroupType.Civilization => LIGHT_PURPLE,
                ChatGroupType.Territory => LIGHT_PURPLE,
                ChatGroupType.Universe => GOLD,
                ChatGroupType.Region => GRAY,
                ChatGroupType.Tell => WHITE,
                _ => WHITE
            };

            return color;
        }

        /// <summary>
        /// Remove color codes (e.g. "§c") from a text message received from the server
        /// </summary>
        public static string GetVerbatim(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var idx = 0;
            var data = new char[text.Length];

            for (var i = 0; i < text.Length; i++)
                if (text[i] != '§')
                    data[idx++] = text[i];
                else
                    i++;

            return new string(data, 0, idx);
        }

        /// <summary>
        /// returns the console color code for minecraft color char
        /// </summary>
        /// <param name="minecraftColorChar">char representing the minecraft color</param>
        /// <returns>a console color</returns>
        public static ConsoleColor GetConsoleColorFromMinecraftColor(char minecraftColorChar)
        {
            return minecraftColorChar switch
            {
                '0' => ConsoleColor.Gray, //Should be Black but Black is non-readable on a black background
                '1' => ConsoleColor.DarkBlue,
                '2' => ConsoleColor.DarkGreen,
                '3' => ConsoleColor.DarkCyan,
                '4' => ConsoleColor.DarkRed,
                '5' => ConsoleColor.DarkMagenta,
                '6' => ConsoleColor.DarkYellow,
                '7' => ConsoleColor.Gray,
                '8' => ConsoleColor.DarkGray,
                '9' => ConsoleColor.Blue,
                'a' => ConsoleColor.Green,
                'b' => ConsoleColor.Cyan,
                'c' => ConsoleColor.Red,
                'd' => ConsoleColor.Magenta,
                'e' => ConsoleColor.Yellow,
                'f' => ConsoleColor.White,
                'r' => ConsoleColor.Gray,
                _ => ConsoleColor.Gray
            };
        }

        /// <summary>
        /// returns the minecraft color char for console color code
        /// </summary>
        /// <param name="consoleColor">char representing the minecraft color</param>
        /// <returns>a minecraft color char</returns>
        public static char GetMinecraftColorFromConsoleColor(ConsoleColor consoleColor)
        {
            return consoleColor switch
            {
                ConsoleColor.Black => '0',
                ConsoleColor.DarkBlue => '1',
                ConsoleColor.DarkGreen => '2',
                ConsoleColor.DarkCyan => '3',
                ConsoleColor.DarkRed => '4',
                ConsoleColor.DarkMagenta => '5',
                ConsoleColor.DarkYellow => '6',
                ConsoleColor.Gray => '7',
                ConsoleColor.DarkGray => '8',
                ConsoleColor.Blue => '9',
                ConsoleColor.Green => 'a',
                ConsoleColor.Cyan => 'b',
                ConsoleColor.Red => 'c',
                ConsoleColor.Magenta => 'd',
                ConsoleColor.Yellow => 'e',
                ConsoleColor.White => 'f',
                _ => throw new ArgumentOutOfRangeException(nameof(consoleColor), consoleColor, null)
            };
        }
    }
}
