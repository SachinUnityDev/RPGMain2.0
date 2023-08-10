using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class FlawlessBtnPtrEvents : MonoBehaviour
    {

        public void Init(WoodGameData woodGameData)
        {
            if(woodGameData.isFlawless)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}