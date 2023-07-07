using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace Town
{
    public class NPCInteractBtnView : MonoBehaviour
    {
        TempleView templeView;
        TempleModel templeModel;
        AllBuildSO allBuildSO;
        GameObject npcIntPortraitPrefab; 
        private void Awake()
        {
                
        }
        public void InitInteractBtns(TempleView templeView)
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

            foreach (NPCInteractData npcInteract in templeModel.npcInteractData)
            {
                if(npcInteract.npcState == NPCState.UnLockedNAvail)
                {
                    
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