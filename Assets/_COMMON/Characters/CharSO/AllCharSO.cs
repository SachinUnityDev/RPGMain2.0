using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Town;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using static DG.Tweening.DOTweenModuleUtils;
using Interactables;
using NUnit.Framework;
using System;

namespace Common
{
    [CreateAssetMenu(fileName = "AllCharSO", menuName = "Common/AllCharSO")]

    public class AllCharSO : ScriptableObject
    {
        public List<CharacterSO> allAllySO = new List<CharacterSO>(); 
        public List<CharacterSO> allEnemySO= new List<CharacterSO>();
        public List<CharacterSO> charList = new List<CharacterSO>();
      
        [Header("Hex portraits BG")]
        public Sprite hexPortBg;

        [Header("Background Portrait")]
        public Sprite bgPortClicked;
        public Sprite bgPortUnClicked;
        public Sprite bgPortUnAvail;

        public List<ArchetypeData> allArchetypeData = new List<ArchetypeData>();
        public List<AbbasClasstypeData> allAbbasClasstypeData = new List<AbbasClasstypeData>();
        private void Awake()
        {
            charList.Clear();
            charList.AddRange(allAllySO);
            charList.AddRange(allEnemySO);
            FillCommonDetails(); 
        }

        public AbbasClasstypeData GetAbbasClasstypeData(ClassType classType)
        {
            int index = allAbbasClasstypeData.FindIndex(t => t.classType == classType);
            if (index != -1)
            {
                return allAbbasClasstypeData[index];
            }
            return null; 
        }

        public ArchetypeData GetArchTypeData(Archetype archeType)
        {
            int index = allArchetypeData.FindIndex(t=>t.archetype== archeType);
            if(index != -1)
            {
                return allArchetypeData[index];
            }
            return null; 
        }
        public CharacterSO GetAllySO(CharNames charName)
        {
            int index = allAllySO.FindIndex(t => t.charName == charName);
            if (index != -1)
            {
                return allAllySO[index];
            }
            else
            {
                Debug.Log("Char SO not found" + charName);
                return null;
            }
        }

        public CharacterSO GetEnemySO(CharNames charName)
        {
            int index = allEnemySO.FindIndex(t => t.charName == charName);
            if (index != -1)
            {
                return allEnemySO[index];
            }
            else
            {
                Debug.Log("Char SO not found" + charName);
                return null;
            }
        }
        public CharacterSO GetCharSO(CharNames charName)
        {
            int index = charList.FindIndex(t=>t.charName == charName);
            if (index != -1)
            {
                return charList[index];
            }
            else
            {
                Debug.Log("Char SO not found" + charName);
                return null;
            }
        }

   
        public string GetDesc(StatName statName)
        {
            string str = "";
            switch (statName)
            {
                case StatName.None:
                    str = ""; 
                    break;
                case StatName.health:
                    str = "Health is health, you know it.";
                    break;
                case StatName.stamina:
                    str = "Fuel for using skills.";
                    break;
                case StatName.fortitude:
                    str = "Value that shows your fear or faith.";
                    break;
                case StatName.hunger:
                    str = "When full, get Starving status.";
                    break;
                case StatName.thirst:
                    str = "When full, get Unslakable status"; 
                    break;
                default:
                    break;
            }
            return str;
        }
        public string GetDesc(AttribName attribName)
        {
            string str = " ";

            switch (attribName)
            {
                case AttribName.None:
                    str = ""; 
                    break;
                case AttribName.dmgMin:
                    str ="Min value for Physical and Magical attacks."; 
                    break;
                case AttribName.acc:
                    str = "Chance to land Physical attack to a target.";
                    break;
                case AttribName.focus:
                    str = "Determinant value for hitting correct target by Magical attacks or Misfire."; 
                    break;
                case AttribName.luck:
                    str = "Determinant value for Critical and Feeble hits."; 
                    break;
                case AttribName.morale:
                    str = "Determinant value for gain or lose AP."; 
                    break;
                case AttribName.haste:
                    str = "Main determinant for turn order. Increases chance to gain AP by using Move skills."; 
                    break;
                case AttribName.vigor:
                    str = "Determinant value for base Health. Increases chance to withstand Hunger."; 
                    break;
                case AttribName.willpower:
                    str = "Determinant value for base Stamina. Increases chance to withstand Thirst."; 
                    break;
                case AttribName.armorMin:
                    str = "Min value to mitigate incoming Physical damage."; 
                    break;
                case AttribName.dodge:
                    str = "Chance to evade a Physical attack."; 
                    break;
                case AttribName.fireRes:
                    str = "Percentage value to mitigate incoming Fire damage."; 
                    break;
                case AttribName.earthRes:
                    str = "Percentage value to mitigate incoming Earth damage."; 
                    break;
                case AttribName.waterRes:
                    str = "Percentage value to mitigate incoming Water damage.";
                    break;
                case AttribName.airRes:
                    str = "Percentage value to mitigate incoming Air damage.";
                    break;
                case AttribName.lightRes:
                    str = "Percentage value to mitigate incoming Light damage.";
                    break;
                case AttribName.darkRes:
                    str = "Percentage value to mitigate incoming Dark damage.";
                    break;
                case AttribName.staminaRegen:
                    str = "Value to gain Stamina per rd.";
                    break;
                case AttribName.hpRegen:
                    str = "Value to gain Health per rd.";
                    break;
                case AttribName.fortOrg:
                    str = "Value to reset Fortitude after each combat.";
                    break;
                case AttribName.armorMax:
                    str = "Max value to mitigate incoming Physical damage.";
                    break;
                case AttribName.dmgMax:
                    str = "Max value for Physical and Magical attacks.";
                    break;
                default:
                    break;
            }
            return str; 
        }

        float GetMinLimit(AttribName attribName)
        {
            float val = 0f; 
            switch (attribName)
            {
                case AttribName.None:
                    val = 0f; 
                    break;
                case AttribName.dmgMin:
                    val = 0f;
                    break;
                case AttribName.acc:
                    val = 0f;
                    break;
                case AttribName.focus:
                    val = 0f;
                    break;
                case AttribName.luck:
                    val = 0f;
                    break;
                case AttribName.morale:
                    val = 0f;
                    break;
                case AttribName.haste:
                    val = 0f;
                    break;
                case AttribName.vigor:
                    val = 0f;
                    break;
                case AttribName.willpower:
                    val = 0f;
                    break;
                case AttribName.armorMin:
                    val = 0f;
                    break;
                case AttribName.dodge:
                    val = 0f;
                    break;
                case AttribName.fireRes:
                    val = -30f;
                    break;
                case AttribName.earthRes:
                    val = -30f;
                    break;
                case AttribName.waterRes:
                    val = -30f;
                    break;
                case AttribName.airRes:
                    val = -30f;
                    break;
                case AttribName.lightRes:
                    val = -20f;
                    break;
                case AttribName.darkRes:
                    val = -20f;
                    break;
                case AttribName.staminaRegen:
                    val = 0f;
                    break;
                case AttribName.hpRegen:
                    val = 0f;
                    break;
                case AttribName.fortOrg:
                    val = -24f;
                    break;
                case AttribName.armorMax:
                    val = 0f;
                    break;
                case AttribName.dmgMax:
                    val = 0f;
                    break;
                default:
                    break;
            }
            return val; 

        }
        float GetMinLimit(StatName statName)
        {
            float val = 0f; 
            switch (statName)
            {
                case StatName.None:
                    break;
                case StatName.health:
                    val = 0f; 
                    break;
                case StatName.stamina:
                    val = 0f;
                    break;
                case StatName.fortitude:
                    val = -30f;
                    break;
                case StatName.hunger:
                    val = 0f;
                    break;
                case StatName.thirst:
                    val = 0f;
                    break;
                default:                   
                    break;
            }
            return val;

        }
        float GetMaxLimit(AttribName attribName) 
        {

            float val = 0f;
            switch (attribName)
            {
                case AttribName.None:
                    break;
                case AttribName.dmgMin:
                    val = 30f;break;
                case AttribName.acc:
                    val = 12f;break;
                case AttribName.focus:
                    val = 12f;break;
                case AttribName.luck:
                    val = 12f; break;
                case AttribName.morale:
                    val = 12f; break;
                case AttribName.haste:
                    val = 12f; break;
                case AttribName.vigor:
                    val = 100f; break;
                case AttribName.willpower:
                    val = 100f; break;
                case AttribName.armorMin:
                    val = 30f; break;
                case AttribName.dodge:
                    val = 12f;break;
                case AttribName.fireRes:
                    val = 80f; break;
                case AttribName.earthRes:
                    val = 80f; break;
                case AttribName.waterRes:
                    val = 80f; break;
                case AttribName.airRes:
                    val = 80f; break;
                case AttribName.lightRes:
                    val = 60f; break;
                case AttribName.darkRes:
                    val = 60f; break;
                case AttribName.staminaRegen:
                    val = 6f; break;
                case AttribName.hpRegen:
                    val = 6f; break;
                case AttribName.fortOrg:
                    val = 24f; break;
                case AttribName.armorMax:
                    val = 30f; break;
                case AttribName.dmgMax:
                    val = 30f; break;
                default:
                    break;
            }
            return val;
        }
        float GetMaxLimit(StatName statName)
        {
            float val = 0f; 
            switch (statName)
            {
                case StatName.None:
                    break;
                case StatName.health:
                    val = 400f;break;
                case StatName.stamina:
                    val = 300f;break;
                case StatName.fortitude:
                    val = 30f; break;
                case StatName.hunger:
                    val = 100f;break;
                case StatName.thirst:
                    val = 100f; break;
                default:
                    break;
            }
            return val;
        }

        void FillCommonDetails()
        {
            foreach (CharacterSO charSO in charList)
            {
                foreach (AttribData attribData in charSO.AttribList)
                {
                    attribData.desc = GetDesc(attribData.AttribName); 
                    attribData.minLimit = GetMinLimit(attribData.AttribName);
                    attribData.maxLimit = GetMaxLimit(attribData.AttribName);
                }
                foreach (StatData statData in charSO.statList)
                {
                    statData.desc = GetDesc(statData.statName);
                    statData.minLimit = GetMinLimit(statData.statName);
                    statData.maxLimit = GetMaxLimit(statData.statName);
                }
            }
        }
    }
    [Serializable]
    public class AbbasClasstypeData
    {
        public ClassType classType;
        public Sprite spriteN;        
    }

    [Serializable]
    public class ArchetypeData
    {
        public Archetype archetype;
        public Sprite spriteN;
        public Sprite spriteHL;
    }
}
