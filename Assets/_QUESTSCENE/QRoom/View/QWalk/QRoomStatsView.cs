using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Quest
{



    public class QRoomStatsView : MonoBehaviour
    {
        [SerializeField] Transform header;
        [SerializeField] Transform statsPanel;
        [SerializeField] Transform hungerBar;
        [SerializeField] Transform thirstBar;

        void Start()
        {
            gameObject.SetActive(false);
        }

        public void StatViewInit(CharController charController)
        {
            if (charController == null)
            {
                Debug.LogError("charController is null");
                return; 
            }
                

            header.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = charController.charModel.charNameStr;


            foreach (Transform child in statsPanel)
            {
                child.GetComponent<QRoomStatDataView>().InitStat(charController.charModel); 
            }

            // hunger and thirst Data
            StatData hungerData = charController.GetStat(StatName.hunger);

            hungerBar.GetComponent<Image>().fillAmount = hungerData.currValue / hungerData.maxLimit;

            StatData thirstData = charController.GetStat(StatName.thirst);
            thirstBar.GetComponent<Image>().fillAmount = thirstData.currValue / thirstData.maxLimit;

        }
    }
}