using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;


namespace Common
{
    public class ScrollPortraitSlotController : MonoBehaviour, IDropHandler, IPointerClickHandler
    {

        public CharNames charInSlot;


        [Header("FOR DROP CONTROLS")]
        [SerializeField] PortraitDragNDrop portraitDragNDrop;

        public void OnDrop(PointerEventData eventData)
        {


        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        void Start()
        {

        }

    }



}

