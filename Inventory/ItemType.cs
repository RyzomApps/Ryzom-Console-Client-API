///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

namespace API.Inventory
{
    /// <summary>
    /// Sell filter uses two 64b database values to build a bitfield, so the item type limit is 128 for now
    /// </summary>
    public enum ItemType
    {
        DAGGER,
        SWORD,
        MACE,
        AXE,
        SPEAR,
        STAFF,
        TWO_HAND_SWORD,
        TWO_HAND_AXE,
        PIKE,
        TWO_HAND_MACE,
        AUTOLAUCH,
        BOWRIFLE,
        LAUNCHER,
        PISTOL,
        BOWPISTOL,
        RIFLE,
        AUTOLAUNCH_AMMO,
        BOWRIFLE_AMMO,
        LAUNCHER_AMMO,
        PISTOL_AMMO,
        BOWPISTOL_AMMO,
        RIFLE_AMMO,
        SHIELD,
        BUCKLER,
        LIGHT_BOOTS,
        LIGHT_GLOVES,
        LIGHT_PANTS,
        LIGHT_SLEEVES,
        LIGHT_VEST,
        MEDIUM_BOOTS,
        MEDIUM_GLOVES,
        MEDIUM_PANTS,
        MEDIUM_SLEEVES,
        MEDIUM_VEST,
        HEAVY_BOOTS,
        HEAVY_GLOVES,
        HEAVY_PANTS,
        HEAVY_SLEEVES,
        HEAVY_VEST,
        HEAVY_HELMET,
        ANKLET,
        BRACELET,
        DIADEM,
        EARING,
        PENDANT,
        RING,
        SHEARS,
        ArmorTool,
        AmmoTool,
        MeleeWeaponTool,
        RangeWeaponTool,
        JewelryTool,
        ToolMaker,
        CAMPSFIRE,
        MEKTOUB_PACKER_TICKET,
        MEKTOUB_MOUNT_TICKET,
        ANIMAL_TICKET,
        FOOD,
        MAGICIAN_STAFF,
        HAIR_MALE,
        HAIRCOLOR_MALE,
        TATOO_MALE,
        HAIR_FEMALE,
        HAIRCOLOR_FEMALE,
        TATOO_FEMALE,
        SERVICE_STABLE,
        JOB_ELEMENT,
        GENERIC,

        UNDEFINED,
        NB_ITEM_TYPE = UNDEFINED,
        LIMIT_64 = 64
    }

}
