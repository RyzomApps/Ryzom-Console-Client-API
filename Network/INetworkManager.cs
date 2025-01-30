using System.Collections.Generic;
using API.Entity;

namespace API.Network
{
    /// <summary>
    /// used to control the connection
    /// </summary>
    public interface INetworkManager
    {
        /// <summary>
        /// Send - updates when packets were received
        /// </summary>
        void Send(uint gameCycle);

        /// <summary>
        /// Updates the whole connection with the frontend.
        /// Call this method evently.
        /// </summary>
        /// <returns>'true' if data were sent/received.</returns>
        bool Update();

        /// <summary>
        /// Send updates
        /// </summary>
        void Send();

        /// <summary>
        /// Gets the current server tick per second
        /// </summary>
        double[] GetTps();

        /// <summary>
        /// sendMsgToServer Helper
        /// selects the message by its name and pushes it to the connection
        /// </summary>
        void SendMsgToServer(string sMsg);

        /// <summary>
        /// Buffers a target action
        /// </summary>
        void PushTarget(in byte slot);

        /// <summary>
        /// Returns the corresponding Entity Manager
        /// </summary>
        IEntityManager GetApiEntityManager();

        /// <summary>
        /// Last tick sent by the server from the network connection
        /// </summary>
        uint GetCurrentServerTick();

        /// <summary>
        /// This is the mainland selected for the selected character
        /// </summary>
        string PlayerSelectedHomeShardName { get; set; }

        bool FreeTrial { get; set; }

        string UserPrivileges { get; set; }
    }
}
