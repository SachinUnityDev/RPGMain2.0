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
        //private void Awake()
        //{
        //    CopyDefault2Profile(); 
        
        //}

       

     

        public void ResetTodefault()
        {
            foreach (KeyBindingData bindData in allKeyBindingData)
            {
                bindData.keyPressed = bindData.keyDefault; 
            }
        }

        public KeyBindingData GetKeyBindingData(GameState gameState, KeyCode keyCode)
        {
            int index = allKeyBindingData.FindIndex(t => t.gameState == gameState && t.keyPressed == keyCode);
            if (index != -1)
                return allKeyBindingData[index];

            return null;
        }
   
        public bool  AreKeysDiffFrmDefault()
        {
            List<KeyBindingData> misMatchLs =
                        allKeyBindingData.Where(t=>t.keyPressed != t.keyDefault).ToList();
            if(misMatchLs.Count > 0) 
                return true;
            return false; 
        }
        public void ResetDefault2Profile() // new game Start 
        {
            foreach (KeyBindingData bindData in allKeyBindingData)
            {
                bindData.keyDefault = bindData.keyPressed;
            }
        }

    }




}
