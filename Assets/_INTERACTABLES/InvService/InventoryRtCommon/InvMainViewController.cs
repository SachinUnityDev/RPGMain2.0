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

        [Header("Level view ")]
        public InvTraitsView invTraitsView;

        private void OnEnable()
        {
            invCommViewController =
                transform.GetComponentInChildren<InvRightViewController>(true);

            btmCharViewController =
                transform.parent.GetComponentInChildren<BtmCharViewController>(true);

        }
        void OnDisable()
        {
           // InvService.Instance.OnCharSelectInvPanel += OnCharSelected;
        }
        public void Load()
        {
            btmCharViewController =
                    transform.parent.GetComponentInChildren<BtmCharViewController>(true);
            btmCharViewController.gameObject.transform.SetParent(transform);
            transform.parent.gameObject.SetActive(true);
            InvService.Instance.isInvPanelOpen = true;
        }
        public void UnLoad()
        {
            if (InvService.Instance.overLoadCount > 0)
                return;
            transform.parent.gameObject.SetActive(false);
            //InvService.Instance.isInvPanelOpen = false; 
        }

        public void Init()
        {
            //invCommViewController =
            //   transform.GetChild(1).GetComponent<InvRightViewController>();
            //invCommViewController =
            //  transform.GetComponentInChildren<InvRightViewController>(true);
            invCommViewController.Init();
            btmCharViewController.Init();
            btmCharViewController.gameObject.transform.SetParent(transform);
            levelView.LevelViewInit();
            invTraitsView.Init(); 

        }
        public HelpName GetHelpName()
        {
            return helpName;
        }

    }


}
