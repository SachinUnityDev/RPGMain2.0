using Common;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
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
        private void Awake()
        {
            gameObject.SetActive(false);
        }
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
            PopulateMidTrans();    
            PopulateBtmTrans();
        }
        void PopulateTopTrans()
        {
            skillDataSO = SkillService.Instance.GetSkillSO(charController.charModel.charName);
            skillData = skillDataSO.GetSkillData(skillName); 
            SkillInclination skillIncli = skillData.skillIncli;
            
            skillHexSO = SkillService.Instance.skillHexSO;
            skillViewSO = SkillService.Instance.skillViewSO; 
            // skill Incli...
            topTrans.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                    =    skillHexSO.GetSkillIncliSprite(skillIncli);

            topTrans.GetChild(0).GetComponent<Image>().sprite
                    = skillHexSO.GetSkillIncliSpriteBG(skillIncli);
            // attack type...
            AttackType attackType = skillData.attackType;
            topTrans.GetChild(1).GetChild(0).GetComponent<Image>().sprite
                             = skillHexSO.GetSkillAttackType(attackType);
            topTrans.GetChild(1).GetComponent<Image>().sprite
                             = skillHexSO.GetSkillAttackTypeBG(attackType);


            // skill name.. 
            topTrans.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text
                    = skillName.ToString().CreateSpace(); 

            // hexes ... 
            // for three hexes get 
            List<PerkType> perkChain = skillModel.perkChain; 
            PerkHexData perkHexData = skillDataSO.GetPerkHexData(perkChain, skillName);

            int i = 0;
            foreach (HexNames perkHex in perkHexData.hexNames)
            {
                topTrans.GetChild(3).GetChild(i).GetComponent<Image>().sprite =
                                                        skillHexSO.GetHexSprite(perkHex);
                i++;
                if (i > 3)
                {
                   // expand the skillcard ..
                   // Expand the mid card by some val as height of text go 
                   // setactive => true....
                } 
            }
        }
        void PopulateMidTrans()
        {            
            for (int i = 0; i < skillModel.descLines.Count; i++)
            {
                midTrans.GetChild(i).GetComponent<TextMeshProUGUI>().text
                                                   = skillModel.descLines[i]; 
            }  
            
        }
        void PopulateBtmTrans()
        {
            // skillModel has it 
            btmTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = skillModel.staminaReq + "\n" + "Stm";
            if (skillModel.castTime > 0)
            {
                btmTrans.GetChild(1).GetComponent<TextMeshProUGUI>().text
                = skillModel.castTime + "\n" + "Rds";
            }
            else
            {
                btmTrans.GetChild(1).GetComponent<TextMeshProUGUI>().text
                = "";
            }
            
            string dmgTypeStr = "";
            string dmgPrint = "";
            if(skillModel.dmgType.Count != 0)
            foreach (DamageType dmg in skillModel.dmgType)
            {
                dmgTypeStr = dmgTypeStr + dmg.ToString()+ ", ";    
            }
            if (dmgTypeStr.Length > 2)
            {
                dmgPrint = dmgTypeStr.Substring(0,dmgTypeStr.Length - 2); 
            }
            btmTrans.GetChild(3).GetComponent<TextMeshProUGUI>().text
                                                             = dmgPrint;
        }
    }
}