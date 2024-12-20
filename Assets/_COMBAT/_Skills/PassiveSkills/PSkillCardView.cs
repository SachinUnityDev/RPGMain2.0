using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Combat
{
    public class PSkillCardView : MonoBehaviour
    {


        [SerializeField] PassiveSkillsController pSkillController;
        [SerializeField] CharController charController;
        
        [SerializeField] PassiveSkillName pSkillName;


        [Header("Transform ref NTBR")]
        [SerializeField] TextMeshProUGUI pSkillNametxt;
        [SerializeField] Transform txtContainer;
        
        [Header("SkillData SO")]
  
        List<string> descLines = new List<string>();
        private void OnEnable()
        {
            PassiveSkillCardInit();
            CombatEventService.Instance.OnCharOnTurnSet -= OnCharOnTurnSet;
            CombatEventService.Instance.OnCharOnTurnSet += OnCharOnTurnSet; 
        }

        private void OnDisable()
        {
            ClearData();
            CombatEventService.Instance.OnCharOnTurnSet -= OnCharOnTurnSet;
        }
        private void OnDestroy()
        {
            ClearData();
            CombatEventService.Instance.OnCharOnTurnSet -= OnCharOnTurnSet;
        }
        void OnCharOnTurnSet(CharController charController)
        {
            gameObject?.SetActive(false);                
        }

        void ClearData()
        {
            pSkillName = PassiveSkillName.None; 
            descLines.Clear();            
        }
        void PassiveSkillCardInit()
        {

            if (GameService.Instance.currGameModel.gameScene == GameScene.COMBAT)
            {
                charController = CombatService.Instance?.currCharClicked;
                if (charController == null) return;
                pSkillController = charController.GetComponent<PassiveSkillsController>();
                pSkillName = PassiveSkillService.Instance.currPSkillName;

                pSkillNametxt.text = pSkillName.ToString().CreateSpace();
                descLines = PassiveSkillService.Instance.descLines; 
                for (int i = 0; i < txtContainer.childCount; i++)
                {
                    if(i < descLines.Count)
                    {
                        txtContainer.GetChild(i).GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);    
                        txtContainer.GetChild(i).GetComponent<TextMeshProUGUI>().text
                                                                            = descLines[i];
                    }
                    else
                    {
                        txtContainer.GetChild(i).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
                    }
                }
                gameObject.SetActive(true);
            }
        }
   


       
    }
}