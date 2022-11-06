using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using Combat;

namespace Common
{
    public class SaveView : MonoBehaviour
    {



        void Start()
        {

        }

        void OnSlotBtnPressed()
        {
            GameObject btn = EventSystem.current.currentSelectedGameObject;
            int index = btn.transform.GetSiblingIndex();



        }

    }

 





}
