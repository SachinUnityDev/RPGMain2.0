using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    public class GridEnums
    {

    }

    public enum HighlightOverTile
    {
        Lane,
        PointTopTris,
        FlatTopTris,
        Diamond,
        Hex,
    }

    public enum TileState
    {
        Normal,  //char to be marked, empty tile won t be marked        
        Hover,   //       
        AutoSelect, //Cursor beneath will Spinning ,         
        TargetMarked,// skill Select . either thru cursor 
        TargetCollateral, // collarateral target which get effected by using the skill 
        TargetSelect,// Clicked ....on Skill FX will be shown 
        CharOnTurn,
    }
}
