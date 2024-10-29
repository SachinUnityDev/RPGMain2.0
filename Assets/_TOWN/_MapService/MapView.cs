using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class MapView : MonoBehaviour, IPanel, iHelp
    {
        [SerializeField] HelpName helpName; 
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

        [Header(" Ui clic k ctrl")]
        [SerializeField] float prevClick; 


        void OnEnable()
        {
            toggleMapBtn.onClick.AddListener(OnToggleMapBtnPressed);           
            closeBtn.onClick.AddListener(OnCloseBtnPressed);
           
        }

        public void InitMapView()
        {
            nekkiView = FindObjectOfType<NekkiView>(true);
            worldView= FindObjectOfType<WorldView>(true);
            nekkiView.InitTown(this);
            worldView.InitTown(this); 
            prevClick = Time.time;
        }
       
        void OnToggleMapBtnPressed()
        {
            if (Time.time - prevClick < 0.25f)
            {
                return;
            }
            else
            {
                prevClick = Time.time;
            }

            //if (index < maxIndex - 1)
            //{
            //    index++;
            //}
            //else
            //{
            //    index = 0;
            //}
            //for (int i = 0; i < maxIndex; i++)
            //{
            //    transform.GetChild(i).gameObject.SetActive(false);
            //}
            //transform.GetChild(index).gameObject.SetActive(true);
            if (worldView.gameObject.activeInHierarchy)
            {
                worldView.gameObject.SetActive(false);
                nekkiView.gameObject.SetActive(true);
            }
            else
            {
                worldView.gameObject.SetActive(true);
                nekkiView.gameObject.SetActive(false);
            }
        }
        void OnCloseBtnPressed()
        {
            UnLoad();
        }
        public void Load()
        {
            GameService.Instance.currGameModel.gameScene = GameScene.InMapInteraction;
            MapService.Instance.InitMapService();
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(this.gameObject, true); 
            worldView.gameObject.SetActive(false);
            nekkiView.gameObject.SetActive(true);   
            
        }

        public void UnLoad()
        {
           // MapService.Instance.SaveState();
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
            GameService.Instance.currGameModel.gameScene = GameScene.InTown;
        }

        public void Init()
        {
          
        }

        public void LoadLocation(LocationName locationName)
        {
            // MIgrate to CONTAINER WITH CITY MAPS AFTER DEMO 
            //for (int i = 0; i < maxIndex; i++)
            //{
            //  ILocation iloc =    transform.GetChild(i).GetComponent<ILocation>();
            //    if (iloc != null)
            //    {
            //        if(iloc.locationName== locationName)
            //        {
            //            transform.GetChild(i).gameObject.SetActive(true);
            //            index = i; 
            //        }
            //        else
            //        {
            //            transform.GetChild(i).gameObject.SetActive(false); // name not matching 
            //            Debug.Log(" town Name not matching "+ locationName);
            //        }
            //    }
            //    else
            //    {
            //        transform.GetChild(i).gameObject.SetActive(false); // not a iloc
            //        UnLoad();
            //    }
            //}
            if (locationName == LocationName.None)
                return;
            if (locationName == LocationName.Nekkisari)
                OnToggleMapBtnPressed();  // nekkisari click
           

        }

        public HelpName GetHelpName()
        {
            return helpName;
        }
    }

}

