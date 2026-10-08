///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace API.BotChat
{
    /// <summary>
    /// One entry of the trade list currently displayed by the server
    /// (an item, a skill, a pact, a mission item, ... depending on slot type).
    /// </summary>
    public struct TradeEntry
    {
        /// <summary>Slot index in the current page (0..7). Use it for Buy().</summary>
        public byte Index;
        /// <summary>Sheet id of the traded object (0 if the slot is empty).</summary>
        public uint SheetId;
        /// <summary>Item quality (0 for skills/pacts).</summary>
        public uint Quality;
        /// <summary>Quantity available in the stack.</summary>
        public uint Quantity;
        /// <summary>Price in the currency given by Currency.</summary>
        public uint Price;
        /// <summary>Currency of the price (see RYMSG::TTradeCurrency, 0 = dappers).</summary>
        public uint Currency;
        /// <summary>Type of the slot (see game_share/trade_slot_type.h).</summary>
        public uint SlotType;
        /// <summary>Vendor type (see TBotChatSellerType).</summary>
        public uint SellerType;
        /// <summary>Faction type if the price is paid in faction points (see pvp_clan.h).</summary>
        public uint FactionType;
        /// <summary>String manager id of the item name (0 = none).</summary>
        public uint NameId;
        /// <summary>String manager id of the vendor name for resale items (0 = none).</summary>
        public uint VendorNameId;
        /// <summary>True if the local player meets the prerequisites for this entry.</summary>
        public bool PrerequisitValid;
    }

    /// <summary>
    /// Gives plugins access to the bot chat (trading) session of the client:
    /// events when a chat opens/closes, impulses to start a trade, browse
    /// pages and buy items, and read access to the trade list database branch.
    /// </summary>
    public interface IBotChatManager
    {
        /// <summary>
        /// Raised when a bot opens a dynamic chat (BOTCHAT:DYNCHAT_OPEN).
        /// </summary>
        /// <param name="botUid">Entity id of the bot.</param>
        event Action<uint> OnDynChatOpen;

        /// <summary>
        /// Raised when a bot closes a dynamic chat (BOTCHAT:DYNCHAT_CLOSE).
        /// </summary>
        /// <param name="botUid">Entity id of the bot.</param>
        event Action<uint> OnDynChatClose;

        /// <summary>
        /// Raised when the server force-ends the bot chat (BOTCHAT:FORCE_END),
        /// e.g. when the player moves away from the bot.
        /// </summary>
        event Action OnSessionForceEnd;

        /// <summary>
        /// Raised when the content of the trade list (SERVER:TRADING) changed
        /// after a new page or a refresh was received.
        /// </summary>
        event Action OnTradeListUpdated;

        /// <summary>
        /// Id of the current trade session as sent by the client in the last
        /// StartTrade* call (0 when no trade session is running).
        /// </summary>
        ushort CurrentSessionId { get; }

        /// <summary>Page id of the trade list currently loaded (0..N, read from the database).</summary>
        uint PageId { get; }

        /// <summary>True if the server announced more pages after the current one.</summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Read the current trade list from the client database branch SERVER:TRADING.
        /// </summary>
        /// <returns>Up to 8 entries; empty slots (sheet id 0) are skipped.</returns>
        List<TradeEntry> GetTradeList();

        /// <summary>
        /// Start a trade session for items with the bot currently in front of the player.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendStartTradeItem();

        /// <summary>
        /// Start a trade session for teleports with the bot currently in front of the player.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendStartTradeTeleport();

        /// <summary>
        /// Start a trade session for faction items with the bot currently in front of the player.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendStartTradeFaction();

        /// <summary>
        /// Start a trade session for skills with the bot currently in front of the player.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendStartTradeSkill();

        /// <summary>
        /// Start a trade session for pacts with the bot currently in front of the player.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendStartTradePact();

        /// <summary>
        /// Start a trade session for actions (phrases) with the bot currently in front of the player.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendStartTradeAction();

        /// <summary>
        /// Ask the server for the next page of the current item trade session.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendNextTradePage();

        /// <summary>
        /// Ask the server to refresh the trade list of the current bot.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendRefreshTradeList();

        /// <summary>
        /// Set the filters of the trade list. Pass 0xFFFFFFFF for "no filter" on
        /// quality/price, 0 for class/part/type to disable them.
        /// </summary>
        /// <param name="minQuality">Minimum item quality (uint.MaxValue = no filter)</param>
        /// <param name="maxQuality">Maximum item quality (uint.MaxValue = no filter)</param>
        /// <param name="minPrice">Minimum price (uint.MaxValue = no filter)</param>
        /// <param name="maxPrice">Maximum price (uint.MaxValue = no filter)</param>
        /// <param name="minClass">Minimum RM class type (0 = no filter)</param>
        /// <param name="maxClass">Maximum RM class type (0 = no filter)</param>
        /// <param name="itemPart">Item part filter (0 = no filter)</param>
        /// <param name="itemType">Item type filter (0 = no filter)</param>
        /// <returns>True if the impulse was sent.</returns>
        bool SendSetFilters(uint minQuality, uint maxQuality, uint minPrice, uint maxPrice, byte minClass, byte maxClass, byte itemPart, byte itemType);

        /// <summary>
        /// Buy an item of the current trade list.
        /// </summary>
        /// <param name="index">Index of the entry in the trade list (0..7)</param>
        /// <param name="quantity">Quantity to buy</param>
        /// <returns>True if the impulse was sent.</returns>
        bool SendBuyItem(byte index, ushort quantity);

        /// <summary>
        /// Sell an item from the inventory to the current vendor (BOTCHAT:SELL).
        /// Format: u8 inventory, u16 slot, u16 quantity, u32 price.
        /// The server requires the player to stand in front of a merchant
        /// (active interlocutor). Inventory ids: 0 handling, 1 temporary,
        /// 2 equipment, 3 hotbar, 4 bag.
        /// </summary>
        /// <param name="inventoryId">Inventory id (bag = 4).</param>
        /// <param name="slot">Slot index inside the inventory.</param>
        /// <param name="quantity">Quantity to sell.</param>
        /// <param name="price">Unit price offered to the vendor.</param>
        bool SendSell(byte inventoryId, ushort slot, ushort quantity, uint price);

        /// <summary>
        /// End the current trade session (BOTCHAT:END). A new session must be
        /// started afterwards to continue trading.
        /// </summary>
        /// <returns>True if the impulse was sent.</returns>
        bool SendEndTrade();
    }
}
