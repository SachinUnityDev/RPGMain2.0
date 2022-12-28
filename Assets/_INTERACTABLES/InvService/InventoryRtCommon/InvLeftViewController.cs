using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Interactables
{
    public class InvLeftViewController : MonoBehaviour, IPanel
    {
        [Header("PARENT VIEW CONTROLLER")]
        public InvRightViewController invRightViewController;
        public BtmCharViewController btmCharViewController;
        public LevelViewController levelViewController; 


       [SerializeField] GameObject AttributesPanel;
       [SerializeField] GameObject StatsPanel;
       [SerializeField] GameObject traitsPanel;
        public CharNames selectchar;

        [Header("Manual Level Up")]
        [SerializeField] GameObject levelUpPanel; 
        
    
        private void Start()
        {
            invRightViewController = transform.GetChild(0).GetComponent<InvRightViewController>();
            levelViewController = transform.GetChild(1).GetComponent<LevelViewController>();
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
            transform.parent.gameObject.SetActive(true);
        }

        public void UnLoad()
        {
            transform.parent.gameObject.SetActive(false);
        }

        public void Init()
        {
            invRightViewController.Init();
            btmCharViewController.Init();
            levelViewController.Init();

        }
    }


}
