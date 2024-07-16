using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ProfilePgView : MonoBehaviour
    {
       LoadView loadView; 
       public void Init(LoadView loadView)
        {
            this.loadView = loadView;
            int i = 0; 
            foreach ( GameModel gameModel in GameService.Instance.allGameModel)
            {
                ProfileBtnPtrEvents profileBtnPtrEvents = transform.GetChild(i).GetComponent<ProfileBtnPtrEvents>();

                profileBtnPtrEvents.Init(gameModel,this, loadView);    
            }
        }   
        
        public void OnProfileBtnClicked(GameModel gameModel)
        {
           // in load view get the slot view updated // in case of MB go directly to game
           // 
        }
    }
}