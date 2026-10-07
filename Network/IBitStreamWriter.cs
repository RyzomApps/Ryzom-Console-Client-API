///////////////////////////////////////////////////////////////////
// IBitStreamWriter
//
// Write-only interface for impulse payloads. Member names match the
// Serial() methods of Client/Stream/BitMemoryStream.cs so that
// implementations are one-liners and existing plugin payload writers
// (e.g. BugTests' Payload class) can implement it directly.
///////////////////////////////////////////////////////////////////

namespace API.Network
{
    /// <summary>
    /// Writes values in Ryzom wire format (as described in msg.xml) into an
    /// outgoing bit stream. Provided by INetworkManager.SendImpulse to plugins.
    /// </summary>
    public interface IBitStreamWriter
    {
        void U8(byte v);
        void S8(sbyte v);
        void U16(ushort v);
        void S16(short v);
        void S32(int v);
        void U32(uint v);
        void U64(ulong v);
        void F32(float v);
        void Bool(bool v);

        /// <summary>UTF-16 string ("uc" in msg.xml).</summary>
        void Uc(string v);

        /// <summary>Raw bytes.</summary>
        void Bytes(byte[] v);
    }
}
