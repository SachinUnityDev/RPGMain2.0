using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;


namespace Interactables
{
    public class WeaponViewController : MonoBehaviour, IPanel
    {
        [SerializeField] CharNames charSelect;
        [SerializeField] Image gemImg; 
        void Start()
        {
            InvService.Instance.OnCharSelectInvPanel += PopulateWeaponPanel;
            ItemService.Instance.OnGemEnchanted += 
                            (CharController charController)=>PopulateGemEnchanted(); 

            UnLoad();
            gemImg = transform.GetChild(1).GetChild(0).GetComponent<Image>();

        }
        void OnEnable()
        {
            Load();
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
            Init();           
            PopulateGemEnchanted(); 
        }
        void PopulateWeaponPanel(CharModel charModel)
        {
            charSelect = charModel.charName;
            Sprite sprite = WeaponService.Instance.weaponSO.GetSprite(charSelect);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                = sprite;
         
        }

        void PopulateGemEnchanted()
        {          
            CharController charController = InvService.Instance.charSelectController;
            
            ItemModel itemModel = charController.itemController.itemModel;
            GemChargeData charge = itemModel.gemChargeData; 
            if(charge == null)
            {
                gemImg.gameObject.SetActive(false);
                return; 
            }
            GemSO gemSO =
                     ItemService.Instance.GetGemSO(charge.gemName);
            gemImg.gameObject.SetActive(true);
            gemImg.sprite = gemSO.iconSprite; 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {
            charSelect = InvService.Instance.charSelect;
        }

    }

}

