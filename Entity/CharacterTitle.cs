﻿///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

using System;

namespace API.Entity
{
    /// <summary>
    /// Enum of character (player) title
    /// </summary>
    /// <author>Matthieu 'Trap' Besson</author> 
    /// <author>Nevrax France</author> 
    /// <date>October 2003</date>
    public static class CharacterTitle
    {
        public enum Titles
        {
            Refugee = 0,
            Homin,
            Novice_Artisan,
            Novice_Warrior,
            Novice_Harvester,
            Novice_Magician,
            Artisan_Apprentice,
            Magician_Apprentice,
            Defensive_Magician,
            Offensive_Magician,
            Mentalist,
            Summoner,
            Master_Of_Illusion,
            Mind_Lord,
            Healer,
            Priest,
            Master_Of_Life,
            Celestial_Guardian,
            Disturber,
            Affliction_Bringer,
            Master_Of_Torment,
            Avatar_Of_Sorrow,
            Destroyer,
            Archmage,
            Master_Of_Pain,
            Avatar_Of_Destruction,
            Elementalist,
            Alchemist,
            Biomancer,
            Master_Of_Energies,
            Chosen_Of_Atys,
            Warrior_Apprentice,
            Melee_Warrior,
            Range_Fighter,
            Light_Armsman,
            Heavy_Armsman,
            Close_Fighter,
            Gunman,
            Heavy_Gunman,
            Advanced_Gunman,
            Advanced_Heavy_Gunman,
            Bludgeoner,
            Skewerer,
            Slasher,
            Smasher,
            Impaler,
            Cleaver,
            Advanced_Close_Fighter,
            Maceman,
            Brute,
            Spearman,
            Axeman,
            Swordsman,
            Heavy_Maceman,
            Pikeman,
            Heavy_Axeman,
            Heavy_Swordsman,
            Knifeman,
            Hand_To_Hand_Fighter,
            Bowman,
            Pistoleer,
            Heavy_Bowman,
            Artilleryman,
            Rifleman,
            Master_Maceman,
            Master_Brute,
            Master_Spearman,
            Master_Axeman,
            Master_Swordsman,
            Master_Heavy_Maceman,
            Master_Pikeman,
            Master_Heavy_Axeman,
            Master_Heavy_Swordsman,
            Master_Knifeman,
            Master_Hand_To_Hand_Fighter,
            Master_Bowman,
            Master_Pistoleer,
            Master_Heavy_Bowman,
            Master_Artilleryman,
            Master_Rifleman,
            Armorer_Apprentice,
            Jeweler_Apprentice,
            Melee_Weapon_Smith_Apprentice,
            Range_Weapon_Smith_Apprentice,
            Heavy_Armorer,
            Light_Armorer,
            Medium_Armorer,
            Shield_Smith,
            Jeweler,
            Melee_Weapon_Smith,
            Melee_Heavy_Weapon_Smith,
            Melee_Light_Weapon_Smith,
            Range_Weapon_Smith,
            Range_Heavy_Weapon_Smith,
            Advanced_Heavy_Armorer,
            Advanced_Light_Armorer,
            Advanced_Medium_Armorer,
            Advanced_Shield_Smith,
            Advanced_Jeweler,
            Advanced_Melee_Weapon_Smith,
            Advanced_Melee_Heavy_Weapon_Smith,
            Advanced_Melee_Light_Weapon_Smith,
            Advanced_Range_Weapon_Smith,
            Advanced_Range_Heavy_Weapon_Smith,
            Expert_Heavy_Armorer,
            Expert_Light_Armorer,
            Expert_Medium_Armorer,
            Expert_Shield_Smith,
            Expert_Jeweler,
            Expert_Melee_Weapon_Smith,
            Expert_Melee_Heavy_Weapon_Smith,
            Expert_Melee_Light_Weapon_Smith,
            Expert_Range_Weapon_Smith,
            Expert_Range_Heavy_Weapon_Smith,
            Heavy_Armorer_Master,
            Light_Armorer_Master,
            Medium_Armorer_Master,
            Shield_Smith_Master,
            Jeweler_Master,
            Melee_Weapon_Smith_Master,
            Melee_Heavy_Weapon_Smith_Master,
            Melee_Light_Weapon_Smith_Master,
            Range_Weapon_Smith_Master,
            Range_Heavy_Weapon_Smith_Master,
            Forager_Apprentice,
            Forager,
            Desert_Forager,
            Forest_Forager,
            Jungle_Forager,
            Lacustre_Forager,
            Prime_Roots_Forager,
            Advanced_Desert_Forager,
            Advanced_Forest_Forager,
            Advanced_Jungle_Forager,
            Advanced_Lacustre_Forager,
            Advanced_Prime_Roots_Forager,
            Expert_Desert_Forager,
            Expert_Forest_Forager,
            Expert_Jungle_Forager,
            Expert_Lacustre_Forager,
            Expert_Prime_Roots_Forager,
            Master_Desert_Forager,
            Master_Forest_Forager,
            Master_Jungle_Forager,
            Master_Lacustre_Forager,
            Master_Prime_Roots_Forager,
            Kami_Ally,
            Karavan_Ally,

            Title00000,
            Title00001, // Journeyer
            Title00002, // Novice Kitin Hunter
            Title00003, // Kitin Hunter
            Title00004, // Master Kitin Hunter
            Title00005, // Kitin Eradicator
            Title00006, // Kitin Mass Murderer
            Title00007, // Matis Guardian
            Title00008, // Fyros Guardian
            Title00009, // Tryker Guardian
            Title00010, // Zorai Guardian
            Title00011, // Atys Guardian
            Title00012,
            Title00013,
            Title00014, // The fortunate
            Title00015, // Jinxed
            Title00016,
            Title00017,
            Title00018,
            Title00019,
            Title00020, // Fyros Patriot
            Title00021, // Matis Vassal
            Title00022, // Tryker Citizen
            Title00023, // Zorai Initiate
            Title00024, // Kami Disciple
            Title00025, // Karavan Follower
            Title00026, // Fyros Akenak
            Title00027, // Matis Noble
            Title00028, // Tryker Taliar
            Title00029, // Zorai Awakened
            Title00030, // Marauder
            Title00031, // Fyros Ambassador
            Title00032, // Matis Ambassador
            Title00033, // Tryker Ambassador
            Title00034, // Zorai Ambassador
            Title00035,
            Title00036,
            Title00037,
            Title00038,
            Title00039,
            Title00040,
            Title00041,
            Title00042,
            Title00043,
            Title00044,
            Title00045,
            Title00046,
            Title00047, // Machinegunner
            Title00048, // Assault Machinegunner
            Title00049,
            Title00050, // Apprentice Butcher
            Title00051, // Butcher
            Title00052, // Apprentice Florist
            Title00053, // Florist
            Title00054, // Apprentice Water-Carrier
            Title00055, // Water-Carrier
            Title00056, // Apprentice Magnetic
            Title00057, // Magnetic Cartographe
            Title00058, // Apprentice Toolmaker
            Title00059, // Toolmaker
            Title00060, // Apprentice Rescuer
            Title00061, // Rescuer
            Title00062, // Apprentice Larvester
            Title00063, // Larvester
            Title00064, // Apprentice Scrollmaker
            Title00065, // Scrollmaker
            Title00066,
            Title00067,
            Title00068,
            Title00069,
            Title00070,
            Title00071,
            Title00072,
            Title00073,
            Title00074,
            Title00075,
            Title00076,
            Title00077,
            Title00078,
            Title00079, // Wayfarer

            // Special title for focus beta testers
            WIND = Title00079, // Title for player come from old Windermeer community
            FBT,

            // GM and GUIDE titles
            BeginGmTitle,
            SGM = BeginGmTitle,
            GM,
            VG,
            SG,
            G,

            // other reserved titles
            CM,
            EM,
            EG,
            OBSERVER,
            EndGmTitle = OBSERVER,

            NB_CHARACTER_TITLE
        }

        /// <summary>
        /// Return the unlocalized string
        /// </summary>
        public static string ToString(in Titles r)
        {
            return r.ToString();
        }

        public static Titles ToCharacterTitle(in string ct)
        {
            Enum.TryParse<Titles>(ct, out var value);
            return value;
        }

        /// <summary>
        /// Get a character title linked with a special privilege ( gm, guide,...)
        /// </summary>
        public static Titles GetGmTitleFromPrivilege(in string privilege)
        {
            if (privilege.Length < 3)
            {
                return Titles.NB_CHARACTER_TITLE;
            }

            var buf = privilege[1..^1];
            return ToCharacterTitle(buf);
        }

        public static bool IsCsrTitle(in string title)
        {
            var titleEnum = ToCharacterTitle(title);
            var bIsCsrTitle = (titleEnum >= Titles.SGM && titleEnum <= Titles.CM);

            return bIsCsrTitle;
        }
    }
}
