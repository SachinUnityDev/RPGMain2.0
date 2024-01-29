using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; 

namespace Combat
{
    public static class SkillTxtFilter 
    {
       
        public static string SkillFilter(this string skillDescStr)
        {
            string result = skillDescStr; 
            string replace = @"<style=Heal>Heal</style>";
            string pattern = @"\bHeal\b";
            result = Regex.Replace(result, pattern, replace);

           
            string replace1 = @"<style=Physical>Physical</style>";
            string pattern1 = @"\bPhysical\b";
            result = Regex.Replace(result, pattern1, replace1);

            
            string replace2 = @"<style=Bleed>Bleed</style>";
            string pattern2 = @"\bBleed\b";
            result = Regex.Replace(result, pattern2, replace2);

            string replace3 = @"<style=Burn>Burn</style>";
            string pattern3 = @"\bBurn\b";
            result = Regex.Replace(result, pattern3, replace3);

            string replace4 = @"<style=Fire>Fire</style>";
            string pattern4 = @"\bFire\b";
            result = Regex.Replace(result, pattern4, replace4);

            string replace5 = @"<style=Poison>Poison</style>";
            string pattern5 = @"\bPoison\b";
            result = Regex.Replace(result, pattern5, replace5);

            string replace6 = @"<style=Earth>Earth</style>";
            string pattern6 = @"\bEarth\b";
            result = Regex.Replace(result, pattern6, replace6);

            string replace7 = @"<style=States>States</style>";
            string pattern7 = @"\bStates\b";
            result = Regex.Replace(result, pattern7, replace7);

            string replace8 = @"<style=Move>Move</style>";
            string pattern8 = @"\bMove\b";
            result = Regex.Replace(result, pattern8, replace8);

            string replace9 = @"<style=Stamina>Stamina</style>";
            string pattern9 = @"\bStamina\b";
            result = Regex.Replace(result, pattern9, replace9);

            string replace10 = @"<style=Fortitude>Fortitude</style>";
            string pattern10 = @"\bFortitude\b";
            result = Regex.Replace(result, pattern10, replace10);

            string replace11 = @"<style=Water>Water</style>";
            string pattern11 = @"\bWater\b";
            result = Regex.Replace(result, pattern11, replace11);

            string replace12 = @"<style=Air>Air</style>";
            string pattern12 = @"\bAir\b";
            result = Regex.Replace(result, pattern12, replace12);

            string replace13 = @"<style=Dark>Dark</style>";
            string pattern13 = @"\bDark\b";
            result = Regex.Replace(result, pattern13, replace13);

            string replace14 = @"<style=Light>Light</style>";
            string pattern14 = @"\bLight\b";
            result = Regex.Replace(result, pattern14, replace14);

            string replace15 = @"<style=Pure>Pure</style>";
            string pattern15 = @"\bPure\b";
            result = Regex.Replace(result, pattern15, replace15);

            return result; 
            //string input = "test, and test but not testing.  But yes to test";
            //string pattern = @"\btest\b";
            //string replace = "text";
            //string result = Regex.Replace(input, pattern, replace);
            //Console.WriteLine(result);

        }




        //    public static List<int> SplitTris(this int tris)
        //    {
        //        List<int> split = new List<int>();
        //        int temp = tris;

        //        while (temp > 0)
        //        {
        //            split.Add(temp % 10);
        //            temp = temp / 10;
        //        }
        //        foreach (int item in split)
        //        {
        //            Debug.Log("split  " + item);
        //        }
        //        return split;

        //    }

    

        // create a extension class that will process and Filter 







       
    }


}

