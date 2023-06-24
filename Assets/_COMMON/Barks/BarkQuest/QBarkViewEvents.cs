using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Quest
{


    public class QBarkViewEvents : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI barkTxt;
        QbarkView qbarkView;
        AudioSource audioSource; 
        void Start()
        {
            transform.GetChild(1).gameObject.SetActive(false);

        }

        public void InitQBarkViewEvents(QbarkView qBarkView)
        {
            this.qbarkView = qBarkView;                    
        }

        public void ShowBark(BarkCharData barkCharData)
        {
            barkTxt.text = barkCharData.str;
            transform.GetChild(1).gameObject.SetActive(true);           
        }
        public void CloseBark()
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        
    }
}