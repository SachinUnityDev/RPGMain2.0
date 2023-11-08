using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{



}
public class ControlEnums 
{
  
    public enum GamePlayOptions
    {
        None, 
        MortalBlood,  // Auto save to one specific slot ....If Abbas died game is over 
        TighterTimeLine, 
        Both, 
    }


    public enum GameMode
    {
        CampaignMode,  // Story mode .. continues in sequences ... 
        UphillMode,  // Rogue like mode .. some chosen players
                     // ...Combat .. and progress no story 
    }

}
