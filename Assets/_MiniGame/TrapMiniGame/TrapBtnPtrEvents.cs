using DG.Tweening;
using Quest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public class TrapBtnPtrEvents : MonoBehaviour
    {

        [SerializeField] int letter;
        

        [SerializeField] int currLetter;


        [Header(" Global Var")]
        [SerializeField] AllTrapMGSO alltrapMGSO;
        [SerializeField] TrapView trapView;

        public void InitTiles(TrapView trapView, AllTrapMGSO allTrapMGSO)
        {
            this.trapView= trapView;    
            this.alltrapMGSO= allTrapMGSO;
        }
        public void ShowGreyTile()
        {
            //BG
            transform.GetComponent<Image>().sprite = alltrapMGSO.greyTL;
            // LETTER
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().DOFade(0.2f, 0.01f);
            //FX
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false); // FX
        }
        public void ShowDefaultTile()
        {
            //BG
            transform.GetComponent<Image>().sprite = alltrapMGSO.defaultTL;
            // LETTER
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().DOFade(1f, 0.01f);
            //FX
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true); // FX

            //transform.GetChild(1).gameObject.SetActive(true);
            //transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        }

        public void OnWrongHit()
        {
            //BG
            transform.GetComponent<Image>().sprite = alltrapMGSO.mistakeHitTL;
        }
        public void OnCorrectHit()
        {
            //BG
            transform.GetComponent<Image>().sprite = alltrapMGSO.correctHitTL;
        }

    }
}