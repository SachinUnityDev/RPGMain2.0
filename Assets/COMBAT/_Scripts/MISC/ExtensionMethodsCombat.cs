using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System.Security.Policy;
using Quest;

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

        public static string AttribStrName(this AttribName statName)
        {
            string str = ""; 
            switch (statName)
            {        
                case AttribName.damage:
                    str = "Damage";
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
                case AttribName.armor:
                    str = "Armor";
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
        public static int GetChanceFrmList(this List<float> chances)
        {
            int val = UnityEngine.Random.Range(0, 100);
            chances.Insert(0, 0f);
            for (int i = 1; i < chances.Count; i++)
            {
                if (val > chances[i - 1] && val <= chances[i])
                    return i - 1; 
            }
            return 0; 
        }


        public static bool IsNodeTimeDataMatch(this NodeTimeData nodeTimeData1, NodeTimeData nodeTimeData2)
        {
            if(nodeTimeData1.nodeData.nodeType == nodeTimeData2.nodeData.nodeType 
               && nodeTimeData1.nodeData.questName == nodeTimeData2.nodeData.questName
               && nodeTimeData1.nodeData.locName == nodeTimeData2.nodeData.locName
               )
             return true;
            else
             return false; 
        }

        public static bool IsNodeDataMatch(this NodeData nodeData1, NodeData nodeData2)
        {
            if (nodeData1.nodeType == nodeData2.nodeType
               && nodeData1.questName == nodeData2.questName
               && nodeData1.locName == nodeData2.locName
               )
                return true;
            else
                return false;
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

