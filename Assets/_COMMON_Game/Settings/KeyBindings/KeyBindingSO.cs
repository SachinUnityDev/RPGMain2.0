using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class KeyBindingData
    {
        public GameState gameState;
        public KeyBindFuncs keyfunc;
        public KeyCode keyPressed;

        public KeyBindingData(GameState gameState, KeyBindFuncs keyfunc, KeyCode keyPressed)
        {
            this.gameState = gameState;
            this.keyfunc = keyfunc;
            this.keyPressed = keyPressed;
        }
    }

    [CreateAssetMenu(fileName = "KeyBindingDataSO", menuName = "Control Service/KeyBindingDataSO")]

    public class KeyBindingSO : ScriptableObject
    {
        public List<KeyBindingData> allKeyBindingData = new List<KeyBindingData>();  
    }




}
