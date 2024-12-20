using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;

namespace Quest
{


    public class CurioViewPg1PtrEvents : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI curioNameTxt;
        [SerializeField] TextMeshProUGUI openDescTxt;


        [Header("Tool Container")]
        [SerializeField] ToolView toolView; 
        [SerializeField] Button interactBtn;
        [SerializeField] Button exitBtn; 

        [Header("global Var")]
        [SerializeField] CurioView curioView;
        [SerializeField] CurioModel curioModel;
        [SerializeField] CurioColEvents curioColEvents;

        [SerializeField] int curioNo; 
        
        [SerializeField] QRoomModel qRoomModel;
        void Start()
        {
            interactBtn.onClick.AddListener(OnInteractBtnPressed);
            exitBtn.onClick.AddListener(OnExitPressed); 
        }
        public void InitPage1(CurioView curioView, CurioModel curioModel
                                , CurioColEvents curioColEvents, int curioNo)
        {        
            this.curioView= curioView;
            this.curioModel= curioModel;
            this.curioColEvents= curioColEvents;    
            this.curioNo= curioNo;
            FillCurio();
        }

        void FillCurio()
        {
            curioNameTxt.text= curioModel.curioName.ToString().CreateSpace();
            openDescTxt.text = curioModel.openDesc.ToString().CreateSpace();

            // Fill Slots
            // get Tools as toolModels
            FillSlot();
        }

        void FillSlot()
        {
            toolView.InitToolList(curioModel.toolName, curioModel.toolName2);             
        }


        void OnInteractBtnPressed()
        {
            // open page 2 
            curioView.ShowPage2(toolView.toolSelect);            
            // change the sprite to open sprite
            
            curioColEvents.OnInteractBegin();
        }
        void OnExitPressed()
        {
            curioView.UnLoad();
        }
        

    }
}