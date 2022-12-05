using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfCourage : SuffixBase
    {       
        public override SuffixNames suffixName => SuffixNames.OfCourage;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override List<int> buffIndex { get; set; }
        public override CharController charController { get; set; }
        public override List<string> displayStrs { get; set; }
        //of Courage    NA	3-5 fortitude org	6-9 fortitude orign
        int val1, val2, val3;
        string  str1, str2, str3; 
        public override void SuffixInit(GenGewgawQ genGewgawQ)
        {
            this.genGewgawQ = genGewgawQ;
        }
        public override void ApplySuffixFX(CharController charController)
        {
           
            this.charController = charController; 
            buffIndex = new List<int>();
        }
        protected override void ApplyFXLyric()
        {
            //NA	
        }
        protected override void ApplyFXFolkloric()
        {
             val2 = Random.Range(3, 6); 

            int index = 
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.fortOrg, val2, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }

        protected override void ApplyFXEpic()
        {
             val3 = Random.Range(6, 10);

            int index = 
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.fortOrg, val3, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }

        public override void RemoveFX()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t)); 
            buffIndex.Clear();
        }

        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                  
                    break;
                case GenGewgawQ.Folkloric:
                    str2 = $"+{val2} Fortitude Origin"; 
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val3} Fortitude Origin";
                    displayStrs.Add(str3);
                    break;
                default:
                    break; 
            }
            
            return displayStrs;
        }


    }


}

