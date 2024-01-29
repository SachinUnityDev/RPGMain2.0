using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    [System.Serializable]
    public class TileData
    {
        public Vector3Int tilePos;
        public TileState tileState; 
    }

    [CreateAssetMenu(fileName = "GridPos2TilePos", menuName = "Combat/GridPos2TilePos")]
    public class GridPos2TilePosSO : ScriptableObject
    {
        public TileData[] gridAlly = new TileData[7];
        public TileData[] gridEnemy = new TileData[7];        
    }
}

