using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common;
using TMPro;
using System.Linq;
using System;

namespace Combat
{
    public class SkillBtnsPointerEvents : MonoBehaviour
        //, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        //const float waitTime = 0.1f;
        //[SerializeField] int expBeyondLineNo = 3;
        //[SerializeField] Vector2 skillCardBaseDim = new Vector2(328f, 325f); 
        //public SkillHexSO skillHexSO;
        ////public GameObject skillCardPrefab; 
        
        //[SerializeField]GameObject skillCard;
        //[SerializeField] int index =-1;

        ////public SkillCardData skillCardData; 
        //public SkillSelectState skillState;
        //public SkillNames prevSkillHovered; 

        //[SerializeField] List<string> alliesDesc = new List<string>();
        //[SerializeField] List<string> enemyDesc = new List<string>();
        //[SerializeField] List<string> bothAllyNEnemy = new List<string>();
        //[SerializeField] List<string> attackTypeLs = new List<string>();
        //[SerializeField] List<string> damageTypeLS = new List<string>();
        //[SerializeField] List<string> sunStrReturn = new List<string>(); 
        //[SerializeField] List<string> finalDesc = new List<string>();

        //[Header("SkillState Display")]
        //[SerializeField] TextMeshProUGUI skillStateTxt;
        //void Start()
        //{
        //    prevSkillHovered = SkillNames.None;
        //}
        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    if (skillCardData.skillModel.skillName == SkillNames.None) return; 
        //    ShowSkillCard(); 
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    SkillServiceView.Instance.index = -1;
          
        //        StartCoroutine(Wait()); 
        //}

        //public void ShowSkillCard()
        //{
        //    skillCard.SetActive(true);
        //    SkillServiceView.Instance.pointerOnSkillIcon = true; 
        //    SkillDataSO skillSO = SkillService.Instance
        //               .GetSkillSO(CombatService.Instance.currCharClicked.charModel.charName);
        //    index = gameObject.transform.GetSiblingIndex();
        //    SkillServiceView.Instance.index = index;
        //    // UPDATE SKILL SERVICE 
        //    if (skillSO != null && index < skillSO.allSkills.Count)
        //        SkillService.Instance.On_SkillHovered(CombatService.Instance.currCharClicked.charModel.charName,
        //                    skillSO.allSkills[index].skillName);
        //    else { Debug.Log("Skill SO is null " + index);return;  } 

        //   // SkillService.Instance.currCharName = CombatService.Instance.currCharClicked.charModel.charName;



        //    skillCardData = SkillService.Instance.skillCardData;

        //    //float htSkillIcon = gameObject.GetComponent<RectTransform>().rect.height;          

        //  //  SortSkillCardData();
        //    PopulateSkillCard();
        //}

        //public void PopulateSkillCard()
        //{
        //    // Populate heading
        //    Transform skillBtnHovered = gameObject.transform;

        //    Transform Heading = skillCard.transform.GetChild(0).GetChild(1).GetChild(1); 
        //    Heading.GetComponent<TextMeshProUGUI>().text = SkillService.Instance.currSkillHovered.ToString().CreateSpace();

        //    Transform Desc = skillCard.transform.GetChild(0).GetChild(2);
        //    ToggleTxt(Desc);

        //    //float htOfTxt = skillCard.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<RectTransform>().rect.height;
        //    int lines = skillCardData.descLines.Count; 
        //    if (lines > expBeyondLineNo)
        //    {
        //        int incr = lines - expBeyondLineNo;  
        //        skillCard.GetComponent<RectTransform>().sizeDelta
        //            = new Vector2(skillCardBaseDim.x , skillCardBaseDim.y + incr * 40f);

        //    }
        //    else
        //    {
        //        skillCard.GetComponent<RectTransform>().sizeDelta
        //          = new Vector2(skillCardBaseDim.x, skillCardBaseDim.y);
        //    }
        //    float skillBtnHt = skillBtnHovered.GetComponent<RectTransform>().rect.height;
        //    float htSkillcard = skillCard.GetComponent<RectTransform>().rect.height;

        //    float xPos = skillBtnHovered.localPosition.x;
        //    float yPos = (skillBtnHt + htSkillcard/3); 
        //    Vector3 pos = new Vector3(xPos, yPos, 1);
        //    skillCard.GetComponent<RectTransform>().anchoredPosition = pos; 

        //    for (int i = 0; i < skillCardData.descLines.Count; i++)
        //    {
        //        Desc.transform.GetChild(i).gameObject.SetActive(true);
        //        Desc.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text
        //                                                             = skillCardData.descLines[i];               

        //    }
        //    // populate round and stamina Info
        //    PopulateHexesInSkillCard();
        //    PopulateLowerValues();
        //    PopulateUpperCorners();
        //    PopulateSkillState();
        //}

        //public void PopulateSkillState()
        //{
        //    // grab the UI and show the state
        //    skillStateTxt.text  = skillCardData.skillModel.GetSkillState().ToString();



        //}
        //public void PopulateLowerValues()
        //{
            
        //    skillCard.transform.GetChild(1).GetChild(2).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text
        //        = skillCardData.skillModel.staminaReq.ToString();
        //    skillCard.transform.GetChild(1).GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text
        //       = skillCardData.skillModel.skillLvl.ToString();

        //    string str = "";
        //    if (skillCardData.skillModel.cd <= 0)
        //        str = "No cd";
        //    else
        //        str =$"{skillCardData.skillModel.cd} Rd"; 

        //    skillCard.transform.GetChild(0).GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text
        //        = str; 

        //}
        //public void PopulateUpperCorners()
        //{
        //    // get attack type from skillmodel
        //    // get respective sprite and background from skillHexSO
        //    // zero down on the images and allocate 
        //    AttackType attackType = skillCardData.skillModel.attackType;
        //    Sprite atSprite=  skillHexSO.allAttacksSprites.Find(t => t.attackType == attackType).attackTypeSprite;
        //    Sprite bGSprite = skillHexSO.allAttacksSprites.Find(t => t.attackType == attackType).attackTypeBG;

        //    skillCard.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = bGSprite;
        //    skillCard.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = atSprite;

        //    SkillInclination skillIncli = skillCardData.skillModel.skillInclination;
        //    Sprite SLSprite = skillHexSO.allSkillIncli.Find(t => t.SkillIncliType == skillIncli).SkillIncliSprite;
        //    Sprite bgSpriteSIncli = skillHexSO.allSkillIncli.Find(t => t.SkillIncliType == skillIncli).SkillIncliBG; 
        //    skillCard.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = bgSpriteSIncli;            
        //    skillCard.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = SLSprite;
        //}

        //public void PopulateHexesInSkillCard()
        //{
        //    Sprite sprite1 = null, sprite2 = null, sprite3 = null;
        //    //Debug.Log("Skillcard data " + skillCardData); 

        //    if (skillCardData.skillModel.allPerkHexes.Count == 0) return;
        //    foreach (var ls in skillCardData.skillModel.allPerkHexes)
        //    {
        //        List<PerkType> SCperkChain = skillCardData.perkChain.OrderBy(e => e).ToList();
        //        List<PerkType> LSPerkChain = ls.perkChain.OrderBy(e => e).ToList();

        //        if (SCperkChain.Count == 0)  // none case 
        //        {
        //            sprite1 = skillHexSO.allHexes.Find(t => t.hexName
        //                    == skillCardData.skillModel.allPerkHexes[0].hexName[0]).hexSprite;
        //            sprite2 = skillHexSO.allHexes.Find(t => t.hexName
        //                  == skillCardData.skillModel.allPerkHexes[0].hexName[1]).hexSprite;
        //            sprite3 = skillHexSO.allHexes.Find(t => t.hexName
        //                  == skillCardData.skillModel.allPerkHexes[0].hexName[2]).hexSprite;
        //        }
        //        else if (SCperkChain.SequenceEqual(LSPerkChain))
        //        {
        //            sprite1 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexName[0]).hexSprite;
        //            sprite2 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexName[1]).hexSprite;
        //            sprite3 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexName[2]).hexSprite;
        //        }

        //        Transform hexParent = skillCard.transform.GetChild(0);
        //        hexParent.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite1;
        //        hexParent.GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprite2;
        //        hexParent.GetChild(0).GetChild(2).GetComponent<Image>().sprite = sprite3;

        //    }
        //}

        //public void RefreshIconAsPerState()
        //{
        //    skillState = skillCardData.skillModel.GetSkillState();
        //   // ChangeSkillFrame(skillState);
        //    switch (skillState)
        //    {
        //        case SkillSelectState.Clickable:
        //            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //            //ChangeSkillFrame(SkillSelectState.Clickable);
        //            break;
        //        case SkillSelectState.UnClickable_InCd:
        //            gameObject.GetComponent<Image>().color = new Color(0.5f, 1, 1, 0.8f); 

        //            break;
        //        case SkillSelectState.UnClickable_NoTargets:
        //            gameObject.GetComponent<Image>().color = new Color(0.6f, 1, 1, 0.8f);

        //            break;
        //        case SkillSelectState.UnClickable_NoStamina:
        //            gameObject.GetComponent<Image>().color = new Color(0.9f, 1, 1, 0.8f);

        //            break;
        //        case SkillSelectState.Unclickable_notOnCastPos:
        //            gameObject.GetComponent<Image>().color = new Color(0.2f, 1, 1, 0.8f);

        //            break;
        //        case SkillSelectState.Unclickable_passiveSkills:
        //            gameObject.GetComponent<Image>().color = new Color(0.3f, 1, 1, 0.8f);

        //            break;
        //        case SkillSelectState.Unclickable_notCharsTurn:
        //            gameObject.GetComponent<Image>().color = new Color(1f, 1, 1, 0.9f);

        //            break;
        //        case SkillSelectState.Clicked:
        //            // change frame from skillSO
        //           // ChangeSkillFrame(SkillSelectState.Clicked);
        //            break;
              
           
        //        default:
        //            break;
        //    }


        //}

        //#region HELPER Methods
        //IEnumerator Wait()
        //{
        //    yield return new WaitForSeconds(waitTime);
        //    if (!SkillServiceView.Instance.pointerOnSkillCard && SkillServiceView.Instance.index == -1)
        //         skillCard.SetActive(false);
        //}

        //void ToggleTxt(Transform transform)
        //{
        //    foreach (Transform child in transform)
        //    {
        //        child.gameObject.SetActive(false);
        //    }
        //}

        //#endregion
     
        //public void OnPointerClick(PointerEventData eventData)
        //{

        //    if (eventData.button == PointerEventData.InputButton.Left)           
        //                        SkillServiceView.Instance.SkillBtnPressed();
        //    //if (eventData.button == PointerEventData.InputButton.Left)
        //    //    ChangeSkillFrame(SkillSelectState.Clickable); 
        //    //if (skillState == SkillSelectState.Clickable)
        //    //{
        //    //    SkillServiceView.Instance.SkillBtnPressed();             
        //    //    skillState = SkillSelectState.Clicked;
        //    //    skillCardData.skillModel.SetSkillState(skillState);               
        //    //}
        //    //else if(skillState == SkillSelectState.Clicked)
        //    //{               
        //    //    skillState = SkillSelectState.Clickable;
        //    //    skillCardData.skillModel.SetSkillState(skillState);               
        //    //}
        //    // ChangeSkillFrame(skillState);
        //}
        //public void ChangeSkillFrame(SkillSelectState _skillState)  // call from  skill Controller
        //{
        //    foreach (Transform child in transform.parent)
        //    {
        //        child.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillNormalFrame;
        //        child.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;  
                
        //    }
        //    if (_skillState == SkillSelectState.Clicked)
        //    {
        //       // transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillSelectFrame;
        //        transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one*1.25f;
        //    }

        //}

        //#region  ACTION METHODS

        //void SortSkillCardData()
        //{
        //    if (prevSkillHovered == SkillService.Instance.currSkillHovered) return;
        //    Debug.Log("Inside the SKILL SORT ");
        //    attackTypeLs = skillCardData.descLines?.Where(t => t.Contains(AttackType.Ranged.ToString())).ToList();

        //    bothAllyNEnemy.Clear(); 
        //    bothAllyNEnemy = skillCardData.descLines?.Where(t => t.Contains("<style=Enemy>") && t.Contains("<style=Allies>")).ToList();

        //    enemyDesc.Clear();
        //    enemyDesc = skillCardData.descLines?.Where(t => t.Contains("<style=Enemy>")).Except(bothAllyNEnemy).ToList();

        //    alliesDesc.Clear();
        //    alliesDesc = skillCardData.descLines?.Where(t => t.Contains("<style=Allies>")).Except(bothAllyNEnemy).ToList();

        //    //alliesDesc = SumUpLS(alliesDesc);
        //    //enemyDesc = SumUpLS(enemyDesc);
        //    //bothAllyNEnemy = SumUpLS(bothAllyNEnemy);

        //    finalDesc.Clear();
        //    finalDesc.AddRange(alliesDesc);
        //    finalDesc.AddRange(enemyDesc);
        //    finalDesc.AddRange(bothAllyNEnemy);
        //    finalDesc.Distinct().ToList();

        //    skillCardData.descLines = finalDesc;
        //}

        //List<string> SumUpLS(List<string> stringLS)
        //{
        //    sunStrReturn = new List<string>();

        //    List<string> damageTypeLS = SortbyDamageType(stringLS);
        //    List<string> attributesLS = SortByAttributes(stringLS);
        //    List<string> statesLS = SortByStates(stringLS);
        //    List<string> miscLS = stringLS.Except(damageTypeLS).Except(attributesLS).Except(statesLS).ToList();

        //    sunStrReturn.AddRange(damageTypeLS);
        //    sunStrReturn.AddRange(attributesLS);
        //    sunStrReturn.AddRange(statesLS);
        //    sunStrReturn.AddRange(miscLS);


        //    return sunStrReturn; 
        //}



        //List<string> SortbyDamageType(List<string> stringLS)
        //{
        //    List<string> strReturn = new List<string>(); 

        //    for (int i = 1; i < Enum.GetNames(typeof(DamageType)).Length; i++)
        //    {
        //        string str = ((DamageType)i).ToString();

        //        strReturn.AddRange(stringLS.Where(t => t.Contains(str)).ToList());
        //    }
        //    return strReturn;
        //}

        //List<string> SortByAttributes(List<string> stringLS)
        //{
        //    List<string> strReturn = new List<string>();

        //    for (int i = 1; i < Enum.GetNames(typeof(StatsName)).Length; i++)
        //    {
        //        string str = ((StatsName)i).ToString();

        //        strReturn.AddRange(stringLS.Where(t => t.Contains(str)).ToList());
        //    }
        //    return strReturn;         

        //}
        //List<string> SortByStates(List<string> stringLS)
        //{
        //    List<string> strReturn = new List<string>();

        //    for (int i = 1; i < Enum.GetNames(typeof(CharStateName)).Length; i++)
        //    {
        //        string str = ((CharStateName)i).ToString();

        //        strReturn.AddRange(stringLS.Where(t => t.Contains(str)).ToList());
        //    }
        //    return strReturn;
        //}

       // #endregion
    }


}
// loop thru all the icons on charClicked and refresh
//public void GetSkillState(GameObject SkillIconObj)
//{
//    //return skillModel.skillSelState; 

//    ////int index2 = SkillIconObj.transform.GetSiblingIndex();
//    //CharController currSelChar = CombatService.Instance.currCharClicked;

//    //SkillDataSO skillSO = SkillService.Instance.GetSkillSO(currSelChar.charModel.charName);
//    //SkillNames skillName = skillSO.allSkills[index2].skillName;

//    // skillState = SkillService.Instance.GetSkillState(skillName); 
//}
////public void SetIconSkillState(SkillSelectState _skillState)
////{
////    skillState = _skillState; 
////}