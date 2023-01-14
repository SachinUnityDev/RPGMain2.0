using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System.Security.Policy;
using System;

namespace Common
{
    public class InvSkillViewMain : MonoBehaviour, IPanel
    {

        // get reference to all skill SO 
        // get ref to skill service
        public event Action<SkillNames> OnSkillSelectedInPanel;

        public bool isPerkClickAvail = false; 

        [Header("To be ref")] 
        public AllSkillSO allSkillSO; 
        public SkillViewSO skillViewSO;
        public LeftSkillView leftSkillView;
        public RightSkilllView rightSkillView;

        public BtmCharViewController BtmCharViewController;
        [Header("Not to be ref")]
        public InvXLViewController invXLViewController;
       
        private void Start()
        {
            invXLViewController = transform.parent.parent.GetComponent<InvXLViewController>();

        }

        public void Init()
        {
            Load();
        }

        public void Load()
        {
            BtmCharViewController.gameObject.transform.SetParent(transform);
        }

        public void UnLoad()
        {
           
        }

        public void On_SkillSelectedInPanel(SkillNames skillName)
        {
            OnSkillSelectedInPanel.Invoke(skillName);
        }



    }
}