using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class MapViewController : MonoBehaviour
    {
        [SerializeField] Button nekkisariBtn;
        [SerializeField] Button muraboBtn;
        [SerializeField] Button maghileBtn;

        [Header("BUTTONS")]
        [SerializeField] Button closeBtn;
        [SerializeField] Button toggleMapBtn;
        [SerializeField] Button toggletxtBtn;
        [SerializeField] Button toggleTownBtn; //only world map
        [Header("Town")]
        [SerializeField] GameObject townMapGO;
        [SerializeField] GameObject townTxtGO; 

        [Header("World")]
        [SerializeField] GameObject worldMapGO;
        [SerializeField] GameObject worldTxtGO;
        [SerializeField] GameObject townsInWorldMapGO; 

        bool istownMapOpen = false;
        bool isTextShown = false;
        bool isTownShownInWorldMap = true; 
        void Start()
        {
            toggleMapBtn.onClick.AddListener(OnToggleMapBtnPressed);
            toggletxtBtn.onClick.AddListener(OnToggleTxtPressed);
            toggleTownBtn.onClick.AddListener(OnToggleTownPressed);
            ToggleTxt(true, true);
            townsInWorldMapGO.SetActive(true);
        }

        void OnToggleTownPressed()
        {
            if (isTownShownInWorldMap)
            {
                townsInWorldMapGO.SetActive(false); 
            }
            else
            {
                townsInWorldMapGO.SetActive(true);
            }
            isTownShownInWorldMap = !isTownShownInWorldMap; 
        }
        void ToggleTxt(bool town, bool world)
        {
            townTxtGO.SetActive(town);
            worldTxtGO.SetActive(world);
            if (town || world)
                isTextShown = true;
            else
                isTextShown = false; 
        }
        void OnToggleTxtPressed()
        {
            if (istownMapOpen)
            {
                if(isTextShown)
                    ToggleTxt(false, false); 
                else
                    ToggleTxt(true, false);
               
            }
            else
            {          // world map               
                    if (isTextShown)
                        ToggleTxt(false, false);
                    else
                        ToggleTxt(false, true);                                
            }
            
        }



        void OnToggleMapBtnPressed()
        {            
            if (istownMapOpen)
            {  // to open world map 
                townMapGO.SetActive(false);
                worldMapGO.SetActive(true);
                ToggleTxt(false, true);
                toggleTownBtn.gameObject.SetActive(true);
            }
            else
            {  // to open town map
                townMapGO.SetActive(true);
                worldMapGO.SetActive(false);
                ToggleTxt(true, false);
                toggleTownBtn.gameObject.SetActive(false);
            }
            istownMapOpen = !istownMapOpen;
        }


    }

}

