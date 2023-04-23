using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
      public enum MonthName
      {
        None,
        FeatherOfThePeafowl,
        AntlersOfTheDeer,
        HunchOfTheCamel,
        WingOfTheLocust,
        WhiskeyOfTheCat,
        TailOfTheLizard,
        ShellOfTheTurtle,
        EarOfTheWolf,
        BeakOfTheWoodpecker,
        ClawOfTheBear,
        PeltOfTheOtter,
        EyeOfTheHawk,     

      }   


    [CreateAssetMenu(fileName = "MonthSO", menuName = "Calendar Service/MonthSO")]
    public class MonthSO : ScriptableObject
    {

        public int orderInSeq; 
        public MonthName monthName;
        public string monthNameShort;
        public string monthNameStr; 
        public string monthDesc;
        public List<string> monthSpecs;

        private void Awake()
        {
            orderInSeq = (int)monthName; 
            monthNameShort = GetMonthNameShort(monthName);
            monthNameStr = GetMonthNameStr(monthName); 


        }


        public string GetMonthNameStr(MonthName _monthName)
        {
            switch (_monthName)
            {
                case MonthName.FeatherOfThePeafowl: return "Feather of the Peafowl";
                case MonthName.AntlersOfTheDeer: return "Antlers of the Deer";
                case MonthName.HunchOfTheCamel: return "Hunch of the Camel";
                case MonthName.WingOfTheLocust: return "Wing of the Locust";
                case MonthName.WhiskeyOfTheCat: return "Whiskey of the Cat";
                case MonthName.TailOfTheLizard: return "Tail of the Lizard";
                case MonthName.ShellOfTheTurtle: return "Shell of the Turtle";
                case MonthName.EarOfTheWolf: return "Ear of the Wolf";
                case MonthName.BeakOfTheWoodpecker: return "Beak of the Woodpecker";
                case MonthName.ClawOfTheBear: return "Claw of the Bear";
                case MonthName.PeltOfTheOtter: return "Pelt of the Otter";
                case MonthName.EyeOfTheHawk: return "Eye of the Hawk";
                default: return null;
            }
        }



        public string GetMonthNameShort(MonthName _monthName)
        {
            switch (_monthName)
            {
                case MonthName.FeatherOfThePeafowl: return "FotP";
                case MonthName.AntlersOfTheDeer: return "AotD";
                case MonthName.HunchOfTheCamel: return "HotC";
                case MonthName.WingOfTheLocust: return "WotL";
                case MonthName.WhiskeyOfTheCat: return "WotC";
                case MonthName.TailOfTheLizard: return "TotL";
                case MonthName.ShellOfTheTurtle: return "SotT";
                case MonthName.EarOfTheWolf: return "EotW";
                case MonthName.BeakOfTheWoodpecker: return "BotW";
                case MonthName.ClawOfTheBear: return "CotB";
                case MonthName.PeltOfTheOtter: return "PotO";
                case MonthName.EyeOfTheHawk: return "EotH";
                default: return null;
            }
        }



    }




}
