using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class TrapBtnPtrEvents : MonoBehaviour
    {

        [SerializeField] int letter;
        [SerializeField] TrapView trapView;

        [SerializeField] int currLetter;

        [SerializeField] TrapMGSO trapMGSO; 


        private void Awake()
        {

        }

        public void ShowNLTile()
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true); 
        }
        public void ShowHLTile()
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        }

        public void OnWrongHit()
        {

        }
        public void OnCorrectHit()
        {

        }

    }
}