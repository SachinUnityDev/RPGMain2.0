using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System;

namespace Common
{
    public class InvSkillViewMain : MonoBehaviour, IPanel, iHelp
    {
        public event Action<SkillModel> OnSkillSelectedInPanel;

        public bool isPerkClickAvail = false;

        [Header("help")]
        [SerializeField] HelpName helpName;

        [Header("To be ref")] 
        public AllSkillSO allSkillSO; 
        public SkillViewSO skillViewSO;
        public LeftSkillView leftSkillView;
        public RightSkillView rightSkillView;

        public BtmCharViewController BtmCharViewController;
        [Header("Not to be ref")]
        public InvXLViewController invXLViewController;
       
        private void Start()
        {
            invXLViewController = transform.parent.parent.GetComponent<InvXLViewController>();

        }

        public void Init()
        {
            SkillService.Instance.OnSkillHovered += SkillHovered;  
            Load();

        }

        public void Load()
        {
            BtmCharViewController.gameObject.transform.SetParent(transform);

        }

        void SkillHovered()
        {


        }

        public void UnLoad()
        {
           
        }

        public void On_SkillSelectedInPanel(SkillModel skillModel)
        {
            OnSkillSelectedInPanel.Invoke(skillModel);
        }


        public HelpName GetHelpName()
        {
            return helpName;
        }

    }
}