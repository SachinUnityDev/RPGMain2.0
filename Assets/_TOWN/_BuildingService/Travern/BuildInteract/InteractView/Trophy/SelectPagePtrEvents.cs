using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;

namespace Town
{

    public class SelectPagePtrEvents : MonoBehaviour  // trophy and pelt page 
    {

        [SerializeField] Button trophyBtn;
        [SerializeField] Button peltBtn;
        

        TrophyView trophyView; 
        void Start()
        {
            trophyBtn.onClick.AddListener(OnTrophyBtnPressed);
            peltBtn.onClick.AddListener(OnPeltBtnPressed); 
        }

        void OnTrophyBtnPressed()
        {            
            trophyView.InitSelectPage(TavernSlotType.Trophy); 
        }

        void OnPeltBtnPressed()
        {
            trophyView.InitSelectPage(TavernSlotType.Pelt);
        }
        public void InitSelectPage(TrophyView trophyView)
        {
            this.trophyView = trophyView;
            //Iitems item =
            //         BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall; 


            
        }  
        void FillTrophySlot(Iitems item)
        {


        }

        public void AddItemToTrophy(Iitems item)
        {
            
        }


    }
}