using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "AllCurioSO", menuName = "Interactable/AllCurioSO")]
    public class AllCurioSO : ScriptableObject
    {
        public List<CurioSO> allCurioSO = new List<CurioSO>();  
        public CurioSO GetCurioSO(CurioNames curioName)
        {
            int index = allCurioSO.FindIndex(t => t.curioName == curioName); 
            if(index != -1)
            {
                return allCurioSO[index];
            }
            Debug.Log(" curio So not found !!" + curioName);
            return null;
        }
    }
}