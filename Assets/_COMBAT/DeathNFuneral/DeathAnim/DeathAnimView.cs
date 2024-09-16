using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Common;
using Spine.Unity;
using UnityEngine.UI;
using System;
using System.Linq;
using Spine;

namespace Combat
{
    public class DeathObjData
    {
        public GameObject deathGO;
        public CharController charController; 
        public DeathObjData(GameObject deathGO, CharController charController)
        {
            this.deathGO = deathGO;
            this.charController = charController;
        }
    }

    public class DeathAnimView : MonoBehaviour
    {
        [Header(" Death Goddess prefab TBR")]
        [SerializeField] GameObject deathGoddess;
        [SerializeField] float intermediateSize = 0.85f;
        [SerializeField] List<DeathObjData> deathGOs = new List<DeathObjData>();
        bool isAnimPlaying = false;
        void Start()
        {
            
            CharService.Instance.OnCharDeath -= SpawnDeathGodess;
            CharService.Instance.OnCharDeath += SpawnDeathGodess;
        }
        private void OnDisable()
        {
            CharService.Instance.OnCharDeath -= SpawnDeathGodess;
        }


        void SpawnDeathGodess(CharController charController)
        {
            GameObject charGO = charController.gameObject;
            GameObject deathGoddessGO = Instantiate(deathGoddess, charController.gameObject.transform.position, Quaternion.identity);       
            DeathObjData deathObjData = new DeathObjData(deathGoddessGO, charController);
            deathGoddessGO.transform.SetParent(charGO.transform);
            deathGOs.Add(deathObjData);
            if (!isAnimPlaying)
            {
                StartCoroutine(PlayDeathAnim());
                isAnimPlaying = true;
            }
        }
        IEnumerator PlayDeathAnim()
        {
            yield return new WaitForEndOfFrame();
            foreach (DeathObjData deathObjData in deathGOs)
            {
              deathObjData.deathGO.GetComponent<DeathGodessFX>().PlayDeathAnim(deathObjData.charController);
            }
            isAnimPlaying = false;
        }
               



    }
}





