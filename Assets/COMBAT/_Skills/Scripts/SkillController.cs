using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Reflection;
using System.Linq;
using System;
using DG.Tweening;

namespace Combat
{
    //[Serializable]
    //public class SkillAIPercent
    //{
    //    SkillNames



    //}


    public class SkillController : MonoBehaviour   // mananges Skill for one character
    {
        [Header("CHARACTER RELATED")]        
        [SerializeField] CharMode charMode;
        CharController charController;
        public CharNames charName;
        public int charID;  


        public int currSkillID;
        // SKILLS
        public List<SkillNames> allSkillInChar = new List<SkillNames>();        
        public List<SkillNames> unLockedSkills = new List<SkillNames>();

        public List<SkillModel> allSkillModels = new List<SkillModel>();

        public List<SkillBase> allSkillBases;

        public List<PerkBase> allPerkBases = new List<PerkBase>();

        public List<SkillModel> ClickableSkills = new List<SkillModel>(); 

        public SkillDataSO SkillDataSO;
        public int skillID = 0; 
        SkillNames mySkillName;
        // PERKS 

        void Start()
        {
            // on Start of Combat
            allSkillBases = new List<SkillBase>();
          
            charController = gameObject.GetComponent<CharController>();
            charName = charController.charModel.charName; 
            charMode = charController.charModel.charMode;
            charID = charController.charModel.charID;
            CharService.Instance.OnCharAddedToParty += PopulateSkillList1; 
            //PopulateSkillList1();
            PopulatePerkList();
            CombatEventService.Instance.OnSOR += CheckNUpdateSkillState;
            CombatEventService.Instance.OnEOR += PopulatePerkList;

        }

        public void UpdatePerkState(PerkNames _perkName, PerkSelectState _state)
        {
            foreach (var perkbase in allPerkBases)
            {
                if (perkbase.perkName == _perkName)
                {
                    perkbase.state = _state; 
                }
            }
        }

        #region Skill and Perk Init

        public void PopulateSkillList1(CharNames charName)
        {
            SkillDataSO = SkillService.Instance.GetSkillSO(charName);
            foreach (SkillData skill in SkillDataSO.allSkills)
            {
                allSkillInChar.Add(skill.skillName);

                if (skill.skillUnLockStatus == 1) // 1 = unlock, 0 locked, -1 NA
                {
                    unLockedSkills.Add(skill.skillName);
                }
            }
            foreach (var skillSO in SkillDataSO.allSkills)
            {
                SkillBase skillbase = SkillService.Instance.skillFactory.GetSkill(skillSO.skillName);

                skillbase.charName = SkillDataSO.charName;
                mySkillName = skillbase.skillName;// redundant stmt
                                                  // allSkillBases.ForEach(t => Debug.Log("SKILLBASES INIT" + t.skillName));
                allSkillBases.Add(skillbase);
                skillID++;  // could use random generation here 
                skillbase.SkillInit(this); // pass in all the params when all skills are coded

            }
        }
  
        public void PopulatePerkList()
        {
            foreach (SkillNames _skillName in unLockedSkills)
            {
               
                List<PerkModelData> skillPerkData = SkillService.Instance.allSkillPerksData
                                                    .Where(t => t.skillName == _skillName).ToList();
             
                foreach (PerkModelData perkData in skillPerkData)
                {                  
                    var P1 = Activator.CreateInstance(perkData.perkBase) as PerkBase;
                    P1.state = SkillService.Instance.allSkillPerksData.Find(t => t.perkName == P1.perkName).state;
                   // Debug.Log("PERK BASE ADDED .... " + P1.charName);
                    allPerkBases.Add(P1);
                    P1.SkillInit(this); 
                }
            }      

        }
        #endregion

        public void SkillHovered(SkillNames _skillName)
        {
            SkillServiceView.Instance.skillCardData.perkChain.Clear();
            SkillServiceView.Instance.skillCardData.descLines.Clear();
            
            allSkillBases.Find(t => t.skillName == _skillName).SkillHovered();

            List<PerkModelData> clickedPerkList = SkillService.Instance.allSkillPerksData
                .Where(t => t.skillName == _skillName && t.state == PerkSelectState.Clicked).ToList();

            clickedPerkList.ForEach(t => SkillServiceView.Instance.skillCardData.perkChain.Add(t.perkType));
           
            clickedPerkList.ForEach(t => allPerkBases.Find(x => x.perkName == t.perkName).SkillInit(this));
            clickedPerkList.ForEach(t => allPerkBases.Find(x => x.perkName == t.perkName).SkillHovered()); 
        }

   
        public void SkillSelect(SkillNames _skillName)
        {
            allSkillBases.Find(t => t.skillName == _skillName).SkillSelected();
          
            List<PerkModelData> clickedPerkList = SkillService.Instance.allSkillPerksData
                .Where(t => t.skillName == _skillName && t.state == PerkSelectState.Clicked).ToList();

           // clickedPerkList.ForEach(t => Debug.Log("CLCIKED PERK " + t.perkName));

            foreach (var skillPerkdata in SkillService.Instance.allSkillPerksData)
            {
                if ((skillPerkdata.skillName == _skillName) && (skillPerkdata.state == PerkSelectState.Clicked))
                {
                    foreach (var perkbase in allPerkBases)
                    {
                        if(perkbase.perkName == skillPerkdata.perkName)
                        {
                            clickedPerkList.ForEach(t => Debug.Log(t.perkName + "PERK BASE CLICKED"));
                           // perkbase.SkillInit(this);
                            perkbase.SkillSelected();                             
                        }
                    }
                }
            }      

        }    

      

        public void CheckNUpdateSkillState()
        {
           
            //cd .. 0 cast this even this round double strike
            //cd... 1 cast this only next round .... 
            //cd .. so on and so forth

            int cdGap = 0;
            foreach (var skill in allSkillBases)
            {
                cdGap = CombatService.Instance.currentRound - skill.skillModel.lastUsedInRound;
                //if (cdGap < 0)
                //{
                //  //  Debug.Log("Cool Calc Error");
                //}
                if (cdGap < skill.skillModel.cd)
                {
                    skill.skillModel.SetSkillState(SkillSelectState.UnClickable_InCd);                    
                   // Debug.Log("SkillUsed" + cdGap);
                }
                else
                {
                    //SkillServiceView.Instance.UpdateSkillIconTxt(skill.skillName, cdGap);
                    skill.skillModel.SetSkillState(SkillSelectState.Clickable);

                }
            }
        }

        public void StartAISkillInController()
        {
            SkillModel selectedSkillModel = SkillSelectByAI();
            if(selectedSkillModel != null)
            {
              
                SkillService.Instance.currSkillName = selectedSkillModel.skillName;
                //SkillService.Instance.On_SkillSelected(selectedSkillModel.charName);// dependencies skillName

                // fixes skill select call  and currSKill controller to skill Service
                allSkillBases.Find(t => t.skillName == selectedSkillModel.skillName).SkillSelected();
               // SkillSelect?.Invoke(selectedSkillModel.skillName);  // message broadcaster 

                Debug.Log("SELECTED SKILLS" + selectedSkillModel.skillName);
            
                allSkillBases.Find(t => t.skillName == selectedSkillModel.skillName).PopulateAITarget();
                // Set the target ..i.e currTargetDyna .. etc 
                Debug.Log("TARGETS" + SkillService.Instance.currentTargetDyna.charGO.name); 
                SkillService.Instance.OnAITargetSelected(selectedSkillModel);
            }
            else
            {
                  SkillService.Instance.On_PostSkill();
            }
        }
        public SkillModel SkillSelectByAI()
        {
            float netBaseWt = 0f; ClickableSkills.Clear(); 
            foreach (SkillNames skillName in unLockedSkills)
            {
                SkillModel skillModel = allSkillModels.Find(t => t.skillName == skillName);
                skillModel.SetSkillState( SkillServiceView.Instance.UpdateSkillState(skillModel));
                if (skillModel.GetSkillState() == SkillSelectState.Clickable)
                {
                    Debug.Log("SKILL MODEL" + skillModel.skillName);
                    netBaseWt += skillModel.baseWeight;
                    ClickableSkills.Add(skillModel);
                }
            }
            if (ClickableSkills.Count < 1) return null;
            //int index = UnityEngine.Random.Range(0, ClickableSkills.Count);
            for (int i = 0; i < ClickableSkills.Count; i++)
            {
                if (GetSkillModelByBaseWtChance(netBaseWt, i))
                    return ClickableSkills[i]; 
            }
            int random = UnityEngine.Random.Range(0, ClickableSkills.Count); 
            return ClickableSkills[random];// to remove error 
        }

        bool GetSkillModelByBaseWtChance(float NetWt, int i)
        {
            float skillchance = (ClickableSkills[i].baseWeight / NetWt)*100f;

            return skillchance.GetChance(); 
        }

    }

}

//allSkillBases.Find(t => t.skillName == _skillName).PopulateTargetPos();  


//SkillModel skillmodel = allSkillModels.Find(t => t.skillName == _skillName);

//if(skillmodel.GetSkillState() == SkillSelectState.Clickable)
//    skillmodel.SetSkillState(SkillSelectState.Clicked);
//else
//{
//    skillmodel.SetSkillState(SkillSelectState.Clicked);return; 
//}

// SkillServiceView.Instance.UpdateSkillState(skillmodel);


// 15, 40 , 5, 1,1 =62   15/62*100
//public void PopulateSkillList()
//{          

//     SkillDataSO = SkillService.Instance.GetSkillSO(charName);
//    foreach (SkillData skill in SkillDataSO.allSkills)
//    {

//       allSkillInChar.Add(skill.skillName);

//        if (skill.skillUnLockStatus == 1) // 1 = unlock, 0 locked, -1 NA
//        {
//            unLockedSkills.Add(skill.skillName);
//        }
//    }
//    foreach (var skillSO in SkillDataSO.allSkills)
//    {

//        SkillBase skillbase = SkillService.Instance.skillFactory.GetSkill(skillSO.skillName);

//       // SkillBase mySkillBase = Activator.CreateInstance(skills.Value) as SkillBase;


//        //foreach (var skills in SkillService.Instance.skillFactory)
//        //{
//        // Pass in the default move and patience bases here or its replacement here.. 
//        // yea use skill type here rather than name
//       // if (skills.Key == skillSO.skillName)
//            {   //HAVE FAITH THIS IS WORKING

//            // if skillName is default move or defaultPatience
//            // set the charName in new methods 
//            //if (skills.Key == SkillNames.DefaultMove || skills.Key == SkillNames.DefaultPatience)
//            // {
//            // SkillBase mySkillBase = Activator.CreateInstance(skills.Value) as SkillBase;
//            // }
//            // CURRENTLY SETTING THE CHAR NAME FOR ALL THE SKILLS IN SKILL BASES 
//            skillbase.charName = SkillDataSO.charName;
//                mySkillName = skillbase.skillName;// redundant stmt
//               // allSkillBases.ForEach(t => Debug.Log("SKILLBASES INIT" + t.skillName));
//                allSkillBases.Add(skillbase);
//                skillID++;
//            skillbase.SkillInit(this); // pass in all the params when all skills are coded
//         //   }                   
//       // }
//    }      
//}
