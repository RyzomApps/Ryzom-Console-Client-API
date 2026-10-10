///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Minecraft Console Client'
// https://github.com/ORelio/Minecraft-Console-Client
// which is released under CDDL-1.0 License
// http://opensource.org/licenses/CDDL-1.0
// Copyright 2021 ORelio and Contributers
///////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace API.Commands
{
    /// <summary>
    /// CommandBase class with constructor for creating command for plugins.
    /// </summary>
    public class GenericCommand : CommandBase
    {
        private readonly IClient.CommandRunner _runner;

        /// <inheritdoc/>
        public override string CmdName { get; }

        /// <inheritdoc/>
        public override string CmdUsage { get; }

        /// <inheritdoc/>
        public override string CmdDesc { get; }

        /// <inheritdoc/>
        public override bool Run(IClient handler, string command, out string responseMsg, Dictionary<string, object> localVars)
        {
            // TODO: add a command runner with status and message
            responseMsg = _runner(command, GetArgs(command));
            return true;
        }

        /// <inheritdoc/>
        public override CommandCategory CmdCategory { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cmdName">Name of the command</param>
        /// <param name="cmdDesc">Description of the command. Support tranlation.</param>
        /// <param name="cmdUsage">Usage of the command</param>
        /// <param name="callback">Method for handling the command</param>
        public GenericCommand(string cmdName, string cmdDesc, string cmdUsage, IClient.CommandRunner callback)
        {
            CmdName = cmdName;
            CmdDesc = cmdDesc;
            CmdUsage = cmdUsage;
            CmdCategory = CommandCategory.Plugin;
            _runner = callback;
        }

        /// <summary>
        /// Constructor with explicit command category
        /// </summary>
        /// <param name="cmdName">Name of the command</param>
        /// <param name="cmdDesc">Description of the command. Support tranlation.</param>
        /// <param name="cmdUsage">Usage of the command</param>
        /// <param name="category">Category of the command</param>
        /// <param name="callback">Method for handling the command</param>
        public GenericCommand(string cmdName, string cmdDesc, string cmdUsage, CommandCategory category, IClient.CommandRunner callback)
        {
            CmdName = cmdName;
            CmdDesc = cmdDesc;
            CmdUsage = cmdUsage;
            CmdCategory = category;
            _runner = callback;
        }
    }
}