using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Interactables
{
    public class InvMainViewController : MonoBehaviour, IPanel, iHelp
    {
        [Header("help")]
        [SerializeField] HelpName helpName;

        [Header("PARENT VIEW CONTROLLER")]
        public InvRightViewController invCommViewController;
        public BtmCharViewController btmCharViewController;
        public LevelView levelViewController; 


       [SerializeField] GameObject AttributesPanel;
       [SerializeField] GameObject StatsPanel;
       [SerializeField] GameObject traitsPanel;
        public CharNames selectchar;

        [Header("Manual Level Up")]
        [SerializeField] GameObject levelUpPanel;

        private void Awake()
        {
            invCommViewController = transform.GetChild(1).GetComponent<InvRightViewController>();
            levelViewController = transform.GetChild(0).GetComponent<LevelView>();
            btmCharViewController = transform.GetChild(2).GetComponent<BtmCharViewController>();
     
            InvService.Instance.OnCharSelectInvPanel += OnCharSelected; 
        }

        public void OnCharSelected(CharModel charModel)
        {


        }
        void PopulateStatsPanel()
        {
            


        }

        void PopulateAttributePanel()
        {

        }

        void PopulateTraitsPanel()
        {

        }

        public void Load()
        {
            btmCharViewController.gameObject.transform.SetParent(transform);
            transform.parent.gameObject.SetActive(true);
            InvService.Instance.isInvPanelOpen = true;
        }

        public void UnLoad()
        {   
            transform.parent.gameObject.SetActive(false);
            InvService.Instance.isInvPanelOpen = false; 
        }

        public void Init()
        {
            invCommViewController.Init();
            btmCharViewController.Init();
            levelViewController.Init();
            btmCharViewController.gameObject.transform.SetParent(transform);

        }
        public HelpName GetHelpName()
        {
            return helpName;
        }

    }


}
