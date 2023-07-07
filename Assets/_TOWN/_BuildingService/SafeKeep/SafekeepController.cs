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
        safekeepModel = new (safekeepSO);
    }
}
