using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class GuideServices : MonoSingletonGeneric<GuideServices>
    {
        public ExclaimHLController exclaimHLController;
        public HelpController helpController;
        private void Start()
        {
            exclaimHLController = GetComponent<ExclaimHLController>();
            helpController = GetComponent<HelpController>();
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.V))
            {
                exclaimHLController.exclaimHLView.ShowGuideMarkInSeq(GuideMarkLoc.QuestJurnoBtn
                    , GuideMarkLoc.MapBtn);                

            }
        }
    }
}