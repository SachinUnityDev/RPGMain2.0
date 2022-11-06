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


        void Awake()
        {
            isPlankClicked = false;
            plusButton = transform.GetChild(1).GetComponent<Button>();
            plusButton.onClick.AddListener(OnButtonPressed);
            clickedStatestr = "Press any key";  
            DisplayKeyBindingTxt(); 
        }

        private void Start()
        {
        
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
            keyBindingTxt.text = keyBindingData.keyPressed.ToString();
            keyBindingController = keyController; 
           
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
            keyBindingTxt.text = keyBindingData.keyPressed.ToString();
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
                            keyBindingController.keyBindingModel
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

