using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Intro
{
    public class QuickStartView : MonoBehaviour
    {
        [SerializeField] QuickStartPg1View quickStartPg1View;
        [SerializeField] QuickStartPg2View quickStartPg2View;

     

        public void InitQuickStart()
        {
            quickStartPg1View.QuickStartPg1Init(this);
            quickStartPg2View.QuickStartPg2Init(this);
        }
    }
}