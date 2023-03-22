using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{


    public class PotionOptsBtnView : MonoBehaviour
    {

        [SerializeField] Button healthPotionBtn;
        [SerializeField] Button staminaPotionBtn;
        [SerializeField] Button fortPotionBtn;
        Image healthPtr;
        Image staminaPtr;
        Image fortPtr;
        List<Image> ptrImgs;
            
        CraftView craftView; 
        
        void Awake()
        {
            healthPotionBtn.onClick.AddListener(OnHealthPotionPressed);
            staminaPotionBtn.onClick.AddListener(OnStaminaPotionPressed);
            fortPotionBtn.onClick.AddListener(OnFortPotionPressed);

            // img off 
            healthPtr = healthPotionBtn.transform.GetChild(0).GetComponent<Image>();
            staminaPtr = staminaPotionBtn.transform.GetChild(0).GetComponent<Image>();
            fortPtr = fortPotionBtn.transform.GetChild(0).GetComponent<Image>();
            ptrImgs = new List<Image>() { healthPtr, staminaPtr, fortPtr };
        }

        void DisablePtr()
        {
            for (int i = 0; i < ptrImgs.Count; i++)
            {
                ptrImgs[i].gameObject.SetActive(false); 
            }
        }
        public void InitPotionPtrEvents(CraftView craftView)
        {
            this.craftView = craftView;
        }


        void OnHealthPotionPressed()
        {
            DisablePtr();
            healthPtr.gameObject.SetActive(true);
            craftView.PotionSelect(1); 
        }
        void OnStaminaPotionPressed()
        {
            DisablePtr();
            staminaPtr.gameObject.SetActive(true);
            craftView.PotionSelect(2);
        }
        void OnFortPotionPressed()
        {
            DisablePtr();
            fortPtr.gameObject.SetActive(true);
            craftView.PotionSelect(3);
        }


    }
}