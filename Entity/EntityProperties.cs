///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

using System;
using System.Collections;

namespace API.Entity
{
    /// <summary>
    /// Class to manage some properties of an entity.
    /// </summary>
    /// <author>Guillaume PUZIN</author> 
    /// <author>Nevrax France</author> 
    /// <date>2001</date> 
    public class EntityProperties
    {
        private readonly BitArray _properties;

        /// <summary>
        /// Indexed Property
        /// </summary>
        public bool this[uint nBitNbWanted] => _properties[(int)nBitNbWanted];

        /// <summary>
        /// Possibility to select the entity 
        /// </summary>
        public bool IsSelectable { get => _properties[0]; set => _properties[0] = value; }

        public bool IsGivable { get => _properties[1]; set => _properties[1] = value; }

        /// <summary>
        /// Possibility to talk to the entity
        /// </summary>
        public bool IsTalkableTo { get => _properties[2]; set => _properties[2] = value; }

        public bool IsUsable { get => _properties[3]; set => _properties[3] = value; }

        public bool IsLiftable { get => _properties[4]; set => _properties[4] = value; }

        public bool IsLookableAt { get => _properties[5]; set => _properties[5] = value; }

        /// <summary>
        /// Possibility to attack the entity
        /// </summary>
        public bool IsAttackable { get => _properties[6]; set => _properties[6] = value; }

        /// <summary>
        /// Possibility to heal or cure the entity
        /// </summary>
        public bool IsCurativable { get => _properties[7]; set => _properties[7] = value; }

        /// <summary>
        /// Possibility to invite the entity into a team
        /// </summary>
        public bool IsInvitable { get => _properties[8]; set => _properties[8] = value; }

        /// <summary>
        /// Possibility to harvest the entity
        /// </summary>
        public bool IsHarvestable { get => _properties[9]; set => _properties[9] = value; }

        /// <summary>
        /// Possibility to exchange items with the entity
        /// </summary>
        public bool CanExchangeItem { get => _properties[10]; set => _properties[10] = value; }

        /// <summary>
        /// Possibility to mount the entity
        /// </summary>
        public bool IsMountable { get => _properties[11]; set => _properties[11] = value; }

        /// <summary>
        /// Possibility to loot the entity
        /// </summary>
        public bool IsLootable { get => _properties[12]; set => _properties[12] = value; }

        /// <summary>
        /// Entity is AFK
        /// </summary>
        public bool IsAfk { get => _properties[13]; set => _properties[13] = value; }

        /// <summary>
        /// Entity is invulnerable
        /// </summary>
        public bool IsInvulnerable { get => _properties[14]; set => _properties[14] = value; }

        public bool FreeProperty { get => _properties[15]; set => _properties[15] = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityProperties()
        {
            _properties = new BitArray(16);
        }

        /// <summary>
        /// Constructor from value (2 Bytes - 16 Bits)
        /// </summary>
        public EntityProperties(ushort p)
        {
            _properties = new BitArray(BitConverter.GetBytes(p));
        }
    }
}