///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace API.Inventory
{

    public interface IInventoryManager
    {
        /// <summary>
        /// Equip a part of the player with a bag entry (the params are path in the database)
        /// </summary>
        /// <param name="bagPath">INVENTORY:BAG:165</param>
        /// <param name="invPath">INVENTORY:HAND:0 OR INVENTORY:EQUIP:5</param>
        void Equip(in string bagPath, in string invPath);

        /// <summary>
        /// Get hand item. Can be NULL (nothing in the hand)
        /// </summary>
        IItemImage GetHandItem(uint index);

        /// <summary>
        /// Get equip item
        /// </summary>
        IItemImage GetEquipmentItem(uint index);

        /// <summary>
        /// Get item of pack animal. BeastIndex ranges from 0 to (MAX_INVENTORY_ANIMAL - 1)
        /// </summary>
        IItemImage GetPackAnimalItem(uint beastIndex, uint index);

        /// <summary>
        /// Get item of bag
        /// </summary>
        IItemImage GetBagItem(uint index);

        /// <summary>
        /// List all non-empty bag entries (live read, works even when the
        /// inventory was initialized before the bag branch was received).
        /// </summary>
        List<IBagEntry> GetBagEntries();

        /// <summary>
        /// List all non-empty hand entries (handling branch: right hand, left hand).
        /// Index of the entries is the referenced bag slot.
        /// </summary>
        List<IBagEntry> GetHandEntries();

        /// <summary>
        /// List all non-empty hotbar entries (pocket slots).
        /// Index of the entries is the referenced bag slot.
        /// </summary>
        List<IBagEntry> GetHotbarEntries();
    }

    /// <summary>
    /// One non-empty bag slot.
    /// </summary>
    public interface IBagEntry
    {
        /// <summary>Slot index in the bag (used as slot for selling).</summary>
        uint Index { get; }

        /// <summary>The item in this slot.</summary>
        IItemImage Item { get; }
    }
}