using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

public class FortifyMainView : MonoBehaviour, IPanel
{
    [Header("TBR")]
    public Transform selectPanel;
    public Transform fortifyPanel;
    public Transform UnsocketPanel;

    public List<Transform> allPanels;

    [Header("BUY SELF")]
    [SerializeField] Currency currency;

    private void Start()
    {
        allPanels = new List<Transform>() { selectPanel, fortifyPanel, UnsocketPanel };
    }
    public void OnExitBtnPressed()
    {
        UnLoad();
    }
    public void Init()
    {
        selectPanel.GetComponent<SelectView>().InitMainPage(this);
        fortifyPanel.GetComponent<FortifyView>().InitFortifyView(this);
        UnsocketPanel.GetComponent<UnSocketView>().InitUnSocketView(this);
    }

    public void OnReverseBtnPressed()
    {
        Load();
    }
    public void Load()
    {
        UIControlServiceGeneral.Instance.TogglePanelOnInGrp(selectPanel.gameObject, true);
    }

    public void UnLoad()
    {
        UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
    }
}
