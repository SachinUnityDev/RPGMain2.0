using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Town
{


    public class BuildCharIntView : MonoBehaviour
    {
        BuildView buildView;

        [SerializeField] BuildingModel buildModel;
        [SerializeField] BuildingNames buildingName;

        Transform portContainer;
        private void Awake()
        {
            portContainer = GetComponent<Transform>();
        }
        public void InitIntPorts(BuildView buildView, BuildingModel buildModel)
        {
            this.buildView = buildView;
            this.buildModel = buildModel;
            FillCharIntBtn();
           
        }
        void FillCharIntBtn()
        {
            BuildingSO buildSO = BuildingIntService.Instance.allBuildSO
                                    .GetBuildSO(buildingName);
            int i = 0;
            foreach (CharIntData charInteract in buildModel.charInteractData)
            {
                if (charInteract.compState == NPCState.UnLockedNAvail)
                {
                    portContainer.GetChild(i).gameObject.SetActive(true);
                    portContainer.GetChild(i).GetComponent<BuildCharPortPtrEvents>()
                        .InitPortPtrEvents(charInteract, buildModel, buildView);
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