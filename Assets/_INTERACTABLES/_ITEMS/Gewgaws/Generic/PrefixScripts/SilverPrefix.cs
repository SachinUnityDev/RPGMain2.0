using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class Silver : PrefixBase, ILyric, IFolkloric, IEpic
    {
        //LYRIC
        // "*(1-2) armor, 
        //*+8-12% dmg vs beastmen"	
        
        //Folkloric
        //"* (2-2) armor,
        //* +14-20% dmg vs beastmen"
        
        // EPIC
        //	"* (2-3) armor,
        //* +22-28% dmg vs beastmen,
        //* +8 dark res"

        public override PrefixNames prefixName => PrefixNames.Silver; 

        int val11, val12, val21,val22, val31, val32;
        string str1, str2, str3;
        //public override void PrefixInit(GenGewgawQ genGewgawQ)
        //{
        //    this.genGewgawQ = genGewgawQ;
        //}
        //public  void ApplyPrefix(CharController charController)
        //{
        //    this.charController = charController;
        //    buffIndex = new List<int>();
        //}
        public void ApplyFXLyric()
        {
            val11 = 1; val12 = 2; 

            int index =
            charController.buffController.ApplyBuffOnRange(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.armor, val11,val12, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void ApplyFXFolkloric()
        {        
            val21 = 2; val22 = 2;
            int index =
            charController.buffController.ApplyBuffOnRange(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.armor, val21, val22, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }

        public void ApplyFXEpic()
        {
            val31 = 2; val32 = 3;
            int index =
            charController.buffController.ApplyBuffOnRange(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.armor, val31, val32, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void LyricInit()
        {
           
        }

        public void RemoveFXLyric()
        {
            
        }

        public void FolkloricInit()
        {
            
        }

        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

        public void EpicInit()
        {
            
        }

        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

        //public override List<string> DisplayStrings()
        //{
        //    displayStrs.Clear();
        //    switch (genGewgawQ)
        //    {
        //        case GenGewgawQ.Lyric:
        //            str1 = $"+{val11}-{val12} Armor";
        //            displayStrs.Add(str1);
        //            break;
        //        case GenGewgawQ.Folkloric:
        //            str2 = $"+{val21}-{val22} Armor";
        //            displayStrs.Add(str2);
        //            break;
        //        case GenGewgawQ.Epic:
        //            str3 = $"+{val31}-{val32} Armor";
        //            displayStrs.Add(str3);
        //            break;
        //        default:
        //            break;
        //    }

        //    return displayStrs;
        //}
    }
}