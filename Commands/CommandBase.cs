///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Minecraft Console Client'
// https://github.com/ORelio/Minecraft-Console-Client
// which is released under CDDL-1.0 License
// http://opensource.org/licenses/CDDL-1.0
// Copyright 2021 ORelio and Contributers
///////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Commands
{
    /// <summary>
    /// Represents an internal RCC command: CommandBase name, source code and usage message
    /// To add a new command, inherit from this class while adding the command class to the folder "Commands".
    /// If inheriting from the 'CommandBase' class and placed in the 'Commands' namespace, the command will be
    /// automatically loaded and available in main chat prompt, scripts, remote control and command help.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// The command name
        /// </summary>
        public abstract string CmdName { get; }

        /// <summary>
        /// CommandBase description with translation support. Please add your message in Translations.cs file and return mapping
        /// key in this property
        /// </summary>
        public abstract string CmdDesc { get; }

        /// <summary>
        /// Usage message, e.g.: '&lt;arg_1&gt; [arg_2]'
        /// </summary>
        /// <remarks>Should not contain the command itself. Arguments should not contain spaces.</remarks>
        public abstract string CmdUsage { get; }

        /// <summary>
        /// Category this command belongs to. Defaults to <see cref="CommandCategory.Plugin"/> for plugin-registered commands.
        /// </summary>
        public virtual CommandCategory CmdCategory => CommandCategory.Plugin;

        /// <summary>
        /// Get the translated version of command description.
        /// </summary>
        /// <returns>Translated command description</returns>
        public string GetCmdDescTranslated()
        {
            var s1 = string.IsNullOrEmpty(CmdUsage) ? "" : " §a"; // If either one is empty, no colon :
            var s2 = string.IsNullOrEmpty(CmdDesc) ? "" : "§e: §f"; // If either one is empty, no colon :

            return $"\u00a7e{CmdName}{s1}{CmdUsage}{s2}{CmdDesc}";
        }

        /// <summary>
        /// Perform the command
        /// </summary>
        /// <param name="handler">Client</param>
        /// <param name="command">The full command, eg: 'mycommand arg1 arg2'</param>
        /// <param name="localVars">Local variables passed along with the command (may be null)</param>
        /// <returns>A confirmation/error message, or "" if no message</returns>
        [Obsolete("Use overload with boolean return value")]
        public virtual string Run(IClient handler, string command, Dictionary<string, object> localVars)
        {
            var success = Run(handler, command, out var responseMsg, localVars);
            return success ? responseMsg : $"{command} failed";
        }

        /// <summary>
        /// Perform the command
        /// </summary>
        /// <param name="handler">Client</param>
        /// <param name="command">The full command, eg: 'mycommand arg1 arg2'</param>
        /// <param name="responseMsg">Output parameter for confirmation/error message, or empty string if no message</param>
        /// <param name="localVars">Local variables passed along with the command (may be null)</param>
        /// <returns>True if the command executed successfully, false otherwise</returns>
        public abstract bool Run(IClient handler, string command, out string responseMsg, Dictionary<string, object> localVars);


        /// <summary>
        /// Return a list of aliases for this command.
        /// Override this method if you wish to put aliases to the command
        /// </summary>
        public virtual IEnumerable<string> GetCmdAliases()
        {
            return [];
        }

        /// <summary>
        /// Run a command's remaining work on a background task and deliver the
        /// final result later through the client logger, like a normal command
        /// result. The command's Run should return true immediately with a text
        /// such as "Command sent - result follows...".
        /// The task (e.g. waiting for string responses) handles its own timeout.
        /// </summary>
        /// <param name="handler">Client; used to marshal the result back to the main thread</param>
        /// <param name="work">Work executed on the task pool; returns the final command result (success flag + rendered text)</param>
        protected static void RunAsyncTask(IClient handler, Func<(bool success, string text)> work)
        {
            Task.Run(() =>
            {
                (bool success, string text) result;

                try
                {
                    result = work();
                }
                catch (Exception e)
                {
                    result = (false, $"Command error: {e.Message}");
                }

                handler.InvokeOnMainThread(() =>
                {
                    var logger = handler.GetLogger();

                    if (result.success)
                        logger.Info(result.text);
                    else
                        logger.Error(result.text);
                });
            });
        }

        /// <summary>
        /// Check if at least one argument has been passed to the command
        /// </summary>
        public static bool HasArg(string command)
        {
            var firstSpace = command.IndexOf(' ');
            return firstSpace > 0 && firstSpace < command.Length - 1;
        }

        /// <summary>
        /// Extract the argument string from the command
        /// </summary>
        /// <returns>Argument or "" if no argument</returns>
        public static string GetArg(string command)
        {
            return HasArg(command) ? command[(command.IndexOf(' ') + 1)..] : "";
        }

        /// <summary>
        /// Extract the arguments as a string array from the command
        /// </summary>
        /// <returns>Argument array or empty array if no arguments</returns>
        public static string[] GetArgs(string command)
        {
            var args = GetArg(command).Split(' ');
            return args.Length == 1 && args[0] == "" ? new string[] { } : args;
        }
    }
}
