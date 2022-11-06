//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Common; 
//namespace Combat
//{



//    public class SkillAIController : MonoBehaviour
//    {

//        public List<SkillNames> allSkillWithTargets = new List<SkillNames>();
//        public List<SkillAIData> GetBaseWeights = new List<SkillAIData>();

//        public SkillController skillController; 

//        void Start()
//        {
//            skillController = GetComponent<SkillController>(); 
//        }

//        public List<SkillNames> GetSkillsClickable()
//        {
          

//            return null; 

//        }

//        public void ModifySkillAIDataBaseWts()
//        {
//            // check thru a series of conditions in a class 
//            // keep on adding - subtracting base weights 
//            // update the data base list 
//        }

//        public void SkillSelection(CharController charController)
//        {
           


//        }

//        public void ApplyTheSkill()
//        {
//            // apply the skill with the highest weights 

//        }
    
//    }
//}

////public bool GetTargetInRange()
////{

////    CharController prevCharController = CombatService.Instance.currCharOnTurn;

////    // get curr Char turn 
////    // talk to skill Service find the skillMgrs and their targets 
////    // make a data class of skills their targets, base weights etc


////    return false;
////}


////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;
////using System.Linq;
////using UnityEngine.UI;
////using UnityEngine.EventSystems;
////using UnityEngine.Events; 
////using DG.Tweening;
////using TMPro;
////using Common;

////namespace Combat
////{
////    //public class PerkWithSelectState
////    //{
////    //    public PerkNames perkName;
////    public SelectState selectState; 
////    public PerkWithSelectState(PerkNames _perkName, SelectState _uiSelectionState)
////    {
////        perkName = _perkName;
////        selectState = _uiSelectionState; 
////    }
////}
////    [System.Serializable]
////    public class SkillCardData
////    {
////        public SkillModel skillModel;
////        public List<PerkType> perkChain = new List<PerkType>();
////        public List<string> descLines = new List<string>();
////    }

////    public class SkillServiceView : MonoSingletonGeneric<SkillServiceView>
////    {
////        #region Declarations
////        // algo for sprite toggles. 
////        GameObject skillPanel;
////        const int skillBtnCount = 8;

////        //  [SerializeField] List<Button> SkillBtns = new List<Button>();
////        [SerializeField] Button optionsBtn;
////        [SerializeField] Button fleeBtn;
////        [SerializeField] Sprite LockedSkillIconSprite;
////        [SerializeField] Sprite NASkillIconSprite;
////        [SerializeField] PerkSelectionController perkSelectionController;
////        [SerializeField] Transform currTransHovered;

////        [Header("SKILL CARD PARAMS")]
////        public SkillHexSO skillHexSO;
////        [SerializeField] List<GameObject> allySkillCards = new List<GameObject>();
////        [SerializeField] List<GameObject> enemySkillCards = new List<GameObject>();
////        [SerializeField] GameObject SkillPanel;
////        // [SerializeField] GameObject currSkillCard;
////        [SerializeField] GameObject textPrefab;
////        [SerializeField] Vector2 posOffset;
////        public int index;
////        GameObject SkillCard;
////        public SkillCardData skillCardData;
////        public SkillController skillManager;
////        //public SkillBase skillBase;

////        #endregion

////        void Start()
////        {
////            index = -1;
////            optionsBtn.onClick.AddListener(OnOptionBtnPressed);

////            skillPanel = GameObject.FindGameObjectWithTag("SkillPanel");

////            CombatEventService.Instance.OnSOT +=
////               () => SetSkillsPanel(CombatService.Instance.currCharOnTurn.charModel.charName);

////            CombatEventService.Instance.OnCharClicked +=
////                () => SetSkillsPanel(CombatService.Instance.currCharClicked.charModel.charName);

////        }

////        public void SkillCardExit()
////        {
////            Destroy(SkillCard);
////        }

////        public void OnSkillBtnHovered(GameObject hoveredObject)
////        {
////            if (index != hoveredObject.transform.GetSiblingIndex())
////            {
////                index = hoveredObject.transform.GetSiblingIndex();
////                SkillCardExit();
////            }
////            else
////            {
////                return;
////            }
////            SkillDataSO skillSO = SkillService.Instance
////                       .GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);

////            if (skillSO != null)
////                SkillService.Instance.currSkillHovered = skillSO.allSkills[index].skillName;
////            else Debug.Log("Skill SO is null ");
////            SkillService.Instance.currCharName = CombatService.Instance.currCharOnTurn.charModel.charName;

////            SkillService.Instance.On_SkillHovered(CombatService.Instance.currCharOnTurn.charModel.charName);

////            ShowSkillCard(index);
////            PopulateSkillCard();
////        }

////        void ToggleTxt(Transform transform)
////        {
////            foreach (Transform child in transform)
////            {
////                child.gameObject.SetActive(false);
////            }
////        }

////        void ShowSkillCard(int index)
////        {
////            int lvl = skillCardData.perkChain.Count;
////            Transform skillBtnHovered = SkillPanel.transform.GetChild(index);
////            DynamicPosData dyna =
////                    GridService.Instance.GetDyna4GO(CharacterService.Instance
////                                    .GetCharGOWithName(SkillService.Instance.currCharName));

////            if (dyna.charMode == CharMode.Ally)
////            {
////                if (lvl == 0)
////                {
////                    SkillCard = GameObject.Instantiate(allySkillCards[0]);
////                }
////                else if (lvl == 1)
////                {
////                    SkillCard = GameObject.Instantiate(allySkillCards[1]);
////                }
////                else if (lvl == 2)
////                {
////                    SkillCard = GameObject.Instantiate(allySkillCards[2]);
////                }
////                else
////                {
////                    SkillCard = GameObject.Instantiate(allySkillCards[3]);
////                }

////            }
////            else if (dyna.charMode == CharMode.Enemy)
////            {
////                if (lvl < 2)
////                {
////                    SkillCard = GameObject.Instantiate(enemySkillCards[0]);
////                }
////                else
////                {
////                    SkillCard = GameObject.Instantiate(enemySkillCards[1]);
////                }
////            }
////            Vector3 pos = new Vector3(skillBtnHovered.position.x, SkillCard.transform.position.y, 1);
////            SkillCard.transform.parent = skillBtnHovered;
////            SkillCard.transform.position = pos;
////        }

////        public void PopulateSkillCard()
////        {
////            // Populate heading
////            Transform Heading = SkillCard.transform.GetChild(1);
////            Heading.GetComponent<TextMeshProUGUI>().text = SkillService.Instance.currSkillHovered.ToString().CreateSpace();

////            Transform Desc = SkillCard.transform.GetChild(2);
////            ToggleTxt(Desc);
////            // create chaltau Object pool.. 
////            for (int i = 0; i < skillCardData.descLines.Count; i++)
////            {
////                Desc.transform.GetChild(i).gameObject.SetActive(true);
////                Desc.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text
////                                                                     = skillCardData.descLines[i];
////            }
////            // populate round and stamina Info
////            PopulateHexesInSkillCard();

////        }

////        public void PopulateHexesInSkillCard()
////        {
////            Sprite sprite1 = null, sprite2 = null, sprite3 = null;

////            foreach (var ls in skillCardData.skillModel.allPerkHexes)
////            {
////                List<PerkType> SCperkChain = skillCardData.perkChain.OrderBy(e => e).ToList();
////                List<PerkType> LSPerkChain = ls.perkChain.OrderBy(e => e).ToList();

////                if (SCperkChain.Count == 0)  // none case 
////                {
////                    sprite1 = skillHexSO.allHexes.Find(t => t.hexName
////                            == skillCardData.skillModel.allPerkHexes[0].hexName[0]).hexSprite;
////                    sprite2 = skillHexSO.allHexes.Find(t => t.hexName
////                          == skillCardData.skillModel.allPerkHexes[0].hexName[1]).hexSprite;
////                    sprite3 = skillHexSO.allHexes.Find(t => t.hexName
////                          == skillCardData.skillModel.allPerkHexes[0].hexName[2]).hexSprite;
////                }
////                else if (SCperkChain.SequenceEqual(LSPerkChain))
////                {
////                    sprite1 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexName[0]).hexSprite;
////                    sprite2 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexName[1]).hexSprite;
////                    sprite3 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexName[2]).hexSprite;
////                }

////                Transform hexParent = SkillCard.transform.GetChild(0);
////                hexParent.GetChild(0).GetComponent<Image>().sprite = sprite1;
////                hexParent.GetChild(1).GetComponent<Image>().sprite = sprite2;
////                hexParent.GetChild(2).GetComponent<Image>().sprite = sprite3;

////            }
////        }

////        public void OnOptionBtnPressed()
////        {
////            perkSelectionController.On_OptionBtnPressed();
////        }

////        public void OnFleeBtnPressed()
////        {

////        }

////        public void SkillBtnPressed()
////        {
////            GameObject btn = EventSystem.current.currentSelectedGameObject;
////            int index = btn.transform.GetSiblingIndex();
////            Debug.Log("current pressed skill btn" + index);
////            SkillDataSO skillSO = SkillService.Instance
////                        .GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);
////            Debug.Log("Skill index " + skillSO.charName);
////            if (skillSO != null)
////                SkillService.Instance.currSkillName = skillSO.allSkills[index].skillName;

////            SkillService.Instance.On_SkillSelected
////                (CombatService.Instance.currCharOnTurn.charModel.charName);

////        }



////        public void UpdateSkillBtntxt(CharNames _charName, SkillNames _skillName, int posOnSkillPanel)
////        {
////            SkillModel skillModel = SkillService.Instance.GetSkillModel(_charName, _skillName);

////            int cdGap = CombatService.Instance.currentRound - skillModel.lastUsedInRound;
////            string displayTxt = "";
////            if (cdGap <= 0)
////                displayTxt = "";
////            else
////                displayTxt = cdGap.ToString();
////            skillPanel.transform.GetChild(posOnSkillPanel).GetChild(0).GetComponent<TextMeshProUGUI>().text
////                = displayTxt;
////        }
////        public void SetSkillsPanel(CharNames _charName)
////        {

////            // no set active false .. change the sprite 
////            // update the image alpha as per skillState 
////            foreach (SkillDataSO skillSO in SkillService.Instance.allCharSkillSO)
////            {
////                if (skillSO.charName == _charName)
////                {
////                    for (int i = 0; i < skillSO.allSkills.Count; i++)
////                    {
////                        if (skillSO.allSkills[i].skillUnLockStatus == 1)
////                        {
////                            // skillPanel.transform.GetChild(i).gameObject.SetActive(true);
////                            Transform skillIconTranform = skillPanel.transform.GetChild(i);
////                            skillIconTranform.GetComponent<Image>().sprite
////                                                                = skillSO.allSkills[i].skillIconSprite;
////                            SkillNames skillName = skillSO.allSkills[i].skillName;

////                            Debug.Log("SkillName in Question" + skillName);

////                            SkillModel skillModel = SkillService.Instance.GetSkillModel(_charName, skillName);
////                            if (skillModel == null)
////                                Debug.Log("SkillName in Question" + skillName);
////                            skillIconTranform.GetComponent<SkillBtnsPointerEvents>().RefreshIconAsPerState();

////                            //UpdateSkillBtntxt( skillSO.charName, skillSO.allSkills[i].skillName, i); 


////                            // CHNAGE HERE .. for SKill Sprite solution
////                        }
////                        else if (skillSO.allSkills[i].skillUnLockStatus == 0)
////                        {
////                            //skillPanel.transform.GetChild(i).gameObject.SetActive(true);
////                            skillPanel.transform.GetChild(i).GetComponent<Image>().sprite = LockedSkillIconSprite;
////                        }
////                        else
////                        {
////                            skillPanel.transform.GetChild(i).GetComponent<Image>().sprite = NASkillIconSprite;
////                        }

////                    }
////                    // to make the extra button as not available 
////                    for (int i = skillSO.allSkills.Count; i < skillBtnCount; i++)
////                    {
////                        skillPanel.transform.GetChild(i).GetComponent<Image>().sprite = NASkillIconSprite;
////                    }
////                }
////            }
////        }
////    }
////}
//////for (int i = 0; i < 8; i++)
//////{
//////    Button btn = skillPanel.transform.GetChild(i).GetComponent<Button>();
//////    if (btn != null)
//////    {
//////        SkillBtns.Add(btn);
//////       // btn.onClick.AddListener(SkillBtnPressed);                        
//////    }
//////}    


//////public void UpdateSkillIconTxt(SkillNames skillName, int cdGap)
//////{
//////    // loop thru all the skill Icons.. if it matches the SkillName update txt


//////    string displayTxt = "";
//////    if (cdGap <= 0)
//////        displayTxt = "";
//////    else
//////        displayTxt = cdGap.ToString(); 


//////    for (int i = 0; i < skillBtnCount ; i++)
//////    {
//////        SkillDataSO skillSO = SkillService.Instance
//////                .GetSkillSO(CombatService.Instance.currentCharSelected.charModel.charName);
//////        if (skillSO != null && skillSO.allSkills[i].skillUnLockStatus !=-1) // Skill is not NA
//////        {
//////            Debug.Log("SKILL Index" + i); 
//////           if(skillName == skillSO.allSkills[i].skillName)
//////            {                       

//////                skillPanel.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text
//////                                                            = displayTxt; 
//////            }
//////        }
//////    }
//////}
