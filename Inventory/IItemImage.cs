///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

namespace API.Inventory
{
    public interface IItemImage
    {
        uint GetSheetId();
        ushort GetQuality();
        ushort GetQuantity();
        byte GetUserColor();
        byte GetCharacBuffs();
        uint GetPrice();
        uint GetWeight();
        uint GetNameId();
        byte GetInfoVersion();
        byte GetResaleFlag();
        bool GetLockedByOwner();
    }
}