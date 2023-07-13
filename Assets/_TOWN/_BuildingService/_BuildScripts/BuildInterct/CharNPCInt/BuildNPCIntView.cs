using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ServiceModel.Security;
using UnityEngine;


namespace Town
{
    public class BuildNPCIntView : MonoBehaviour
    {
        BuildView buildView;
        
        [SerializeField] BuildingModel buildModel;
        [SerializeField]BuildingNames buildingName;

        Transform portContainer;
        private void Awake()
        {
            portContainer= GetComponent<Transform>();
        }
        public void InitIntPorts(BuildView buildView, BuildingModel buildModel)
        {
            this.buildView = buildView;
            this.buildModel = buildModel;            
            FillNpcIntBtn(); 
           
        }
        void FillNpcIntBtn()
        {
            BuildingSO buildSO = BuildingIntService.Instance.allBuildSO
                                    .GetBuildSO(buildingName);
            int i = 0; 
            foreach (NPCIntData npcInteract in buildModel.npcInteractData)
            {
                if(npcInteract.npcState == NPCState.UnLockedNAvail)
                {
                    portContainer.GetChild(i).gameObject.SetActive(true);
                    portContainer.GetChild(i).GetComponent<BuildNPCPortPtrEvents>()
                        .InitPortPtrEvents(npcInteract, buildModel, buildView); 
                    i++; 
                }
            }

            for (int j = i; j < portContainer.childCount; j++)
            {
                portContainer.GetChild(j).gameObject.SetActive(false);
            }
        }
     

    }
}