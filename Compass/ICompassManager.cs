///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace API.Compass
{
    /// <summary>
    /// One compass landmark received from the server.
    /// Mirrors the official client's CCompassDialogsManager entries.
    /// </summary>
    public struct CompassEntry
    {
        /// <summary>X position in world units (divide by 1000 for meters/in-game coordinates).</summary>
        public int X;
        /// <summary>Y position in world units (divide by 1000 for meters/in-game coordinates).</summary>
        public int Y;
        /// <summary>String manager id of the landmark title (0 = none).</summary>
        public uint TextId;
        /// <summary>Compressed index of the tracked bot (only for JOURNAL:ADD_COMPASS_BOT, uint.MaxValue = none).</summary>
        public uint BotCompressedIndex;
        /// <summary>True when the entry came from JOURNAL:ADD_COMPASS_BOT (moving bot target).</summary>
        public bool IsBot;
    }

    /// <summary>
    /// Tracks the compass landmarks the server sends (mission targets and
    /// other JOURNAL:ADD_COMPASS entries).
    /// </summary>
    public interface ICompassManager
    {
        /// <summary>Current compass entries as sent by the server.</summary>
        /// <returns>Read-only list of entries</returns>
        IReadOnlyList<CompassEntry> GetEntries();

        /// <summary>
        /// Raised whenever an entry is added or removed.
        /// </summary>
        event Action OnCompassChanged;
    }
}
