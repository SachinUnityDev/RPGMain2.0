using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
using Common;
namespace Combat
{
    public class PerkSelectionController : MonoBehaviour, IPanel

    {
        [Header("TO BE CONNECTED")]
        public GameObject perkSelPanel;
        

        [SerializeField] Button[] perkBtns;
        [SerializeField] Button[] rightSkillBtns; 
    
        [SerializeField] SkillNames currSkill;
        [SerializeField] CharNames currChar; 
        [SerializeField] Transform skillPanel;
        [SerializeField] Transform weaponPanel;
        [SerializeField] Transform campingPanel;
        [SerializeField] Transform skillPtsDisplay;
        [SerializeField] Transform displaySubPanel;
        [SerializeField] Transform lockUnLockPerk; 
        [SerializeField]List<PerkModelData> allPerkInSkill;
        [SerializeField] TextMeshProUGUI skilltxt;
        [SerializeField] TextMeshProUGUI charNameTxt; 
        [SerializeField] PerkModelData currSelectedPerk = null;
        [SerializeField] SkillLvl maxSkillLvl ;
        [SerializeField] TextMeshProUGUI perkDesc; 
        void Start()
        {
           
           // UIControlServiceCombat.Instance.ToggleUIStateScale(perkSelPanel, UITransformState.None);
        }
        public void UpdateSkillPts()
        {
            skilltxt.text = SkillService.Instance.currSkillPts.ToString(); 
        }

        void On_SkillBtnPressed()
        {           
            PopulateRightPerkBtns(lockUnLockPerk, currSkill);
        }

        public void On_OptionBtnPressed()
        {
            UIControlServiceCombat.Instance.ToggleUIStateScale(perkSelPanel, UITransformState.Open);

            PerkPanelSelected(perkSelPanel); 

            //PerkPanelSelected(skillPanel.gameObject); 

        }


        void PerkPanelSelected(GameObject _perkSelPanel)
        {
            CombatService.Instance.combatState = CombatState.INCombat_Pause;

            currChar = CombatService.Instance.currCharOnTurn.charModel.charName;
            lockUnLockPerk = _perkSelPanel.transform.GetChild(0).GetChild(0).GetChild(1);
            currSkill = SkillService.Instance.currSkillName;
            if (currSkill == SkillNames.None)
            {
                currSkill = SkillService.Instance.defaultSkillName;
            }
            lockUnLockPerk.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text  // HEADER
                                        = currSkill.ToString().CreateSpace();

            //charNameTxt.text = CombatService.Instance.currCharOnTurn    // left HEADER 
            //                            .charModel.charName.ToString().CreateSpace();
            foreach (SkillDataSO skillSO in SkillService.Instance.allCharSkillSO)
            {
                if (skillSO.charName == currChar)
                {
                    lockUnLockPerk.GetChild(1).GetComponentInChildren<Image>().sprite
                            = skillSO.allSkills?.Find(t => t.skillName
                                    == currSkill).skillIconSprite;
                }
            }

            PopulateLeftSkillBtns();
            PopulateRightPerkBtns(lockUnLockPerk, currSkill);
        }
        void PopulatePerkDescription()
        {

            // To be done.. 

        }

        void PopulateLeftSkillBtns()
        {
            SkillDataSO skillSO = SkillService.Instance.GetSkillSO(currChar);
            int i = 0, j = 0, k = 0, maxI = 4, maxJ = 4;
            foreach (SkillData skilldata in skillSO.allSkills)
            {                
                if(skilldata.skillType == SkillTypeCombat.Skill1 || skilldata.skillType == SkillTypeCombat.Skill2
                    || skilldata.skillType == SkillTypeCombat.Skill3||skilldata.skillType == SkillTypeCombat.Ulti)
                {
                    skillPanel.transform.GetChild(1).GetChild(i).gameObject.SetActive(true); 
                    skillPanel.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite =
                       skilldata.skillIconSprite;
                    i++; 
                }                
                else if(skilldata.skillType == SkillTypeCombat.Weapon || skilldata.skillType == SkillTypeCombat.Patience
                     || skilldata.skillType == SkillTypeCombat.Move )
                {
                    weaponPanel.transform.GetChild(1).GetChild(j).gameObject.SetActive(true);
                    weaponPanel.transform.GetChild(1).GetChild(j).GetComponent<Image>().sprite =
                      skilldata.skillIconSprite;
                    j++;

                }
                //else if (skilldata.skillType == SkillTypeCombat.Uzu)
                //{
                //    weaponPanel.transform.GetChild(1).GetChild(k).GetComponent<Image>().sprite =
                //      skilldata.SkillSprite;
                //}
            }


            for (int m = 0; m < 4; m++)
            {
                //Debug.Log(m);
                rightSkillBtns[m] = skillPanel.transform.GetChild(1).GetChild(m).GetComponent<Button>();
                rightSkillBtns[m].onClick.AddListener(On_SkillBtnPressed);
            }
            ClearExtra(skillPanel.transform.GetChild(1), maxI, i);
            ClearExtra(weaponPanel.transform.GetChild(1), maxJ, j);
        }

        //HELPER 
        void ClearExtra(Transform UIParent, int max, int currVal )
        {
            if (currVal <= max)
            {
                for (int l = currVal; l < max; l++)
                    UIParent.GetChild(l).gameObject.SetActive(false);
            }
        }

        void PopulateRightPerkBtns(Transform _lockUnlockPanel, SkillNames _skillNames)
        {
            // FILL TEXT IN BTNS 
            List<PerkNames> allPerksInChar = new List<PerkNames>();

            maxSkillLvl = SkillLvl.Level0;
            allPerkInSkill.Clear();
            allPerkInSkill = SkillService.Instance.GetAllPerkdata(_skillNames);
            //allPerkInSkill.ForEach(t => Debug.Log("PERK" + t.perkName)); 
            for (int k = 0; k < _lockUnlockPanel.GetChild(2).childCount; k++)
            {
                _lockUnlockPanel.GetChild(2).GetChild(k).gameObject.SetActive(true);
            }
            perkBtns = _lockUnlockPanel.GetChild(2).GetComponentsInChildren<Button>();
           // allPerkInSkill.ForEach(t => Debug.Log("perks name " + t.perkName)); 
            int i = 0;
            for (i = 0; i < allPerkInSkill.Count; i++)
            {
               // Debug.Log("Value of " + i); 
                perkBtns[i].gameObject.SetActive(true);
                perkBtns[i].onClick.AddListener(On_PerkPressed);

                perkBtns[i].GetComponentInChildren<TextMeshProUGUI>().text 
                                = allPerkInSkill[i].perkName.ToString().CreateSpace();
                UIControlServiceCombat.Instance.TogglePerkSelectState(UIElementType.PerkButton,
                                         perkBtns[i].gameObject, allPerkInSkill[i].state);
            }

            for (int j = i; j < perkBtns.Length; j++)
            {
                perkBtns[j].gameObject.SetActive(false);
            }

            UpdateSkillTxt();
        }
        void On_PerkPressed()
        {
            //Debug.Log(" button " + EventSystem.current.currentSelectedGameObject.name);
            
            GameObject btn = EventSystem.current.currentSelectedGameObject;
            int index = btn.transform.GetSiblingIndex();
             if (allPerkInSkill[index].state == PerkSelectState.UnClickable
               || allPerkInSkill[index].state == PerkSelectState.Clicked) return;
         
            currSelectedPerk = allPerkInSkill[index];
            if (CanPerkBeClicked())
            {
                SkillService.Instance.PerkUnLock(allPerkInSkill[index].perkName, btn);
                UpdateSkillTxt();
                ChangeOtherState();
                ChangeAllBtnSelectionState();
            }                    
        }

        bool CanPerkBeClicked()
        {
            foreach (PerkModelData skillnPerk in allPerkInSkill)
            {
                if (skillnPerk.perkLvl > maxSkillLvl && skillnPerk.state == PerkSelectState.Clicked)
                {
                    maxSkillLvl = skillnPerk.perkLvl; 
                }
            }
            if (currSelectedPerk.perkLvl == maxSkillLvl+1)
                return true;
            else
                return false;
        }


        void ChangeAllBtnSelectionState()
        {           
            int i = 0;
            for (i = 0; i < allPerkInSkill.Count; i++)
            {

                UIControlServiceCombat.Instance.TogglePerkSelectState(UIElementType.PerkButton,
                    perkBtns[i].gameObject, allPerkInSkill[i].state);
            }
        }

        void ChangeOtherState()
        {
            // other state in the same lvl is  unclickable
            //if there is on
            // +1 lvl state that does not have this perk as pre req is unclickable
            //+2 lvl state that does have this state is cliable other is unclickable
            // CLICKED LIST TO BE CREATED ..................:D
            PerkModelData SameLvlPerk = allPerkInSkill.Find(t => t.perkLvl == currSelectedPerk.perkLvl && t != currSelectedPerk);
            SkillService.Instance.SetPerkState(SameLvlPerk.perkName, PerkSelectState.UnClickable);
            List<PerkModelData> clickedList = allPerkInSkill.Where(t => t.state == PerkSelectState.Clicked).ToList();
            clickedList.ForEach(t => Debug.Log("Clicked perk" + t.perkName)); 
            if (currSelectedPerk.perkLvl + 1 <= SkillLvl.Level3)
            {
                List<PerkModelData> clickablePerks = new List<PerkModelData>(); 
                List<PerkModelData> OneLvlUpPerks = allPerkInSkill.Where(t => t.perkLvl == currSelectedPerk.perkLvl + 1).ToList();
                foreach (PerkModelData oneLvlUp in OneLvlUpPerks)
                {
                    foreach (PerkNames perkName in oneLvlUp.preReqList)
                    {
                        if (perkName == PerkNames.None)
                        {
                            clickablePerks.Add(oneLvlUp);
                            break;
                        }
                        foreach (PerkModelData clickedData in clickedList)
                        {
                           if( clickedData.perkName == perkName)
                           {
                                clickablePerks.Add(oneLvlUp);           
                           }
                        }
                    }
                }  

                clickablePerks.ForEach(t => t.state = PerkSelectState.Clickable);

                List<PerkModelData> UnclickablePerks = OneLvlUpPerks.Where(t => !clickablePerks
                                    .Any(p => p.perkName == t.perkName)).ToList();

                UnclickablePerks.ForEach(t => t.state = PerkSelectState.UnClickable);

                if (currSelectedPerk.perkLvl + 2 <= SkillLvl.Level3)
                {
                    // chk for unclicable perks to be 

                    List<PerkModelData> twoLvlUpPerks = allPerkInSkill.Where(t => t.perkLvl == currSelectedPerk.perkLvl + 2).ToList();
                    List<PerkModelData> lvl2ClickablePerks = new List<PerkModelData>(); 
                    foreach (PerkModelData clickablePerk in clickablePerks)
                    {
                        List<PerkModelData> tempData = new List<PerkModelData>(); 
                         tempData = twoLvlUpPerks.Where(t => t.preReqList.Contains(clickablePerk.perkName) 
                                                || t.preReqList.Contains(PerkNames.None)|| t.preReqList.Contains(currSelectedPerk.perkName)).ToList();

                      //  tempData.ForEach(t => Debug.Log("Hll" + t.perkName)); 
                        lvl2ClickablePerks.AddRange(tempData); 
                    }              
                    lvl2ClickablePerks.ForEach(t => t.state = PerkSelectState.Clickable);

                    List<PerkModelData> Lvl2UnclickablePerks = twoLvlUpPerks.Where(t => !lvl2ClickablePerks
                                        .Any(p => p.perkName == t.perkName)).ToList();

                    Lvl2UnclickablePerks.ForEach(t => t.state = PerkSelectState.UnClickable);
                }

            }
        }       

        void UpdateSkillTxt()
        {
            lockUnLockPerk.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text  // HEADER
                                     = allPerkInSkill.Where(t => t.state == PerkSelectState.Clicked)
                                     .ToList().Count.ToString(); 


        }

        public void Load()
        {
           
        }

        public void UnLoad()
        {
            
        }

        public void Init()
        {
            //perkSelPanel = gameObject;
            ////= GameObject.FindGameObjectWithTag("PerkSelectionPanel");
            //allPerkInSkill = new List<PerkModelData>();
            //displaySubPanel = perkSelPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1);
            //skillPanel = displaySubPanel.GetChild(0);
            //weaponPanel = displaySubPanel.GetChild(1);
            //campingPanel = displaySubPanel.GetChild(2);
            //skillPtsDisplay = displaySubPanel.GetChild(3);
            //skilltxt = skillPtsDisplay.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            //rightSkillBtns = new Button[4];
        }
    }
}





// clickablePerks.ForEach(t => Debug.Log("CLICKABLEs" + t.perkName));
// List<SkillPerkData> clickablePerks = OneLvlUpPerks.Where(t => t.preReqList
//.Contains(currSelectedPerk.perkName) || t.preReqList
//.Contains(PerkNames.None) || t.preReqList
//.Contains(prevPerk.perkName)).ToList();
