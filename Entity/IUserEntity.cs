///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

namespace API.Entity
{
    public interface IUserEntity : IEntity
    {
        /// <summary>
        /// Change the entity selected.
        /// </summary>
        /// <param name="slot">slot now selected (CLFECOMMON::INVALID_SLOT for an empty selection)</param>
        /// <param name="client">client associated with the entity</param>
        /// <remarks>Can be different from the entity targeted (in combat mode for example)</remarks>
        void Selection(byte slot);

        /// <summary>
        /// Default attack on the current selection.
        /// </summary>
        void Attack();
    }
}