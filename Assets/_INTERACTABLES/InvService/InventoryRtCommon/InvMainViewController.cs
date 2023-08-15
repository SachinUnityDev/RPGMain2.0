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
        [Header("Level view ")]
        public LevelView levelView; 


       [SerializeField] GameObject AttributesPanel;
       [SerializeField] GameObject StatsPanel;
       [SerializeField] GameObject traitsPanel;
        public CharNames selectchar;


        private void Awake()
        {
            invCommViewController = transform.GetChild(1).GetComponent<InvRightViewController>();
           
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
            btmCharViewController.gameObject.transform.SetParent(transform);
            levelView.LevelViewInit();

        }
        public HelpName GetHelpName()
        {
            return helpName;
        }

    }


}
