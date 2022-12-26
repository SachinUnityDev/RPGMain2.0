using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class AmetystPrefix : PrefixBase, IEpic
    {

//        "*+4 Morale in day, 
//* receive 12% more damage
//*-2 Focus"
        public override PrefixNames prefixName => PrefixNames.Ametyst;

        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index = charController.buffController.ApplyNInitBuffOnDay(CauseType.PrefixGenGewgaw, (int)prefixName
                            , charController.charModel.charID, StatsName.morale, 4, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index = charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
             , charController.charModel.charID, StatsName.focus, -2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            Debug.Log("recieve 12 % more damage");
            // buff physical and magical 

        }

        public void EpicInit()
        {
        //    val31 = 3; val32 = UnityEngine.Random.Range(6, 10);
        //    val33 = UnityEngine.Random.Range(6, 10);

            string str = $"+4 Morale at day";
            displayStrs.Add(str);
            str = $"-2 focus";
            displayStrs.Add(str);
            str = $"Recieve 12% more damage";
            displayStrs.Add(str);
            
        }

        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
    }
}