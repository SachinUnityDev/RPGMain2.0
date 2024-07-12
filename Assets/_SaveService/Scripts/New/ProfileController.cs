using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class ProfileController : MonoBehaviour
    {
        public ProfileView profileView;

       


     
        public string GetProfilePath()
        {
            string basePath = SaveService.Instance.basePath;
            ProfileSlot profileSlot = ProfileSlot.Profile1;
            GameModel gameModel = GameService.Instance.GetGameModel((int)profileSlot);  
            switch (profileSlot)
            {
                case ProfileSlot.Profile1:
                    return basePath + "profile1/";                   
                case ProfileSlot.Profile2:
                    return basePath + "profile2/";                   
                case ProfileSlot.Profile3:
                    return basePath + "profile3/";                    
                case ProfileSlot.Profile4:
                    return basePath + "profile4/";
                case ProfileSlot.Profile5:
                    return basePath + "profile5/";                    
                case ProfileSlot.Profile6:
                    return basePath + "profile6/";                    
                default:
                    return basePath + "profile1/";
            }            
        }
    }
}