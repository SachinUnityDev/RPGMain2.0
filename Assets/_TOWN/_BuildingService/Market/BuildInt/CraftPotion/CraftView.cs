using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class CraftView : MonoBehaviour, IPanel
    {
        [Header("TBR")]
        [SerializeField] Transform potionOptsBtnContainer;
        [SerializeField] Transform recipeSlotContainer;
        [SerializeField] Transform currTransform;
        [SerializeField] Transform costCurrtrans;


        private void Awake()
        {
            
        }

        public void Init()
        {

        }

        public void Load()
        {
            
        }

        public void UnLoad()
        {
           
        }
    }
}