using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System.Security.Policy;
using Quest;
using Combat;

public static class ExtensionMethodsCombat
    {

        

        public static List<int> SplitTris(this int tris)
        {
            List<int> split = new List<int>();
            int temp = tris;           
               
            while (temp > 0)
            {
                split.Add(temp % 10);
                temp = temp / 10;
            }                 
            foreach (int item in split)
            {
                Debug.Log("split  " + item);
            }
            return split;         

        }
        public static string ToTitleCase(this string input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

    public static string CreateSpace(this string str)
        {
            // Converts "AbbasTheSkirmisher" to "Abbas The Skirmisher"    
            string[] split = Regex.Split(str, @"(?<!^)(?=[A-Z])");
            string finalStr = "";
            string mySpace = " "; 
            foreach (var str1 in split)
            {
                finalStr = string.Concat(finalStr,' ', str1); 
            }
            return finalStr.TrimStart(); 
                

        }

        public static string AttribStrName(this AttribName statName)
        {
            string str = ""; 
            switch (statName)
            {        
                case AttribName.dmgMin:
                    str = "Damage Min";
                    break;
                case AttribName.acc:
                    str = "Accuracy";
                    break;
                case AttribName.focus:
                    str = "Focus";
                    break;
                case AttribName.luck:
                    str = "Luck";
                    break;
                case AttribName.morale:
                    str = "Morale";
                    break;
                case AttribName.haste:
                    str = "Haste";
                    break;
                case AttribName.vigor:
                    str = "Vigor";
                    break;
                case AttribName.willpower:
                    str = "Willpower";
                    break;
                case AttribName.armorMin:
                    str = "Armor Min";
                    break;
                case AttribName.dodge:
                    str = "Dodge";
                    break;
                case AttribName.fireRes:
                    str = "Fire Resistance";
                    break;
                case AttribName.earthRes:
                    str = "Earth Resistance";
                    break;
                case AttribName.waterRes:
                    str = "Water Resistance";
                    break;
                case AttribName.airRes:
                    str = "Air Resistance";
                    break;
                case AttribName.lightRes:
                    str = "Light Resistance";

                    break;
                case AttribName.darkRes:
                    str = "Dark Resistance";
                    break;
                case AttribName.staminaRegen:
                    str = "Stamina Regen";
                    break;
                case AttribName.fortOrg:
                    str = "Fortitude Origin";
                    break;
                case AttribName.hpRegen:
                    str = "HP Regen";
                    break;
                case AttribName.armorMax:
                    str = "Armor Max";
                break;
                case AttribName.dmgMax:
                    str = "Damage Max";
                    break;
                default:
                    break;
            }
            return str; 

        }

        public static bool IsWithinRange(this int pos)
        {
            if (pos < 8 && pos > 0)
                return true;
            else return false; 
        }

        public static int Round2Int(this float num)
        {
            int i = Mathf.FloorToInt(num);  
            Debug.Log("number " + num % 1);  
            if (num%1 > 0.55f)
            {
                return i++;
            }
            else
            {
                return i; 
            }
        }
        public static int GetLayerOrder(this DynamicPosData dyna)
        {

            if (dyna.currentPos == 2 || dyna.currentPos == 5)// back lane 
            {
                return -3; 
            }
            else if (dyna.currentPos == 1 || dyna.currentPos == 4 || dyna.currentPos == 7) // mid Lane
            {
                return 0; 
            }
            else if(dyna.currentPos == 3 || dyna.currentPos == 6)
            {
                return 3; 
            }
            return 0;
        }

        public static bool GetChance(this float percent)
        {
            int chance = UnityEngine.Random.Range(0, 100);
            if (chance < percent)
                return true;
            else return false; 

        }
        public static int GetChanceFrmList(this List<float> chances)
        {
            chances.Insert(0, 0f);
            List<float> cumulativeChances = new List<float>();
            float cumChance = 0f;     
            foreach (float chance in chances) 
            { 
                cumChance+= chance;
                cumulativeChances.Add(cumChance);  
            }

        float val = UnityEngine.Random.Range(0F, cumulativeChances[cumulativeChances.Count-1]);

        //cumulativeChances.Insert(cumulativeChances.Count, 100);
        for (int i = 0; i < cumulativeChances.Count-1 ; i++)
            {
                if (val > cumulativeChances[i] && val <= cumulativeChances[i+1])
                    return i--; 
            }
            return 0; 
        }
  
        public static CharMode FlipCharMode(this CharMode _charMode)
        {
            if (_charMode == CharMode.Ally)
            {
                return CharMode.Enemy; 
            }
            if(_charMode == CharMode.Enemy)
            {
                return CharMode.Ally; 
            }
            return CharMode.None; 
        }

        public static AttribName GetCounterAttrib(this AttribName attribName)
        {
            if (attribName == AttribName.dmgMin)
                return AttribName.dmgMax;
            if (attribName == AttribName.dmgMax)
                return AttribName.dmgMin;
            if (attribName == AttribName.armorMax)
                return AttribName.armorMin;
            if (attribName == AttribName.armorMin)
                return AttribName.armorMax;

            return AttribName.None;
        }
    public static bool IsAttribDamage(this AttribName attribName)
    {
        if (attribName == AttribName.dmgMin)
            return true;
        if (attribName == AttribName.dmgMax)
            return true;


        return false;
    }
    public static bool IsAttribArmor(this AttribName attribName)
    {
        if (attribName == AttribName.armorMin)
            return true;
        if (attribName == AttribName.armorMax)
            return true;


        return false;
    }
}



