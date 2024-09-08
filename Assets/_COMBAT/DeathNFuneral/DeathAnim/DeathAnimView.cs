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
    public class DeathAnimView : MonoBehaviour
    {
        [Header(" Death Goddess prefab TBR")]
        [SerializeField] GameObject deathGoddess;
        [SerializeField] float intermediateSize = 0.85f;


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
 
            deathGoddessGO.transform.SetParent(charGO.transform);
            deathGoddessGO.GetComponent<DeathGodessFX>().PlayDeathAnim(charController); 
        }
 
       



    }
}





