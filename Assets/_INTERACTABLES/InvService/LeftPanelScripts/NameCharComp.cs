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
            PrintName(InvService.Instance.charSelectController.charModel); 
        }

        void PrintName(CharModel charModel)
        {
            nametxt.text 
            = $"{charModel.charNameStr} the {charModel.cultType.ToString().CreateSpace()} {charModel.classType.ToString().CreateSpace()} [<b>{charModel.raceType.ToString().CreateSpace()}</b>]";
        }

      
    }



}
