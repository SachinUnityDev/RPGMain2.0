using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class LevelService : MonoSingletonGeneric<LevelService>
    {

        /// <summary>
        /// move to ally and enemy controller
        /// lvl Model allies to record and hold data in the proper format
        /// 
        /// 
        /// </summary>


        public LevelUpSO levelUpSO;
        public LvlUpCompSO lvlUpCompSO;
        public LvlNExpSO lvlNExpSO;

        public LevelModel lvlModel; 


        [Header("Global var")]
        [SerializeField] CharModel charModel;
        [SerializeField] CharController charController;
        private void Start()
        {
            
        }

        public void Init()
        {
            if (lvlModel == null)
                lvlModel = new LevelModel(); 
        }

        public void LevelUpInitAlly(CharController charController)  // ALLY
        {
            charModel = charController.charModel;
            this.charController = charController;

            CharacterSO charSO = CharService.Instance.GetCharSO(charModel);

            Levels initLvl = (Levels)charModel.charLvl;
            Levels finalLvl = (Levels)charSO.charLvl;
            if (charModel.orgCharMode == CharMode.Ally)
            {
                AutoLvlUpAlly(initLvl, finalLvl);
            }
        }

        // to be called from the view controller
        public void ManLvlUp(CharNames charName, List<LvlData> optionChosen)
        {
            charController = CharService.Instance.GetCharCtrlWithName(charName);
            // apply to char Controller
            foreach (LvlData attrib in optionChosen)
            {
                charController.ChangeAttrib(CauseType.LevelUp, 1, 1, attrib.attribName
                    , attrib.val, true);
            }
            lvlModel.AddOptions2ChosenStack(charName, optionChosen, (Levels)charModel.charLvl);
        }

        // connect to event on exp
        public bool ChkLvlUp(CharModel charModel, int expNeeded)
        {
            int currExp = charModel.expPoints;
            if (currExp - expNeeded >= 0)
                return true;
            else
                return false; 
        }

        void AutoLvlUpAlly(Levels initLvl, Levels finalLvl)
        {
          
            int lvlDiff = finalLvl - initLvl;
            if ((lvlDiff) == 1)  // can be removed
            {
                AutoLvlUpByOne(finalLvl);
            }
            else
            {                
                for (int i = (int)initLvl; i <= (int)finalLvl; i++)
                {
                    AutoLvlUpByOne((Levels)(i + 1)); 
                }
            }
        }

        void AutoLvlUpByOne(Levels finalLvl)
        {
            ArcheType heroType = charModel.heroType;
            LvlDataComp lvlDataComp = lvlUpCompSO.GetLvlData(heroType, finalLvl);
            Add2ManPendingStack(finalLvl);

            //int expNeeded = lvlNExpSO.GetTotalExpPts4Lvl((int)finalLvl);
            //if (ChkLvlUp(charModel, expNeeded))
            
            foreach (LvlData stat in lvlDataComp.allStatDataAuto)
                {                 
                        charController.ChangeAttrib(CauseType.LevelUp, 1, 1, stat.attribName
                           , stat.val, true);                  
                }
           
            charModel.charLvl++; 
        }
        void Add2ManPendingStack(Levels finalLvl)
        {
            CharNames charName = charController.charModel.charName;
             LvlDataComp lvlDataComp = lvlUpCompSO.GetLvlData(charModel.heroType, finalLvl);
            List<LvlData> option1 = lvlDataComp.allStatDataOption1;
            List<LvlData> option2 = lvlDataComp.allStatDataOption2;

            lvlModel.AddOptions2PendingStack(charName, option1, option2, finalLvl);
        }


        













        #region BEASTIARY 
        public void LevelUpInitBeastiary(CharController charController)
        {
            charModel = charController.charModel;
            this.charController = charController;
            CharacterSO charSO = BestiaryService.Instance.GetEnemySO(charController.charModel.charName);

            Levels initLvl = (Levels)charModel.charLvl;
            Levels finalLvl = (Levels)charSO.charLvl;
            AutoLvlUpEnemies(initLvl, finalLvl);  // enemies should not level up untill spawned
        }


        void AutoLvlUpEnemies(Levels initLvl, Levels finalLvl)
        {
           
            for (int i = (int)initLvl; i <= (int)finalLvl; i++)
            {
                AutoLvlUpByOneEnemy((Levels)(i + 1));
            }
        }
   
        void AutoLvlUpByOneEnemy(Levels finalLvl)
        {
            if (charModel == null)
            {
                Debug.Log("CharModel found null " + finalLvl);
                return;
            }
            
            List<AttribData> lvlUpStats = levelUpSO.GetLvlUpIncr4Stats(charModel, finalLvl);
           if(lvlUpStats == null)
            {
                Debug.Log("charName " + charModel.charName);
                Debug.Log("Lvl up stat error" + lvlUpStats.Count);
                return;
            }
            foreach (AttribData attrib in lvlUpStats)
            {               
                charController.ChangeAttrib(CauseType.LevelUp, 1, 1, attrib.AttribName
                    , attrib.currValue, false);
            }
            charModel.charLvl++;

        }
        #endregion




    
      
    }


}

