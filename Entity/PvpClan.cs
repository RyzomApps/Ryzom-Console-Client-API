///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

namespace API.Entity
{
    /// <remarks>
    /// If you change this enumeration do not forget to update database.xml and local_database.xml
    /// </remarks>
    public enum PvpClan
    {
        None,
        Neutral,

        // begin of "real" clans (None and Neutral are excluded)
        BeginClans,
        // begin the cults section
        BeginCults = BeginClans,

        Kami = BeginClans,
        Karavan,

        // end of cults
        EndCults = Karavan,
        // begin of civilizations
        BeginCivs,

        Fyros = BeginCivs,
        Matis,
        Tryker,
        Zorai,

        // end of civilizations
        EndCivs = Zorai,

        Marauder,
        // end of clans
        EndClans = Marauder,

        Unknown,
        NbClans = Unknown,
        // number of bits needed to store all valid values (all but Unknown)
        NbBits = 4
    };
}