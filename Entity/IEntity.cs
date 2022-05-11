///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

using System.Numerics;

namespace API.Entity
{
    public interface IEntity
    {
        Vector3 Pos { get; }

        Vector3 Front { get; set; }

        Vector3 Dir { get; set; }
    }
}