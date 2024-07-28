using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class LoadSlotsView : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] List<GameModel> allGameModelsInProfile = new List<GameModel>();
        [SerializeField] SaveSlot saveSlot; 

        public void Init(List<GameModel> allGameModelsInProfile)
        { 
            this.allGameModelsInProfile = allGameModelsInProfile.DeepClone(); 
            

        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }
    }
}