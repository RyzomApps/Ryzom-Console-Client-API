///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

using API.Client;
using API.Sheet;
using System.Collections.Generic;

namespace API.Helper
{
    /// <summary>
    /// Resolves display names for trade list and inventory items.
    ///
    /// Two-phase usage: call <see cref="RequestNames"/> once with all name ids
    /// of the entries to display (this fires STRING_MANAGER:STRING_RQ impulses
    /// for every id not cached yet), then resolve each entry with
    /// <see cref="Resolve"/> after the server answered (e.g. on the next
    /// command run or via the string-manager cache).
    /// </summary>
    public static class ItemNameResolver
    {
        /// <summary>
        /// Request the display strings for all given string ids that are not
        /// cached yet. Missing ids are requested from the server; already
        /// cached ids are left untouched.
        /// </summary>
        /// <param name="client">Client providing string manager and network.</param>
        /// <param name="nameIds">String manager ids (0 is skipped).</param>
        public static void RequestNames(IClient client, IEnumerable<uint> nameIds)
        {
            var stringManager = client.GetApiStringManager();
            var networkManager = client.GetApiNetworkManager();

            foreach (var nameId in nameIds)
            {
                if (nameId == 0)
                    continue;

                // GetString returns false for uncached ids and sends the
                // request; for cached ids it returns the value immediately.
                stringManager.GetString(nameId, out _, networkManager);
            }
        }

        /// <summary>
        /// Resolve a display name: cached string from the string manager if
        /// available, otherwise the sheet name from sheet_id.bin, otherwise a
        /// numeric fallback.
        /// </summary>
        /// <param name="client">Client providing string manager and sheet factory.</param>
        /// <param name="nameId">String manager id of the item name (0 = none).</param>
        /// <param name="sheetId">Sheet id used as fallback (may be 0).</param>
        /// <returns>The resolved name; never null or empty.</returns>
        public static string Resolve(IClient client, uint nameId, uint sheetId)
        {
            if (nameId != 0 &&
                client.GetApiStringManager().GetString(nameId, out var name, client.GetApiNetworkManager()) &&
                !string.IsNullOrEmpty(name))
            {
                return name;
            }

            return SheetName(client, sheetId);
        }

        /// <summary>
        /// Sheet name fallback: human readable sheet name from sheet_id.bin or
        /// a numeric placeholder if the sheet id is unknown or zero.
        /// </summary>
        public static string SheetName(IClient client, uint sheetId)
        {
            if (sheetId == 0)
                return "<unknown>";

            ISheetId id = client.GetApiSheetIdFactory().SheetId(sheetId);
            var name = id.Name;

            return string.IsNullOrEmpty(name) ? $"sheet {sheetId}" : name;
        }
    }
}
