using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    [CreateAssetMenu(fileName = "AllNotifySO", menuName = "Common/AllNotifySO")]
    public class AllNotifySO : ScriptableObject
    {
       public List<NotifySO> allNotifySO = new List<NotifySO>();    

    }
}  