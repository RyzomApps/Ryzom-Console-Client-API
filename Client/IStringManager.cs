using API.Network;

namespace API.Client
{
    /// <summary>
    /// Manage string organized as conditional clause grouped into phrase.
    /// This class can choose at runtime one of the clause depending
    /// on passed parameters.
    /// </summary>
    public interface IStringManager
    {
        /// <summary>
        /// request the stringId from the local cache or if missing ask the server
        /// </summary>
        bool GetString(uint stringId, out string result, INetworkManager networkManager);
    }
}