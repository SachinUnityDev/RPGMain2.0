using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace Interactables
{

    public class SkullPrefix : PrefixBase, IFolkloric, IEpic
    {

        //NA	, * melee attackers take 1-4 dark dmg"	.....
        //"     * melee attackers take 2-6 dark dmg"
        public override PrefixNames prefixName => PrefixNames.Skull;
    
        public void FolkloricInit()
        {

        }
        public void ApplyFXFolkloric()
        {
          
        }
        public void EpicInit()
        {

        }
        public void ApplyFXEpic()
        {

        }

        public void RemoveFXEpic()
        {
           
        }

        public void RemoveFXFolkloric()
        {
            
        }
    }
}