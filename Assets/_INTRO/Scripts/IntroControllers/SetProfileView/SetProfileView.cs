using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class SetProfileView : MonoBehaviour
    {

        [SerializeField] ContinueBtnSetProf continueBtnSetProf;
        [SerializeField] Transform container; 


        public void Init()
        {
            int i = 0; 
            foreach (Transform child in container)
            {
                GameModel gameModel = GameService.Instance.GetGameModel(i);                 
               child.GetComponent<SetProfilePtrEvents>().Init(gameModel, this);
                i++; 
            }
        }
    }
}