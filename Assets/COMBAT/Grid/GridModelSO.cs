using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq; 


namespace Combat
{
    [CreateAssetMenu(fileName = "GridModelSO", menuName = "Combat/GridModelSO")]
    public class GridModelSO : ScriptableObject
    {
        [Header("Tiles types")]
        public Tile[] allyTiles = new Tile[4]; // normal , hover, select, onSkillTarget
                                               //Normal(enemy/ally), Hover(enemy/ally), AutoSelect(enemy/ally), Target(enemy/ally)
        public Tile[] enemyTiles = new Tile[4]; // normal , hover, select, onSkillTarget

        [Header("Position HL")]
        public Sprite[] posHL = new Sprite[5]; // Single, Lane,  Tris, Diamond, Hex,

        [Header("Tile FX")]
        public GameObject allyOnTurnHL;
        public GameObject enemyOnTurnHL;
        //public GameObject allyTargetHL;
        //public GameObject enemyTargetHL;




        public Tile GetTile(TileState _tileState, CharMode _charMode)
        {
           if(_charMode == CharMode.Ally)            
                return allyTiles[(int)_tileState];
            if (_charMode == CharMode.Enemy)
                return enemyTiles[(int)_tileState];
            else
                Debug.Log("tile not found");
            
            return null; 
        }

        //public GameObject GetFXOverTile(TileState tileState)
        //{
        //    if(tileState == TileState.CharOnTurn)
        //    {

        //        return allyOnTurnHL;

        //    }
        //    return null; 
        //}


        public Sprite GetPosHL(HighlightOverTile _charOnTile)
        {
            return posHL[(int)_charOnTile];
        }
    }


}



