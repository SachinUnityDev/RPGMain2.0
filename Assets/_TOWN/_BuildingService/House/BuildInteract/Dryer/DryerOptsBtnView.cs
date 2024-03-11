using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{


    public class DryerOptsBtnView : MonoBehaviour
    {
        [SerializeField] Button driedMeatBtn;
        [SerializeField] Button driedFish;
        [SerializeField] Button driedGrape;
        Image meatPtr;
        Image fishPtr;
        Image grapePtr;
        List<Image> ptrImgs;

        DryerView dryerView;

        void Awake()
        {
            driedMeatBtn.onClick.AddListener(OnDriedMeatBtnPressed);
            driedFish.onClick.AddListener(OnDiedFishBtnPressed);
            driedGrape.onClick.AddListener(OnDriedGrapeBtnPressed);
        }
        void DisablePtr()
        {
            for (int i = 0; i < ptrImgs.Count; i++)
            {
                ptrImgs[i].gameObject.SetActive(false);
            }
        }
        public void InitDryerPtrEvents(DryerView dryerView)
        {
            this.dryerView = dryerView;
            meatPtr = driedMeatBtn.transform.GetChild(0).GetComponent<Image>();
            fishPtr = driedFish.transform.GetChild(0).GetComponent<Image>();
            grapePtr = driedGrape.transform.GetChild(0).GetComponent<Image>();
            ptrImgs = new List<Image>() { meatPtr, fishPtr, grapePtr };
        }
        public void OnDriedMeatBtnPressed()
        {
            DisablePtr();
            meatPtr.gameObject.SetActive(true);
            dryerView.DryerItemSelect(1);
        }
        void OnDiedFishBtnPressed()
        {
            DisablePtr();
            fishPtr.gameObject.SetActive(true);
            dryerView.DryerItemSelect(2);
        }
        void OnDriedGrapeBtnPressed()
        {
            DisablePtr();
            grapePtr.gameObject.SetActive(true);
            dryerView.DryerItemSelect(3);
        }
    }
}