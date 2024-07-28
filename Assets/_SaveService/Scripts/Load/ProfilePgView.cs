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
            for (int i = 0; i < transform.childCount; i++)
            {
                ProfileBtnPtrEvents profileBtnPtrEvents = transform.GetChild(i).GetComponent<ProfileBtnPtrEvents>();
                profileBtnPtrEvents.Init(this, loadView);
            }           
        }   
    }
}