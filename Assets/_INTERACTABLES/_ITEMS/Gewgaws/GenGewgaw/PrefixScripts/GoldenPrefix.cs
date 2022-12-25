using Combat;
using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class GoldenPrefix : PrefixBase, IEpic
    {
        //NA // NA //* -2 Luck.... +22-28% dmg vs human, elf, dwarf races... +10 light res
        public override PrefixNames prefixName => PrefixNames.Golden;

        int valdmg, valLR;
        string str;
     
        public void EpicInit()
        {
            valLR = UnityEngine.Random.Range(10, 16); 
            valdmg = UnityEngine.Random.Range(22, 29);
            str = $"-2 Luck";
             displayStrs.Add(str);
            str = $"+{valLR} Light Res";
            displayStrs.Add(str);
            str = $"+{valdmg} Dmg vs Humans, Elves and Dwarves";
            displayStrs.Add(str);
        }

        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;
            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.luck, -2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.lightRes, valLR, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
              charController.strikeController.ApplyDmgAltBuff(valdmg, CauseType.PrefixGenGewgaw, (int)prefixName
            , charController.charModel.charID, TimeFrame.Infinity, -1, true, AttackType.None, DamageType.None
            , CultureType.None, RaceType.Human);
            dmgAltBuffIndex.Add(index);


            index =
              charController.strikeController.ApplyDmgAltBuff(valdmg, CauseType.PrefixGenGewgaw, (int)prefixName
            , charController.charModel.charID, TimeFrame.Infinity, -1, true, AttackType.None, DamageType.None
            , CultureType.None, RaceType.Elf);
            dmgAltBuffIndex.Add(index);

            index =
              charController.strikeController.ApplyDmgAltBuff(valdmg, CauseType.PrefixGenGewgaw, (int)prefixName
            , charController.charModel.charID, TimeFrame.Infinity, -1, true, AttackType.None, DamageType.None
            , CultureType.None, RaceType.Dwarf);
            dmgAltBuffIndex.Add(index);

        }
        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgBuff(t));
            dmgAltBuffIndex.Clear();
            buffIndex.Clear();

        }
    }
}
