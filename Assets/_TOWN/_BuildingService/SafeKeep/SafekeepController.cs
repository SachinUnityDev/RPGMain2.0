using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

public class SafekeepController : MonoBehaviour
{
    public SafekeepModel safekeepModel;

    BuildingSO safekeepSO;
    [Header("TBR")]
    public MarketView marketView;
    void Start()
    {
        safekeepSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Safekeep);
        safekeepModel = new SafekeepModel(safekeepSO);
        BuildingIntService.Instance.allBuildModel.Add(safekeepModel);

    }

    public void UnLockBuildIntType(BuildInteractType buildIntType, bool unLock)
    {
        //foreach (BuildIntTypeData buildData in safekeepModel.buildIntTypes)
        //{
        //    if (buildData.BuildIntType == buildIntType)
        //    {
        //        buildData.isUnLocked = unLock;
        //        safekee.InitBuildIntBtns(houseView as BuildView, houseModel as BuildingModel);
        //    }
        //}
    }
}
