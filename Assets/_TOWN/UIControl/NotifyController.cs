using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class NotifyController : MonoBehaviour
    {
        public List<NotifyModel> allNotifyModel = new List<NotifyModel>();  
        public NotifyBoxView notifyBoxView;

    
        public void InitController(AllNotifySO allNotifySO)  
        {   
            Canvas canvas =  FindObjectOfType<Canvas>();
            notifyBoxView = canvas.GetComponentInChildren<NotifyBoxView>(true);
            foreach (var notifySO in allNotifySO.allNotifySO)
            {
                NotifyModel notifyModel = new NotifyModel(notifySO);
                allNotifyModel.Add(notifyModel);    
            }
        }

        public NotifyModel GetNotifyModel(NotifyName notifyName)
        {
            int index = allNotifyModel.FindIndex(t=>t.NotifyName== notifyName);
            if(index != -1)
            {
                return allNotifyModel[index];   
            }
            else
            {
                Debug.Log("notify model"); 
                return null;
            }
        }
        
    }
}