using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StmRegenView : MonoBehaviour
{
    CharController charController;
    public void InitNFillHPView(CharController charController)
    {
        this.charController = charController;
        AttribData stmRegenData = charController.GetAttrib(AttribName.staminaRegen);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i < stmRegenData.currValue)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
