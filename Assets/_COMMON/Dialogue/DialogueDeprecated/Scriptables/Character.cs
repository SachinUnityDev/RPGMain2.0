using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Dialogue
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Character Service/Character")]
    public class Character : ScriptableObject
    {
        public string Name;
        public Sprite portrait;
    }

}



