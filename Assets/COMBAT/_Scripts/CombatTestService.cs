using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
namespace Combat
{
    public class CombatTestService : MonoBehaviour
    {
        [SerializeField] Button Pack1Btn;
        [SerializeField] Button Pack2Btn;
        

        private void Start()
        {
            Pack1Btn.onClick.AddListener(OnPack1BtnPressed);
            Pack2Btn.onClick.AddListener(OnPack2BtnPressed); 
        }

        void OnPack1BtnPressed()
        {
            // GridService.Instance.SetAllyPreTactics();
          //  GridService.Instance.SetEnemy(EnemyPack.RatPack1);
        }

        void OnPack2BtnPressed()
        {
            //GridService.Instance.SetAllyPreTactics();
          //  GridService.Instance.SetEnemy(EnemyPack.RatPack2);
        }

        private void Update()
        {
       
        }

    }
}

