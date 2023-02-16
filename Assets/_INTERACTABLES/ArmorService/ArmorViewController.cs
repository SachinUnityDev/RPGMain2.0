using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;
using TMPro;

namespace Interactables
{
    public class ArmorViewController : MonoBehaviour, IPanel
    {
        [SerializeField] CharNames charSelect;
        [SerializeField] List<Transform> socketSlots;
        [SerializeField] List<string> displayStrs = new List<string>();

        void Start()
        {        
            InvService.Instance.OnCharSelectInvPanel += PopulateArmorPanel;
            UnLoad();
        }
        void OnEnable()
        {
         //   if (InvService.Instance.isInvPanelOpen)
             // Load();
        }
        #region Load UnLoad
        public void Load()
        {          
            CharModel charModel = InvService.Instance.charSelectController.charModel; 
            PopulateArmorPanel(charModel); 
           
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {
            charSelect = InvService.Instance.charSelect;
            Load(); 
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
            PopulateMaterial(charModel);
            PopulateBuffStrings(); 
        }

        #endregion

  




        #region Socket Controls

        void PopulateGemSocketed()
        {
            CharController charController = InvService.Instance.charSelectController;
            ItemModel itemModel = charController.itemController.itemModel;
           // Iitems[] divGemSocketed = itemModel.divItemsSocketed; 
            Iitems supportGemSocketed = itemModel.supportItemSocketed;
            GemSO gemSO; 
            if(supportGemSocketed != null)
            {
                gemSO =
                ItemService.Instance.GetGemSO((GemNames)supportGemSocketed.itemName);
                socketSlots[2].GetChild(0).gameObject.SetActive(true); 
                 socketSlots[2].GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
            }
            else
            {
                socketSlots[2].GetChild(0).gameObject.SetActive(false);
            }

            if (itemModel.divItemsSocketed[0] != null)
            {               
                gemSO = ItemService.Instance.GetGemSO((GemNames)itemModel.divItemsSocketed[0].itemName);
                socketSlots[0].GetChild(0).gameObject.SetActive(true);
                socketSlots[0].GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
            }
            else
            {
                socketSlots[0].GetChild(0).gameObject.SetActive(false);
            }

            if (itemModel.divItemsSocketed[1] != null)
            {
                gemSO = ItemService.Instance.GetGemSO((GemNames)itemModel.divItemsSocketed[1].itemName);
                socketSlots[1].GetChild(0).gameObject.SetActive(true);
                socketSlots[1].GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
            }
            else
            {
                socketSlots[1].GetChild(0).gameObject.SetActive(false);
            }
        }
        #endregion

        #region SIDE PANEL BOTTOM : MATERIAL

        void PopulateMaterial(CharModel charModel)
        {
            transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                = charModel.armorType.ToString(); 


        }
        #endregion

        #region BUFF Strings 

        void PopulateBuffStrings()
        {
           displayStrs.Clear();
            CharController charController = InvService.Instance.charSelectController;
            ItemModel itemModel = charController.itemController.itemModel;
            Iitems supportItem = itemModel.supportItemSocketed;

            if(supportItem != null)
            {
                GemBase gem = supportItem as GemBase;
                gem.allDisplayStr.ForEach(t => displayStrs.Add(t));                     
            }
            if (itemModel.divItemsSocketed[0] != null)
            {
                GemBase gem = itemModel.divItemsSocketed[0] as GemBase; 
                gem.allDisplayStr.ForEach(t => displayStrs.Add(t));
            }
            if (itemModel.divItemsSocketed[1] != null)
            {
                GemBase gem = itemModel.divItemsSocketed[1] as GemBase;
                gem.allDisplayStr.ForEach(t => displayStrs.Add(t));
            }

            //get card and print 

            Transform container = transform.GetChild(4).GetChild(0).GetChild(1);
            int count = displayStrs.Count;
            for (int i =0; i<4; i++)
            {
                if (i < count)
                {
                    container.GetChild(i).gameObject.SetActive(true);   
                    container.GetChild(i).GetComponent<TextMeshProUGUI>().text
                    = displayStrs[i];
                }
                else
                {
                    container.GetChild(i).gameObject.SetActive(false);
                }
            }


        }

        #endregion

    }

}

