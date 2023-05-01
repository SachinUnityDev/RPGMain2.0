using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class SupportServices : MonoSingletonGeneric<SupportServices>
    {
        public GuideController guideController;
        private void Start()
        {
            guideController = GetComponent<GuideController>();
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.V))
            {
                guideController.guideView.ShowGuideMarkInSeq(GuideMarkLoc.QuestJurnoBtn
                    , GuideMarkLoc.MapBtn);                

            }
        }
    }
}