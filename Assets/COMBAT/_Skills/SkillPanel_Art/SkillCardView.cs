using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;


namespace Combat
{
    public class SkillCardView : MonoBehaviour
    {

        [SerializeField] SkillController1 skillController;
        [SerializeField] CharController charController;
        [SerializeField] SkillModel skillModel;
        [SerializeField] SkillNames skillName;


        [Header("Transform ref NTBR")]
        [SerializeField] Transform topTrans;
        [SerializeField] Transform midTrans;
        [SerializeField] Transform btmTrans;

        [Header("SkillData SO")]
        [SerializeField] SkillDataSO skillDataSO;
        SkillData skillData; // data from SkillDataSO 

        [Header("Skill Hex SO & SkillView SO ")]
        SkillHexSO skillHexSO;
        SkillViewSO skillViewSO; 
        private void OnEnable()
        {
             OnSkillHovered();     
        }
        void OnSkillHovered()
        {
           if(GameService.Instance.gameModel.gameState == GameState.InTown)
           {
                charController = InvService.Instance.charSelectController;
                skillController = charController.skillController; 
                skillModel  = SkillService.Instance.skillModelHovered;
                skillName = skillModel.skillName;
           }
           PopulateTopTrans();
        }
        void PopulateTopTrans()
        {
            skillDataSO = SkillService.Instance.GetSkillSO(charController.charModel.charName);
            skillData = skillDataSO.GetSkillData(skillName); 
            SkillInclination skillIncli = skillData.skillIncli;
            
            skillHexSO = SkillService.Instance.skillHexSO;
            skillViewSO = SkillService.Instance.skillViewSO; 

            topTrans.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                    =    skillHexSO.GetSkillIncliSprite(skillIncli);
            AttackType attackType = skillData.attackType;
            topTrans.GetChild(1).GetChild(0).GetComponent<Image>().sprite
                = skillHexSO.GetSkillAttackType(attackType);   



        }
        void PopulateMidTrans()
        {

        }
        void PopulateBtmTrans()
        {

        }
    }
}