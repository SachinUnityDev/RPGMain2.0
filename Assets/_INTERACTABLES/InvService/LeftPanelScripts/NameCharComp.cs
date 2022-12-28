using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

namespace Interactables
{
    public class NameCharComp : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nametxt; 
        void Start()
        {
            nametxt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            InvService.Instance.OnCharSelectInvPanel += PrintName; 
           
        }

        void PrintName(CharModel charModel)
        {
            nametxt.text = charModel.charNameStr;

        }

      
    }



}
