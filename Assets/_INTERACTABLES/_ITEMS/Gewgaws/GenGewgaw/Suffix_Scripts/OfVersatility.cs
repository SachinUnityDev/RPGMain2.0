using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfVersatility : SuffixBase, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfVersatility;

      //  NA	+1 (two random utility stats)	+2 (two random utility stats)
        int  val2, val3;
       
        AttribName statsName21, statsName22, statsName31, statsName32;

        public void FolkloricInit()
        {
            statsName21 = GetRandUtilStat(AttribName.None);
            statsName22 = GetRandUtilStat(statsName21);
            val2 = 1;

            string str = $"+{val2} {statsName21}";
            displayStrs.Add(str);
            str = $"+{val2} {statsName22}";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            , charController.charModel.charID, statsName21, val2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

          
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, statsName22, val2, TimeFrame.Infinity, -1, true);


            buffIndex.Add(index);
        }

        public void EpicInit()
        {
            statsName31 = GetRandUtilStat(AttribName.None);
            statsName32 = GetRandUtilStat(statsName31);
            val3 = 2;
            string str = $"+{val3} {statsName31}";
            displayStrs.Add(str);
            str = $"+{val3} {statsName32}";
            displayStrs.Add(str);
        }

        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, statsName31, val3, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

         
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, statsName32, val3, TimeFrame.Infinity, -1, true);


            buffIndex.Add(index);
        }
      //  NA	+1 (two random utility stats)	+2 (two random utility stats)
        AttribName GetRandUtilStat(AttribName _statsName)
        {
            AttribName statsName = AttribName.None;

            do
            {
                int ran = UnityEngine.Random.Range(1, 5);
                switch (ran)
                {
                    case 1:
                        statsName = AttribName.focus;
                        break;
                    case 2:
                        statsName = AttribName.morale;
                        break;
                    case 3:
                        statsName = AttribName.haste;
                        break;
                    case 4:
                        statsName = AttribName.luck;
                        break;
                    default:
                        break;
                }
            }
            while (statsName != _statsName);

            return statsName;


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
