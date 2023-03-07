using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class BountyBoardView : MonoBehaviour, IPanel
    {
        public void Init()
        {
          
        }

        public void Load()
        {
          
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}