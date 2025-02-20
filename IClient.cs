﻿///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Bukkit'
// https://github.com/Bukkit/Bukkit
// which is released under GNU General Public License v3.0.
// https://www.gnu.org/licenses/gpl-3.0.en.html
// Copyright 2021 Bukkit Team
///////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using API.Client;
using API.Database;
using API.Inventory;
using API.Logger;
using API.Network;
using API.Network.Web;
using API.Plugins.Interfaces;
using API.Sheet;

namespace API
{
    /// <summary>
    /// Represents a client implementation.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Returns the primary logger associated with this client instance.
        /// </summary>
        /// <returns>Logger associated with this client</returns>
        ILogger GetLogger();

        /// <summary>
        /// Returns the network manager which controls the connection to the ryzom server.
        /// </summary>
        /// <returns>network manager associated with this client</returns>
        INetworkManager GetApiNetworkManager();

        /// <summary>
        /// Returns the management for dynamically generated text from servers
        /// </summary>
        /// <returns>management for dynamically generated text</returns>
        IStringManager GetApiStringManager();

        /// <summary>
        /// Returns the class that encapsulates the separate client database components
        /// </summary>
        /// <returns>the client database manager</returns>
        IDatabaseManager GetApiDatabaseManager();

        /// <summary>
        /// Returns the manager for all plugins of the client
        /// </summary>
        /// <returns>manager for all plugins</returns>
        IPluginManager GetPluginManager();

        /// <summary>
        /// Returns the class that manages all sheets
        /// </summary>
        /// <returns>Class to manage all sheets</returns>
        ISheetManager GetApiSheetManager();

        /// <summary>
        /// Returns the class that can generate sheets from strings and numbers
        /// </summary>
        /// <returns>Factory for SheetIds from names and ids</returns>
        ISheetIdFactory GetApiSheetIdFactory();

        /// <summary>
        /// Returns the class that gives direct access to inventory slots (bag, hands, and equip inventory).
        /// </summary>
        /// <returns>Class to manage inventory slots</returns>
        IInventoryManager GetApiInventoryManager();

        /// <summary>
        /// Returns the class that can request websites and images from the web.
        /// </summary>
        /// <returns>Class to request websites and images from the web.</returns>
        IWebTransfer GetWebTransfer();

        /// <summary>
        /// Online State
        /// </summary>
        /// <returns>true when the client has entered a game and is online</returns>
        bool IsInGame();

        /// <summary>
        /// Perform an internal RCC command (not a server command, use SendText() instead for that!)
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="responseMsg">May contain a confirmation or error message after processing the command, or "" otherwise.</param>
        /// <param name="localVars">Local variables passed along with the command</param>
        /// <returns>true if the command was indeed an internal RCC command</returns>
        bool PerformInternalCommand(string command, out string responseMsg, Dictionary<string, object> localVars = null);

        /// <summary>
        /// Register a command in command prompt. Command will be automatically unregistered when unloading the Plugin.
        /// </summary>
        /// <param name="cmdName">Name of the command</param>
        /// <param name="cmdDesc">Description/usage of the command</param>
        /// <param name="cmdUsage">Usage example</param>
        /// <param name="callback">Method for handling the command</param>
        /// <returns>True if successfully registered</returns>
        bool RegisterCommand(string cmdName, string cmdDesc, string cmdUsage, CommandRunner callback);

        /// <summary>
        /// CommandBase runner definition.
        /// Returned string will be the output of the command
        /// </summary>
        /// <param name="command">Full command</param>
        /// <param name="args">Arguments in the command</param>
        /// <returns>CommandBase result to display to the user</returns>
        delegate string CommandRunner(string command, string[] args);

        /// <summary>
        /// Invoke a task on the main thread and wait for completion
        /// </summary>
        /// <param name="task">Task to run without return value</param>
        /// <example>InvokeOnMainThread(methodThatReturnsNothing);</example>
        /// <example>InvokeOnMainThread(() => methodThatReturnsNothing(argument));</example>
        /// <example>InvokeOnMainThread(() => { yourCode(); });</example>
        void InvokeOnMainThread(Action task);

        /// <summary>
        /// Invoke a task on the main thread, wait for completion and retrieve return value.
        /// </summary>
        /// <param name="task">Task to run with any type or return value</param>
        /// <returns>Any result returned from task, result type is inferred from the task</returns>
        /// <example>bool result = InvokeOnMainThread(methodThatReturnsAbool);</example>
        /// <example>bool result = InvokeOnMainThread(() => methodThatReturnsAbool(argument));</example>
        /// <example>int result = InvokeOnMainThread(() => { yourCode(); return 42; });</example>
        /// <typeparam name="T">Type of the return value</typeparam>
        T InvokeOnMainThread<T>(Func<T> task);

        /// <summary>
        /// Send a chat message or command to the server (Enqueues messages)
        /// </summary>
        /// <param name="text">Text to send to the server</param>
        void SendText(string text);
    }
}