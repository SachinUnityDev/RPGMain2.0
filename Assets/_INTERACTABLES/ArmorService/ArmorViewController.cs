using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;

namespace Interactables
{
    public class ArmorViewController : MonoBehaviour, IPanel
    {
        [SerializeField] CharNames charSelect;
        [SerializeField] List<Transform> socketSlots; 

     
        void Start()
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                socketSlots.Add(transform.GetChild(i)); 
            }
            InvService.Instance.OnCharSelectInvPanel += PopulateArmorPanel;
            UnLoad();
        }
        void OnEnable()
        {
            Load(); 
        }
        #region Load UnLoad
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
            PopulateArmorPanel(InvService.Instance.charSelectController.charModel); 
           
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {
           
        }

        #endregion

        #region Populate Armor Content

        void PopulateArmorPanel(CharModel charModel)
        {
            charSelect = charModel.charName; 
            Sprite sprite = ArmorService.Instance.armorSO.GetSprite(charSelect);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                = sprite;
            PopulateGemSocketed();
        }
       

        #endregion

        #region Populate Gem Content

        #endregion




        #region Socket Controls

        void PopulateGemSocketed()
        {
            CharController charController = InvService.Instance.charSelectController;
            ItemModel itemModel = charController.itemController.itemModel;
            List<Iitems> divGemSocketed = itemModel.divItemsSocketed; 
            Iitems supportGemSocketed = itemModel.supportItemSocketed;
            GemSO gemSO; 
            if(supportGemSocketed != null)
            {
                gemSO =
                ItemService.Instance.GetGemSO((GemNames)supportGemSocketed.itemName);
                 socketSlots[2].GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
            }
            if (divGemSocketed.Count == 0) {

                socketSlots[0].GetChild(0).GetComponent<Image>().sprite = null;
                socketSlots[1].GetChild(0).GetComponent<Image>().sprite = null;
                return; 
            }
            if(divGemSocketed.Count == 1)
            {
                gemSO = ItemService.Instance.GetGemSO((GemNames)divGemSocketed[0].itemName);
                socketSlots[0].GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
                socketSlots[1].GetChild(0).GetComponent<Image>().sprite = null;
                return; 
            }
            if (divGemSocketed.Count == 1)
            {
                gemSO = ItemService.Instance.GetGemSO((GemNames)divGemSocketed[0].itemName);
                socketSlots[0].GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
                gemSO = ItemService.Instance.GetGemSO((GemNames)divGemSocketed[1].itemName);
                socketSlots[1].GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
                return;
            }
        }
        #endregion 




    }

}

