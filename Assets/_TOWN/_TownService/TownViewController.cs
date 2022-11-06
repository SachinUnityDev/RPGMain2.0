using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
namespace Town
{
    public class TownViewController : MonoBehaviour
    {

        [Header("Left Town Btns")]
        [SerializeField] Button rosterBtn;
        [SerializeField] Button jobBtn;
        [SerializeField] Button inventoryBtn;


        [Header("Right Town Btns")]
        [SerializeField] Button eventBtn;
        [SerializeField] Button questScrollBtn;
        [SerializeField] Button mapBtn;

        [SerializeField] GameObject InteractionPanel;

        [SerializeField] List<GameObject> allPanels = new List<GameObject>(); 

        void Start()
        {
            rosterBtn.onClick.AddListener(OnRosterBtnPressed);
            jobBtn.onClick.AddListener(OnJobsBtnPressed);
            inventoryBtn.onClick.AddListener(OnInvBtnPressed);

            eventBtn.onClick.AddListener(OnEventBtnPressed);
            questScrollBtn.onClick.AddListener(OnQuestScrollBtnPressed);
            mapBtn.onClick.AddListener(OnMapBtnPressed);
        }

        void OnRosterBtnPressed()
        {
            RosterService.Instance.OpenRosterView(); 
        }
        void OnJobsBtnPressed()
        {

        }
        void OnInvBtnPressed()
        {
           // InventoryService.Instance.invViewController.

        }

        void OnEventBtnPressed()
        {


        }
        void OnQuestScrollBtnPressed()
        {

        }
        void OnMapBtnPressed()
        {

        }



    }



}
