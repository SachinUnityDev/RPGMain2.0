using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Common
{
    public class ClearmindBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        ClearmindView clearMindView; 
        TempTraitController tempTraitController;
        [SerializeField] CharNames charName;

        [SerializeField] Transform clearMindtxtTrans; 
        public void InitBtnEvents(CharNames charName, TempTraitController tempTraitController, ClearmindView clearMindView)
        {
            this.clearMindView = clearMindView; 
            this.tempTraitController = tempTraitController;
            this.charName = charName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tempTraitController.OnClearMindPressed();
            clearMindView.OnClearMindPressed(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
          clearMindtxtTrans.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            clearMindtxtTrans.gameObject.SetActive(false); 
        }
    }
}