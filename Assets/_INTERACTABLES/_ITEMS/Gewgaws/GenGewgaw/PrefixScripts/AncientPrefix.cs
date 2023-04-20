using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class AncientPrefix : PrefixBase, ILyric, IEpic, IFolkloric
    {
        public override PrefixNames prefixName => PrefixNames.Ancient;

        // Lyric: exp from Curio 60% more
        // folkoric : exp from Curio 90% more
        // Epic : exp from Curio 120% more

        public void ApplyFXEpic()
        {
            
        }

        public void ApplyFXFolkloric()
        {
        }

        public void ApplyFXLyric()
        {
            
        }

        public void EpicInit()
        {
            
        }

        public void FolkloricInit()
        {

        }

        public void LyricInit()
        {
            
        }

        public void RemoveFXEpic()
        {
            
        }

        public void RemoveFXFolkloric()
        {

        }

        public void RemoveFXLyric()
        {
            
        }
    }
}