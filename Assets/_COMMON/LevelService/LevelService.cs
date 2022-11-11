using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class LevelService : MonoSingletonGeneric<LevelService>
    {

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

        public void LevelUpInit(CharController charController)
        {
            charModel = charController.charModel;
            this.charController = charController;
            
           CharacterSO charSO = CharService.Instance.GetCharSO(charModel);
            if (charSO == null)
                charSO = BestiaryService.Instance.GetEnemySO(charController.charModel.charName);
            
            Levels initLvl = (Levels)charModel.charLvl;
            Levels finalLvl = (Levels)charSO.charLvl;
            if (charModel.orgCharMode == CharMode.Ally)
            {
                AutoLvlUpAlly(initLvl, finalLvl);                
            }
            else
            {
                AutoLvlUpEnemies(initLvl, finalLvl);
            }
        }
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
            HeroType heroType = charModel.heroType;
            LvlDataComp lvlDataComp = lvlUpCompSO.GetLvlData(heroType, finalLvl);
            Add2ManPendingStack(finalLvl);

            //int expNeeded = lvlNExpSO.GetTotalExpPts4Lvl((int)finalLvl);
            //if (ChkLvlUp(charModel, expNeeded))
            //{
            foreach (StatData stat in lvlDataComp.allStatDataAuto)
                {
                    if (stat.statsName == StatsName.damage || stat.statsName == StatsName.armor)
                    {
                        charController.ChangeStatRange(CauseType.LevelUp, 1, 1, stat.statsName
                            , stat.minRange, stat.maxRange, true);
                        // stack it up in level up model
                    }
                    else
                    {
                        charController.ChangeStat(CauseType.LevelUp, 1, 1, stat.statsName
                           , stat.currValue, true);
                    }

                }
           // }
            charModel.charLvl++; 
           
        }
        void Add2ManPendingStack(Levels finalLvl)
        {
            CharNames charName = charController.charModel.charName;
             LvlDataComp lvlDataComp = lvlUpCompSO.GetLvlData(charModel.heroType, finalLvl);
            List<StatData> option1 = lvlDataComp.allStatDataOption1;
            List<StatData> option2 = lvlDataComp.allStatDataOption2;

            lvlModel.AddOptions2PendingStack(charName, option1, option2, finalLvl);
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
            
            List<StatData> lvlUpStats = levelUpSO.GetLvlUpIncr4Stats(charModel, finalLvl);
           if(lvlUpStats == null)
            {
                Debug.Log("charName " + charModel.charName);
                Debug.Log("Lvl up stat error" + lvlUpStats.Count);
                return;
            }
                
            
            
         
            foreach (StatData stat in lvlUpStats)
            {
                if (stat.statsName == StatsName.damage || stat.statsName == StatsName.armor)
                {
                    charController.ChangeStatRange(CauseType.LevelUp, 1, 1, stat.statsName
                        , stat.minRange, stat.maxRange, false);
                    // stack it up in level up model
                }
                else
                {
                    charController.ChangeStatRange(CauseType.LevelUp, 1, 1, stat.statsName
                        , stat.minRange, stat.maxRange, false);
                }
            }
            charModel.charLvl++;

        }


        // to be called from the view controller
        public void ManLvlUp(CharNames charName, List<StatData> optionChosen)
        {
            charController = CharService.Instance.GetCharCtrlWithName(charName);
            // apply to char Controller
            foreach (StatData stat in optionChosen)
            {
                if (stat.statsName == StatsName.damage || stat.statsName == StatsName.armor)
                {
                    charController.ChangeStatRange(CauseType.LevelUp, 1, 1, stat.statsName
                        , stat.minRange, stat.maxRange, true);
                    // stack it up in level up model
                }
                else
                {
                    charController.ChangeStat(CauseType.LevelUp, 1, 1, stat.statsName
                       , stat.currValue, true);
                }
            }
            lvlModel.AddOptions2ChosenStack(charName, optionChosen, (Levels)charModel.charLvl); 
        }

    
      
    }


}

