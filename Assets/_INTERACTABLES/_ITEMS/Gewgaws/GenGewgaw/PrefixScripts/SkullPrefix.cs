using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace Interactables
{

    public class SkullPrefix : PrefixBase, IFolkloric, IEpic, ILyric
    {

        // lYric: -1 Attacker morale for 2 rounds
        // folk: , * melee attackers take 1-4 dark dmg"	..... -1 Attacker morale for 2 rounds
        //epic: * melee attackers take 2-6 dark dmg...-2 Attacker morale for 2 rounds"

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

        public void LyricInit()
        {
        }

        public void ApplyFXLyric()
        {
        }

        public void RemoveFXLyric()
        {
        }
    }
}