using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


namespace Combat
{
    public class SkilllCardView : MonoBehaviour
    {

        [SerializeField] SkillController1 skillController;
        [SerializeField] CharController charController;
        [SerializeField] SkillModel skillModel;
        [SerializeField] SkillNames skillName;
        private void Start()
        {
            SkillService.Instance.SkillHovered += OnSkillHovered;     
        }
    

        void OnSkillHovered()
        {
           if(GameService.Instance.gameModel.gameState == GameState.InTown)
           {
                charController = InvService.Instance.charSelectController;
                skillController = charController.skillController; 
           }
        }

        void ShowSkillCard()
        {
            skillModel = skillController.GetSkillModel(skillName);  
            // fill upthe skill card 

           
        }
        void PopulateTopPanel()
        {

        }
        void PopulateMidPanel()
        {

        }
        void PopulateBtmPanel()
        {


        }

    }
}