using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class MapView : MonoBehaviour, IPanel
    {
        [SerializeField] Button nekkisariBtn;
        
        [Header("BUTTONS")]
        [SerializeField] Button closeBtn;
        [SerializeField] Button toggleMapBtn;  // to be  kept
        
        
        [Header("Town")]
        [SerializeField] NekkiView nekkiView;        

        [Header("World")]
        [SerializeField] WorldView worldView;
        [SerializeField] int index = 0;
        [SerializeField] int maxIndex = 2; 
        void Start()
        {
            toggleMapBtn.onClick.AddListener(OnToggleMapBtnPressed);           
            closeBtn.onClick.AddListener(OnCloseBtnPressed);
           
        }

        public void InitMapView()
        {
            nekkiView.InitTown(this);
            worldView.InitTown(this); 
        }
       
        void OnToggleMapBtnPressed()
        {            
            
            if(index < maxIndex-1)
            {
                index++;
            }
            else
            {
                index = 0; 
            }
            for (int i = 0; i < maxIndex; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(index).gameObject.SetActive(true);
        }

        //void ShowWorldMap()
        //{
        //    Nekkisari_townMapGO.SetActive(false);
        //    worldMapGO.SetActive(true);
        //    ToggleTxt(false, true);
        //    toggleTownBtn.gameObject.SetActive(true);
          
        //}

        //void ShowNekkisariTown()
        //{
        //    Nekkisari_townMapGO.SetActive(true);
        //    worldMapGO.SetActive(false);
        //    ToggleTxt(true, false);
        //    toggleTownBtn.gameObject.SetActive(false);
        //}

        void OnCloseBtnPressed()
        {
            UnLoad();
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(this.gameObject, true); 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {
          
        }

        public void LoadLocation(LocationName locationName)
        {
            for (int i = 0; i < maxIndex; i++)
            {
              ILocation iloc =    transform.GetChild(i).GetComponent<ILocation>();
                if (iloc != null)
                {
                    if(iloc.locationName== locationName)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.GetChild(i).gameObject.SetActive(false); // name not matching 
                    }
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false); // not a iloc
                }
            }


        }

    }

}

