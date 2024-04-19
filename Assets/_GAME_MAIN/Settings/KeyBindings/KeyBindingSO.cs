using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class KeyBindingData
    {
        public GameState gameState;
        public KeyBindFuncs keyfunc;
        public KeyCode keyPressed;
        public KeyCode keyDefault; 
        public KeyBindingData(GameState gameState, KeyBindFuncs keyfunc, KeyCode keyPressed, KeyCode keyDefault)
        {
            this.gameState = gameState;
            this.keyfunc = keyfunc;
            this.keyPressed = keyPressed;
            this.keyDefault= keyDefault;

        }
    }

    [CreateAssetMenu(fileName = "KeyBindingDataSO", menuName = "Control Service/KeyBindingDataSO")]

    public class KeyBindingSO : ScriptableObject
    {
        public List<KeyBindingData> allKeyBindingData = new List<KeyBindingData>();
        private void Awake()
        {
            CopyDefault2Profile(); 
        
        }

        public KeyBindingData GetKeyBindingData(GameState gameState, KeyCode keyPressed)
        {
            // game stateNone == game State all
            List<KeyBindingData> allbind = allKeyBindingData.Where(t=>(t.gameState== gameState || t.gameState == GameState.None) 
                                        && t.keyPressed == keyPressed).ToList();
            
            if(allbind.Count>0)
            return  allKeyBindingData[0]; 
            return null;
        }

        public bool ChgKeyBindingData(GameState gameState, KeyCode keyPressed)
        {
            KeyBindingData keyBindingData = GetKeyBindingData(gameState, keyPressed);
            if (keyBindingData == null) return false; 
            keyBindingData.keyPressed = keyPressed;
            return true; 
        }

        public void ResetTodefault()
        {
            foreach (KeyBindingData bindData in allKeyBindingData)
            {
                bindData.keyPressed = bindData.keyDefault; 
            }
        }

        public void CopyDefault2Profile() // new game Start 
        {
            foreach (KeyBindingData bindData in allKeyBindingData)
            {
                bindData.keyDefault = bindData.keyPressed;
            }
        }

    }




}
