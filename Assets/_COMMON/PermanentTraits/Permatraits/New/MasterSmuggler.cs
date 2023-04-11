using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class MasterSmuggler : PermaTraitBase
    {
    //+3 Luck in Coast	
        //Extra loot in Coast, Cave and Sewers
    
        public override PermaTraitName permaTraitName => PermaTraitName.MasterSmuggler;
        public override void ApplyTrait(CharController charController )
        {     
            base.ApplyTrait( charController);          
             
            LandscapeService.Instance.OnLandscapeEnter += IncLootInCoast;
            LandscapeService.Instance.OnLandscapeEnter += OnLandScapeEnter; 
        }

        void OnLandScapeEnter(LandscapeNames landName)
        {
            if (landName != LandscapeNames.Coast)
                return;
            int buffID =
                 charController.landscapeController
                 .ApplyLandscapeBuff(CauseType.PermanentTrait, (int)permaTraitName,LandscapeNames.Coast, AttribName.luck, 2);

            allbuffID.Add(buffID);
        }
        


        void IncLootInCoast(LandscapeNames landName)
        {
            if (landName != LandscapeNames.Coast)
                return;



        }


        public override void EndTrait()
        {
            base.EndTrait();
            LandscapeService.Instance.OnLandscapeEnter -= IncLootInCoast;
            LandscapeService.Instance.OnLandscapeEnter -= OnLandScapeEnter;

        }


    }
}

