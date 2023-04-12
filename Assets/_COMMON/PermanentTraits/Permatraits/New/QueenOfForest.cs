using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class QueenOfForest : PermaTraitBase
    {
        //Immune to Flat Footed in Rainforest, Jungle and Swamp	
        //Immune to Rainforest, Jungle and Swamp hazards(Handicaps)	
        //+1 Haste in Rainforest, Jungle and Swamp

        public override PermaTraitName permaTraitName => PermaTraitName.QueenOfTheForest;

        public override void ApplyTrait(CharController charController)
        {
            base.ApplyTrait(charController);
            LandscapeService.Instance.OnLandscapeEnter += OnLandScapeEnter;

        }
        void OnLandScapeEnter(LandscapeNames landName)
        {
            if (landName != LandscapeNames.Rainforest || landName != LandscapeNames.Jungle ||
                landName != LandscapeNames.Swamp)
                return;
            int buffID =
                 charController.landscapeController
                 .ApplyLandscapeBuff(CauseType.PermanentTrait, (int)permaTraitName, landName
                 , AttribName.haste, 1);
            allbuffID.Add(buffID);

            buffID =
            charController.landscapeController.ApplyLandscapeCharStateBuff(CauseType.PermanentTrait,
                (int)permaTraitName, charID, landName, CharStateName.FlatFooted, true); 
            allStateBuffId.Add(buffID); 
        }

        public override void EndTrait()
        {
            base.EndTrait();
            LandscapeService.Instance.OnLandscapeEnter += OnLandScapeEnter;
        }

    }



}

