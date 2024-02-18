using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System; 

namespace Common
{

    public class PermaTraitsService : MonoSingletonGeneric<PermaTraitsService>
    {      
     
        public PermaTraitsFactory permaTraitsFactory;
        public event Action<CharController, PermaTraitBase> OnPermaTraitAdded;

        public AllPermaTraitSO allPermaTraitSO;


        [Header("Perma trait Card")]
        [SerializeField] GameObject permaTraitCardPrefab; 
        public GameObject permaTraitGO; 

        void Start()
        {
            permaTraitsFactory = GetComponent<PermaTraitsFactory>();
            CreatePermaTraitCardGO(); 
        }

        void CreatePermaTraitCardGO()
        {
            GameObject canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (permaTraitGO == null)
            {
                permaTraitGO = Instantiate(permaTraitCardPrefab);
            }
            permaTraitGO.transform.SetParent(canvasGO.transform);
            permaTraitGO.transform.SetAsLastSibling();
            permaTraitGO.transform.localScale = Vector3.one;
            permaTraitGO.SetActive(false);
        }

    }

}
