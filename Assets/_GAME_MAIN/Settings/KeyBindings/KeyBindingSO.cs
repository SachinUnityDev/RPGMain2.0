using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class KeyBindingData
    {
        public GameScene gameScene;
        public KeyBindFuncs keyfunc;
        public KeyCode keyPressed;
        public KeyCode keyDefault; 
        public KeyBindingData(GameScene gameScene, KeyBindFuncs keyfunc, KeyCode keyPressed, KeyCode keyDefault)
        {
            this.gameScene = gameScene;
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

        public KeyBindingData GetKeyBindingData(GameScene gameScene, KeyCode keyCode)
        {
            int index = allKeyBindingData.FindIndex(t => t.gameScene == gameScene && t.keyPressed == keyCode);
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
