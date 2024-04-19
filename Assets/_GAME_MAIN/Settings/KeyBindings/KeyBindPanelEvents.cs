using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq; 

namespace Common
{
    public class KeyBindPanelEvents : MonoBehaviour
    { 
        public bool isPlankClicked;

        [SerializeField] Button plusButton;

        [Header("Text fields")]
        [SerializeField] TextMeshProUGUI nametxt;
        [SerializeField] TextMeshProUGUI keyBindingTxt; 
        [SerializeField] string clickedStatestr;
        
        [Header("Populated on plank init")]
        [SerializeField] KeyBindingData keyBindingData;
        [SerializeField] KeyBindingsController keyBindingController;


        void Start()
        {
            isPlankClicked = false;
            plusButton = transform.GetChild(1).GetComponent<Button>();
            plusButton.onClick.AddListener(OnButtonPressed);
            clickedStatestr = "Press any key";  
           // DisplayKeyBindingTxt(); 
        }


        void OnButtonPressed()
        {
            if (!isPlankClicked)         
                SetClickedState();           
            else           
                SetUnclickedState();        
        }

        public void PopulateKeyBindings(KeyBindingData keyBindingData
                                                , KeyBindingsController keyController)
        {
            this.keyBindingData = keyBindingData;
            nametxt.text = keyBindingData.keyfunc.ToString();
            keyBindingTxt.text = GetKeyBindingStr(keyBindingData.keyPressed); 
            keyBindingController = keyController; 
           
        }

        string GetKeyBindingStr(KeyCode key)
        {
            string str = ""; 
            switch (key)
            {                            
                case KeyCode.Keypad0:
                    str = "num 0"; 
                    break;
                case KeyCode.Keypad1:
                    str = "num 1";
                    break;
                case KeyCode.Keypad2:
                    str = "num 2";
                    break;
                case KeyCode.Keypad3:
                    str = "num 3"; break;
                case KeyCode.Keypad4:
                    str = "num 4";
                    break;
                case KeyCode.Keypad5:
                    str = "num 5";
                    break;
                case KeyCode.Keypad6:
                    str = "num 6";
                    break;
                case KeyCode.Keypad7:
                    str = "num 7";
                    break;
                case KeyCode.Keypad8:
                    str = "num 8";
                    break;
                case KeyCode.Keypad9:
                    str = "num 9";
                    break;
                case KeyCode.KeypadPeriod:
                    str = "num Period";
                    break;
                case KeyCode.KeypadDivide:
                    str = "num Divide";
                    break;
                case KeyCode.KeypadMultiply:
                    str = "num Multiply";
                    break;
                case KeyCode.KeypadMinus:
                    str = "num Minus";
                    break;
                case KeyCode.KeypadPlus:
                    str = "num Plus";
                    break;                
                case KeyCode.Alpha0:
                    str = "0";
                    break;
                case KeyCode.Alpha1:
                    str = "1";
                    break;
                case KeyCode.Alpha2:
                    str = "2";
                    break;
                case KeyCode.Alpha3:
                    str = "3";
                    break;
                case KeyCode.Alpha4:
                    str = "4";
                    break;
                case KeyCode.Alpha5:
                    str = "5";
                    break;
                case KeyCode.Alpha6:
                    str = "6";
                    break;
                case KeyCode.Alpha7:
                    str = "7";
                    break;
                case KeyCode.Alpha8:
                    str = "8";
                    break;
                case KeyCode.Alpha9:
                    str = "9";
                    break;
                default:
                    str = key.ToString();
                    break;
            }
            return str; 
        }


        void DisplayAssignmentTxt()
        {          
            keyBindingTxt.text = clickedStatestr;
            if (isPlankClicked)
            {             
                keyBindingTxt.DOFade(0.25f, 1f)                   
                    .SetLoops(-1, LoopType.Yoyo)
                    ;            
            }
        }

        void DisplayKeyBindingTxt()
        {
            keyBindingTxt.text = GetKeyBindingStr(keyBindingData.keyPressed);
            keyBindingTxt.DOPause();
            keyBindingTxt.DOFade(1f, 0.1f); 
           
        }
        public void SetClickedState()
        {
            ChgOtherPlankStatus();          
            isPlankClicked = true;
            DisplayAssignmentTxt();
            keyBindingController.isCLICKED_STATE = true; 
        }
        public void SetUnclickedState()
        {
            isPlankClicked = false;           
            DisplayKeyBindingTxt();
            keyBindingController.isCLICKED_STATE = false;
        }

        public void OnGUI() // ASSignment
        {

            if (Event.current.isKey && Event.current.type == EventType.KeyDown
                            && isPlankClicked)
            {
                {
                    KeyCode key = Event.current.keyCode;
                    KeyBindingData keyData =
                            keyBindingController.keyBindingSO
                                 .allKeyBindingData.FirstOrDefault(t => t.keyfunc == keyBindingData.keyfunc);

                        keyData.keyPressed = key; // Assignment
                        SetUnclickedState(); 
                        Debug.Log("Key Assigned" + key);
                }
            }
        }
        

        void ChgOtherPlankStatus()
        {           
            Transform parentTrans = transform.parent;
            foreach (Transform child in parentTrans)
            {
                if (child.gameObject == this.gameObject) continue;
                KeyBindPanelEvents keyBindEvents = child.GetComponent<KeyBindPanelEvents>();
                if (keyBindEvents.isPlankClicked)
                {
                    keyBindEvents.SetUnclickedState();
                }
            }
        }

    }
}

