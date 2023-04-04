using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfEndurance : SuffixBase, ILyric, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfEndurance;
 
        //1 vigor or 1 willpower	2 vigor or 2 willpower	3-4 vigor or 3-4 willpower
        int valLyric, valFolk, valEpic;
      
        AttribName statsName1, statsName2, statsName3;
        public void LyricInit()
        {
            valLyric = 1;
            if (true.TheToss())
            {
                statsName1 = AttribName.vigor;
            }
            else
            {
                statsName1 = AttribName.willpower;
            }
            string str = $"+{valLyric} {statsName1}";
             displayStrs.Add(str);
        }
        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;

            int index =              
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            ,charController.charModel.charID, statsName1, valLyric, TimeFrame.Infinity, -1, true);       
            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
           

            valFolk = 2;
            if (true.TheToss())
            {
                statsName2 = AttribName.vigor;
            }
            else
            {
                statsName2 = AttribName.willpower;
            }
            string str = $"+{valFolk} {statsName2}";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
                   charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            , charController.charModel.charID, statsName2, valFolk, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            valEpic = UnityEngine.Random.Range(3, 5);
            if (true.TheToss())
            {
                statsName3 = AttribName.vigor;
            }
            else
            {
                statsName3 = AttribName.willpower;
            }
            string str = $"+{valEpic} {statsName3}";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;
            int index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            , charController.charModel.charID, statsName3, valEpic, TimeFrame.Infinity, -1, true);
            
            buffIndex.Add(index);
        }
        //1 vigor or 1 willpower	2 vigor or 2 willpower	3-4 vigor or 3-4 willpower
        public void RemoveFXLyric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
    }
}

