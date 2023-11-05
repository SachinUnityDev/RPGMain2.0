using Common;
using Interactables;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Combat
{
    public class SkillCardView : MonoBehaviour
    {
        [SerializeField] const float skillCardHt = 308f;
        [SerializeField] const float midTransHt = 111f;

        [SerializeField] int incrVal = 0;

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

        [Header(" In combat for testing ")]
        [SerializeField] TextMeshProUGUI skillStateTxt;             
        private void OnEnable()
        {
            SkillCardInit();
        }

        private void OnDisable()
        {
            ClearData(); 
            ResetWidthHeight();
        }
        void ResetWidthHeight()
        {
            RectTransform skillCardRect = transform.GetComponent<RectTransform>();
            RectTransform midTransRect = midTrans.GetComponent<RectTransform>();
          
                midTransRect.sizeDelta
                        = new Vector2(midTransRect.sizeDelta.x, midTransHt);
                skillCardRect.sizeDelta
                        = new Vector2(skillCardRect.sizeDelta.x, skillCardHt);
          
        }
        void ClearData()
        {
            skillName = SkillNames.None;
            skillModel = null;
            skillDataSO = null; 
            incrVal= 0;
        }
        void SkillCardInit()
        {
            ClearData();
            if (GameService.Instance.gameModel.gameState == GameState.InTown ||
               GameService.Instance.gameModel.gameState == GameState.InQuestRoom)
            {
                charController = InvService.Instance.charSelectController;
                skillController = charController.skillController; 
                skillModel  = SkillService.Instance.skillModelHovered;
                skillName = skillModel.skillName;
            }
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)              
            {
                charController = CombatService.Instance.currCharClicked;
                skillController = charController.skillController;
                skillModel = SkillService.Instance.skillModelHovered;
                skillName = skillModel.skillName;
            }
            if(skillName == SkillNames.None || charController == null)
            {
                ClearData();
                gameObject.SetActive(false);
                return;
            }
            if(skillModel.skillUnLockStatus == 0 || skillModel.skillUnLockStatus == -1)
            {
                ClearData();
                gameObject.SetActive(false);
                return;
            }
            FillTopTrans();
            FillMidTrans();    
            FillBtmTrans();
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                skillStateTxt.gameObject.SetActive(true);
                SkillStateDsply();
            }
            else
            {
                skillStateTxt.gameObject.SetActive(false);
            }   
        }
        void SkillStateDsply()
        {  
            if (skillModel == null) return;
            SkillSelectState skillState = skillModel.GetSkillState();
            
            switch (skillState)
            {
                case SkillSelectState.None:
                    break;
                case SkillSelectState.Clickable:
                    skillStateTxt.text = "Clickable";
                    break;
                case SkillSelectState.UnHoverable:
                    skillStateTxt.text = "UnHoverable";
                    break;
                case SkillSelectState.Unclickable_passiveSkills:
                    skillStateTxt.text = "PassiveSkills";
                    break;
                case SkillSelectState.UnClickable_InCd:
                    skillStateTxt.text = "In CD";
                    break;
                case SkillSelectState.Unclickable_notCharsTurn:
                    skillStateTxt.text = "Not Char Turn";
                    break;
                case SkillSelectState.Unclickable_notOnCastPos:
                    skillStateTxt.text = "not on castPos";
                    break;
                case SkillSelectState.UnClickable_NoTargets:
                    skillStateTxt.text = "No targets";
                    break;
                case SkillSelectState.UnClickable_NoStamina:
                    skillStateTxt.text = "No stamina";
                    break;
                case SkillSelectState.UnClickable_InTactics:
                    skillStateTxt.text = "In Tactics";
                    break;
                case SkillSelectState.Clicked:
                    skillStateTxt.text = "Clicked";
                    break;
                case SkillSelectState.UnClickable_NoActionPts:
                    skillStateTxt.text = "No action pts";
                    break;
                case SkillSelectState.UnClickable_Locked:
                    skillStateTxt.text = "Locked";
                    break;
                case SkillSelectState.UnClickable_Misc:
                    skillStateTxt.text = "Misc";
                    break;
                default:
                    break;
            }
        }

      
        void FillTopTrans()
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
            }
        }
        void FillMidTrans()
        {
            int lines = skillModel.descLines.Count;
            // get skill card height             
            RectTransform skillCardRect = transform.GetComponent<RectTransform>();
            RectTransform midTransRect = midTrans.GetComponent<RectTransform>();
            if (lines > 2)
            {
                // increase size 
                int incr = lines - 2;
                incrVal = incr * 40; 
                midTransRect.sizeDelta
                        = new Vector2(midTransRect.sizeDelta.x, midTransHt + incrVal);
                skillCardRect.sizeDelta
                        = new Vector2(skillCardRect.sizeDelta.x, skillCardHt + incrVal);
            }
            else
            {
                // reduce to org size 
                incrVal= 0;
                midTransRect.sizeDelta
                        = new Vector2(midTransRect.sizeDelta.x, midTransHt );
                skillCardRect.sizeDelta
                        = new Vector2(skillCardRect.sizeDelta.x, skillCardHt);
            }
            int j = 0; 
            foreach (Transform child in midTrans)
            {
                if(j < lines)
                {
                    child.gameObject.SetActive(true);
                    child.GetComponent<TextMeshProUGUI>().text
                                                   = skillModel.descLines[j];
                }
                else
                {
                    child.gameObject.SetActive(false);  
                }
                j++; 
            }
        }
        void FillBtmTrans()
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

        public int GetIncVal()
        {
            return incrVal; 
        }

    }
}