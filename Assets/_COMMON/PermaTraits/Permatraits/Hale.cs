using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace Common
{
    public class Hale : PermaTraitBase
    {
       // Immune to some Sicknesses(on the note)
       // When Poisoned: +4 Vigor
       // On consume Cucumber: Gain 5-10 Hp
        public override PermaTraitName permaTraitName => PermaTraitName.Hale;
        public override void ApplyTrait()
        {
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
            ItemService.Instance.OnItemConsumed += OnCucumberConsumed; 

            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Flu, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Scabies, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.RatBiteFever, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Rabies, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Nausea, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.SoreThroat, TimeFrame.Infinity, 1);
        }

        void OnCucumberConsumed(CharController charController, Iitems iitem)
        {
            if (charController.charModel.charID != charID) return; 
            if(iitem.itemName == (int)FruitNames.Cucumber && iitem.itemType == ItemType.Fruits)
            {
                charController.ChangeStat(CauseType.PermanentTrait, (int)permaTraitName
                                                          , charID, StatName.health, UnityEngine.Random.Range(5, 11));
            }
        }
        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Poisoned)
            {
                int buffId =
                    charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName,
                                           charID, AttribName.vigor, +4, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);
            }
        }
        void OnCharStateEnd(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Poisoned)
            {
                foreach (int buffId in allBuffIds)
                {
                    charController.buffController.RemoveBuff(buffId);
                }
            }
        }
    }





}
