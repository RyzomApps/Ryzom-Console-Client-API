﻿///////////////////////////////////////////////////////////////////
// This file contains modified code from 'Ryzom - MMORPG Framework'
// http://dev.ryzom.com/projects/ryzom/
// which is released under GNU Affero General Public License.
// http://www.gnu.org/licenses/
// Copyright 2010 Winch Gate Property Limited
///////////////////////////////////////////////////////////////////

namespace API.ActionHandler
{
    /// <summary>
    /// interface for action handlers
    /// </summary>
    /// <author>Nicolas Brigand</author>
    /// <author>Nevrax France</author>
    /// <date>2002</date>  
    public interface IActionHandler
    {
        // Execute the answer to the action
        // Params has the following form : paramName=theParam|paramName2=theParam2|...
        public void Execute(object pCaller, in string sParams);

        //static string getParam(in string Params, in string ParamName);

        //static void getAllParams(in string Params, System.Collections.Generic.List<System.Tuple<string, string>> AllParams);
    }
}