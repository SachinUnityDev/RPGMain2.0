using Common;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    //* 1-1 armor * 1 dodge"..........	"* (1-2) armor * 2 dodge"......	"* (2-2) armor,  3 dodge +4-8 earth res"
    public class HidePrefix : PrefixBase, ILyric, IFolkloric, IEpic
    {
        public override PrefixNames prefixName => PrefixNames.Hide;
        int valER; 

        public void LyricInit()
        {
            string str = $"+1-1 Armor";
            displayStrs.Add(str);
            str = $"+1 Dodge";
            displayStrs.Add(str);
        }
        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
              charController.buffController.ApplyBuffOnRange(CauseType.PrefixGenGewgaw, (int)prefixName
               , charController.charModel.charID, StatsName.armor, 1,1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
              charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
               , charController.charModel.charID, StatsName.dodge, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
            string str = $"+1-2 Armor";
            displayStrs.Add(str);
            str = $"+2 Dodge";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
              charController.buffController.ApplyBuffOnRange(CauseType.PrefixGenGewgaw, (int)prefixName
               , charController.charModel.charID, StatsName.armor, 1, 2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
              charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
               , charController.charModel.charID, StatsName.dodge, 2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }           

        public void EpicInit()
        {
            valER = UnityEngine.Random.Range(4, 9);
            string str = $"+2-2 Armor";
            displayStrs.Add(str);
            str = $"+3 Dodge";
            displayStrs.Add(str);
            str = $"{valER} Earth Res";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;
            int index =
              charController.buffController.ApplyBuffOnRange(CauseType.PrefixGenGewgaw, (int)prefixName
               , charController.charModel.charID, StatsName.armor, 2, 2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
              charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
               , charController.charModel.charID, StatsName.dodge, 3, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
             charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.earthRes, valER, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));         
            buffIndex.Clear();
        }

        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

        public void RemoveFXLyric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
    }
}

