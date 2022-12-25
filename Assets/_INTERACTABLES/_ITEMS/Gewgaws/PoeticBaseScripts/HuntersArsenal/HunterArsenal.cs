using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class HunterArsenal : PoeticSetBase
    {
        public override PoeticSetName poeticSetName => PoeticSetName.FirstHuntersArsenal;
        
        //"when Hungry: +3 vigor,when Thirsty: +3 Wp"	
        //+1 Morale per Animal in enemy party	
        //Enemy Party: Animals: First 3 rds of combat: -4 Haste

        public override void BonusFx()
        {
            TempTraitService.Instance.OnTempTraitStart += OnTempTraitSpec;

            charController = InvService.Instance.charSelectController;
        }

        void OnTempTraitSpec(TempTraitData tempTraitData)
        {
           

            if (tempTraitData.tempTraitName  == TempTraitName.Hungry)
            {
              
                int index =
                    charController.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                    , charController.charModel.charID, StatsName.vigor, 3, TimeFrame.Infinity, -1, true);
                buffIndex.Add(index);
            }
            if (tempTraitData.tempTraitName == TempTraitName.Thirsty)
            {
                int index =
                   charController.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                   , charController.charModel.charID, StatsName.willpower, 3, TimeFrame.Infinity, -1, true);
                        buffIndex.Add(index);
            }
        }

        public override void RemoveBonusFX()
        {
            TempTraitService.Instance.OnTempTraitStart -= OnTempTraitSpec;

        }
    }
}