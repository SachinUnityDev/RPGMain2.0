using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Common;

namespace Combat
{
    public class HpRegenView : MonoBehaviour
    {
        CharController charController; 

        public void InitNFillHPView(CharController charController)
        {
            this.charController = charController;
            AttribData hpRegenData = charController.GetAttrib(AttribName.hpRegen);

            for (int i = 0; i < transform.childCount; i++)
            {
                if(i < hpRegenData.currValue) 
                    transform.GetChild(i).gameObject.SetActive(true);
                else
                    transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}