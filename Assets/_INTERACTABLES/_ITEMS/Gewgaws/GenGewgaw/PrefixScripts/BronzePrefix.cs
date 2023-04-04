using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class BronzePrefix : PrefixBase, ILyric, IFolkloric
    {
        //* -1 (random utility stat),
        //* +8-12% dmg vs serpents"	"
        //* -1 (random utility stat),
        //* +13-18% dmg vs serpents"	NA
        public override PrefixNames prefixName => PrefixNames.Bronze;

        int valLyric, valFolkLyric;
        AttribName statName1, statName2;

        int dmgLyric, dmgFolkLyric;
        public void LyricInit()
        {
            valLyric = -1;
            string stat = statName1.ToString();
            string str = $"{valLyric}" + stat;
            displayStrs.Add(str);
            dmgLyric = UnityEngine.Random.Range(8, 13); 
        }
        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;

            AttribName statsName = GetRandomUtilityStat();
            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, statsName, valLyric, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);

            index = 
            charController.strikeController.ApplyDmgAltBuff(dmgLyric, CauseType.PrefixGenGewgaw, (int)prefixName
                 , charController.charModel.charID, TimeFrame.Infinity, -1, true, AttackType.None, DamageType.None
                 , CultureType.Reptile); 
            dmgAltBuffIndex.Add(index);

            index =
            charController.strikeController.ApplyDmgAltBuff(dmgLyric, CauseType.PrefixGenGewgaw, (int)prefixName
                 , charController.charModel.charID, TimeFrame.Infinity, -1, true, AttackType.None, DamageType.None
                 , CultureType.Arachnid);
            dmgAltBuffIndex.Add(index);

        }
        public void FolkloricInit()
        {
            valFolkLyric = -1;
            string stat = statName2.ToString(); 
            string str = $"{valFolkLyric}" + stat;
            displayStrs.Add(str);
            dmgFolkLyric = UnityEngine.Random.Range(13, 19); 
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;

            AttribName statsName = GetRandomUtilityStat();
            int index =
                   charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                    , charController.charModel.charID, statsName, valFolkLyric, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
                charController.strikeController.ApplyDmgAltBuff(dmgFolkLyric, CauseType.PrefixGenGewgaw, (int)prefixName
              , charController.charModel.charID, TimeFrame.Infinity, -1, true, AttackType.None, DamageType.None
              , CultureType.Reptile);
            dmgAltBuffIndex.Add(index);

            index =
              charController.strikeController.ApplyDmgAltBuff(dmgFolkLyric, CauseType.PrefixGenGewgaw, (int)prefixName
            , charController.charModel.charID, TimeFrame.Infinity, -1, true, AttackType.None, DamageType.None
            , CultureType.Arachnid);
            dmgAltBuffIndex.Add(index);
        }

        AttribName GetRandomUtilityStat()
        {
            int beginInt = (int)AttribName.focus;
            int ran = Random.Range(beginInt, beginInt + 4);
            return (AttribName)ran;
        }

        public void RemoveFXLyric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgBuff(t));
            dmgAltBuffIndex.Clear();
            buffIndex.Clear();
        }
        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgBuff(t));
            dmgAltBuffIndex.Clear();
            buffIndex.Clear();
        }
    }
}

