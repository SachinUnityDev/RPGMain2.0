using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Town;
using System.Linq;


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

        //public List<CharModel> GetCharUnLocked()
        //{
        //    //allCharUnLocked = CharService.Instance.allyInPLayControllers
        //    //         .Where(t => t.charModel.stateOfChar == StateOfChar.UnLocked).ToList();

        //    //return allCharUnLocked;

        //}

    }
}
