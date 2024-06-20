using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{
    public class ArchetypeView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image img;
        [SerializeField] TextMeshProUGUI text; 


        [SerializeField] Archetype archetype;
        [SerializeField] ArchetypeData archetypeData;
        [SerializeField] CharModel charModel; 
        void Start()
        {
            img = GetComponent<Image>();
            InvService.Instance.OnCharSelectInvPanel += InitArcheType;
            CharController charController = InvService.Instance.charSelectController;
            InitArcheType(charController.charModel);
        }
        void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= InitArcheType;
        }

        void InitArcheType(CharModel charModel)
        {
            this.charModel = charModel;
            archetype = charModel.archeType;
            archetypeData = CharService.Instance.allCharSO.GetArchTypeData(archetype);
            SetNormal(); 
        }

        void SetHL()
        {
            img.sprite = archetypeData.spriteHL;
            text.text = charModel.archeType.ToString();
            text.gameObject.SetActive(true);
        }
        void SetNormal()
        {
            img.sprite = archetypeData.spriteN;
            text.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHL();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetNormal();
        }
    }
}