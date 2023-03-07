using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace Town
{
    public class NPCInteractBtnView : MonoBehaviour
    {
        TempleViewController templeView;
        TempleModel templeModel;
        AllBuildSO allBuildSO;
        GameObject npcIntPortraitPrefab; 
        private void Awake()
        {
                
        }
        public void InitInteractBtns(TempleViewController templeView)
        {
            this.templeView = templeView;
            templeModel = BuildingIntService.Instance.templeController.templeModel;
            allBuildSO = BuildingIntService.Instance.allBuildSO;
            FillNPCInteractBtn(); 
            FillCharInteactBtns();
        }
        void FillNPCInteractBtn()
        {

            BuildingSO templeSO = allBuildSO.GetBuildSO(BuildingNames.Temple);

            foreach (NPCInteractData npcInteract in templeModel.allNPCInteractData)
            {
                if(npcInteract.npcState == NPCState.UnLocked)
                {
                  //  templeView.    
                }
            }


            // depending on char in the model/SO  fill here
            // init tradeView and talkView from here for each unloaked NPC char 
        }
        void FillCharInteactBtns()
        {

        }

    }
}