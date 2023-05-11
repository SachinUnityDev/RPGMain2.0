using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class DisplayFame : MonoBehaviour
    {

        public int fameVal;
        [SerializeField] FameSO fameSO;
        [SerializeField] FameType fameType;
        private void Start()
        {
                
        }
        public void Display()
        {
             fameVal = FameService.Instance.fameController.fameModel.fameVal;  
            fameType = FameService.Instance.GetFameType();
            fameSO = FameService.Instance.fameSO;

            transform.GetChild(0).GetComponent<Image>().sprite = fameSO.GetFameTypeSprite(fameType);
            TextMeshProUGUI fameTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); 
                fameTxt.text = fameSO.GetFameTypeStr(fameType);   
                fameTxt.color = fameSO.GetFameTypeColor(fameType);  
        }

 
    }
}