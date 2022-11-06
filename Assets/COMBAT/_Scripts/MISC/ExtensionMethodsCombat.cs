using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq; 


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
            return finalStr; 
                

        }

        public static string RealName(this StatsName statName)
        {
            string str = ""; 
            switch (statName)
            {
                case StatsName.None:
                    str = ""; 
                    break;
                case StatsName.health:
                    str = "Health"; 
                    break;
                case StatsName.stamina:
                    str = "Stamina"; 
                    break;
                case StatsName.fortitude:
                    str = "Fortitude"; 
                    break;
                case StatsName.hunger:
                    str = "Hunger";
                    break;
                case StatsName.thirst:
                    str = "Thirst";

                    break;
                case StatsName.damage:
                    str = "Damage";
                    break;
                case StatsName.acc:
                    str = "Accuracy";
                    break;
                case StatsName.focus:
                    str = "Focus";
                    break;
                case StatsName.luck:
                    str = "Luck";
                    break;
                case StatsName.morale:
                    str = "Morale";

                    break;
                case StatsName.haste:
                    str = "Haste";
                    break;
                case StatsName.vigor:
                    str = "Vigor";
                    break;
                case StatsName.willpower:
                    str = "Willpower";

                    break;
                case StatsName.armor:
                    str = "Armor";
                    break;
                case StatsName.dodge:
                    str = "Dodge";
                    break;
                case StatsName.fireRes:
                    str = "Fire Resistance";
                    break;
                case StatsName.earthRes:
                    str = "Earth Resistance";
                    break;
                case StatsName.waterRes:
                    str = "Water Resistance";
                    break;
                case StatsName.airRes:
                    str = "Air Resistance";
                    break;
                case StatsName.lightRes:
                    str = "Light Resistance";

                    break;
                case StatsName.darkRes:
                    str = "Dark Resistance";
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


        public static bool GetChance(this float percent)
        {
            int chance = UnityEngine.Random.Range(0, 100);
            if (chance < percent)
                return true;
            else return false; 

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




    }

