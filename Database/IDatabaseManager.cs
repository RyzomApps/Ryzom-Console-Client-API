///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

namespace API.Database
{
    public interface IDatabaseManager
    {
        /// <summary>
        /// Return the value of a property (the update flag is set to false)
        /// </summary>
        /// <param name="name">is the name of the property</param>
        /// <returns>the value of the property</returns>
        long GetProp(string name);

        /// <summary>
        /// Resets the specified bank. All leaf values are set to 0.
        /// </summary>
        /// <param name="gc">GameCycle (no idea what it is exactly, probably some time value)</param>
        /// <param name="bank">The banks we want to reset</param>
        void ResetBank(in uint gc, in uint bank);
    }
}