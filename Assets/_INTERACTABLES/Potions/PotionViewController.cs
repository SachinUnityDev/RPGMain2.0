using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 


namespace Interactables
{
    public class PotionViewController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        // Potions card control on hovering upon the inventory or Combat 
        // in COMBAT 
        //  consume on hold right click will consume Item.. 

        // ONLY IN QUEST 
        // consume option only prep phase quest scene, camp or combat(no option menu) 
        // In Common inventory Panel Options(right click) : Consume ,  equip (As in move to active panel) and dispose(literally Destroy)
        //IN active Inventory Panel Option (right click ) : Consume, Unequip(Move to last spot in the panel) and Dispose 

        // IN TOWN 
        // potion can only be equip/ unequip or dispose 



        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }


        void Start()
        {

        }

     
    }


}

