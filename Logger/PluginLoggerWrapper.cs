using API.Plugins.Interfaces;
using System.Text;

namespace API.Logger
{
    /// <summary>
    /// The PluginLoggerWrapper class is a modified <see cref="ILogger"/> that prepends all
    /// logging calls with the name of the plugin doing the logging. The API for
    /// PluginLoggerWrapper is exactly the same as <see cref="ILogger"/>.
    /// </summary>
    public class PluginLoggerWrapper : ILogger
    {
        private readonly string _pluginName;
        private readonly IClient _client;

        /// <summary>
        /// Creates a new PluginLoggerWrapper that extracts the name from a plugin.
        /// </summary>
        /// <param name="context">A reference to the plugin</param>
        /// <param name="client">The main client instance</param>
        public PluginLoggerWrapper(IPlugin context, IClient client)
        {
            _client = client;
            var prefix = context.GetDescription().GetPrefix();
            _pluginName = prefix != null ? new StringBuilder().Append('[').Append(prefix).Append("] ").ToString() : $"[{context.GetDescription().GetName()}] ";
        }

        /// <summary>
        /// Forwards the message to the client so listeners (e.g. Mimic) can
        /// observe other plugins' output. Never throws into the caller.
        /// </summary>
        private void Fire(string level, string msg)
        {
            try
            {
                _client?.OnPluginLog(_pluginName, level, msg);
            }
            catch
            {
                // Never let a listener fault break the logging plugin.
            }
        }

        /// <inheritdoc/>
        public bool DebugEnabled { get; set; } = false;

        /// <inheritdoc/>
        public bool WarnEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool InfoEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool ErrorEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool ChatEnabled { get; set; } = true;

        /// <inheritdoc/>
        public void Chat(string msg, params object[] args)
        {
            Chat(string.Format(msg, args));
        }

        /// <inheritdoc/>
        public void Chat(object msg)
        {
            Chat(msg.ToString());
        }

        /// <inheritdoc/>
        public void Debug(string msg, params object[] args)
        {
            Debug(string.Format(msg, args));
        }

        /// <inheritdoc/>
        public void Debug(object msg)
        {
            Debug(msg.ToString());
        }

        /// <inheritdoc/>
        public void Error(string msg, params object[] args)
        {
            Error(string.Format(msg, args));
        }

        /// <inheritdoc/>
        public void Error(object msg)
        {
            Error(msg.ToString());
        }

        /// <inheritdoc/>
        public void Info(string msg, params object[] args)
        {
            Info(string.Format(msg, args));
        }

        /// <inheritdoc/>
        public void Info(object msg)
        {
            Info(msg.ToString());
        }

        /// <inheritdoc/>
        public void Warn(string msg, params object[] args)
        {
            Warn(string.Format(msg, args));
        }

        /// <inheritdoc/>
        public void Warn(object msg)
        {
            Warn(msg.ToString());
        }

        /// <inheritdoc/>
        public void Debug(string msg)
        {
            if (!DebugEnabled)
                return;

            _client.GetLogger().Debug(_pluginName + msg);
            Fire("debug", msg);
        }

        /// <inheritdoc/>
        public void Info(string msg)
        {
            if (!InfoEnabled)
                return;

            _client.GetLogger().Info(_pluginName + msg);
            Fire("info", msg);
        }

        /// <inheritdoc/>
        public void Warn(string msg)
        {
            if (!WarnEnabled)
                return;

            _client.GetLogger().Warn(_pluginName + msg);
            Fire("warn", msg);
        }

        /// <inheritdoc/>
        public void Error(string msg)
        {
            if (!ErrorEnabled)
                return;

            _client.GetLogger().Error(_pluginName + msg);
            Fire("error", msg);
        }

        /// <inheritdoc/>
        public void Chat(string msg)
        {
            if (ChatEnabled)
                return;

            _client.GetLogger().Chat(_pluginName + msg);
            Fire("chat", msg);
        }
    }
}
