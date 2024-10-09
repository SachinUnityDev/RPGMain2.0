using Common;
using Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Combat
{
    public class SkillCardView : MonoBehaviour
    {
        [SerializeField] const float skillCardHt = 308f;
        [SerializeField] const float midTransHt = 111f;

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

        [Header("Skill Card Cd and Max Use")]
        [SerializeField] Transform cdNMaxUse; 

        [Header(" In combat for testing ")]
        [SerializeField] TextMeshProUGUI skillStateTxt;

        [Header(" Global var")]
        int incr;
        [SerializeField] int incrVal = 0;
        List<string> descLine = new List<string>(); 
        private void OnEnable()
        {
            ResetWidthHeight();
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
            ResetSize();
        }
        void ResetSize()
        {
            RectTransform skillCardRect = transform.GetComponent<RectTransform>();
            RectTransform midTransRect = midTrans.GetComponent<RectTransform>();
            midTransRect.sizeDelta
                    = new Vector2(midTransRect.sizeDelta.x, midTransHt);
            skillCardRect.sizeDelta
                    = new Vector2(skillCardRect.sizeDelta.x, skillCardHt);
        }
        void SkillCardInit()
        {
            ClearData();
            if (GameService.Instance.currGameModel.gameScene == GameScene.InTown ||
               GameService.Instance.currGameModel.gameScene == GameScene.InQuestRoom)
            {
                charController = InvService.Instance?.charSelectController;
                if (charController == null) return; 
                skillController = charController.skillController; 
                skillModel  = SkillService.Instance.skillModelHovered;
                skillName = skillModel.skillName;
            }
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)              
            {
                charController = CombatService.Instance?.currCharClicked;
                if (charController == null) return;
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
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
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
            skillStateTxt.text = skillState.ToString(); 
            //switch (skillState)
            //{
            //    case SkillSelectState.None:
            //        break;
            //    case SkillSelectState.Clickable:
            //        skillStateTxt.text = "Clickable";
            //        break;
            //    case SkillSelectState.UnHoverable:
            //        skillStateTxt.text = "UnHoverable";
            //        break;
            //    case SkillSelectState.Unclickable_passiveSkills:
            //        skillStateTxt.text = "PassiveSkills";
            //        break;
            //    case SkillSelectState.UnClickable_InCd:
            //        skillStateTxt.text = "In CD";
            //        break;
            //    case SkillSelectState.Unclickable_notCharsTurn:
            //        skillStateTxt.text = "Not Char Turn";
            //        break;
            //    case SkillSelectState.Unclickable_notOnCastPos:
            //        skillStateTxt.text = "not on castPos";
            //        break;
            //    case SkillSelectState.UnClickable_NoTargets:
            //        skillStateTxt.text = "No targets";
            //        break;
            //    case SkillSelectState.UnClickable_NoStamina:
            //        skillStateTxt.text = "No stamina";
            //        break;
            //    case SkillSelectState.UnClickable_InTactics:
            //        skillStateTxt.text = "In Tactics";
            //        break;
            //    case SkillSelectState.Clicked:
            //        skillStateTxt.text = "Clicked";
            //        break;
            //    case SkillSelectState.UnClickable_NoActionPts:
            //        skillStateTxt.text = "No action pts";
            //        break;
            //    case SkillSelectState.UnClickable_Locked:
            //        skillStateTxt.text = "Locked";
            //        break;
            //    case SkillSelectState.UnClickable_Misc:
            //        skillStateTxt.text = "Misc";
            //        break;

            //    default:
            //        break;
            //}
        }      
        void FillTopTrans()
        {
            skillDataSO = SkillService.Instance.GetSkillSO(charController.charModel.charName);
            skillData = skillDataSO.GetSkillData(skillName);
            if (skillData == null)
            {
                Debug.LogError("skilldata missing" + skillName);
            }
            SkillInclination skillIncli = skillModel.skillInclination;
            
            skillHexSO = SkillService.Instance.skillHexSO;
            skillViewSO = SkillService.Instance.skillViewSO; 
            // skill Incli...
            topTrans.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                    =    skillHexSO.GetSkillIncliSprite(skillIncli);

            topTrans.GetChild(0).GetComponent<Image>().sprite
                    = skillHexSO.GetSkillIncliSpriteBG(skillIncli);
            // attack type...
            AttackType attackType = skillModel.attackType;
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
            if(perkHexData == null)
            {               
                return;
            }
            int i = 0;
            if(perkHexData.hexNames.Count() > 0)
            foreach (HexNames perkHex in perkHexData.hexNames)
            {
                topTrans.GetChild(3).GetChild(i).GetComponent<Image>().sprite =
                                                        skillHexSO.GetHexSprite(perkHex);
                i++;            
            }
        }
        void FillMidTrans()
        {
            descLine = skillModel.GetDescLines(); 
            int lines = descLine.Count;           
            // get skill card height             
            RectTransform skillCardRect = transform.GetComponent<RectTransform>();
            RectTransform midTransRect = midTrans.GetComponent<RectTransform>();
            incrVal = 0;
            int j = 0;
            incr = 0; 
            ResetSize();
            foreach (Transform child in midTrans)
            {
                if(j < lines)
                {
                    child.gameObject.SetActive(true);
                    TextMeshProUGUI textM = child.GetComponent<TextMeshProUGUI>();
                    textM.text = descLine[j];
                    if (textM!= null || textM.text != null)
                    {
                        UpdateTextHeight(textM);
                      //  Debug.Log(" j val" + j);
                    }                     
                    else
                    {
                        Debug.LogError(" j val" + j); 
                    }                    
                }
                else
                {
                    child.gameObject.SetActive(false);
                    incrVal = 0;                    
                    incr = 0;
                }
                j++; 
            }
            if (lines > 2)
            {
                incrVal = 0;
                incr = 0;
                // increase size 
                incr += lines - 2;// also updated in update Txt Ht

                RectTransform txtRect = midTrans.GetChild(0).GetComponent<RectTransform>();
                float txtHt = txtRect.sizeDelta.y;
                //Debug.Log("TXT HT" + txtHt);
                incrVal += incr * (int)(txtHt);// correction factor
                midTransRect.sizeDelta
                        = new Vector2(midTransRect.sizeDelta.x, midTransHt + incrVal);
                skillCardRect.sizeDelta
                        = new Vector2(skillCardRect.sizeDelta.x, skillCardHt + incrVal);
            }
            else
            {
                // reduce to org size 
                incrVal = 0; incr = 0;
                midTransRect.sizeDelta
                        = new Vector2(midTransRect.sizeDelta.x, midTransHt);
                skillCardRect.sizeDelta
                        = new Vector2(skillCardRect.sizeDelta.x, skillCardHt);
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
            cdNMaxUse.GetComponent<SkillCardCdUseView>().InitSkillCdNUse(skillModel);

        }

    

        void UpdateTextHeight(TextMeshProUGUI textM)
        {
            // Get the current text from the TextMeshPro component
            string text = textM.text;
            incr = 0; incrVal= 0;
          //  Debug.Log("The Desc" + textM.text);
            
            // Check if the text length exceeds the maximum length
            if (text.Length > 30)
            {
                // Calculate the new height based on the number of lines required               
                int numberOfLines = Mathf.CeilToInt((float)text.Length / 30);
                float newHeight = textM.fontSize * numberOfLines;

                incrVal += (int)((numberOfLines-1)* textM.rectTransform.sizeDelta.y); 
                // Adjust the text component's rect transform height
                textM.rectTransform.sizeDelta = new Vector2(textM.rectTransform.sizeDelta.x, newHeight);
            }
            else
            {
                textM.rectTransform.sizeDelta = new Vector2(textM.rectTransform.sizeDelta.x, 40);
            }
        }
    }
}