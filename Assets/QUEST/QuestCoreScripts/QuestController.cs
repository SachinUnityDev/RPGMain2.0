using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class QuestController : MonoBehaviour
    {
        public bool PartyRaceChk(CultureType _cultsType, List<GameObject> _CharacterInPlay)
        {
            foreach (GameObject go in _CharacterInPlay)
            {
                if( go?.GetComponent<CharController>().charModel.cultType == _cultsType)
                {
                    return true; 

                }    
            }
            return false;
        }



    }

}

