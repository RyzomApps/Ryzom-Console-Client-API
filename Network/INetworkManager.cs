///////////////////////////////////////////////////////////////////
// INetworkManager — full replacement for Ryzom-Console-Client/API/Network/INetworkManager.cs
// New: SendImpulse + SendMultipartAction (see bottom of the interface).
///////////////////////////////////////////////////////////////////

using System;
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

        /// <summary>
        /// Builds a named impulse (name from msg.xml), lets the callback fill
        /// the payload and pushes the packet to the connection (sent at next update).
        /// </summary>
        /// <param name="msgName">Message name, e.g. "CL_MAIN_CHARSHEET:..." or any name in msg.xml.</param>
        /// <param name="fillPayload">Optional callback writing the payload bits.</param>
        /// <returns>'false' if the name is unknown or the client is not connected.</returns>
        bool SendImpulse(string msgName, Action<IBitStreamWriter> fillPayload = null);

        /// <summary>
        /// Sends a GenericMultiPart action (FE::GenericMultiPart) directly over
        /// the network connection (sent at next update).
        /// </summary>
        /// <returns>'false' if the client is not connected.</returns>
        bool SendMultipartAction(byte number, short part, short nbBlock, byte[] partContent, bool allowExceedingMaxSize = true);
    }
}
