using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Common
{
    public class LevelUpEnemiesController : MonoBehaviour
    {

        //+1 foc, luck, mor, init
        //+2 vigor, +2 wp
        //%20 more armor and dmg
        //%20 more dodge and acc
        //+10 to D&L resistances which are 0-10
        //+15 to D&L resistances which are 10 and above
        //+10 to elem.resistances which are 0-10
        //+15 to elem. resistances which are 10-40
        //+20 to elem. resistances which are 40 and above

        //CharController charController;

        //public void ApplyLevelUp(CharController _charController)
        //{
        // //   if (charController.charModel.racetype ==RaceType.)




        //}

        //public void Level2Boost()
        //{
        //    charController.ChangeStat(CauseType.LevelUp, 0, charController, StatsName.acc, 2);
        //    charController.ChangeStat(CauseType.LevelUp, 0, charController,StatsName.morale, 2);
        //    charController.ChangeStat(CauseType.LevelUp, 0, charController, StatsName.haste, 2);
        //    charController.ChangeStat(CauseType.LevelUp, 0, charController, StatsName.vigor, 1);
        //    charController.ChangeStat(CauseType.LevelUp, 0, charController, StatsName.willpower, 1);

        //    PercentBasedIncr(StatsName.dodge, 15);
        //    PercentBasedIncr(StatsName.acc, 15);

        //    MinMaxPercentIncr(StatsName.damage, 15);
        //    MinMaxPercentIncr(StatsName.armor, 15);

        //    RangeBasedIncr(StatsName.lightRes, 10, 15, 15);
        //    RangeBasedIncr(StatsName.darkRes, 10, 15, 15);
        //    RangeBasedIncr(StatsName.fireRes, 10, 15, 20);
        //    RangeBasedIncr(StatsName.earthRes, 10, 15, 20);
        //    RangeBasedIncr(StatsName.waterRes, 10, 15, 20);
        //    RangeBasedIncr(StatsName.airRes, 10, 15, 20);

        //}

        //void MinMaxPercentIncr(StatsName _statName, int _percent)
        //{
        //    float baseValueMin = charController.GetStat(_statName).minRange;
        //    float baseValueMax = charController.GetStat(_statName).maxRange;

        //    int statExtraMax = (int)((_percent / 100) * baseValueMax);
        //    int statExtraMin = (int)((_percent / 100) * baseValueMin);

        //    charController.ChangeStatRange(CauseType.LevelUp, 0, charController, _statName, statExtraMin, statExtraMax);

        //}

        //void PercentBasedIncr(StatsName _statName, int _percent)
        //{
        //    float baseValue = charController.GetStat(_statName).currValue;
        //    int statExtra = (int)((_percent / 100) * baseValue);
        //    charController.ChangeStat(CauseType.LevelUp, 0, charController, _statName, statExtra);

        //}
        //void RangeBasedIncr(StatsName _statName, int below10, int bet10N40, int above40)
        //{

        //    float baseValue = charController.GetStat(_statName).currValue;
        //    if (baseValue <= 10f)
        //    {

        //        charController.ChangeStat(CauseType.LevelUp, 0, charController, _statName, below10);

        //    }
        //    else if (baseValue > 10 && baseValue <= 40)
        //    {
        //        charController.ChangeStat(CauseType.LevelUp, 0, charController, _statName, bet10N40);

        //    }
        //    else if (baseValue > 40)
        //    {
        //        charController.ChangeStat(CauseType.LevelUp, 0, charController, _statName, above40);

        //    }


        //}

    } 

 }

