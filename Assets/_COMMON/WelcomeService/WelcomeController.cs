using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

public class WelcomeController : MonoBehaviour
{

    private void Start()
    {
        
    }
    #region Seq Action on Welcome Run

    public void OnEnterTavern(BuildingModel buildModel, BuildView buildView)
    {
        if (buildModel.buildingName != BuildingNames.Tavern) return;
        if (!WelcomeService.Instance.isWelcomeRun) return;
        WelcomeService.Instance.welcomeView.RevealWelcomeTxt("Click Greybrow's portrait and Talk");
        BuildingIntService.Instance.OnBuildInit -= OnEnterTavern;
    }

    #endregion
}
