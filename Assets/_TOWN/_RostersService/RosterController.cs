using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Town;
using System.Linq;
using Interactables;

namespace Common
{
    public class RosterController : MonoBehaviour
    {

        [SerializeField] List<CharModel> allCharUnLocked; 
        public List<CharModel> GetCharAvailableInTown(LocationName location)
        {
            List<CharModel> allCharInTown = CharService.Instance.allyUnLockedCompModels 
                        .Where(t => t.currCharLoc == location).ToList();
            return allCharInTown;
        }

        public List<CharController> GetCharInOtherTown()
        {
            List<CharController> allCharInOtherTown = CharService.Instance.allyInPlayControllers
                     .Where(t => t.charModel.availOfChar == AvailOfChar.Available &&
                      t.charModel.stateOfChar == StateOfChar.UnLocked 
                      && t.charModel.currCharLoc != TownService.Instance.townModel.currTown).ToList();

            return allCharInOtherTown;  
        }

        public List<CharController> GetCharUnlockedWithStatusUpdated()
        {
            List<CharController> allUnLocked = CharService.Instance.allyInPlayControllers
                   .Where(t => t.charModel.stateOfChar == StateOfChar.UnLocked).ToList();

            foreach (CharController charCtrl in allUnLocked)
            {
                if (!FameService.Instance.fameController.IsFameBehaviorMatching(charCtrl))
                    charCtrl.charModel.availOfChar = AvailOfChar.UnAvailable_Fame;
                else if (charCtrl.charModel.currCharLoc != TownService.Instance.townModel.currTown)
                    charCtrl.charModel.availOfChar = AvailOfChar.UnAvailable_Loc; 
                else if(ChkInParty(charCtrl))
                    charCtrl.charModel.availOfChar = AvailOfChar.UnAvailable_InParty;
                else if(!ChkPreReq(charCtrl))
                    charCtrl.charModel.availOfChar = AvailOfChar.UnAvailable_Prereq;
                else
                    charCtrl.charModel.availOfChar = AvailOfChar.Available;
            }
            return allUnLocked; 
        }
        bool ChkInParty(CharController charCtrl)
        {
           return CharService.Instance.allCharsInPartyLocked
                .Any(t=>t.charModel.charID == charCtrl.charModel.charID);
        }

        bool ChkPreReq(CharController charCtrl)
        {
            // stash and main inv 
            List<ItemDataWithQty> allItemQty = charCtrl.charModel.GetPrereqsItem();
            bool hasItem1 = InvService.Instance.invMainModel.HasItemInQtyCommOrStash(allItemQty[0]);
            bool hasItem2 = InvService.Instance.invMainModel.HasItemInQtyCommOrStash(allItemQty[1]);
            if (hasItem1 || hasItem2) return true;
            return false;
        }


    }
}
