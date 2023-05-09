using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
   public class NotifyModel 
    {

            public NotifyName NotifyName;
            [TextArea(5, 10)]
            public string notifyStr = "";
            public bool isDontShowAgainTicked = false;

        public NotifyModel(NotifySO notifySO)
        {
            this.NotifyName = notifySO.NotifyName;
            this.notifyStr = notifySO.notifyStr; 
            this.isDontShowAgainTicked = notifySO.isDontShowAgainTicked;
        }
    }
}