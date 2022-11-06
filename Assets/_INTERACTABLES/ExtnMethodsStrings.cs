using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Combat;

public static class ExtnMethodsStrings
{

    public static string[] SeparateWords(this string str)
    {
        char separator = ' ';

        string[] splitStr = str.Split(separator); 

        return splitStr; 
    }

    public static RaceType GetRaceTypeFrmRaceTypeHero(this RaceTypeHero raceTypeHero)
    {
        RaceType raceType = RaceType.None; 
        for (int i = 1; i < Enum.GetNames(typeof(RaceType)).Length; i++)
        {
            string raceTypeStr =((RaceType)i).ToString();
            string raceTyHeroStr = raceTypeHero.ToString(); 

            if(raceTyHeroStr == raceTypeStr)
            {
                raceType = (RaceType)i;
            }

        }
        return raceType;
    }

    public static RaceTypeHero GetRaceTypHFrmRaceType(this RaceType raceType)
    {
        RaceTypeHero raceTypeHero = RaceTypeHero.None;
        for (int i = 1; i < Enum.GetNames(typeof(RaceTypeHero)).Length; i++)
        {
            string raceTyHeroStr = ((RaceTypeHero)i).ToString();
            string raceStr = raceType.ToString();

            if (raceTyHeroStr == raceStr)
            {
                raceTypeHero = (RaceTypeHero)i;
            }

        }
        return raceTypeHero;

    }

    public static bool TheToss(this bool myToss)
    {
       int tossF = UnityEngine.Random.Range(1, 3);
        if (tossF == 1)
            return true; 
        else
            return false;   
    }



}
