using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Common
{
    public class BGClicked : MonoBehaviour, IPointerClickHandler
    {
        public IntroCtrl introController;
        public void OnPointerClick(PointerEventData eventData)
        {
         //   introController.RollBack2MainMenu();  
            Debug.Log("Roll back to game object");
        }      
    }




}
